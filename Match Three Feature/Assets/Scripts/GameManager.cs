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

    #region Instantiation
    private void GenerateGrid() //Spawn 3x5 grid of closed boxes, track with a list
    {
        int count = 0;
        for (float i = 3.5f; i >= -3.5f; i -= 3.5f) { //runs down y coords
            for (float j = -8f; j <= 8f; j += 4f) { //runs down x coords
                SpawnBox(j, i, count);
                count++;
            }
        }
    }

    private void SpawnBox(float xCoord, float yCoord, int count) //Spawns a closed box and adds it to the boardList
    {
        GameObject spawnInstance = Instantiate(closedTile, new Vector2(xCoord, yCoord), Quaternion.identity);
        spawnInstance.GetComponent<ClosedTileManager>().listIndex = count;
        boardList.Add(spawnInstance);
    }
    #endregion

    #region Click Register
    public void OpenBox(GameObject obj)
    {
        ClosedTileManager objScript = obj.GetComponent<ClosedTileManager>();
        int objIndex = objScript.listIndex;
        //Debug.Log(objIndex);

        GameObject prize = PrizeRoll();
        objScript.RevealPrize(prize);
    }

    public void SetPrizeInList(GameObject obj, int index)
    {
        boardList[index] = obj;
    }

    private GameObject PrizeRoll() //Rolls the probability and returns the appropriate GameObject
    {
        int roll = Random.Range(1, 101);
        Debug.Log("PrizeRoll(): Rolled " + roll);

        if (roll <= 50) {
            Debug.Log("PrizeRoll(): Replacing tile with a Mini Tile");
            return miniTile;
        } else if (roll > 50 && roll <= 75) {
            Debug.Log("PrizeRoll(): Replacing tile with a Minor Tile");
            return minorTile;
        } else if (roll > 75 && roll <= 90) {
            Debug.Log("PrizeRoll(): Replacing tile with a Maxi Tile");
            return maxiTile;
        } else if (roll > 90 && roll <= 98) {
            Debug.Log("PrizeRoll(): Replacing tile with a Major Tile");
            return majorTile;
        } else if (roll > 98 && roll <= 100) {
            Debug.Log("PrizeRoll(): Replacing tile with a Grand Tile");
            return grandTile;
        } else {
            Debug.Log("<color=red> PrizeRoll(): Number rolled fell outside of bounds. </color>");
            return null;
        }
    }
    #endregion

    #region Victory Condition
    private void CheckVictory() //Check for the Victory condition
    {

    }

    private void VictoryCondition()
    {

    }
    #endregion
}
