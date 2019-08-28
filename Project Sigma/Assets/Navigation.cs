using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public CellGrid cellGrid;
    public float stepSize = 1f;
    public float keyDelay = 0.2f;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) && time >= keyDelay || Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + stepSize, 0);
            time = 0;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && time >= keyDelay || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - stepSize, 0);
            time = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && time >= keyDelay || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - stepSize, transform.position.y, 0);
            time = 0;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && time >= keyDelay || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + stepSize, transform.position.y, 0);
            time = 0;
        }
    }
}
