using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameManager gameManager;

    //List of prefabs, references set up in the Editor
    public GameObject closedTile;
    public GameObject miniTile;
    public GameObject minorTile;
    public GameObject maxiTile;
    public GameObject majorTile;
    public GameObject grandTile;

    public GameObject victoryScreen;
    public TextMeshPro victoryText;

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

    #region Scene Setup
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
    public void OpenBox(GameObject obj) //Player input tracked by OnMouseDown() in ClosedTileManager
    {
        ClosedTileManager objScript = obj.GetComponent<ClosedTileManager>();
        int objIndex = objScript.listIndex;

        GameObject prize = PrizeRoll(); //Receive results of PrizeRoll()
        objScript.RevealPrize(prize);
        CheckVictory();
    }

    public void SetPrizeInList(GameObject obj, int index)
    {
        boardList[index] = obj;
    }

    private GameObject PrizeRoll() //Rolls the probability, increments victory condition array, and returns the appropriate GameObject.
    {
        int roll = Random.Range(1, 101);
        Debug.Log("PrizeRoll(): Rolled " + roll);

        if (roll <= 50) {
            Debug.Log("PrizeRoll(): Replacing tile with a Mini Tile");
            prizeCount[0]++;
            return miniTile;
        } else if (roll > 50 && roll <= 75) {
            Debug.Log("PrizeRoll(): Replacing tile with a Minor Tile");
            prizeCount[1]++;
            return minorTile;
        } else if (roll > 75 && roll <= 90) {
            Debug.Log("PrizeRoll(): Replacing tile with a Maxi Tile");
            prizeCount[2]++;
            return maxiTile;
        } else if (roll > 90 && roll <= 98) {
            Debug.Log("PrizeRoll(): Replacing tile with a Major Tile");
            prizeCount[3]++;
            return majorTile;
        } else if (roll > 98 && roll <= 100) {
            Debug.Log("PrizeRoll(): Replacing tile with a Grand Tile");
            prizeCount[4]++;
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
        for (int i = 0; i <= 4; i++) {
            if (prizeCount[i] >= 3) {
                switch (i) {
                    case 0:
                        VictoryCondition("Mini Prize!", "MiniTile(Clone)");
                        break;
                    case 1:
                        VictoryCondition("Minor Prize!", "MinorTile(Clone)");
                        break;
                    case 2:
                        VictoryCondition("Maxi Prize!", "MaxiTile(Clone)");
                        break;
                    case 3:
                        VictoryCondition("Major Prize!", "MajorTile(Clone)");
                        break;
                    case 4:
                        VictoryCondition("Grand Prize!", "GrandTile(Clone)");
                        break;
                    default:
                        Debug.Log("<color=red> CheckVictory(): prizeCount index fell out of bounds. </color>");
                        break;
                }
            }
        }
    }

    private void VictoryCondition(string prizeText, string prizeType)
    {
        //Clears non-relevant prizes and unopened boxes
        foreach (GameObject n in boardList) {
            if (n.name != prizeType) {
                Destroy(n);
            }
        }

        //Sets up and reveals victory screen
        victoryText.text = prizeText;
        victoryScreen.SetActive(true);
    }
    #endregion
}
