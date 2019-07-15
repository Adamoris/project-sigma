using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public Sprite Artwork;
    public Sprite mapSprite;

    
    
    //This is the name of the weapon.
    public new string name;

    //This is the description for what the weapon does.
    [TextArea(3,10)]
    public string description;

    //This is the might value of the weapon which is added on to the unit's atk stat.
    public int mt;

    //This specifies whether a weapon is ranged or not.
    public enum Range { Melee, Ranged }
    public Range range;

    //This is for melee weapons that can retaliate against ranged attacks.
    public bool DistantCounter { get; set; }

    //This is for ranged weapons that can retaliate against melee attacks.
    public bool CloseCounter { get; set; }

    //This is for applying the effect of an equipment item onto the weapon if needed.
    public Equipment equipmentEffect;

    //-----------------------------------------------------------------------------------
    //This section is for effects of the weapon for the wielder in the Player Phase.



    //This section is for effects of the weapon for the wielder in the Enemy Phase.



    //-----------------------------------------------------------------------------------
    //This section is for effects of the weapon for the foe in the Player Phase.



    //This section is for effects of the weapon for the foe in the Enemy Phase.



    //-----------------------------------------------------------------------------------
    //This section is for effects of the weapon for allies in the Player Phase.



    //This section is for effects of the weapon for allies in the Enemy Phase.



    //-----------------------------------------------------------------------------------
    //This section is for effects of the weapon for the foe's allies in the Player Phase.



    //This section is for effects of the weapon for the foe's allies in the Enemy Phase.



}
