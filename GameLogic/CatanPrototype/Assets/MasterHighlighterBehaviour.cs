using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterHighlighterBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject hightLighter;

    private List<GameObject> objectsCreated = new List<GameObject>();

    [SerializeField]
    private bool userGaveInput = false;
    private GameObject objectPressed = null;

    public Vector3 positionPressed;

    public bool waiting = false;


    private void Start()
    {

    }


    public void SpawnHighlighters(List<Vector3> positions)
    {
        foreach (var position in positions)
        {
            GameObject  highLighter = Instantiate(hightLighter, position, Quaternion.identity, transform);
            highLighter.GetComponent<HighlighterBehaviour>().owner = gameObject;
            objectsCreated.Add(highLighter);
        }
    }

    public IEnumerator WaitForUserInput()
    { 
        waiting = true;
        userGaveInput = false;
        objectPressed = null;
        yield return new WaitUntil(() => userGaveInput);

        positionPressed = objectPressed.transform.position;
        waiting = false;

        foreach (var high in objectsCreated)
        {
            Destroy(high);
        }
    }

    public void SetGaveInput(GameObject objectPressed)
    {
        Debug.Log("intru acilea");
        this.userGaveInput = true;
        this.objectPressed = objectPressed; 
    }
}
