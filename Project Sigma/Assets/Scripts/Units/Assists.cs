using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Assist", menuName = "Assist")]
public class Assists : ScriptableObject
{
    //There are two kinds of abilities that a unit can have at the same time: an assist and special.
    //Assists are abilities of the unit which affects other units.
    
    [Header("Assist General Information")]
    //This is the name of the unit.
    public new string name;

    //This specifies whether the ability is utilized within a combat interaction or outside of it (or both in the case of hybrid).
    public enum EffectField { None, Combat, Field, Hybrid };
    public EffectField effectField;

    //This is the description for what the weapon does.
    [TextArea(3, 10)]
    public string description;
}
