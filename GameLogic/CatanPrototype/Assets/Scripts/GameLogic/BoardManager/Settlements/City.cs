using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : Settlement
{

    public City(Corner corner): base(corner)
    {

    }
    public override int GetNumberOfPoints()
    {
        return 2;
    }

    public override int GetNumberOfResources()
    {
        return 2;
    }

    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/Settlements/AdvancedCity");
    }
}
