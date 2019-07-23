using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Reference", menuName = "Universal")]
public class Universal : ScriptableObject
{
    [Header("Move Class Base Stats")]
    public int armorMovement;
    public int cavalryMovement;
    public int flierMovement;
    public int infantryMovement;


    [Header("Terrain Costs")]
    public int forestCost;
    public int mountainsCost;
    public int plainsCost;


    [Header("Player Information")]
    public new string name;
    public int playerLevel;
    public int premiumCurrency;
}
