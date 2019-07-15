using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Special", menuName = "Special")]
public class Specials : ScriptableObject
{
    //There are two kinds of abilities that a unit can have at the same time: an assist and special.
    //Specials are abilities that are innate to the unit and generally used in a combat sense.

    //This specifies whether the ability is utilized within a combat interaction or outside of it (or both in the case of hybrid).
    public enum EffectField { Combat, Field, Hybrid };
    public EffectField effectField;
}
