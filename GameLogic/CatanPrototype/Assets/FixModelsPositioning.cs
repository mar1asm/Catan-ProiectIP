using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script folosit pentru a corecta pozitia hexagoanelor si a obiectelor de pe el
/// </summary>
public class FixModelsPositioning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 parentPosition = transform.position;
        Transform hexTransform = transform;
        foreach (Transform childrenTransform in transform)
        {
            if(childrenTransform.gameObject.name.Equals("Hex3"))
            {
                hexTransform = childrenTransform;
                break;
            }
        }

        Vector3 corection = -(hexTransform.position - parentPosition);

        Debug.Log(corection);

        hexTransform.position = parentPosition + Vector3.zero;


        foreach (Transform childrenTransform in transform)
        {
            if (childrenTransform.gameObject.name.Equals("Hex3")) continue;
            childrenTransform.position += corection;
        }

        //hexPosition.position = Vector3.zero

    }
}
