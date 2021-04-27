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

    public BoardCoordinate middle;

    protected Connector(Corner c1,Corner c2)
    {
        this.corners[0] = c1;
        this.corners[1] = c2;
        middle = (c1.coordinate + c2.coordinate) / 2;
        //middle = media(c1.coordinate + c2.coordinate);
    }

    public abstract void LoadVFX();

    public virtual GameObject AddVFX2Object(GameObject parent)
    {
        return GameObject.Instantiate(VFX, parent.transform);
    }







}
