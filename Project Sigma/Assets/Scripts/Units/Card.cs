using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //This is for checking if a unit is unlocked by the player.
    public bool unlocked;

    //Artwork.
    public Sprite artwork;
    public Sprite mapSprite;
    public Sprite affinity;

    //This is the name of the unit.
    public new string name;
    public int merge;

    //These are the stats for a neutral IV unit at level 1.
    public int HP_floor;
    public int Atk_floor;
    public int Spd_floor;
    public int Def_floor;
    public int Res_floor;

    //These are the stats for a neutral IV unit at level 50.
    public int HP_ceiling;
    public int Atk_ceiling;
    public int Spd_ceiling;
    public int Def_ceiling;
    public int Res_ceiling;

    //These are the current stats for a neutral IV unit.
    public int HP;
    public int Atk;
    public int Spd;
    public int Def;
    public int Res;

    //This is the currently level of the unit.
    public int Level;

    // The IV value is an int ranging from 0-20 which
    // determines the boon/bane combination of an individual unit.
        // 0 = neutral
        // 1 = +HP/-Atk
        // 2 = +HP/-Spd
        // 3 = +HP/-Def
        // 4 = +HP/-Res
        // 5 = +Atk/-HP
        // 6 = +Atk/-Spd
        // 7 = +Atk/-Def
        // 8 = +Atk/-Res
        // 9 = +Spd/-HP
        // 10 = +Spd/-Atk
        // 11 = +Spd/-Def
        // 12 = +Spd/-Res
        // 13 = +Def/-HP
        // 14 = +Def/-Atk
        // 15 = +Def/-Spd
        // 16 = +Def/-Res
        // 17 = +Res/-HP
        // 18 = +Res/-Atk
        // 19 = +Res/-Spd
        // 20 = +Res/-Def
    public int IV;

    //This indicates the move type of the unit.
    public enum MoveClass { Armor, Cavalry, Flier, Infantry }
    public MoveClass moveClass;

    //This indicates the race of the unit.
    public enum Race { Colossi, Cyren, Ent, Humans, Leviathans, Lupine, Nocturne, Quertzal, Raptors }
    public Race race;
}

