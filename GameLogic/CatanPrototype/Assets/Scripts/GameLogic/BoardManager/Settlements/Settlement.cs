using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Settlement 
{
    public Corner corner;
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
            //VFX.GetComponent<ChangingColorBehaviour>().UpdateColor(_owner);
            //O sa trebuiasca pus cod aici care sa schimbe culoarea cum trebuie
        }
    }
    public GameObject VFX;


    protected Settlement(Corner corner)
    {
        LoadVFX();
        this.corner = corner;
        this.corner.settlement = this;
    }

    public abstract void LoadVFX();
    public virtual GameObject AddVFX2Object(GameObject parent)
    {
        return GameObject.Instantiate(VFX, parent.transform);
    }


    /// <summary>
    /// Genereaza resurse si le da player-ului
    /// (inca nu a fost facut PlayerManager, dupa ce e facut trebuie implementat aici)
    /// </summary>
    public void GenerateResources(ResourceTypes resourceType)
    {
        //give any resources list to the owner
        List<ResourceTypes> l =new List<ResourceTypes>();
        l.Add(resourceType); //nu stiu de unde as putea lua tipul de resursa -Alexandra
        _owner.GetResources(l);
    }

    /// <summary>
    /// Cate resurse genereaza?
    /// </summary>
    /// <returns></returns>
    public abstract int GetNumberOfResources();

    /// <summary>
    /// Cate puncte valoreaza?
    /// </summary>
    /// <returns></returns>
    public abstract int GetNumberOfPoints();
}
