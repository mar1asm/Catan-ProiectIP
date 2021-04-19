using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementBehaviour : MonoBehaviour
{

    private Settlement _settlement;
    public Settlement settlement
    {
        get
        {
            return _settlement;
        }

        set
        {
            _settlement = value;
            CleanVFX();
            _settlement.AddVFX2Object(transform.GetChild(0).gameObject);
        }
    }

    private void CleanVFX()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            child.gameObject.SetActive(false);
        }
    }
    void Awake()
    {
        //distrug cubul care tine locul
        CleanVFX();
    }
}
