using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestTile : Tile
{

    public TestTile(float q, float r): base(q, r)
    {
        
    }

    public TestTile(BoardCoordinate boardCoordinate) : base(boardCoordinate)
    {
    }


    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/Forest");
    }
    public override string GetTypeAsString()
    {
        return "Test";
    }

    public override void SpecialAction()
    {
        Debug.Log("Test");
    }
}
