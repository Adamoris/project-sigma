using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehavior : MonoBehaviour
{
    public bool hideDefaultCursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible &= hideDefaultCursor != true;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }
}
