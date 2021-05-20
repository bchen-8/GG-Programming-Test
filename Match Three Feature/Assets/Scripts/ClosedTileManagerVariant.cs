using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedTileManagerVariant : MonoBehaviour
{
    GameManagerVariation gameManagerVariant;

    public int listIndex {get; set;}

    void Awake()
    {
        gameManagerVariant = GameObject.Find("GameManagerVariation").GetComponent<GameManagerVariation>();
    }

    void OnMouseDown()
    {
        //Debug.Log(this.name);
        gameManagerVariant.OpenBox(gameObject);
    }

    public void RevealPrize(GameObject prize)
    {
        GameObject revealedPrize = Instantiate(prize, this.transform.position, Quaternion.identity);
        gameManagerVariant.SetPrizeInList(revealedPrize, listIndex);
        Destroy(this.gameObject);
    }
}
