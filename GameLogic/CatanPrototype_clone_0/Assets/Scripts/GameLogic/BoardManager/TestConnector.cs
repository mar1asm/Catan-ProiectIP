using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//made by jon

public class TestConnector : Connector
{
    public TestConnector(Corner c1, Corner c2) : base(c1, c2)
    {
    }

    public override void LoadVFX()
    {
        throw new System.NotImplementedException();
    }
}