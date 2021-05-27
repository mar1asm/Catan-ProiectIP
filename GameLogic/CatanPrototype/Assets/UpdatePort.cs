using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePort : MonoBehaviour
{
    // Start is called before the first frame update
    private int _raportx;
    private int _raporty;
    private ResourceTypes _resource;
    public int raportx
    {
        get
        {
            return _raportx;
        }

        set
        {
            _raportx = value;
         
        }
    }
    public int raporty
    {
        get
        {
            return _raporty;
        }

        set
        {
            _raporty = value;
           // updateRaport();
        }
    }
    public ResourceTypes resource
    {
        get
        {
            return _resource;
        }

        set
        {
            _resource = value;
           // updateResources();
        }
    }
    public UpdatePort(int x, int y)
    {
        raportx = x;
        raporty = y;
    }
    public UpdatePort(Vector2Int ratio)
    {
        raportx = ratio[0];
        raporty = ratio[1];
    }
   public void updateRaport()
    {
        Debug.Log("nu e bine uite cat este x si y ");
        Debug.Log(raportx);
        Debug.Log(raporty);
        GameObject vfx = transform.GetChild(1).gameObject; // asta e raportul
        for (int i = 0; i < vfx.transform.childCount; ++i)
        {
            
            vfx.transform.GetChild(i).gameObject.SetActive(false); // toti pe  false
           
        }
        if(raportx==2 && raporty==1)
         vfx.transform.GetChild(0).gameObject.SetActive(true); // 2-1 pe true
        else
            vfx.transform.GetChild(1).gameObject.SetActive(true); // 3-1 pe true 

    }
    public void updateResources()
    {
        GameObject vfx = transform.GetChild(2).gameObject; // asta e raportul
        for (int i = 0; i < vfx.transform.childCount; ++i)
        {

            vfx.transform.GetChild(i).gameObject.SetActive(false); // toti pe  false

        }
        if (resource == ResourceTypes.Wood)
        {
            vfx.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (resource == ResourceTypes.Sheep)
        {
            vfx.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if(resource == ResourceTypes.Wheat)
        {
            vfx.transform.GetChild(2).gameObject.SetActive(true);
        }
        else if (resource == ResourceTypes.Brick)
        {
            vfx.transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (resource == ResourceTypes.Stone)
        {
            vfx.transform.GetChild(4).gameObject.SetActive(true);
        }

    }
}
