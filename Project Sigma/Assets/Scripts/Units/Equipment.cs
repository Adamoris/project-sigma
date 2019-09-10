using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Equipment")]
public class Equipment: ScriptableObject
{
    /*
     * There are four different pieces of equipment that a unit is able to be given.
     * 
     * Slot 1: Personal
     *  The purpose if this slot is to grant boosts that affects the unit itself 
     *  through either effect or stat buffs. 
     *     
     * Slot 2: Combat
     *  The purpose of this slot is to alter the dynamic of combat through various effects.
     * 
     * Slot 3: Others
     *  The purpose of this slot is to buff/debuff either your allies or enemies.
     * 
     * Slot 4: Miscellaneous
     *  The purpose of this slot is to add on additional effects that are either 
     *  exclusive to this slot or otherwise serve as a duplicate for one of the other slots.
     * 
    */

    [Header("Equipment General Information")]
    //This is the name of the unit.
    public new string name;

    //This is the equipment slot this object occupies.
    public enum Slot { None, A, B, C, D };
    [Tooltip("Slot A: affects unit\nSlot B: affects others\nSlot C: combat/field\nSlot D: miscellaneous")]
    public Slot slot;

    //This is the description for what the equipment does.
    [TextArea(3, 10)]
    public string description;

    //This indicated how much it costs for a unit to aquire a skill.
    public int cost;

    //This is for checking if a specific skill is already unlocked before this one can be learned.
    public Equipment prerequisite;


    [Header("Equipment Art Asset")]
    public Sprite icon;


    [Header("Weapon Effectiveness")]
    public bool armor;
    public bool cavalry;
    public bool flier;
    public bool infantry;


    //These enums will be used in the sections below.
    public enum CombatOrder { None, BeforeCombat, StartOfCombat, EndOfCombat }


    [Header("Stat Modifier Effect")]
    [Rename("Combat Order")]
    public CombatOrder combatOrder_statMod;
    public int HPModifier;
    public int atkModifier;
    public int spdModifier;
    public int defModifier;
    public int resModifier;


    [Header("Inheritability")]
    //This is to determine if a weapon is inheritable.
    [HideInInspector]
    public bool inheritable;
    [HideInInspector]
    public bool sword;
    [HideInInspector]
    public bool axe;
    [HideInInspector]
    public bool lance;
    [HideInInspector]
    public bool dagger;
    [HideInInspector]
    public bool bow;
    [HideInInspector]
    public bool magic;
    [HideInInspector]
    public bool beast;


    [Header("Restrictions")]
    [HideInInspector]
    public bool restrictions;
    [HideInInspector]
    public bool swordRestrict;
    [HideInInspector]
    public bool axeRestrict;
    [HideInInspector]
    public bool lanceRestrict;
    [HideInInspector]
    public bool daggerRestrict;
    [HideInInspector]
    public bool bowRestrict;
    [HideInInspector]
    public bool magicRestrict;
    [HideInInspector]
    public bool beastRestrict;
}
