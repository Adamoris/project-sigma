using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("General Weapon Information")]
    //This is the name of the weapon.
    public new string name;

    //This is the description for what the weapon does.
    [TextArea(3, 10)]
    public string description;

    //This is the might value of the weapon which is added on to the unit's atk stat.
    public int mt;

    //This determines whether a weapon will deal magical or physical damage.
    public enum DamageType { None, Magical, Physical}
    public DamageType damageType;

    //This specifies the weapon typing for determining class/inheritance restrictions.
    public enum WeaponType { None, Sword, Spear, Axe, Mace, Disruptor, Staff, Bow, Wand, Gauntlet, Dagger, Transformation }
    public WeaponType weaponType;

    //This is for checking if a specific weapon is already unlocked before this one can be learned.
    public Weapon prerequisite;


    [Header("Weapon Art Assets")]
    public Sprite cardSprite;
    public Sprite mapSprite;

    
    [Header("Inheritability")]
    //This is to determine if a weapon is inheritable.
    public bool inheritable;


    [Header("Weapon Equipment Effect")]
    //This is for applying the effect of an equipment item onto the weapon if needed.
    public Equipment equipmentEffect;


    [Header("Weapon Effectiveness")]
    //This applies a damage boost against an enemy of the specified types if applicable.
    public bool armor;
    public bool cavalry;
    public bool flier;
    public bool infantry;


    //These enums will be used in the sections below.
    public enum CombatOrder { None, BeforeCombat, StartOfCombat, EndOfCombat }
    //-----------------------------------------------------------------------------------
    [Header("Wielder Effect: Player Phase")]
    //This section is for effects of the weapon for the wielder in the Player Phase.
    [Rename("Combat Order")]
    public CombatOrder combatOrder_WEPP;

    [Header("Wielder Effect: Enemy Phase")]
    //This section is for effects of the weapon for the wielder in the Enemy Phase.
    [Rename("Combat Order")]
    public CombatOrder combatOrder_WEEP;

    //-----------------------------------------------------------------------------------
    [Header("Target Effect: Player Phase")]
    //This section is for effects of the weapon for the foe in the Player Phase.
    [Rename("Combat Order")]
    public CombatOrder combatOrder_TEPP;

    [Header("Target Effect: Enemy Phase")]
    //This section is for effects of the weapon for the foe in the Enemy Phase.
    [Rename("Combat Order")]
    public CombatOrder combatOrder_TEEP;

    //-----------------------------------------------------------------------------------
    [Header("Ally Effect: Player Phase")]
    //This section is for effects of the weapon for allies in the Player Phase.
    [Rename("Combat Order")]
    public CombatOrder combatOrder_AEPP;
    [Rename("Include Self")]
    public bool includeSelf_AEPP;
    [Rename("Effect Range")]
    public int range_AEPP;

    [Header("Ally Effect: Enemy Phase")]
    //This section is for effects of the weapon for allies in the Enemy Phase.
    [Rename("Combat Order")]
    public CombatOrder combatOrder_AEEP;
    [Rename("Include Self")]
    public bool includeSelf_AEEP;
    [Rename("Effect Range")]
    public int range_AEEP;

    //-----------------------------------------------------------------------------------
    [Header("Enemy Effect: Player Phase")]
    //This section is for effects of the weapon for the foe's allies in the Player Phase.
    [Rename("Combat Order")]
    public CombatOrder combatOrder_EEPP;
    [Rename("Include Target")]
    public bool includeTarget_EEPP;
    [Rename("Effect Range")]
    public int range_EEPP;

    [Header("Enemy Effect: Enemy Phase")]
    //This section is for effects of the weapon for the foe's allies in the Enemy Phase.
    [Rename("Combat Order")]
    public CombatOrder combatOrder_EEEP;
    [Rename("Include Target")]
    public bool includeTarget_EEEP;
    [Rename("Effect Range")]
    public int range_EEEP;
    //-----------------------------------------------------------------------------------


    //This specifies whether a weapon is ranged or not.
    public enum Range { None, Melee, Ranged }
    [Header("Weapon Range")]
    public Range range;

    //This is for melee weapons that can retaliate against ranged attacks.
    public bool DistantCounter { get; set; }

    //This is for ranged weapons that can retaliate against melee attacks.
    public bool CloseCounter { get; set; }
}
