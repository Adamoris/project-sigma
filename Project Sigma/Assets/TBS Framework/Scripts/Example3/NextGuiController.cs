using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextGuiController : MonoBehaviour
{
    public CellGrid CellGrid;

    public Button NextTurnButton;

    public GameObject InfoPanel;
    public GameObject GameOverPanel;
    public Transform CardAnchor;
    public Canvas Canvas;
    public Transform cameraPivot;

    private GameObject _infoPanel;
    private GameObject _gameOverPanel;

    private bool isGameOver;

    void Awake()
    {
        CellGrid.TurnEnded += OnTurnEnded;
        CellGrid.GameEnded += OnGameEnded;
        CellGrid.UnitAdded += OnUnitAdded;
    }


    private void OnTurnEnded(object sender, EventArgs e)
    {
        NextTurnButton.interactable = ((sender as CellGrid).CurrentPlayer is HumanPlayer);
    }
    private void OnGameEnded(object sender, EventArgs e)
    {
        isGameOver = true;
        _gameOverPanel = Instantiate(GameOverPanel);
        _gameOverPanel.transform.Find("InfoText").GetComponent<Text>().text = "Player " + ((sender as CellGrid).CurrentPlayerNumber + 1) + "\nwins!";
        
        _gameOverPanel.transform.Find("DismissButton").GetComponent<Button>().onClick.AddListener(DismissPanel);
 
        _gameOverPanel.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(), false);

    }

    private void OnUnitAttacked(object sender, AttackEventArgs e)
    {
        if (!(CellGrid.CurrentPlayer is HumanPlayer)) return;

        OnUnitDehighlighted(sender, e);

        if ((sender as Unit).HP <= 0) return;

        OnUnitHighlighted(sender, e);
    }
    private void OnUnitDestroyed(object sender, AttackEventArgs e)
    {
        Destroy(_infoPanel);
        Shift(-20, 0, 0);
    }
    private void OnUnitDehighlighted(object sender, EventArgs e)
    {
        if (isGameOver)
            return;

        Destroy(_infoPanel);
        //Shift(-20, 0, 0);
    }
    private void OnUnitHighlighted(object sender, EventArgs e)
    {
        if (isGameOver)
            return;

        var unit = sender as GenericUnit;
        _infoPanel = Instantiate(InfoPanel);

        float hpScale = (float)((float)(unit).HP / (float)(unit).TotalHP);

        _infoPanel.transform.Find("Name").GetComponent<Text>().text = unit.UnitName;
        _infoPanel.transform.Find("HitPoints").Find("Image").transform.localScale = new Vector3(hpScale,1,1);
        _infoPanel.transform.Find("Attack").Find("Image").transform.localScale = new Vector3((float)unit.Atk/10.0f,1,1);
        _infoPanel.transform.Find("Defence").Find("Image").transform.localScale = new Vector3((float)unit.Def / 10.0f, 1, 1);

        _infoPanel.GetComponent<RectTransform>().SetParent(Canvas.GetComponent<RectTransform>(),false);
        _infoPanel.SetActive(false);
        //Shift(20, 0, 0);
        
    }
    private void OnUnitClicked(object sender, EventArgs e)
    {
        if (isGameOver)
            return;
        var unit = sender as GenericUnit;
        cameraPivot.position = unit.transform.position;
        //Shift(20, 0, 0);
    }
    private void OnUnitDeselected(object sender, EventArgs e)
    {
        if (isGameOver)
            return;
        var unit = sender as GenericUnit;
        //Shift(-20, 0, 0);
    }
    private void OnUnitAdded(object sender, UnitCreatedEventArgs e)
    {
        RegisterUnit(e.unit);
    }

    private void RegisterUnit(Transform unit)
    {
        unit.GetComponent<Unit>().UnitHighlighted += OnUnitHighlighted;
        unit.GetComponent<Unit>().UnitDehighlighted += OnUnitDehighlighted;
        unit.GetComponent<Unit>().UnitDestroyed += OnUnitDestroyed;
        unit.GetComponent<Unit>().UnitAttacked += OnUnitAttacked;
        unit.GetComponent<Unit>().UnitClicked += OnUnitClicked;
        unit.GetComponent<Unit>().UnitDeselected += OnUnitDeselected;
    }
    public void DismissPanel()
    {
        Destroy(_gameOverPanel);
    }
    public void RestartLevel()
    {
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Shift(float x, float y, float z)
    {
        Vector3 temp = new Vector3(x, y, z);
        CardAnchor.position += temp;
    }
}

