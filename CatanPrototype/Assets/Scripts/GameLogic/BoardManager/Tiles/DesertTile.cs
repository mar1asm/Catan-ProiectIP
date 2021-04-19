using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertTile : Tile
{

    public DesertTile(float q, float r) : base(q, r)
    {
 
    }


    public DesertTile(BoardCoordinate boardCoordinate) : base(boardCoordinate)
    {
       
    }
    public override string GetTypeAsString()
    {
        return  "desert";
    }

    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/DesertCacti");
    }


    //Nu stiu sigur daca avem ce special action sa implementam
    public override void SpecialAction()
    {
        
    }
}
