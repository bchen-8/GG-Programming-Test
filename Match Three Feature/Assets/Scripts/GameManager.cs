using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameManager gameManager;

    public GameObject closedTile;
    public GameObject miniTile;
    public GameObject minorTile;
    public GameObject maxiTile;
    public GameObject majorTile;
    public GameObject grandTile;

    int[] prizeCount = new int[5];

    List<GameObject> boardList = new List<GameObject>();

    void Awake()
    {
        //Sets gameManager to this, destroys itself if another instance of gameManager is present in the scene already
        if (!gameManager) {
            gameManager = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        //Instantiates tile objects
        closedTile = Resources.Load<GameObject>("Prefabs/ClosedTile");
        miniTile = Resources.Load<GameObject>("Prefabs/MiniTile");
        minorTile = Resources.Load<GameObject>("Prefabs/MinorTile");
        maxiTile = Resources.Load<GameObject>("Prefabs/MaxiTile");
        majorTile = Resources.Load<GameObject>("Prefabs/MajorTile");
        grandTile = Resources.Load<GameObject>("Prefabs/GrandTile");
    }

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        
    }

    #region Methods
    private void GenerateGrid() //Spawn 3x5 grid of closed boxes, track with a list
    {

    }

    private void CheckVictory() //Check for the Victory condition
    {

    }

    private void VictoryCondition()
    {

    }
    #endregion
}
