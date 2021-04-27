using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Connector
{
    public Boat(Corner c1,Corner c2) : base(c1,c2)
    {
    }
    public override void LoadVFX()
    {
        throw new System.NotImplementedException();
    }

}
