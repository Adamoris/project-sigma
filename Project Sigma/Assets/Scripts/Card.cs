using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public Sprite artwork;

    public new string name;
    public int merge;

    public string affinity;

    public int HP;
    public int Atk;
    public int Spd;
    public int Def;
    public int Res;
    public int Level;

    // The IV value is an int ranging from 0-20 which
    // determines the boon/bane combination of an individual unit.
    // 0 = neutral
    // 1 = +HP/-Atk
    // 2 = +HP/-Spd
    // 3 = +HP/-Def
    // 4 = +HP/-Res
    // 5 = +Atk/-HP
    // 6 = +Atk/-Spd
    // 7 = +Atk/-Def
    // 8 = +Atk/-Res
    // 9 = +Spd/-HP
    // 10 = +Spd/-Atk
    // 11 = +Spd/-Def
    // 12 = +Spd/-Res
    // 13 = +Def/-HP
    // 14 = +Def/-Atk
    // 15 = +Def/-Spd
    // 16 = +Def/-Res
    // 17 = +Res/-HP
    // 18 = +Res/-Atk
    // 19 = +Res/-Spd
    // 20 = +Res/-Def

    public int IV;
}

