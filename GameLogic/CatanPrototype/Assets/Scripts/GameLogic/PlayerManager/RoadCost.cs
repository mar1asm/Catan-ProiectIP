using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCost : Building
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    public RoadCost(int nrSheep, int nrBrick, int nrWood, int nrWheat, int nrStone)
    {
        if (nrBrick > 0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Brick, nrBrick);
        }
        if (nrWood > 0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Wood, nrWood);
        }
        if (nrSheep>0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Sheep, nrSheep);
        }
        if (nrWheat>0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Wheat, nrWheat);
        }
        if (nrStone > 0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Stone, nrStone);
        }
    }
    public RoadCost()
    {
        cost.resourcesRequired.Add(ResourceTypes.Wood, 1);
        cost.resourcesRequired.Add(ResourceTypes.Brick, 1);
    }
    public void verifCost()
    {

    }
}
