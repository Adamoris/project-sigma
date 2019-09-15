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
    private bool highlighted;
    private Collider2D highlightedUnit;
    private Collider2D highlightedCell;

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
        if (y < mapHeight - 1 && ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && time >= keyDelay || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)))
        {
            y += 1;
            UpdateLocation();
            time = 0;
        }
        else if (y > 0 && ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && time >= keyDelay || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
        {
            y -= 1;
            UpdateLocation();
            time = 0;
        }
        else if (x > 0 && ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && time >= keyDelay || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)))
        {
            x -= 1;
            UpdateLocation();
            time = 0;
        }
        else if (x < mapWidth - 1 && ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && time >= keyDelay || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)))
        {
            x += 1;
            UpdateLocation();
            time = 0;
        }
        if (Input.GetKeyDown(KeyCode.Return) && highlighted == true)
        {
            //Debug.Log("entered " + highlightedUnit);
            //Debug.Log("entered1 " + highlightedCell);
            
            if (highlightedUnit != null)
            {
                var unit = highlightedUnit.GetComponent<GenericUnit>();
                unit.OnMouseDown();
                highlighted = false;
                Debug.Log(highlighted);
            }
            if (highlightedCell != null && highlighted == false)
            {
                var cell = highlightedCell.GetComponent<MySquare>();
                cell.OnMouseDown();
            }
            
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        var unit = other.GetComponent<GenericUnit>();
        var cell = other.GetComponent<MySquare>();

        //Debug.Log("unit " + unit);
        //Debug.Log("cell " + cell);
        //Debug.Log("test " + other);

        if (unit != null)
        {
            highlightedUnit = other;
            unit.OnMouseEnter();
            highlighted = true;
        }
        if (cell != null && highlighted == false)
        {
            //Debug.Log("aaa");
            highlightedCell = other;
            cell.OnMouseEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        highlightedUnit = null;
        highlightedCell = null;
        var unit = other.GetComponent<GenericUnit>();
        var cell = other.GetComponent<MySquare>();
        if (unit != null)
        {
            unit.OnMouseExit();
            highlighted = false;
        }
        if(cell != null)
        {
            cell.OnMouseExit();
            //highlighted = false;
        }
    }
}
