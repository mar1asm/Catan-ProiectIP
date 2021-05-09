using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentCardCost : Building
{
    public DevelopmentCardCost(int nrSheep, int nrBrick, int nrWood, int nrWheat, int nrStone)
    {
        if (nrBrick > 0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Brick, nrBrick);
        }
        if (nrWood > 0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Wood, nrWood);
        }
        if (nrSheep > 0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Sheep, nrSheep);
        }
        if (nrWheat > 0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Wheat, nrWheat);
        }
        if (nrStone > 0)
        {
            cost.resourcesRequired.Add(ResourceTypes.Stone, nrStone);
        }
    }
    public DevelopmentCardCost()
    {
        cost.resourcesRequired.Add(ResourceTypes.Wheat, 1);
        cost.resourcesRequired.Add(ResourceTypes.Stone, 1);
        cost.resourcesRequired.Add(ResourceTypes.Sheep, 1);
    }
}
