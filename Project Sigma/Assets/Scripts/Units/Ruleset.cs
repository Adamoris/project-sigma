using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ruleset", menuName = "Ruleset")]
public class Ruleset : ScriptableObject
{
    [Header("Class Base Movement")]
    public int armorMovement;
    public int cavalryMovement;
    public int flierMovement;
    public int infantryMovement;


    [Header("Terrain Costs")]
    public int forestCost;
    public int mountainsCost;
    public int plainsCost;


    [Header("Terrain Sprites")]
    public Sprite forestTile;
    public Sprite mountainTile;
    public Sprite plainsTile;


    [Header("Affinity Sprites")]
    public Sprite club;
    public Sprite spade;
    public Sprite heart;
    public Sprite diamond;


    [Header("Player Information")]
    public new string name;
    public int playerLevel;
    public int premiumCurrency;
}
