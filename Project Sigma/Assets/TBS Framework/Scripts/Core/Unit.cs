using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


/// <summary>
/// Base class for all units in the game.
/// </summary>
public abstract class Unit : MonoBehaviour
{
    Dictionary<Cell, List<Cell>> cachedPaths = null;
    Dictionary<Cell, List<Cell>> cachedAttackTiles = null;
    /// <summary>
    /// UnitClicked event is invoked when user clicks the unit. 
    /// It requires a collider on the unit game object to work.
    /// </summary>
    public event EventHandler UnitClicked;
    /// <summary>
    /// UnitSelected event is invoked when user clicks on unit that belongs to him. 
    /// It requires a collider on the unit game object to work.
    /// </summary>
    public event EventHandler UnitSelected;
    /// <summary>
    /// UnitDeselected event is invoked when user click outside of currently selected unit's collider.
    /// It requires a collider on the unit game object to work.
    /// </summary>
    public event EventHandler UnitDeselected;
    /// <summary>
    /// UnitHighlighted event is invoked when user moves cursor over the unit. 
    /// It requires a collider on the unit game object to work.
    /// </summary>
    public event EventHandler UnitHighlighted;
    /// <summary>
    /// UnitDehighlighted event is invoked when cursor exits unit's collider. 
    /// It requires a collider on the unit game object to work.
    /// </summary>
    public event EventHandler UnitDehighlighted;
    /// <summary>
    /// UnitAttacked event is invoked when the unit is attacked.
    /// </summary>
    public event EventHandler<AttackEventArgs> UnitAttacked;
    /// <summary>
    /// UnitDestroyed event is invoked when unit's HP drop below 0.
    /// </summary>
    public event EventHandler<AttackEventArgs> UnitDestroyed;
    /// <summary>
    /// UnitMoved event is invoked when unit moves from one cell to another.
    /// </summary>
    public event EventHandler<MovementEventArgs> UnitMoved;


    public UnitState UnitState { get; set; }
    public void SetState(UnitState state)
    {
        UnitState.MakeTransition(state);
    }
    // UI elements
    UI_Operator ui_operator;

    private void Awake()
    {
        ui_operator = FindObjectOfType<UI_Operator>();
    }


    // A list of buffs that are applied to the unit.
    public List<Buff> Buffs { get; private set; }

    public int TotalHP { get; private set; }
    protected int TotalMovementPoints;
    protected int TotalActionPoints;
    protected int TotalCounterPoints;

    // Cell that the unit is currently occupying.
    public Cell Cell { get; set; }

    // Stats
    public Card card;

    // Position
    public int x;
    public int y;
    [HideInInspector]
    public bool moved;

    //[Tooltip("This is the unit's name.")]
    [HideInInspector]
    public string UnitName;
    //[Tooltip("A unit is defeated if its Hit Points reach 0.")]
    [ReadOnly]
    public int HP;
    //[Tooltip("This is the range at which a unit is able to attack.")]
    [HideInInspector]
    public int AttackRange;
    //[Tooltip("The higher the Attack, the more damage is inflicted on foes.")]
    [ReadOnly]
    public int Atk;
    //[Tooltip("A unit will attack twice of its speed is at least 5 more than its foe.")]
    [ReadOnly]
    public int Spd;
    //[Tooltip("The higher the Defense, the less damage is taken from physical attacks.")]
    [ReadOnly]
    public int Def;
    //[Tooltip("The higher the Resistance, the less damage is taken from magical attacks.")]
    [ReadOnly]
    public int Res;

    // Determines how far on the grid the unit can move.
    [HideInInspector]
    public int MovementPoints;

    // Determines speed of movement animation.
    [HideInInspector]
    public float MovementSpeed;

    // Determines how many attacks unit can perform in one turn.
    [HideInInspector]
    public int ActionPoints;

    // Determines how many times the unit can counterattack during a combat interaction.
    [HideInInspector]
    public int CounterPoints;
    //public static bool Attacking;

    // Indicates the player that the unit belongs to. 
    // Should correspoond with PlayerNumber variable on Player script.
    public int PlayerNumber;
    //public static int PotentialVictor;

    // Indicates if movement animation is playing.
    public bool isMoving { get; set; }

    private static DijkstraPathfinding _pathfinder = new DijkstraPathfinding();
    private static IPathfinding _fallbackPathfinder = new AStarPathfinding();


    // Method called after object instantiation to initialize fields etc. 
    public virtual void Initialize()
    {
        Buffs = new List<Buff>();
        //Debug.Log(Environment.UserName);
        UnitState = new UnitStateNormal(this);
        card.UpdateStats();
        UnitName = card.name;
        HP = card.HP;
        Atk = card.Atk;
        Spd = card.Spd;
        Def = card.Def;
        Res = card.Res;
        switch (card.range)
        {
            case Card.Range.Melee:
                AttackRange = 1;
                break;
            case Card.Range.Ranged:
                AttackRange = card.rangeFactor;
                break;
            case Card.Range.None:
                AttackRange = 0;
                break;
        }
        TotalHP = HP;
        switch (card.moveClass)
        {
            case Card.MoveClass.Armor:
                TotalMovementPoints = card.ruleset.armorMovement;
                break;
            case Card.MoveClass.Cavalry:
                TotalMovementPoints = card.ruleset.cavalryMovement;
                break;
            case Card.MoveClass.Flier:
                TotalMovementPoints = card.ruleset.flierMovement;
                break;
            case Card.MoveClass.Infantry:
                TotalMovementPoints = card.ruleset.infantryMovement;
                break;
        }

        TotalActionPoints = ActionPoints;
        //TotalCounterPoints = CounterPoints;
    }

    public virtual void OnMouseDown()
    {
        if (PauseMenu.GameIsPaused == false && UnitClicked != null)
            UnitClicked.Invoke(this, new EventArgs());
            ui_operator.cardDisplay.GetComponent<CardDisplay>().card = card;
            //ui_operator.ShowCard();
    }
    public virtual void OnMouseEnter()
    {
        if (PauseMenu.GameIsPaused == false && UnitHighlighted != null)
        {
            UnitHighlighted.Invoke(this, new EventArgs());
            //ui_operator.cardDisplay.GetComponent<CardDisplay>().card = card;
        }
            
    }
    public virtual void OnMouseExit()
    {
        if (UnitDehighlighted != null)
            UnitDehighlighted.Invoke(this, new EventArgs());
    }


    // Method is called at the start of each turn.
    public virtual void OnTurnStart()
    {
        MovementPoints = TotalMovementPoints;
        ActionPoints = TotalActionPoints;
        CounterPoints = 0;
        //CounterPoints = TotalCounterPoints;
        SetState(new UnitStateMarkedAsFriendly(this));
    }

    // Method is called at the end of each turn.
    public virtual void OnTurnEnd()
    {
        cachedPaths = null;
        Buffs.FindAll(b => b.Duration == 0).ForEach(b => { b.Undo(this); });
        Buffs.RemoveAll(b => b.Duration == 0);
        Buffs.ForEach(b => { b.Duration--; });

        SetState(new UnitStateNormal(this));
    }

    // Method is called when units HP drops below 1.
    protected virtual void OnDestroyed()
    {
        Cell.IsTaken = false;
        MarkAsDestroyed();
        Destroy(gameObject);
    }


    // Method is called when unit is selected.
    public virtual void OnUnitSelected()
    {
        SetState(new UnitStateMarkedAsSelected(this));
        if (UnitSelected != null)
            UnitSelected.Invoke(this, new EventArgs());
    }

    // Method is called when unit is deselected.
    public virtual void OnUnitDeselected()
    {
        SetState(new UnitStateMarkedAsFriendly(this));
        if (UnitDeselected != null)
            UnitDeselected.Invoke(this, new EventArgs());
    }


    // Method indicates if it is possible to attack unit given as parameter, 
    // from cell given as second parameter.
    public virtual bool IsUnitAttackable(Unit other, Cell sourceCell)
    {
        if (card.range == Card.Range.Ranged)
        {
            if (sourceCell.GetDistance(other.Cell) <= AttackRange && sourceCell.GetDistance(other.Cell) != 1)
                return true;
        }
        // Change the comparator here to '<=' if you want ranged units to attack units that are closer
        if (sourceCell.GetDistance(other.Cell) == AttackRange)
            return true;

        return false;
    }

    // Method deals damage to unit given as parameter. (In this case the target being attacked)
    public virtual void DealDamage(Unit other)
    {
        if (isMoving)
            return;
        if (ActionPoints == 0)
            return;
        if (!IsUnitAttackable(other, Cell))
            return;

        MarkAsAttacking(other);
        ActionPoints--;
        if (card.range == other.card.range || (card.range == Card.Range.Melee && (other.card.slotA.counter == Equipment.Counter.Melee || other.card.weapon.CloseCounter))
            || (card.range == Card.Range.Ranged && (other.card.slotA.counter == Equipment.Counter.Ranged || other.card.weapon.DistantCounter)))
        {
            other.CounterPoints++;
        }
        if (other.Spd >= Spd + 5)
        {
            other.CounterPoints++;
        }
        if (Spd >= other.Spd + 5)
        {
            CounterPoints++;
        }
        other.Defend(this, Atk);
        if (CounterPoints > 0 && other.HP > 0)
        {
            other.Defend(this, Atk);
        }
        if (other.CounterPoints > 0 && HP > 0)
        {
            Defend(other, other.Atk);
        }
        if (ActionPoints == 0)
        {
            SetState(new UnitStateMarkedAsFinished(this));
            MovementPoints = 0;
        }  
    }

    public virtual void CounterAttack(Unit other)
    {
        MarkAsAttacking(other);
        CounterPoints--;
        other.Defend(this, Atk);

    }

    /*
    public IEnumerator Retaliate(Unit other)
    {
        yield return new WaitForSeconds(0.5f);
        MarkAsAttacking(other);
        other.Defend(this, Atk);
    }
    */

    /// <summary>
    /// Attacking unit calls Defend method on defending unit. 
    /// </summary>
    protected virtual void Defend(Unit other, int damage)
    {
        MarkAsDefending(other);
        //Damage is calculated by subtracting attack factor of attacker and defence/resistance factor of target. 
        //If result is below 1, it is set to 1. This behaviour can be overridden in derived classes.
        if (other.card.weapon.damageType == Weapon.DamageType.Physical)
        {
            var lastHP = HP;
            HP -= Mathf.Clamp(damage - Def, 0, damage);
            Debug.Log(other.card.name + " dealt " + (lastHP - HP) + " physical damage to " + card.name + ".");
            lastHP = HP;
        } else if (other.card.weapon.damageType == Weapon.DamageType.Magical)
        {
            var lastHP = HP;
            HP -= Mathf.Clamp(damage - Res, 0, damage);
            Debug.Log(other.card.name + " dealt " + (lastHP - HP) + " magical damage to " + card.name + ".");
            lastHP = HP;
        }

        if (UnitAttacked != null)
        {
            UnitAttacked.Invoke(this, new AttackEventArgs(other, this, damage));
        }

        if (HP <= 0)
        {
            if (UnitDestroyed != null)
                Debug.Log(this.card.name + " has been defeated.");
                UnitDestroyed.Invoke(this, new AttackEventArgs(other, this, damage));
            OnDestroyed();
        }
        if (HP > 0 && CounterPoints > 0 && (card.range == other.card.range))
        {
            //Debug.Log(name + CounterPoints + " " + other.CounterPoints);
            CounterAttack(other);
        }
    }

    /*
    // Attacking unit calls Defend method on defending unit. 
    protected virtual void Defend(Unit other, int damage)
    {
        if (ActionPoints > 0)
        {
            CounterPoints++;
        }
        MarkAsDefending(other);
        //Debug.Log(this);
        //Damage is calculated by subtracting attack factor of attacker and defence factor of defender. 
        //If result is below 1, it is set to 1. This behaviour can be overridden in derived classes.
        HP -= Mathf.Clamp(damage - Def, 1, damage);
        //Debug.Log(this + "" + HP);
        if (UnitAttacked != null)
            UnitAttacked.Invoke(this, new AttackEventArgs(other, this, damage));
            if (HP > 0)
            {
                //Debug.Log(this + "aaa" + ActionPoints);		
                if (!IsUnitAttackable(other, Cell))
                {
                    //Debug.Log("Counter!");		
                }
                else if (CounterPoints > 0)
                {

                    //Debug.Log("Potential victor: " + PlayerNumber);		
                    PotentialVictor = PlayerNumber;
                    CounterPoints--;
                    //Debug.Log("Counter!: " + this);		
                    StartCoroutine(Retaliate(other));
                }
            }

        if (HP <= 0)
        {
            if (UnitDestroyed != null)
                //Debug.Log("Potential victor: " + other.PlayerNumber);		
                PotentialVictor = other.PlayerNumber;		
                //Debug.Log("dead: " + PlayerNumber);
                UnitDestroyed.Invoke(this, new AttackEventArgs(other, this, damage));
            OnDestroyed();
        }
    }
    */

    // Moves the unit to destinationCell along the path.
    public virtual void Move(Cell destinationCell, List<Cell> path)
    {
        if (isMoving)
            return;
        var totalMovementCost = 0;
        if (card.moveClass == Card.MoveClass.Flier || card.moveClass == Card.MoveClass.Armor)
        {
            totalMovementCost = path.Sum(h => h.BypassMovementCost);
        }
        else
        {
            totalMovementCost = path.Sum(h => h.MovementCost);
        }
        
        if (MovementPoints < totalMovementCost)
            return;

        //Change this line back when adding in the full menu functionality for the combat system.
        //MovementPoints -= totalMovementCost;		
        MovementPoints = 0;

        Cell.IsTaken = false;
        Cell.occupationID = 99;
        Cell = destinationCell;
        destinationCell.IsTaken = true;
        destinationCell.occupationID = PlayerNumber;
        x = Cell.x;
        y = Cell.y;
        if (MovementSpeed > 0)
            StartCoroutine(MovementAnimation(path));
        else
            transform.position = Cell.transform.position;

        if (UnitMoved != null)
            UnitMoved.Invoke(this, new MovementEventArgs(Cell, destinationCell, path));

        moved = true;
    }
    protected virtual IEnumerator MovementAnimation(List<Cell> path)
    {
        isMoving = true;
        path.Reverse();
        foreach (var cell in path)
        {
            Vector3 destination_pos = new Vector3(cell.transform.localPosition.x, transform.localPosition.y, cell.transform.localPosition.z);
            while (transform.localPosition != destination_pos)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination_pos, Time.deltaTime * MovementSpeed);
                yield return 0;
            }
        }
        isMoving = false;
    }

    // Teleports the unit to the destination.
    public virtual void Teleport(Cell destinationCell)
    {
        Cell.IsTaken = false;
        Cell.occupationID = 99;
        Cell = destinationCell;
        destinationCell.IsTaken = true;
        destinationCell.occupationID = PlayerNumber;
        x = Cell.x;
        y = Cell.y;
        transform.position = Cell.transform.position;
    }

    // Method indicates if unit is capable of moving to cell given as parameter.
    public virtual bool IsCellMovableTo(Cell cell)
    {
        return !cell.IsTaken;
    }

    // Method indicates if unit is capable of moving through cell given as parameter.
    public virtual bool IsCellTraversable(Cell cell)
    {
        switch (card.moveClass)
        {
            case Card.MoveClass.Armor:
                return (!cell.IsTaken || cell.occupationID == PlayerNumber) && cell.terrainType != Cell.TerrainType.Mountain;
            case Card.MoveClass.Cavalry:
                return (!cell.IsTaken || cell.occupationID == PlayerNumber) && cell.terrainType != Cell.TerrainType.Forest && cell.terrainType != Cell.TerrainType.Mountain;
            case Card.MoveClass.Flier:
                return !cell.IsTaken || cell.occupationID == PlayerNumber;
            case Card.MoveClass.Infantry:
                return (!cell.IsTaken || cell.occupationID == PlayerNumber) && cell.terrainType != Cell.TerrainType.Mountain;
        }
        return !cell.IsTaken || cell.occupationID == PlayerNumber;
    }

    // Method returns all cells that the unit is capable of moving to.
    public HashSet<Cell> GetAvailableDestinations(List<Cell> cells)
    {
        cachedPaths = new Dictionary<Cell, List<Cell>>();

        var paths = cachePaths(cells);
        foreach (var key in paths.Keys)
        {
            if (!IsCellMovableTo(key))
                continue;
            var path = paths[key];
            if (card.moveClass == Card.MoveClass.Flier || card.moveClass == Card.MoveClass.Armor)
            {
                var pathCost = path.Sum(c => c.BypassMovementCost);
                if (pathCost <= MovementPoints)
                {
                    cachedPaths.Add(key, path);
                }
            }
            else
            {
                var pathCost = path.Sum(c => c.MovementCost);
                if (pathCost <= MovementPoints)
                {
                    cachedPaths.Add(key, path);
                }
            }
            
        }
        return new HashSet<Cell>(cachedPaths.Keys);
    }

    //Method returns all cells that the unit is capable of attacking (assuming full movement).
    public HashSet<Cell> GetAvailableAttackTiles(List<Cell> cells)
    {
        cachedAttackTiles = new Dictionary<Cell, List<Cell>>();

        var tiles = cachePaths(cells);
        foreach (var key in tiles.Keys)
        {
            if (!IsCellMovableTo(key))
                continue;
            var tile = tiles[key];
            if (card.moveClass == Card.MoveClass.Flier || card.moveClass == Card.MoveClass.Armor)
            {
                var pathCost = tile.Sum(c => c.BypassMovementCost);
                switch (card.range)
                {
                    case Card.Range.Melee:
                        Debug.Log(pathCost);
                        if (pathCost == MovementPoints + card.ruleset.plainsCost)
                        {
                            cachedAttackTiles.Add(key, tile);
                        }
                        break;
                    case Card.Range.Ranged:
                        if (pathCost > MovementPoints && pathCost < MovementPoints + card.ruleset.plainsCost * (card.rangeFactor + 1))
                        {
                            cachedAttackTiles.Add(key, tile);
                        }
                        break;
                }
            }
            else
            {
                var pathCost = tile.Sum(c => c.MovementCost);
                switch (card.range)
                {
                    case Card.Range.Melee:
                        if (pathCost > MovementPoints && pathCost <= MovementPoints + card.ruleset.forestCost)
                        {
                            cachedAttackTiles.Add(key, tile);
                        }
                        break;
                    case Card.Range.Ranged:
                        if (pathCost > MovementPoints && pathCost < MovementPoints + card.ruleset.plainsCost * (card.rangeFactor + 1))
                        {
                            //Debug.Log(Cell.GetDistance(key));
                            //Debug.Log("a" + (pathCost - MovementPoints));
                            cachedAttackTiles.Add(key, tile);
                        }
                        break;
                }
                
            }
        }
        return new HashSet<Cell>(cachedAttackTiles.Keys);
    }

    private Dictionary<Cell, List<Cell>> cachePaths(List<Cell> cells)
    {
        var edges = GetGraphEdges(cells);
        var paths = _pathfinder.findAllPaths(edges, Cell);
        return paths;
    }

    public List<Cell> FindPath(List<Cell> cells, Cell destination)
    {
        if(cachedPaths != null && cachedPaths.ContainsKey(destination))
        {
            return cachedPaths[destination];
        }
        return _fallbackPathfinder.FindPath(GetGraphEdges(cells), Cell, destination);
    }

    // Method returns graph representation of cell grid for pathfinding.
    protected virtual Dictionary<Cell, Dictionary<Cell, int>> GetGraphEdges(List<Cell> cells)
    {
        Dictionary<Cell, Dictionary<Cell, int>> ret = new Dictionary<Cell, Dictionary<Cell, int>>();
        foreach (var cell in cells)
        {
            if (IsCellTraversable(cell) || cell.Equals(Cell))
            {
                ret[cell] = new Dictionary<Cell, int>();
                foreach (var neighbour in cell.GetNeighbours(cells).FindAll(IsCellTraversable))
                {
                    if (card.moveClass == Card.MoveClass.Flier || card.moveClass == Card.MoveClass.Armor)
                    {
                        ret[cell][neighbour] = neighbour.BypassMovementCost;
                    }
                    else
                    {
                        ret[cell][neighbour] = neighbour.MovementCost;
                    }
                    
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// Gives visual indication that the unit is under attack.
    /// </summary>
    /// <param name="other"></param>
    public abstract void MarkAsDefending(Unit other);
    /// <summary>
    /// Gives visual indication that the unit is attacking.
    /// </summary>
    /// <param name="other"></param>
    public abstract void MarkAsAttacking(Unit other);
    /// <summary>
    /// Gives visual indication that the unit is destroyed. It gets called right before the unit game object is
    /// destroyed, so either instantiate some new object to indicate destruction or redesign Defend method. 
    /// </summary>
    public abstract void MarkAsDestroyed();

    /// <summary>
    /// Method marks unit as current players unit.
    /// </summary>
    public abstract void MarkAsFriendly();
    /// <summary>
    /// Method mark units to indicate user that the unit is in range and can be attacked.
    /// </summary>
    public abstract void MarkAsReachableEnemy();
    /// <summary>
    /// Method marks unit as currently selected, to distinguish it from other units.
    /// </summary>
    public abstract void MarkAsSelected();
    /// <summary>
    /// Method marks unit to indicate user that he can't do anything more with it this turn.
    /// </summary>
    public abstract void MarkAsFinished();
    /// <summary>
    /// Method returns the unit to its base appearance
    /// </summary>
    public abstract void UnMark();
}

public class MovementEventArgs : EventArgs
{
    public Cell OriginCell;
    public Cell DestinationCell;
    public List<Cell> Path;

    public MovementEventArgs(Cell sourceCell, Cell destinationCell, List<Cell> path)
    {
        OriginCell = sourceCell;
        DestinationCell = destinationCell;
        Path = path;
    }
}
public class AttackEventArgs : EventArgs
{
    public Unit Attacker;
    public Unit Defender;

    public int Damage;

    public AttackEventArgs(Unit attacker, Unit defender, int damage)
    {
        Attacker = attacker;
        Defender = defender;

        Damage = damage;
    }
}
public class UnitCreatedEventArgs : EventArgs
{
    public Transform unit;

    public UnitCreatedEventArgs(Transform unit)
    {
        this.unit = unit;
    }
}
