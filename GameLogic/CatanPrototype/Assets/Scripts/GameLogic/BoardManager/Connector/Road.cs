using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : Connector
{
    public Road(Corner c1, Corner c2) : base(c1,c2)
    {

    }
    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/Settlements/Road");
    }
}
