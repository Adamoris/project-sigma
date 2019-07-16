using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBCardAnimations : MonoBehaviour
{
    Animator card_animator;
    // Start is called before the first frame update
    void Start()
    {
        card_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            card_animator.Play("Card Shift Up");
        }
    }
}
