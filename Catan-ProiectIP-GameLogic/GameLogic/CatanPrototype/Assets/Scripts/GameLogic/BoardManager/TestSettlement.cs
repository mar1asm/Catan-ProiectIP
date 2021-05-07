using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//made by jon
public class TestSettlement : Settlement
{
    public TestSettlement(Corner corner): base(corner)
    {
    }

    public override int GetNumberOfPoints()
    {
        throw new System.NotImplementedException();
    }

    public override int GetNumberOfResources()
    {
        throw new System.NotImplementedException();
    }

    public override void LoadVFX()
    {
        throw new System.NotImplementedException();
    }
}