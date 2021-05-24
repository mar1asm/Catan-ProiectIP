using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyColorAndTransparency : MonoBehaviour
{
    [SerializeField]
    private GameObject vfx;




    private void Start()
    {
        //vfx.GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0, 0));
        vfx.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.5f);
    }

   

}
