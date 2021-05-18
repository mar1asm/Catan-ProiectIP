using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlighterBehaviour : MonoBehaviour
{


    public GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                // do whatever you want

                if (hit.transform == transform)
                {
                    Debug.Log("ai apasat pe mineeeee");
                    owner.GetComponent<MasterHighlighterBehaviour>().SetGaveInput(gameObject);
                }
            }
        }
    }
}
