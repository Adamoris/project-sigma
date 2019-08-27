using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Assist", menuName = "Assist")]
public class Assist : ScriptableObject
{
    //There are two kinds of abilities that a unit can have at the same time: an assist and special.
    //Assists are abilities of the unit which affects other units.
    
    [Header("Assist General Information")]
    //This is the name of the unit.
    public new string name;

    //This is the description for what the weapon does.
    [TextArea(3, 10)]
    public string description;

    //This specifies whether the ability is utilized within a combat interaction or outside of it (or both in the case of hybrid).
    public enum AssistType { None, Heal, Positional, Stats };
    public AssistType assistType;

    //This specifies who the intended target for the assist is.
    public enum Target { None, Other, Self, Mutual }
    [HideInInspector]
    public Target target;

    //These variables are used to specify the field buffs an assist skill may grant.
    [HideInInspector]
    public int buffAtk;
    [HideInInspector]
    public int buffSpd;
    [HideInInspector]
    public int buffDef;
    [HideInInspector]
    public int buffRes;

    //These variables are used for additional details.
    [HideInInspector]
    public bool negatePenalties;
    [HideInInspector]
    public int assistDuration = 1;

    //These variables are used when dealing with assists that heal HP.
    [HideInInspector]
    public int healAmount;
}
