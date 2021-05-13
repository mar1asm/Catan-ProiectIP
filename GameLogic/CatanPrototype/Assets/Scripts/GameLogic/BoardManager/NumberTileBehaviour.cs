using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTileBehaviour : MonoBehaviour
{
    private int _number;
    public int number
    {
        get
        {
            return _number;
        }

        set
        {
            _number = value;
            UpdateNumberTile();
        }
    }

    private void Start()
    {
        transform.position = new Vector3(transform.parent.position.x, 
                                         transform.position.y, 
                                         transform.parent.position.z);
    }


    private void UpdateNumberTile()
    {
        GameObject vfx = transform.GetChild(0).gameObject;
        for(int i = 0; i < vfx.transform.childCount; ++i)
        {
            //Debug.LogWarning(i);
            if(i == _number - 3)
            {
                vfx.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                vfx.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}
