using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade 
{
    public List<ResourceTypes> resourcesNeeded = new List<ResourceTypes>();
    public List<ResourceTypes> resourcesObtained = new List<ResourceTypes>();
    public bool oneTimeUse;

    public Trade(bool oneTimeUse = false)
    {
        this.oneTimeUse = oneTimeUse;
    }


    public override string ToString()
    {
        string toReturn = "";
        toReturn += "Resources needed: ";
        foreach(ResourceTypes resource in resourcesNeeded)
        {
            toReturn += resource;
            toReturn += " ";
        }

        toReturn += "\nResources obtained: "; 
        foreach (ResourceTypes resource in resourcesObtained)
        {
            toReturn += resource;
            toReturn += " ";

        }

        return toReturn;
    }
    public void AddResourceNeeded(ResourceTypes resource)
    {
        resourcesNeeded.Add(resource);
    }

    public void AddResourceObtained(ResourceTypes resource)
    {
        resourcesObtained.Add(resource);
    }
}
