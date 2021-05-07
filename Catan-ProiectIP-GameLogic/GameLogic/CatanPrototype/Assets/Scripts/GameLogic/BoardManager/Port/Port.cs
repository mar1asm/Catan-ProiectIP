using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Port : ILoadVFX
{
    public ResourceTypes resourceToTrade = ResourceTypes.Any;
    //3 : 1, 2 : 1... cel mai bine folosim un Vector2Int
    private Vector2Int ratio;

    private GameObject VFX;

    private GameObject inGameObject;


    public Corner[] corners = new Corner[2];
    public Port(Corner c1, Corner c2, ResourceTypes resourceType, Vector2Int ratio)
    {
        LoadVFX();
        corners[0] = c1;
        corners[1] = c2;
        this.resourceToTrade = resourceType;
        this.ratio = ratio;
       
    }

    public BoardCoordinate middle
    {
        get
        {
            BoardCoordinate bc1 = corners[0].coordinate;
            BoardCoordinate bc2 = corners[1].coordinate;

            return (bc1 + bc2) / 2;
        }
    }

    public Quaternion rotation
    {
        get
        {
            Vector3 v1 = corners[0].coordinate.ToWorldSpace();
            Vector3 v2 = corners[1].coordinate.ToWorldSpace();

            Vector3 relPos = v2 - v1;
            Quaternion rotation = Quaternion.LookRotation(relPos, Vector3.up);
            Debug.Log(rotation.eulerAngles);
            return rotation;
        }
    }

    public int resourcesNeeded
    {
        get
        {
            return ratio.x;
        }
    }


    public int resourcesObtained
    {
        get
        {
            return ratio.y;
        }
    }
    public GameObject AddVFX2Object(GameObject parent)
    {
        if (inGameObject == null)
        {
            inGameObject = parent;
        }
        return GameObject.Instantiate(VFX, parent.transform);
    }

    public void LoadVFX()
    {
        VFX = (GameObject)Resources.Load("GameLogic/Prefabs/PortVFX");
    }
}
