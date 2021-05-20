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


    }

    void Start()
    {
        GenerateGrid();

        /* test debug to check if list was correctly populated
        Debug.Log("Coloring all instances of closed boxes red");
        foreach (GameObject n in boardList) {
            SpriteRenderer nSpriteRenderer = n.GetComponent<SpriteRenderer>();
            nSpriteRenderer.color = new Color (1,0,0);
        }
        */
    }

    void Update()
    {
        
    }

    #region Methods
    private void GenerateGrid() //Spawn 3x5 grid of closed boxes, track with a list
    {

        for (float i = 3.5f; i >= -3.5f; i -= 3.5f) { //runs down y coords

            for (float j = -8f; j <= 8f; j += 4f) { //runs down x coords
                SpawnBox(j, i);
            }
        }
    }

    private void SpawnBox(float xCoord, float yCoord) //Spawns a closed box and adds it to the boardList
    {
        GameObject spawnInstance = Instantiate(closedTile, new Vector2(xCoord, yCoord), Quaternion.identity);
        boardList.Add(spawnInstance);
    }

    private void CheckVictory() //Check for the Victory condition
    {

    }

    private void VictoryCondition()
    {

    }
    #endregion
}
