using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainTile : ResourceTile
{
    public MountainTile(float q, float r) : base(q, r)
    {
        resourceType = ResourceTypes.Stone;
    }


    public MountainTile(BoardCoordinate boardCoordinate) : base(boardCoordinate)
    {
        resourceType = ResourceTypes.Stone;
    }
    public override string GetTypeAsString()
    {
        return "mountain";
    }

    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/SnowyMountain");
    }
}
