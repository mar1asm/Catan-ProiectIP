using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNamesHolderBehaviour : MonoBehaviour
{


    void Start() {
        //StartCoroutine(Test(testNames));
    }


    public IEnumerator Test(List<string> names) {
        yield return StartCoroutine(WaitToChoosePlayer(names));

        Debug.LogWarning(stringChosen + "<- playerul ales");
    }

    [SerializeField]
    public GameObject playerNameChooserPrefab;

    private List<GameObject> objectsCreated = new List<GameObject>();

    private bool playerChose = false;

    public string stringChosen = "";


    public IEnumerator WaitToChoosePlayer(List<string> names) {
        playerChose = false;
        SpawnPlayerNames(names);
        stringChosen = "N/A";
        yield return new WaitUntil(() => playerChose);


        CleanUpChilds();
    }

    public void SetPlayerChose(string value) {
    
        stringChosen = value;
        playerChose = true;
    }

    private void SpawnPlayerNames(List<string> names) {
        foreach (var name in names)
        {
            GameObject gameObject = Instantiate(playerNameChooserPrefab, transform);
            gameObject.GetComponent<PlayerNameChooserBehaviour>().SetText(name);
            objectsCreated.Add(gameObject);
        }    
    }

    



    private void CleanUpChilds() {
        foreach (GameObject gameObject in objectsCreated)
        {
            Destroy(gameObject);
        }
        objectsCreated.Clear();
    }
}
