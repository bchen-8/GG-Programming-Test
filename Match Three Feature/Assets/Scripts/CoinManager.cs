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
        GenerateBoard();
        SumCoinValues();
    }

    void GenerateBoard() //Populates the boardList with Coin Values
    {
        for (int i = 0; i < 15; i++) {
            boardList[i] = new Symbol(null, 0);
        }

        string reelPick;
        foreach (Symbol n in boardList) {
            reelPick = reelStrip[Random.Range(0, 18)];
            Debug.Log("reelPick = "+reelPick);
            n.name = reelPick;

            if (reelPick == "Coin") {
                n.value = RollTable();
            }
        }
    }

    int RollTable() //Rolls Random.Range for a coin value and returns it as an int
    {
        int outcome = -1;
        int roll = Random.Range(1, 101);
        Debug.Log("RollTable(): Rolled " + roll);
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
            Debug.Log("<color=red> RollTable(): Number rolled fell outside of bounds. </color>");
        }

        return outcome;
    }

    void SumCoinValues() //Sums up and prints the value of all Coins in boardList
    {
        int sum = 0;
        int coinCount = 0;
        foreach (Symbol n in boardList) {
            if (n.name == "Coin") {
                coinCount++;
                sum += n.value;
                Debug.Log("SumCoinValues(): Adding "+n.value+" to sum, sum now equals "+sum);
            }
        }

        if (coinCount >= reelStrip.Length) {
            sum *= RollMultiplier();
        }

        Debug.Log("Final Coin Value: "+sum);
    }

    int RollMultiplier() //Rolls Random.Range for a multiplier value and returns it as an int
    {
        int outcome = -1;
        int roll = Random.Range(1, 101);
        Debug.Log("GenerateOutcome(): Rolled " + roll);
        if (roll <= 60) {
            outcome = 2;
        } else if (roll > 60 && roll <= 80) {
            outcome = 3;
        } else if (roll > 80 && roll <= 100) {
            outcome = 4;
        } else {
            Debug.Log("<color=red> RollMultiplier(): Number rolled fell outside of bounds. </color>");
        }

        return outcome;
    }
}
