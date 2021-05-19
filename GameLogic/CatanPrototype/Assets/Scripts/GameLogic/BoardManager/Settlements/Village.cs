using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Village : Settlement
{

    public Village(Corner corner): base(corner)
    {
    }
    public override int GetNumberOfPoints()
    {
        return 1;
    }

    public override int GetNumberOfResources()
    {
        return 1;
    }

    public override void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/Settlements/SmallSettlement");
    }
}
