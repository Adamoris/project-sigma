using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStatCalculator : MonoBehaviour
{
    [HideInInspector]
    public Card unitCard;
    // Start is called before the first frame update
    void Start()
    {
        unitCard = gameObject.GetComponent<GenericUnit>().card;
        //unitCard.HP = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
