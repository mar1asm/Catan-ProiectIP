using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTile : ResourceTile
{

    public BrickTile(float q, float r) : base(q, r)
    {
        resourceType = ResourceTypes.Brick;
    }


    public BrickTile(BoardCoordinate boardCoordinate) : base(boardCoordinate)
    {
        resourceType = ResourceTypes.Brick;
    }
    public override string GetTypeAsString()
    {
        return "brick";
    }

    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/CandyLand1");
    }
}
