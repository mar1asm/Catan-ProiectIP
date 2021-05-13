using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortBehaviour : MonoBehaviour
{
    private Port _port;

    public Port port
    {
        get
        {
            return _port;
        }
        set
        {
            _port = value;
            _port.AddVFX2Object(gameObject.transform.GetChild(0).gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
