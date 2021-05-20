using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public class Symbol
    {
        public string name;
        public int value;

        public Symbol(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public override string ToString()
        {
            return name + ", " + value;
        }
    }

    string[] reelStrip = new string[] {"C", "A", "Coin", "D", "B", "A", "B", "C", "Coin", "A", "D", "C", "B", "A", "Coin", "C", "D", "A"}; //size = 18

    Symbol[] boardList = new Symbol[15];

    void Start()
    {
        
    }

    void GenerateBoard()
    {
        foreach (Symbol n in boardList) {
            string reelPick = reelStrip[Random.Range(0, 18)];
            n.name = reelPick;
            if (reelPick == "Coin") {
                n.value = RollTable();
            } else {
                n.value = 0;
            }
        }
    }

    int RollTable()
    {
        int outcome = -1;
        int roll = Random.Range(1, 101);
        Debug.Log("GenerateOutcome(): Rolled " + roll);
        if (roll <= 60) {
            outcome = 50;
        } else if (roll > 60 && roll <= 75) {
            outcome = 75;
        } else if (roll > 75 && roll <= 85) {
            outcome = 125;
        } else if (roll > 85 && roll <= 95) {
            outcome = 275;
        } else if (roll > 95 && roll <= 97) {
            outcome = 500;
        } else if (roll > 98 && roll <= 100) {
            outcome = 1000;
        } else {
            Debug.Log("<color=red> GenerateOutcome(): Number rolled fell outside of bounds. </color>");
        }

        return outcome;
    }
}
