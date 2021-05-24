using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientActionsMasterBehaviour : MonoBehaviour
{


    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private BoardManagerBehaviour boardManager;

    [SerializeField]
    private MasterHighlighterBehaviour masterHighlighter;

    [SerializeField]
    private GameObject RobberDisplay;


    [SerializeField]
    private ServerSenderBehaviour serverSender;

    private int nbOfPlayersToGiveResources = 0;

    private int nbOfPlayersThatGaveResources = 0;


    public void GiveResourcesToThief() {
        StartCoroutine(GiveResourcesToThiefCoroutine());
    }

    public void MoveThief(int nbOfPlayersToGiveResources) {
        this.nbOfPlayersToGiveResources = nbOfPlayersToGiveResources;
        this.nbOfPlayersThatGaveResources = 0;
        StartCoroutine(MoveThiefCoroutine());
    }
    public void BuildVillage() {
        StartCoroutine(BuildVillageCoroutine());
    }

    public void BuildRoad() {
        StartCoroutine(BuildRoadCoroutine());
    }

    public void PlayerGaveResourcesToRobber() {
        Object obj = new Object();
        lock (obj)
        {
            nbOfPlayersThatGaveResources++;
        }
    }


    #region  Coroutines

    private IEnumerator GiveResourcesToThiefCoroutine() {
        RobberDisplay.SetActive(true);
        RobberDisplayBehaviour robberDisplayBehaviour = RobberDisplay.GetComponent<RobberDisplayBehaviour>();

        yield return StartCoroutine(robberDisplayBehaviour.WaitForUserToGiveResources());  

        string message = "playerLosesResourcesToThief " + UserInfo.GetUsername() + " ";

        var resourcesGiven = robberDisplayBehaviour.resourcesGiven;
        RobberDisplay.SetActive(false);

        message += resourcesGiven[0];

        for(int i = 1 ; i < resourcesGiven.Count; ++i) {
            message += "," + resourcesGiven[i];
        }


        serverSender.Send(message);
    }
    private IEnumerator MoveThiefCoroutine() {

        yield return new WaitUntil(() => nbOfPlayersThatGaveResources == nbOfPlayersToGiveResources);


        var positions = boardManager.PlacesWithoutThief();

        masterHighlighter.SpawnHighlighters(positions);

        yield return StartCoroutine(masterHighlighter.WaitForUserInput());

        Vector3 positionPressed = masterHighlighter.positionPressed;

        BoardCoordinate bc = BoardCoordinate.ToBoardCoordinate(positionPressed);

        string message = "moveThief " + UserInfo.GetUsername() + " " + bc.q + ";" + bc.r;

        serverSender.Send(message);
    }
    private IEnumerator BuildRoadCoroutine() {
        Player client = playerManager.clientPlayer;
        if(!playerManager.roadCraftingCost.verifCost(client)) {
            Debug.Log("Nu ai suficiente resurse ca sa faci un drum, sarakule");
            yield break;
        }


        var pairs = boardManager.GetAvailablePlacesForConnector(client);

        if(pairs.Count == 0) {
            Debug.Log("nu ai unde sa construiesti drumuri bre");
            yield break;
        }

        List<Vector3> positions = new List<Vector3>();

        foreach (var pair in pairs)
        {
            Vector3 middle = (pair.Key.transform.position + pair.Value.transform.position) / 2;
            positions.Add(middle);
        }

        masterHighlighter.SpawnHighlighters(positions);
        yield return StartCoroutine(masterHighlighter.WaitForUserInput());

        BoardCoordinate bc = BoardCoordinate.ToBoardCoordinate(masterHighlighter.positionPressed);

        string message = "placeConnector " + client.nickname + " " + bc.q + ";" + bc.r + " road";


        serverSender.Send(message);

    }
    private IEnumerator BuildVillageCoroutine() {
        Player client = playerManager.clientPlayer;
        if(!playerManager.villageCraftingCost.verifCost(client)) { 
            //probabil ca o sa fie cea cod fancy acilea
            Debug.Log("Nu ai suficiente resurse ca sa construiesti un sat, sarakule");
            yield break;
        }

        var gameObjects = boardManager.GetAvailableCornersForSettlement(client);

        if(gameObjects.Count == 0) {
            Debug.Log("Nu ai unde sa construiesti sate, sarakule");
            yield break;
        } 

        List<Vector3> positions = new List<Vector3>();

        foreach (var corner in gameObjects)
        {
            positions.Add(corner.transform.position);
        }

        masterHighlighter.SpawnHighlighters(positions);

        yield return StartCoroutine(masterHighlighter.WaitForUserInput());

        Vector3 positionPressed = masterHighlighter.positionPressed;

        BoardCoordinate bc = BoardCoordinate.ToBoardCoordinate(positionPressed);

        string message = "placeSettlement " + client.nickname + " " + bc.q + ";" + bc.r + " village";


        serverSender.Send(message);

    }

    #endregion Coroutines

}
