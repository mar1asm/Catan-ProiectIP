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
    Board board;
    DeckPlayer deck;
    public Player(string name, string id, Board b)
    {
        nickname = name;
        ID = id;
        board = b;
    }
    public void ScoreAdd(int value)
    {
        score = score + value;
    }

    public void ScoreSet(int value)
    {
        score = value;
    }

    public Settlement PlaceSettlement(BoardCoordinate boardCoordinate, string type)
    {
        board.PlaceSettlement(this, boardCoordinate, type);
        return null;
    }

    public Connector PlaceConnector(BoardCoordinate bc1, BoardCoordinate bc2, string type)
    {
        board.PlaceConnector(this, bc1, bc2, type);
        return null;
    }
}
