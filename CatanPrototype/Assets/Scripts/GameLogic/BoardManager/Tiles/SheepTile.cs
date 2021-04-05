using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepTile : ResourceTile
{

    public SheepTile(float q, float r) : base(q, r)
    {
        resourceType = ResourceTypes.Sheep;
    }


    public SheepTile(BoardCoordinate boardCoordinate) : base(boardCoordinate)
    {
        resourceType = ResourceTypes.Sheep;
    }
    public override string GetTypeAsString()
    {
        return "sheep";
    }

    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/FencedLand");
    }
}
