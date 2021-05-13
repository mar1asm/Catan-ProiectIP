using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorBehaviour : MonoBehaviour
{
    private Connector _connector;

    public Connector connector
    {
        get
        {
            return _connector;
        }

        set
        {
            _connector = value;
            CleanVFX();
            GameObject vfx = _connector.AddVFX2Object(transform.GetChild(0).gameObject);
            vfx.GetComponent<ChangingColorBehaviour>().UpdateColor(_connector.owner);
        }
    }


    private void CleanVFX()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            child.gameObject.SetActive(false);
        }
    }

}