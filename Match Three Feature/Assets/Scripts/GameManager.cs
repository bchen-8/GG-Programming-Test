using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameManager gameManager;

    int[] prizeCount = new int[5];

    List<GameObject> boardList = new List<GameObject>();

    void Awake()
    {
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
