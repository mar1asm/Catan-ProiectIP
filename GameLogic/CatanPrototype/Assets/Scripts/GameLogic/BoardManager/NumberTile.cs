using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NumberTile 
{
    int Value;

    public BoardCoordinate coordinate;

    public NumberTile()
    {
        Value = 0;
        coordinate = null;
    }
    public NumberTile(int value, BoardCoordinate coordinate)
    {
        Value = value;
        this.coordinate = coordinate;
    }
    public NumberTile(BoardCoordinate coordinate)
    {
        Value = 1;
        this.coordinate = coordinate;
    }
}
