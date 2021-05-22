using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMasterBehaviour : MonoBehaviour
{

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private MasterHighlighterBehaviour masterHighlighter;

    [SerializeField]
    private BoardManagerBehaviour boardManager;

    [SerializeField]
    private ServerSenderBehaviour serverSender;
    public IEnumerator PlaceSettlementAndRoad() {
        yield return null;

        var corners =  boardManager.GetAvailableCornersForSettlementSetup();

        List<Vector3> positions = new List<Vector3>();

        foreach (var corner in corners)
        {
            positions.Add(corner.transform.position);
        }
        masterHighlighter.SpawnHighlighters(positions);

        yield return StartCoroutine(masterHighlighter.WaitForUserInput());

        var positionPressed = masterHighlighter.positionPressed;

        BoardCoordinate bc = BoardCoordinate.ToBoardCoordinate(positionPressed);

        //trimit imd
        //gata bre trimit acuma ho

        string message = "placeSettlement " + playerManager.clientPlayer.nickname + " " + bc.q + ";" + bc.r + " village"; 

        serverSender.Send(message);

        var pairsOfCorners = boardManager.GetConnectorPlacesForCorner(bc);

        positions.Clear();

        foreach (var pair in pairsOfCorners)
        {
            Vector3 middle = (pair.Key.transform.position + pair.Value.transform.position) / 2;
            positions.Add(middle);
        }


        masterHighlighter.SpawnHighlighters(positions);


        yield return StartCoroutine(masterHighlighter.WaitForUserInput());


        positionPressed =  masterHighlighter.positionPressed;


        bc = BoardCoordinate.ToBoardCoordinate(positionPressed);


        message = "placeConnector "  + playerManager.clientPlayer.nickname + " " + bc.q + ";" + bc.r + " road"; 


        serverSender.Send(message);

        message = "nextSetup";

        serverSender.Send(message);
        //trimit acum stai bre


    }
}
