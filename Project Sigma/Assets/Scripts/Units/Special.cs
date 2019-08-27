using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Special", menuName = "Special")]
public class Special : ScriptableObject
{
    //There are two kinds of abilities that a unit can have at the same time: an assist and special.
    //Specials are abilities that are innate to the unit and generally used in a combat sense.
    
    [Header("Special General Information")]
    //This is the name of the unit.
    public new string name;

    //This specifies whether the ability is utilized within a combat interaction or outside of it (or both in the case of hybrid).
    public enum EffectField { None, Combat, Field, Hybrid };
    public EffectField effectField;

    //This is the description for what the equipment does.
    [TextArea(3, 10)]
    public string description;

    //This is the cooldown value of the ability.
    public int cd;
}
