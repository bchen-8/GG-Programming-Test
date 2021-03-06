using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManagerVariation : MonoBehaviour
{
    GameManagerVariation gameManagerVariation;

    //List of prefabs, references set up in the Editor
    public GameObject closedTile;
    public GameObject miniTile;
    public GameObject minorTile;
    public GameObject maxiTile;
    public GameObject majorTile;
    public GameObject grandTile;

    public GameObject victoryScreen;
    public TextMeshPro victoryText;

    int prizeListIndex = 0;
    int victoryCondition = 0;
    int victoryCount = 0;
    GameObject[] prizeTypes;
    List<GameObject> boardList = new List<GameObject>();
    List<GameObject> prizeList = new List<GameObject>();

    void Awake()
    {
        //Sets gameManager to this, destroys itself if another instance of gameManager is present in the scene already
        if (!gameManagerVariation) {
            gameManagerVariation = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        prizeTypes = new GameObject[] { miniTile, minorTile, maxiTile, majorTile, grandTile };
    }

    void Start()
    {
        GenerateGrid();
        GenerateOutcome();
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

    private void GenerateOutcome() //Determines the outcome of the board
    {
        int[] prizeCount = new int[5]; //List keeps track of number of prizes
        int outcome = -1; //Value is set to the winning outcome by the following probability roll

        //Determine the winning prize
        int roll = Random.Range(1, 101);
        Debug.Log("GenerateOutcome(): Rolled " + roll);
        if (roll <= 50) {
            outcome = 0;
        } else if (roll > 50 && roll <= 75) {
            outcome = 1;
        } else if (roll > 75 && roll <= 90) {
            outcome = 2;
        } else if (roll > 90 && roll <= 98) {
            outcome = 3;
        } else if (roll > 98 && roll <= 100) {
            outcome = 4;
        } else {
            Debug.Log("<color=red> GenerateOutcome(): Number rolled fell outside of bounds. </color>");
        }
        prizeCount[outcome] = 3;
        victoryCondition = outcome;
        Debug.Log("prizeCount[" + outcome + "] = " + prizeCount[outcome]);

        for (int i = 0; i <= 4; i++) { //Sets number of prizes not part of the winning outcome to 0, 1, or 2
            if (i != outcome) {
                prizeCount[i] = Random.Range(0, 3);
                Debug.Log("prizeCount["+i+"] = "+prizeCount[i]);
            }
        }

        //Sets up prizeList according to the predetermined outcome from prizeCount
        for (int i = 0; i <= 4; i++) {
            if (prizeCount[i] > 0) {
                for (int j = 0; j < prizeCount[i]; j++) {
                    prizeList.Add(prizeTypes[i]);
                }
            }
        }

        //Shuffles prizeList
        var rand = new System.Random();
        List<GameObject> tempList = new List<GameObject>();
        var randomized = prizeList.OrderBy(item => rand.Next());
        foreach (var value in randomized) {
            tempList.Add(value);
        }
        prizeList = tempList;

        foreach (GameObject n in prizeList) {
            Debug.Log(n.name);
        }
    }

    private void SpawnBox(float xCoord, float yCoord, int count) //Spawns a closed box and adds it to the boardList
    {
        GameObject spawnInstance = Instantiate(closedTile, new Vector2(xCoord, yCoord), Quaternion.identity);
        spawnInstance.GetComponent<ClosedTileManagerVariant>().listIndex = count;
        boardList.Add(spawnInstance);
    }
    #endregion

    #region Click Register
    public void OpenBox(GameObject obj) //Player input tracked by OnMouseDown() in ClosedTileManagerVariant
    {
        ClosedTileManagerVariant objScript = obj.GetComponent<ClosedTileManagerVariant>();
        //int objIndex = objScript.listIndex;

        GameObject prize = prizeList[prizeListIndex];
        IncrementVictory();
        prizeListIndex++;
        objScript.RevealPrize(prize);
        CheckVictory();
    }

    public void SetPrizeInList(GameObject obj, int index)
    {
        boardList[index] = obj;
    }
    #endregion

    #region Victory Condition
    private void IncrementVictory()
    {
        if (prizeList[prizeListIndex].name == prizeTypes[victoryCondition].name) {
            Debug.Log("IncrementVictory(): "+prizeList[prizeListIndex].name+" is equal to "+ prizeTypes[victoryCondition].name);
            victoryCount++;
        }
    }

    private void CheckVictory() //Check for the Victory condition
    {
        if (victoryCount >= 3) {
            switch (victoryCondition) {
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
