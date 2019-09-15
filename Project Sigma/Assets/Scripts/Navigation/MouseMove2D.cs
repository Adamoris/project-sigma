using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove2D : MonoBehaviour
{
    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }

    }
}
