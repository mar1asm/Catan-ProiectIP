using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string nickname;
    public string ID;
    public PlayerColor color;
    int score;
    //DeckPlayer deck;

    public void ScoreAdd(int value)
    {
        score = score + value;
    }

    public void ScoreSet(int value)
    {
        score = value;
    }
}
