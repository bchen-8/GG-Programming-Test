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

    void OnMouseDown()
    {
        //Debug.Log(this.name);
        gameManager.OpenBox(gameObject);
    }

    public void RevealPrize(GameObject prize)
    {
        GameObject revealedPrize = Instantiate(prize, this.transform.position, Quaternion.identity);
        gameManager.SetPrizeInList(revealedPrize, listIndex);
        Destroy(this.gameObject);
    }
}
