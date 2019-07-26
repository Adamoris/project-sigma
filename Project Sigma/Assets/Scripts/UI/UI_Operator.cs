using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Operator : MonoBehaviour
{
    public GameObject cardDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCard()
    {
        cardDisplay.SetActive(true);
    }

    public void HideCard()
    {
        cardDisplay.SetActive(false);
    }
}
