using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [Header("Abstract Details")]
    //This SO holds information that is universal.
    public Ruleset ruleset;
    //This is for checking is a unit is able to be summoned.
    public bool summonable;
    //This is for checking if a unit is unlocked by the player.
    public bool unlocked;

    [Header("Unit General Information")]
    //This is the name of the unit.
    public new string name;
    //This is the description for who the unit is.
    [TextArea(3, 10)]
    public string description;
    // The IV value is an int ranging from 0-20 which
    // determines the boon/bane combination of an individual unit.
    [Tooltip("0   = neutral" + "\n"
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
    private readonly int varianceFactor = 5;
    private string boonKey;
    private string baneKey;

    //This specifies the gender of the unit.
    public enum Gender { None, Male, Female}
    public Gender gender;

    //This specifies the move type of the unit.
    public enum MoveClass { None, Armor, Cavalry, Flier, Infantry }
    //[HideInInspector]
    public MoveClass moveClass;

    //This specifies the damage-type of the unit.
    public enum DamageType { None, Physical, Magical }
    //[HideInInspector]
    public DamageType damageType;

    //This specifies the range of the unit.
    public enum Range { None, Melee, Ranged }
    public Range range;

    //This specifies how far a ranged unit can reach (default is 2).
    public int rangeFactor = 2;

    //The 16 following enums specify the possible class progression paths of the unit.
    public enum PhysicalMeleeArmor { Knight, Brigadier, General, Emperor, Conqueror }
    [Rename("Class")]
    public PhysicalMeleeArmor pma;
    public enum PhysicalRangedArmor { Knight, Brigadier, Sentinel, Emperor, Conqueror }
    [Rename("Class")]
    public PhysicalRangedArmor pra;
    public enum MagicalMeleeArmor { Knight, Tactician, Grandmaster, Hierarch, Hierophant }
    [Rename("Class")]
    public MagicalMeleeArmor mma;
    public enum MagicalRangedArmor { Knight, Tactician, Warlock, Hierarch, Hierophant }
    [Rename("Class")]
    public MagicalRangedArmor mra;

    public enum PhysicalMeleeCavalry { Cavalier, Templar, Heavy_Cavalry, Dragoon }
    [Rename("Class")]
    public PhysicalMeleeCavalry pmc;
    public enum PhysicalRangedCavalry { Cavalier, Templar, Bow_Knight, Dragoon }
    [Rename("Class")]
    public PhysicalRangedCavalry prc;
    public enum MagicalMeleeCavalry { Cavalier, Caster, Battle_Priest, Paladin }
    [Rename("Class")]
    public MagicalMeleeCavalry mmc;
    public enum MagicalRangedCavalry { Cavalier, Caster, Cleric, Paladin }
    [Rename("Class")]
    public MagicalRangedCavalry mrc;

    public enum PhysicalMeleeFlier { Scout, Pegasus_Knight, Falconer, Valkyrie }
    [Rename("Class")]
    public PhysicalMeleeFlier pmf;
    public enum PhysicalRangedFlier { Scout, Pegasus_Knight, Sparrow, Valkyrie }
    [Rename("Class")]
    public PhysicalRangedFlier prf;
    public enum MagicalMeleeFlier { Scout, Dragon_Knight, Raptor, Arbiter}
    [Rename("Class")]
    public MagicalMeleeFlier mmf;
    public enum MagicalRangedFlier { Scout, Dragon_Knight, Phoenix, Arbiter}
    [Rename("Class")]
    public MagicalRangedFlier mrf;

    public enum PhysicalMeleeInfantry { Soldier, Dancer, Warrior, Blademaster, Thief, Berserker, Assassin }
    [Rename("Class")]
    public PhysicalMeleeInfantry pmi;
    public enum PhysicalRangedInfantry { Soldier, Warrior, Archer, Scholar, Marksman, Engineer }
    [Rename("Class")]
    public PhysicalRangedInfantry pri;
    public enum MagicalMeleeInfantry { Soldier, Dancer, Healer, Mage, Druid, Monk, Shaman, Beast}
    [Rename("Class")]
    public MagicalMeleeInfantry mmi;
    public enum MagicalRangedInfantry { Soldier, Dancer, Healer, Mage, Wizard, Sorcerer, Archmage, Reaper }
    [Rename("Class")]
    public MagicalRangedInfantry mri;

    //This indicates the race of the unit.
    public enum Race { None, Alder, Colossi, Cyren, Humans, Leviathans, Lupine, Nocturne, Quertzal, Raptors }
    public Race race;

    //This specifies the unit's affinity.
    public enum Affinity { None, Diamond, Club, Heart, Spade }
    public Affinity affinity;

    //This specifies the unit's geist typing.
    //public enum Geist { None, Lunar, Solar }
    //public Geist geist;

    //This specifies the weapon typing of the unit.
    //public enum WeaponType { None, Sword, Axe, Lance, Dagger, Bow, Magic, Beast}
    //public WeaponType weaponType;


    [Header("Unit's Current Power Level")]
    //This is the currently level of the unit.
    public int level = 1;
    public int merge;
    public enum Rarity { None, Bronze, Silver, Gold }
    public Rarity rarity;


    [Header("Unit Art Assets")]
    //Artwork.
    public Sprite portrait;
    public Sprite mapSprite;
    //public Sprite affinitySprite;


    [Header("Lvl. 1 Base Stats (Bronze)")]
    //These are the stats for a neutral IV unit at level 1 and bronze rarity.
    [Rename("HP")]
    public int baseHP;
    [Rename("Atk")]
    public int baseAtk;
    [Rename("Spd")]
    public int baseSpd;
    [Rename("Def")]
    public int baseDef;
    [Rename("Res")]
    public int baseRes;


    [Header("Growth Rates (%)")]
    //These are the growth rates for the unit's stats.
    [Rename("HP")]
    public int growthHP;
    [Rename("Atk")]
    public int growthAtk;
    [Rename("Spd")]
    public int growthSpd;
    [Rename("Def")]
    public int growthDef;
    [Rename("Res")]
    public int growthRes;


    [Header("Assist/Equipment/Special/Weapon")]
    public Weapon weapon;
    public Assist assist;
    public Special special;
    [Rename("Slot A")]
    public Equipment slotA;
    [Rename("Slot B")]
    public Equipment slotB;
    [Rename("Slot C")]
    public Equipment slotC;
    [Rename("Slot D")]
    public Equipment slotD;
    


    [Header("Reference Values")]
    //These are the current stats for the unit.
    [ReadOnly]
    public int HP;
    [ReadOnly]
    public int Atk;
    [ReadOnly]
    public int Spd;
    [ReadOnly]
    public int Def;
    [ReadOnly]
    public int Res;


    //These are the modifiers for the unit's stats.
    [ReadOnly]
    public int modifierHP;
    [ReadOnly]
    public int modifierAtk;
    [ReadOnly]
    public int modifierSpd;
    [ReadOnly]
    public int modifierDef;
    [ReadOnly]
    public int modifierRes;
    private int rarityModifier;


    //These are the level lists which are stored for the individual units
    public int[] levelListHP;
    public int[] levelListAtk;
    public int[] levelListSpd;
    public int[] levelListDef;
    public int[] levelListRes;


    //Stat Calculation Variables/Methods
    private readonly float rarityFactor = 0.07f;
    public int levelingRange = 49;
    int randomizer;
    int[] levelList;

    //This method is for altering growth rates to match the unit's IV.
    public void FactorIV(int iv)
    {
        switch (iv)
        {
            case 0:
                boonKey = "00000";
                baneKey = "00000";
                break;
            case 1:
                boonKey = "10000";
                baneKey = "01000";
                break;
            case 2:
                boonKey = "10000";
                baneKey = "00100";
                break;
            case 3:
                boonKey = "10000";
                baneKey = "00010";
                break;
            case 4:
                boonKey = "10000";
                baneKey = "00001";
                break;
            case 5:
                boonKey = "01000";
                baneKey = "10000";
                break;
            case 6:
                boonKey = "01000";
                baneKey = "00100";
                break;
            case 7:
                boonKey = "01000";
                baneKey = "00010";
                break;
            case 8:
                boonKey = "01000";
                baneKey = "00001";
                break;
            case 9:
                boonKey = "00100";
                baneKey = "10000";
                break;
            case 10:
                boonKey = "00100";
                baneKey = "01000";
                break;
            case 11:
                boonKey = "00100";
                baneKey = "00010";
                break;
            case 12:
                boonKey = "00100";
                baneKey = "00001";
                break;
            case 13:
                boonKey = "00010";
                baneKey = "10000";
                break;
            case 14:
                boonKey = "00010";
                baneKey = "01000";
                break;
            case 15:
                boonKey = "00010";
                baneKey = "00100";
                break;
            case 16:
                boonKey = "00010";
                baneKey = "00001";
                break;
            case 17:
                boonKey = "00001";
                baneKey = "10000";
                break;
            case 18:
                boonKey = "00001";
                baneKey = "01000";
                break;
            case 19:
                boonKey = "00001";
                baneKey = "00100";
                break;
            case 20:
                boonKey = "00001";
                baneKey = "00010";
                break;
        }
    }

    //This method is for generating the EXP Spread for all stats.
    public void GenerateEXPSpread()
    {
        FactorIV(IV);
        levelListHP = RandomEXP(growthHP + varianceFactor * Convert.ToInt32(boonKey[0]) - varianceFactor * Convert.ToInt32(baneKey[0]), rarity);
        levelListAtk = RandomEXP(growthAtk + varianceFactor * Convert.ToInt32(boonKey[1]) - varianceFactor * Convert.ToInt32(baneKey[1]), rarity);
        levelListSpd = RandomEXP(growthSpd + varianceFactor * Convert.ToInt32(boonKey[2]) - varianceFactor * Convert.ToInt32(baneKey[2]), rarity);
        levelListDef = RandomEXP(growthDef + varianceFactor * Convert.ToInt32(boonKey[3]) - varianceFactor * Convert.ToInt32(baneKey[3]), rarity);
        levelListRes = RandomEXP(growthRes + varianceFactor * Convert.ToInt32(boonKey[4]) - varianceFactor * Convert.ToInt32(baneKey[4]), rarity);
    }

    //This method combines the two methods below to both generate a growth value and its respective randomized EXP list.
    public int[] RandomEXP(int growthRate, Rarity rarity)
    {
        int growthValue = GetGrowthValue(growthRate, rarity);
        return GenerateLevelList(growthValue);
    }

    //This method is for generating the total growth value that a unit will be able to achieve.
    public int GetGrowthValue(int growthRate, Rarity rarity)
    {
        if (rarity == Rarity.Bronze)
        {
            return (int)Mathf.Floor(levelingRange * 0.01f * Mathf.Floor(growthRate * (1f - rarityFactor + (rarityFactor * 1))));
        }
        if (rarity == Rarity.Silver)
        {
            return (int)Mathf.Floor(levelingRange * 0.01f * Mathf.Floor(growthRate * (1f - rarityFactor + (rarityFactor * 2))));
        }
        if (rarity == Rarity.Gold)
        {
            return (int)Mathf.Floor(levelingRange * 0.01f * Mathf.Floor(growthRate * (1f - rarityFactor + (rarityFactor * 3))));
        }
        return 0;
    }

    //This method is for generating the list of valid levels at which to raise the stat.
    public int[] GenerateLevelList(int growthValue)
    {
        //growthValue -= growthValue - ;
        levelList = new int[growthValue];
        for (int j = 0; j < growthValue; j++)
        {
            randomizer = UnityEngine.Random.Range(level + 1, levelingRange + 2);

            while (levelList.Contains(randomizer))
            {
                randomizer = UnityEngine.Random.Range(level + 1, levelingRange + 2);
            }
            levelList[j] = randomizer;
        }
        Array.Sort(levelList);
        return levelList;
    }

    //This method is for altering unit statlines based on unit rarity.
    public void RarityAdjustment(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Bronze:
                rarityModifier = 0;
                break;
            case Rarity.Silver:
                rarityModifier = 1;
                break;
            case Rarity.Gold:
                rarityModifier = 2;
                break;
        }
    }

    //This method is for updating what the stat modifiers should currently be.
    public void CheckModifiers()
    {
        modifierHP = 0;
        modifierAtk = 0;
        modifierSpd = 0;
        modifierDef = 0;
        modifierRes = 0;
        if (level > 1)
        {
            for (int i = 2; i < level + 1; i++)
            {
                if (levelListHP.Contains(i))
                {
                    modifierHP++;
                }
                if (levelListAtk.Contains(i))
                {
                    modifierAtk++;
                }
                if (levelListSpd.Contains(i))
                {
                    modifierSpd++;
                }
                if (levelListDef.Contains(i))
                {
                    modifierDef++;
                }
                if (levelListRes.Contains(i))
                {
                    modifierRes++;
                }
            }
        }
    }

    //This method is for updating the stats to reflect its current state (excluding alterations from combat).
    public void UpdateStats()
    {
        FactorIV(IV);
        RarityAdjustment(rarity);
        //Debug.Log(name + ": " + HP_floor + "," + rarityModifier + "," + modifierHP);
        HP = baseHP + rarityModifier + modifierHP + Convert.ToInt32(boonKey[0]) - Convert.ToInt32(baneKey[0]);
        if (weapon != null)
        {
            Atk = baseAtk + rarityModifier + modifierAtk + Convert.ToInt32(boonKey[1]) - Convert.ToInt32(baneKey[1]) + weapon.mt;
        }
        else
        {
            Atk = baseAtk + rarityModifier + modifierAtk + Convert.ToInt32(boonKey[1]) - Convert.ToInt32(baneKey[1]);
        }
        Spd = baseSpd + rarityModifier + modifierSpd + Convert.ToInt32(boonKey[2]) - Convert.ToInt32(baneKey[2]);
        Def = baseDef + rarityModifier + modifierDef + Convert.ToInt32(boonKey[3]) - Convert.ToInt32(baneKey[3]);
        Res = baseRes + rarityModifier + modifierRes + Convert.ToInt32(boonKey[4]) - Convert.ToInt32(baneKey[4]);
    }

    //This method is for reverting the unit to its lvl. 1 state.
    public void ResetStats()
    {
        level = 1;
        InitializeStats();
    }

    //This method is for updating the stats without resetting the unit to lvl. 1.
    public void InitializeStats()
    {
        if (level <= levelingRange + 1 && level >= 1)
        {
            CheckModifiers();
            UpdateStats();
        }
        
    }

    //This method is for leveling up the unit by 1.
    public void LevelUp()
    {
        if (level <= levelingRange)
        {
            level++;
            InitializeStats();
        }
        
    }

    //This method is for leveling down the unit by 1.
    public void LevelDown()
    {
        if (level > 1)
        {
            level--;
            InitializeStats();
        }
        
    }

}

