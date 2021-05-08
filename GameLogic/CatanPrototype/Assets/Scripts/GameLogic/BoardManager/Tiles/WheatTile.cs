using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatTile : ResourceTile
{

    public WheatTile(float q, float r) : base(q, r)
    {
        resourceType = ResourceTypes.Wheat;
    }


    public WheatTile(BoardCoordinate boardCoordinate) : base(boardCoordinate)
    {
        resourceType = ResourceTypes.Wheat;
    }

    public override string GetTypeAsString()
    {
        return "wheat";
    }

    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/HexTiles/HexaWheat");
    }




}
