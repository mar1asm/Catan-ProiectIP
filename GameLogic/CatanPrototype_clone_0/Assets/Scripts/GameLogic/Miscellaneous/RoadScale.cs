using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadScale : MonoBehaviour
{
    
    void Start()
    {
        transform.localScale = transform.localScale * 0.3f;
    }
}
