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


    private Player stefan, dragos, sebi, mihnea;

    void Start()
    {
        stefan = new Player("Stefan", "1", PlayerColor.Red);
        dragos = new Player("Dragos", "2", PlayerColor.Blue);
        sebi = new Player("Sebi", "3", PlayerColor.Green);
        

        playerManager.AddPlayer(stefan);
        playerManager.AddPlayer(dragos);
        playerManager.AddPlayer(sebi);
        playerManager.AddPlayer(mihnea);

        playerManager.SetPointGoal(10);
        playerManager.Setup();

        StartCoroutine(SimulateGame());
    }


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

        yield return new WaitForSeconds(0.25f);


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
            new Corner(new BoardCoordinate(1.33f, -0.66f)),
            "road");

        var corners = boardManager.GetAvailableCornersForSettlement(dragos);

        Debug.LogWarning("Number of corners:" + corners.Count);

        foreach (var corner in corners)
        {
            Debug.LogWarning(corner.GetComponent<CornerBehaviour>().corner.coordinate.q + " " +
                corner.GetComponent<CornerBehaviour>().corner.coordinate.r);
        }

        turnManager.Next();

        turnManager.TurnLogic(turnManager.currentPlayerIndex);

        StartCoroutine(playerManager.PlayerMovesThief(stefan));

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