using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [Header("Abstract Details")]
    //This SO holds information that is universal.
    public Universal reference;
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
    public enum MoveClass { None, Armor, Cavalry, Flier, Infantry }
    public MoveClass moveClass;

    //This indicates the race of the unit.
    public enum Race { None, Colossi, Cyren, Ent, Humans, Leviathans, Lupine, Nocturne, Quertzal, Raptors }
    public Race race;

    //This indicates the unit's affinity.
    public enum Affinity { None, Diamond, Club, Heart, Spade }
    public Affinity affinity;

    //This indicates the unit's geist typing.
    public enum Geist { None, Lunar, Solar }
    public Geist geist;

    //This indicates the weapon typing of the unit.
    public enum WeaponType { None, Sword, Axe, Lance, Dagger, Bow, Magic, Beast}
    public WeaponType weaponType;


    [Header("Unit's Current Power Level")]
    //This is the currently level of the unit.
    public int level;
    public int merge;
    public enum Rarity { None, one_star, two_star, three_star }
    public Rarity rarity;


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


    [Header("Growth Rates")]
    //These are the stats for a neutral IV unit at level 50.
    [Rename("HP")]
    public int HP_rate;
    [Rename("Atk")]
    public int Atk_rate;
    [Rename("Spd")]
    public int Spd_rate;
    [Rename("Def")]
    public int Def_rate;
    [Rename("Res")]
    public int Res_rate;


    [Header("Super Boon(s)")]
    //These are the potential superboons of the unit.
    [Rename("HP")]
    public bool HP_boon;
    [Rename("Atk")]
    public bool Atk_boon;
    [Rename("Spd")]
    public bool Spd_boon;
    [Rename("Def")]
    public bool Def_boon;
    [Rename("Res")]
    public bool Res_boon;


    [Header("Super Bane(s)")]
    //These are the potential superbanes of the unit.
    [Rename("HP")]
    public bool HP_bane;
    [Rename("Atk")]
    public bool Atk_bane;
    [Rename("Spd")]
    public bool Spd_bane;
    [Rename("Def")]
    public bool Def_bane;
    [Rename("Res")]
    public bool Res_bane;


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

