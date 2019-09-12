using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    public Transform CellsParent;
    public Transform UnitsParent;
    public float keyDelay = 0.2f;
    private int mapWidth;
    private int mapHeight;


    [SerializeField] int x;
    [SerializeField] int y;

    float time;
    // Start is called before the first frame update
    void Start()
    {
        UpdateLocation();
        var map = CellsParent.GetComponent<RectangularSquareGridGenerator>();
        mapWidth = map.Width;
        mapHeight = map.Height;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (y < mapHeight - 1 && (Input.GetKey(KeyCode.UpArrow) && time >= keyDelay || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            y += 1;
            UpdateLocation();
            time = 0;
        }
        else if (y > 0 && (Input.GetKey(KeyCode.DownArrow) && time >= keyDelay || Input.GetKeyDown(KeyCode.DownArrow)))
        {
            y -= 1;
            UpdateLocation();
            time = 0;
        }
        else if (x > 0 && (Input.GetKey(KeyCode.LeftArrow) && time >= keyDelay || Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            x -= 1;
            UpdateLocation();
            time = 0;
        }
        else if (x < mapWidth - 1 && (Input.GetKey(KeyCode.RightArrow) && time >= keyDelay || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            x += 1;
            UpdateLocation();
            time = 0;
        }
    }

    void UpdateLocation()
    {
        foreach (Transform cell in CellsParent)
        {
            var cellPosition = cell.GetComponent<Cell>();
            if (cellPosition.x == x && cellPosition.y == y)
            {
                transform.position = cell.transform.position;
            }
        }
    }
}
