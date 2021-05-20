using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedTileManager : MonoBehaviour
{
    GameManager gameManager;

    public int listIndex {get; set;}

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        //Debug.Log(this.name);
        gameManager.OpenBox(gameObject);
    }
}
