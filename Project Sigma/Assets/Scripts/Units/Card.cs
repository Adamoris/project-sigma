using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    //This is for checking if a unit is unlocked by the player.
    public bool unlocked;

    [Header("Unit General Information")]
    //This is the name of the unit.
    public new string name;
    // The IV value is an int ranging from 0-20 which
    // determines the boon/bane combination of an individual unit.
    [Tooltip("0 = neutral" + "\n"
        + "1   = +HP/-Atk" + "\n"
        + "2   = +HP/-Spd" + "\n"
        + "3   = +HP/-Def" + "\n"
        + "4   = +HP/-Res" + "\n"
        + "5   = +Atk/-HP" + "\n"
        + "6   = +Atk/-Spd" + "\n"
        + "7   = +Atk/-Def" + "\n"
        + "8   = +Atk/-Res" + "\n"
        + "9   = +Spd/-HP" + "\n"
        + "10 = +Spd/-Atk" + "\n"
        + "11 = +Spd/-Def" + "\n"
        + "12 = +Spd/-Res" + "\n"
        + "13 = +Def/-HP" + "\n"
        + "14 = +Def/-Atk" + "\n"
        + "15 = +Def/-Spd" + "\n"
        + "16 = +Def/-Res" + "\n"
        + "17 = +Res/-HP" + "\n"
        + "18 = +Res/-Atk" + "\n"
        + "19 = +Res/-Spd" + "\n"
        + "20 = +Res/-Def")]
    public int IV;

    //This indicates the move type of the unit.
    public enum MoveClass { Armor, Cavalry, Flier, Infantry }
    public MoveClass moveClass;

    //This indicates the race of the unit.
    public enum Race { Colossi, Cyren, Ent, Humans, Leviathans, Lupine, Nocturne, Quertzal, Raptors }
    public Race race;

    //This indicates the unit's affinity.
    public enum Affinity { Diamond, Club, Heart, Spade }
    public Affinity affinity;

    //This indicates the unit's geist typing.
    public enum Geist { Lunar, Solar }
    public Geist geist;

    //This indicates the weapon typing of the unit.
    public enum WeaponType { Sword, Axe, Lance, Dagger, Bow}
    public WeaponType weaponType;


    [Header("Unit Art Assets")]
    //Artwork.
    public Sprite portrait;
    public Sprite mapSprite;
    public Sprite affinitySprite;


    [Header("Lvl. 1 Base Stats")]
    //These are the stats for a neutral IV unit at level 1.
    [Rename("HP")]
    public int HP_floor;
    [Rename("Atk")]
    public int Atk_floor;
    [Rename("Spd")]
    public int Spd_floor;
    [Rename("Def")]
    public int Def_floor;
    [Rename("Res")]
    public int Res_floor;


    [Header("Lvl. 50 Base Stats")]
    //These are the stats for a neutral IV unit at level 50.
    [Rename("HP")]
    public int HP_ceiling;
    [Rename("Atk")]
    public int Atk_ceiling;
    [Rename("Spd")]
    public int Spd_ceiling;
    [Rename("Def")]
    public int Def_ceiling;
    [Rename("Res")]
    public int Res_ceiling;


    [Header("Unit's Current Power Level")]
    //This is the currently level of the unit.
    public int level;
    public int merge;


    [Header("Assist/Equipment/Special/Weapon")]
    public Assists assist;
    [Rename("Slot A")]
    public Equipment slot_A;
    [Rename("Slot B")]
    public Equipment slot_B;
    [Rename("Slot C")]
    public Equipment slot_C;
    [Rename("Slot D")]
    public Equipment slot_D;
    public Specials special;
    public Weapon weapon;


    //These are the current stats for a neutral IV unit.
    [HideInInspector]
    public int HP;
    [HideInInspector]
    public int Atk;
    [HideInInspector]
    public int Spd;
    [HideInInspector]
    public int Def;
    [HideInInspector]
    public int Res;

    //These are the buff/penalty modifiers for the unit's stats.
    [HideInInspector]
    public int HP_modifier;
    [HideInInspector]
    public int Atk_modifier;
    [HideInInspector]
    public int Spd_modifier;
    [HideInInspector]
    public int Def_modifier;
    [HideInInspector]
    public int Res_modifier;

}

