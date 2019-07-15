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
    //private int HP;
    //private int Atk;
    //private int Def;
    //private int Res;
    //private int Spd;

    public enum Slot { A, B, C, D };
    public Slot slot;
}
