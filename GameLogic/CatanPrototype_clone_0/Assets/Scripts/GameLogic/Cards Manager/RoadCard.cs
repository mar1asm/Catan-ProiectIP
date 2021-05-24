using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCard : DevelopmentCard
{
    // Start is called before the first frame update
    public RoadCard(string s)
    {
        type = s;
    }
    public override string getType()
    {
        return "point";
    }
}
