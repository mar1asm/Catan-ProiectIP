using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Connector
{
    private Player _owner;
    public Player owner
    {
        get
        {
            return _owner;
        }
        set
        {
            _owner = value;
            //O sa trebuiasca pus cod aici care sa schimbe culoarea cum trebuie
        }
    }
    public GameObject VFX;

    public Corner[] corners = new Corner[2];


    public BoardCoordinate middle
    {
        get
        {
            return (corners[0].coordinate + corners[1].coordinate) / 2;
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

    protected Connector(Corner c1, Corner c2)
    {
        this.corners[0] = c1;
        this.corners[1] = c2;
        LoadVFX();
        //middle = (c1.coordinate + c2.coordinate) / 2;
        //middle = media(c1.coordinate + c2.coordinate);
    }

    public abstract void LoadVFX();

    public virtual GameObject AddVFX2Object(GameObject parent)
    {
        return GameObject.Instantiate(VFX, parent.transform);
    }







}