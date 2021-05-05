using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestTile : ResourceTile
{

    public ForestTile(float q, float r) : base(q, r)
    {
        resourceType = ResourceTypes.Wood;
    }


    public ForestTile(BoardCoordinate boardCoordinate) : base(boardCoordinate)
    {
        resourceType = ResourceTypes.Wood;
    }


    public override string GetTypeAsString()
    {
        return "forest";
    }

    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/Forest");
    }


}
