using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public Sprite artwork;

    public new string name;
    public string merge;

    public string affinity;

    public int HP;
    public int Atk;
    public int Spd;
    public int Def;
    public int Res;
}

