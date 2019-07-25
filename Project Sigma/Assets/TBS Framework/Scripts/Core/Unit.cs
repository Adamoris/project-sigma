﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;


// Base class for all units in the game.
public abstract class Unit : MonoBehaviour
{
    Dictionary<Cell, List<Cell>> cachedPaths = null;
    // UnitClicked event is invoked when user clicks the unit. 
    // It requires a collider on the unit game object to work.
    public event EventHandler UnitClicked;

    // UnitSelected event is invoked when user clicks on unit that belongs to him. 
    // It requires a collider on the unit game object to work.
    public event EventHandler UnitSelected;

    // UnitDeselected event is invoked when user click outside of currently selected unit's collider.
    // It requires a collider on the unit game object to work.
    public event EventHandler UnitDeselected;

    // UnitHighlighted event is invoked when user moves cursor over the unit. 
    // It requires a collider on the unit game object to work.
    public event EventHandler UnitHighlighted;

    // UnitDehighlighted event is invoked when cursor exits unit's collider. 
    // It requires a collider on the unit game object to work.
    public event EventHandler UnitDehighlighted;

    // UnitAttacked event is invoked when the unit is attacked.
    public event EventHandler<AttackEventArgs> UnitAttacked;

    // UnitDestroyed event is invoked when unit's hitpoints drop below 0.
    public event EventHandler<AttackEventArgs> UnitDestroyed;

    // UnitMoved event is invoked when unit moves from one cell to another.
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

    public int TotalHitPoints { get; private set; }
    protected int TotalMovementPoints;
    protected int TotalActionPoints;

    // Cell that the unit is currently occupying.
    public Cell Cell { get; set; }

    // Stats
    public Card card;

    //[Tooltip("This is the unit's name.")]
    public string UnitName;
    //[Tooltip("A unit is defeated if its Hit Points reach 0.")]
    public int HitPoints;
    //[Tooltip("This is the range at which a unit is able to attack.")]
    public int AttackRange;
    //[Tooltip("The higher the Attack, the more damage is inflicted on foes.")]
    public int AttackFactor;
    //[Tooltip("A unit will attack twice of its speed is at least 5 more than its foe.")]
    public int Speed;
    //[Tooltip("The higher the Defense, the less damage is taken from physical attacks.")]
    public int DefenceFactor;
    //[Tooltip("The higher the Resistance, the less damage is taken from magical attacks.")]
    public int Resistance;

    // Determines how far on the grid the unit can move.
    public int MovementPoints;

    // Determines speed of movement animation.
    public float MovementSpeed;

    // Determines how many attacks unit can perform in one turn.
    public int ActionPoints;
    private int CounterPoints;
    public static bool Attacking;

    // Indicates the player that the unit belongs to. 
    // Should correspoond with PlayerNumber variable on Player script.
    public int PlayerNumber;
    public static int PotentialVictor;

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
        HitPoints = card.HP_ceiling;
        TotalHitPoints = HitPoints;
        if (card.moveClass == Card.MoveClass.Armor)
        {
            TotalMovementPoints = card.reference.armorMovement;
        }
        else if (card.moveClass == Card.MoveClass.Cavalry)
        {
            TotalMovementPoints = card.reference.cavalryMovement;
        }
        else if (card.moveClass == Card.MoveClass.Flier)
        {
            TotalMovementPoints = card.reference.flierMovement;
        }
        else if (card.moveClass == Card.MoveClass.Infantry)
        {
            TotalMovementPoints = card.reference.infantryMovement;
        }

        TotalActionPoints = ActionPoints;
    }

    protected virtual void OnMouseDown()
    {
        if (UnitClicked != null)
            UnitClicked.Invoke(this, new EventArgs());
            ui_operator.cardDisplay.GetComponent<CardDisplay>().card = card;
            ui_operator.ShowCard();
    }
    protected virtual void OnMouseEnter()
    {
        if (UnitHighlighted != null)
            UnitHighlighted.Invoke(this, new EventArgs());
    }
    protected virtual void OnMouseExit()
    {
        if (UnitDehighlighted != null)
            UnitDehighlighted.Invoke(this, new EventArgs());
    }


    // Method is called at the start of each turn.
    public virtual void OnTurnStart()
    {
        MovementPoints = TotalMovementPoints;
        ActionPoints = TotalActionPoints;
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
        other.Defend(this, AttackFactor);
        //Debug.Log("attack: " + this);		
        //Debug.Log("attack: " + other);
        if (ActionPoints == 0)
        {
            SetState(new UnitStateMarkedAsFinished(this));
            MovementPoints = 0;
        }  
    }

    public IEnumerator Retaliate(Unit other)
    {
        yield return new WaitForSeconds(0.5f);
        MarkAsAttacking(other);
        other.Defend(this, AttackFactor);
    }


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
        HitPoints -= Mathf.Clamp(damage - DefenceFactor, 1, damage);
        //Debug.Log(this + "" + HitPoints);
        if (UnitAttacked != null)
            UnitAttacked.Invoke(this, new AttackEventArgs(other, this, damage));
            if (HitPoints > 0)
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

        if (HitPoints <= 0)
        {
            if (UnitDestroyed != null)
                //Debug.Log("Potential victor: " + other.PlayerNumber);		
                PotentialVictor = other.PlayerNumber;		
                //Debug.Log("dead: " + PlayerNumber);
                UnitDestroyed.Invoke(this, new AttackEventArgs(other, this, damage));
            OnDestroyed();
        }
    }


    // Moves the unit to destinationCell along the path.
    public virtual void Move(Cell destinationCell, List<Cell> path)
    {
        if (isMoving)
            return;

        var totalMovementCost = path.Sum(h => h.MovementCost);
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

        if (MovementSpeed > 0)
            StartCoroutine(MovementAnimation(path));
        else
            transform.position = Cell.transform.position;

        if (UnitMoved != null)
            UnitMoved.Invoke(this, new MovementEventArgs(Cell, destinationCell, path));    
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


    // Method indicates if unit is capable of moving to cell given as parameter.
    public virtual bool IsCellMovableTo(Cell cell)
    {
        return !cell.IsTaken;
    }

    // Method indicates if unit is capable of moving through cell given as parameter.
    public virtual bool IsCellTraversable(Cell cell)
    {
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

            var pathCost = path.Sum(c => c.MovementCost);
            if (pathCost <= MovementPoints)
            {
                cachedPaths.Add(key, path);
            }
        }
        return new HashSet<Cell>(cachedPaths.Keys);
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
        else
        {
            return _fallbackPathfinder.FindPath(GetGraphEdges(cells), Cell, destination);
        }
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
                    ret[cell][neighbour] = neighbour.MovementCost;
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
