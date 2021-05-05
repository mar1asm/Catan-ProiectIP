using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Corner 
{
    public BoardCoordinate coordinate;
    public Settlement settlement;

    public GameObject inGameObject;
    public Corner(BoardCoordinate boardCoordinate)
    {
        this.coordinate = boardCoordinate;
    }

    /// <summary>
    /// Activeaza efectul settlementului din acest colt
    /// </summary>
    public void ActivateSettlement()
    {
        if (settlement == null) return;
        settlement.GenerateResources();
    }
}
