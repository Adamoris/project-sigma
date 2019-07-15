using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Assist", menuName = "Assist")]
public class Assists : ScriptableObject
{
    //There are two kinds of abilities that a unit can have at the same time: an assist and special.
    //Assists are abilities of the unit which affects other units.

    //This specifies whether the ability is utilized within a combat interaction or outside of it (or both in the case of hybrid).
    public enum EffectField { Combat, Field, Hybrid };
    public EffectField effectField;
}
