using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private BoardManagerBehaviour boardManager;


    [SerializeField]
    private TurnManagerBehaviour turnManager;


    [SerializeField]
    private MasterHighlighterBehaviour masterHighlighter;


    private Player stefan, dragos, sebi, mihnea;

    void Start()
    {
        
        stefan = new Player("Stefan", "1", PlayerColor.Red);
        dragos = new Player("Dragos", "2", PlayerColor.Blue);
        sebi = new Player("Sebi", "3", PlayerColor.Green);
        

        playerManager.AddPlayer(stefan);
        playerManager.AddPlayer(dragos);
        playerManager.AddPlayer(sebi);

        playerManager.SetPointGoal(10);
        playerManager.Setup();

        StartCoroutine(SimulateGame());
    }


    //Folosit doar ca sa testam...
    IEnumerator SimulateGame()
    {

        #region testStefan
        yield return new WaitForSeconds(0.5f);
        boardManager.AddSettlement(
            stefan, 
            new Corner(new BoardCoordinate(-1.33f, 0.66f)),
            "village");

        Corner secondStef = new Corner(new BoardCoordinate(-1.33f, -0.33f));

        stefan.GetResources(boardManager.GetResourcesFromCorner(secondStef));

        boardManager.AddSettlement(
            stefan,
            secondStef,
            "village");

        boardManager.AddConnector(
            stefan,
            new Corner(new BoardCoordinate(-1.33f, 0.66f)),
            new Corner(new BoardCoordinate(-0.66f, 0.33f)),
            "road");

        boardManager.AddConnector(
            stefan,
            new Corner(new BoardCoordinate(-1.66f, -0.66f)),
            new Corner(new BoardCoordinate(-1.33f, -0.33f)),
            "road");

        #endregion testStefan;

        yield return new WaitForSeconds(0.25f);

        #region testSebi
        boardManager.AddSettlement(
           sebi,
           new Corner(new BoardCoordinate(-0.33f, -1.33f)),
           "village");

        Corner secondSebi = new Corner(new BoardCoordinate(0.66f, -1.33f));


        boardManager.AddSettlement(
            sebi,
            secondSebi,
            "village");


        sebi.GetResources(boardManager.GetResourcesFromCorner(secondSebi));

        boardManager.AddConnector(
            sebi,
            new Corner(new BoardCoordinate(-0.33f, -1.33f)),
            new Corner(new BoardCoordinate(-0.66f, -1.66f)),
            "road");

        boardManager.AddConnector(
            sebi,
            new Corner(new BoardCoordinate(0.66f, -1.33f)),
            new Corner(new BoardCoordinate(1.33f, -1.66f)),
            "road");

        #endregion testSebi
        yield return new WaitForSeconds(0.25f);

        #region testDragos
        boardManager.AddSettlement(
           dragos,
           new Corner(new BoardCoordinate(0.33f, 0.33f)),
           "village");


        Corner secondDragos = new Corner(new BoardCoordinate(-0.33f, 1.66f));

        boardManager.AddSettlement(
            dragos,
            secondDragos,
            "village");

        dragos.GetResources(boardManager.GetResourcesFromCorner(secondDragos));

        boardManager.AddConnector(
            dragos,
            new Corner(new BoardCoordinate(0.33f, 0.33f)),
            new Corner(new BoardCoordinate(0.66f, -0.33f)),
            "road");

        boardManager.AddConnector(
            dragos,
            new Corner(new BoardCoordinate(-0.33f, 1.66f)),
            new Corner(new BoardCoordinate(-0.66f, 1.33f)),
            "road");

        boardManager.AddConnector(
            dragos,
            new Corner(new BoardCoordinate(0.66f, -0.33f)),
            new Corner(new BoardCoordinate(0.33f, -0.66f)),
            "road");


        #endregion testDragos

        //var corners = boardManager.GetAvailableCornersForSettlement(dragos);

        //Debug.LogWarning("Number of corners:" + corners.Count);

        //foreach (var corner in corners)
        //{
        //    Debug.LogWarning(corner.GetComponent<CornerBehaviour>().corner.coordinate.q + " " +
        //        corner.GetComponent<CornerBehaviour>().corner.coordinate.r);
        //}

        // var pairs = boardManager.GetAvailablePlacesForConnector(dragos);

        // Debug.LogWarning("number of connectos: " + pairs.Count);

        // foreach (var pair in pairs)
        // {
        //    var firstCord = pair.Key.GetComponent<CornerBehaviour>().corner.coordinate;
        //    var secondCord = pair.Value.GetComponent<CornerBehaviour>().corner.coordinate;
        //    //Debug.LogWarning("Connector in: (" + firstCord.q + ", " + firstCord.r + ") - (" + secondCord.q + ", " + secondCord.r + ")");

        // }

        var pairs = boardManager.GetAvailablePlacesForConnector(dragos);

        List<Vector3> positions = new List<Vector3>();

        foreach (var pair in pairs)
        {
            Vector3 middle = (pair.Key.transform.position + pair.Value.transform.position) / 2;
            positions.Add(middle);
        }


        masterHighlighter.SpawnHighlighters(positions);
        
        yield return StartCoroutine(masterHighlighter.WaitForUserInput());
        
        Vector3 position = masterHighlighter.positionPressed;

        BoardCoordinate bcPosition = BoardCoordinate.ToBoardCoordinate(position);

        boardManager.AddConnector(dragos, bcPosition, "road");

        turnManager.Next();

        turnManager.TurnLogic(turnManager.currentPlayerIndex);

        //StartCoroutine(playerManager.PlayerMovesThief(stefan));

        //DisplayHandInfo();


        //Corner corner = new Corner(new BoardCoordinate())
        //boardManager.AddSettlement(stefan, )
    }


    private void DisplayHandInfo()
    {
        Debug.LogWarning("Cati playeri??" + playerManager.players.Count);
        foreach (var player in playerManager.players) 
        {
            if (player.GetHandInfo() == null) continue; 
            Debug.LogWarning(player.nickname + " " + player.GetHandInfo());
        }
    }
}
