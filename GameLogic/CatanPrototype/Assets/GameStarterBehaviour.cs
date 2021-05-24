using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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

    [SerializeField]
    private CardManager cardManager;


    [SerializeField]
    private ServerSenderBehaviour serverSender;


    


    private List<string> usernames = new List<string>();
    private List<PlayerColor> playerColors = new List<PlayerColor>();

    void Start()
    {
        if(!UserInfo.IsHost()) return;

        StartCoroutine(StartGame());

    }


    IEnumerator StartGame() {
        //Aflam toti utilizatorii
        yield return StartCoroutine(GetAllUsersInGameSession());

        List<PlayerColor> availableColors = new List<PlayerColor>();

        for(int i = 0 ; i < usernames.Count; ++i)  {
            availableColors.Add((PlayerColor)i);
        }

        
        RandomizeColors(availableColors);
        
        Coroutine sendUsersThread = StartCoroutine(SendUserInfoToAll());


        for(int i = 0 ; i < usernames.Count; ++i) {
            playerManager.AddPlayer(new Player(usernames[i], "?", playerColors[i]));
        }

        //Generam board-ul si dupa trimitem si la ceilalti

        //O sa facem aici un get si obtinem json-ul care reprezinta jocul.... dar pun de mana acum
        string message;
        boardManager.InitializeBoardFromFile("GameLogic/inimioara");
        boardManager.InstantiateBoard();

        foreach (var pair in boardManager.board.tiles)
        {
            Tile tile = pair.Value;
            message = "placeTile " + tile.coordinate.q + " " + tile.coordinate.r + " " + tile.GetTypeAsString();
            if(tile is ResourceTile) {
                int numberTileValue = ((ResourceTile)tile).numberTileValue;
                message += " " + numberTileValue;
            }
            serverSender.Send(message);
        }


        BoardCoordinate thiefPosition = boardManager.board.thiefPosition;

        message = "initThief " + thiefPosition.q + " " + thiefPosition.r;
        serverSender.Send(message);

        //verificam daca s-o trimis mesajul cu playerii
        yield return sendUsersThread;


        //hotaram ordinea (host-ul hotaraste)
        turnManager.SetOrder();


        yield return StartCoroutine(SendOrderToAll());

        cardManager.InitializeDecksFromFile("GameLogic/card1");
                
        Debug.LogWarning("Setup gata!!!!!!!!!");

        yield return new WaitForSeconds(2);


        turnManager.InitSetupOrder();

        Player firstToSetup = turnManager.GetNextPlayerInSetup(); 

        SendSetupMessage(firstToSetup.nickname);
    }


    public void SendSetupMessage(string username) {
        string message = "setup " + username;
        serverSender.Send(message);
    }

    private IEnumerator SendOrderToAll() {
        int[] order = turnManager.order;
        Debug.Log("orderul primit de mine " + order.Length);
        string message = "setOrder ";
        string orderString = "";
        for(int  i = 0; i < order.Length; ++i) {
            if(i != 0) orderString += ",";
            orderString += order[i];
        }
        message += orderString;
        yield return StartCoroutine(serverSender.Send2Server(message));
    }


    private IEnumerator SendUserInfoToAll() {
        string message = "players";
        for(int i = 0 ; i < usernames.Count; ++i) {
            message += " ";
            message += usernames[i] + "," + playerColors[i];
        }
        //Debug.Log(message);
        yield return StartCoroutine(serverSender.Send2Server(message));
    }
    private void RandomizeColors(List<PlayerColor> availableColors) {
        while(availableColors.Count != 0) {
            int index = Random.Range(0, availableColors.Count);
            playerColors.Add(availableColors[index]);
            availableColors.RemoveAt(index);
        }
    }
    

    IEnumerator GetAllUsersInGameSession() {
        string uri = "https://localhost:5001/api/GameSessions/" + UserInfo.GetGameSessionId();

        UnityWebRequest request = UnityWebRequest.Get(uri);
        DownloadHandler downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError("GET ERROR - Eroare la determinarea persoanelor din joc!");
        else 
        {
            long status = request.responseCode;
            if(status == 200 || status == 201) {
                GameSessionJSON gameSessionJSON = JsonUtility.FromJson<GameSessionJSON>(request.downloadHandler.text);

                

                foreach (var user in gameSessionJSON.gameSessionUsers)
                {
                    usernames.Add(user.userName);
                }
            }
            else {
                Debug.LogError(request.downloadHandler.text);
            }
        }
        
    }


    #region previousTest
    //Folosit doar ca sa testam...
    // IEnumerator SimulateGame()
    // {

    //     #region testStefan
    //     yield return new WaitForSeconds(0.5f);
    //     boardManager.AddSettlement(
    //         stefan, 
    //         new Corner(new BoardCoordinate(-1.33f, 0.66f)),
    //         "village");

    //     Corner secondStef = new Corner(new BoardCoordinate(-1.33f, -0.33f));

    //     stefan.GetResources(boardManager.GetResourcesFromCorner(secondStef));

    //     boardManager.AddSettlement(
    //         stefan,
    //         secondStef,
    //         "village");

    //     boardManager.AddConnector(
    //         stefan,
    //         new Corner(new BoardCoordinate(-1.33f, 0.66f)),
    //         new Corner(new BoardCoordinate(-0.66f, 0.33f)),
    //         "road");

    //     boardManager.AddConnector(
    //         stefan,
    //         new Corner(new BoardCoordinate(-1.66f, -0.66f)),
    //         new Corner(new BoardCoordinate(-1.33f, -0.33f)),
    //         "road");

    //     #endregion testStefan;

    //     yield return new WaitForSeconds(0.25f);

    //     #region testSebi
    //     boardManager.AddSettlement(
    //        sebi,
    //        new Corner(new BoardCoordinate(-0.33f, -1.33f)),
    //        "village");

    //     Corner secondSebi = new Corner(new BoardCoordinate(0.66f, -1.33f));


    //     boardManager.AddSettlement(
    //         sebi,
    //         secondSebi,
    //         "village");


    //     sebi.GetResources(boardManager.GetResourcesFromCorner(secondSebi));

    //     boardManager.AddConnector(
    //         sebi,
    //         new Corner(new BoardCoordinate(-0.33f, -1.33f)),
    //         new Corner(new BoardCoordinate(-0.66f, -1.66f)),
    //         "road");

    //     boardManager.AddConnector(
    //         sebi,
    //         new Corner(new BoardCoordinate(0.66f, -1.33f)),
    //         new Corner(new BoardCoordinate(1.33f, -1.66f)),
    //         "road");

    //     #endregion testSebi
    //     yield return new WaitForSeconds(0.25f);

    //     #region testDragos
    //     boardManager.AddSettlement(
    //        dragos,
    //        new Corner(new BoardCoordinate(0.33f, 0.33f)),
    //        "village");


    //     Corner secondDragos = new Corner(new BoardCoordinate(-0.33f, 1.66f));

    //     boardManager.AddSettlement(
    //         dragos,
    //         secondDragos,
    //         "village");

    //     dragos.GetResources(boardManager.GetResourcesFromCorner(secondDragos));

    //     boardManager.AddConnector(
    //         dragos,
    //         new Corner(new BoardCoordinate(0.33f, 0.33f)),
    //         new Corner(new BoardCoordinate(0.66f, -0.33f)),
    //         "road");

    //     boardManager.AddConnector(
    //         dragos,
    //         new Corner(new BoardCoordinate(-0.33f, 1.66f)),
    //         new Corner(new BoardCoordinate(-0.66f, 1.33f)),
    //         "road");

    //     boardManager.AddConnector(
    //         dragos,
    //         new Corner(new BoardCoordinate(0.66f, -0.33f)),
    //         new Corner(new BoardCoordinate(0.33f, -0.66f)),
    //         "road");


    //     #endregion testDragos

    //     //var corners = boardManager.GetAvailableCornersForSettlement(dragos);

    //     //Debug.LogWarning("Number of corners:" + corners.Count);

    //     //foreach (var corner in corners)
    //     //{
    //     //    Debug.LogWarning(corner.GetComponent<CornerBehaviour>().corner.coordinate.q + " " +
    //     //        corner.GetComponent<CornerBehaviour>().corner.coordinate.r);
    //     //}

    //     // var pairs = boardManager.GetAvailablePlacesForConnector(dragos);

    //     // Debug.LogWarning("number of connectos: " + pairs.Count);

    //     // foreach (var pair in pairs)
    //     // {
    //     //    var firstCord = pair.Key.GetComponent<CornerBehaviour>().corner.coordinate;
    //     //    var secondCord = pair.Value.GetComponent<CornerBehaviour>().corner.coordinate;
    //     //    //Debug.LogWarning("Connector in: (" + firstCord.q + ", " + firstCord.r + ") - (" + secondCord.q + ", " + secondCord.r + ")");

    //     // }

    //     var pairs = boardManager.GetAvailablePlacesForConnector(dragos);

    //     List<Vector3> positions = new List<Vector3>();

    //     foreach (var pair in pairs)
    //     {
    //         Vector3 middle = (pair.Key.transform.position + pair.Value.transform.position) / 2;
    //         positions.Add(middle);
    //     }


    //     masterHighlighter.SpawnHighlighters(positions);
        
    //     yield return StartCoroutine(masterHighlighter.WaitForUserInput());
        
    //     Vector3 position = masterHighlighter.positionPressed;

    //     BoardCoordinate bcPosition = BoardCoordinate.ToBoardCoordinate(position);

    //     boardManager.AddConnector(dragos, bcPosition, "road");

    //     turnManager.Next();

    //     turnManager.TurnLogic(turnManager.currentPlayerIndex);

    //     //StartCoroutine(playerManager.PlayerMovesThief(stefan));

    //     //DisplayHandInfo();


    //     //Corner corner = new Corner(new BoardCoordinate())
    //     //boardManager.AddSettlement(stefan, )
    // }

    #endregion

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



[System.Serializable]
public class GameSessionJSON {
    public int id;
    public int status;
    public string createdAt;
    public List<GameSessionExtensionsJSON> extensions;

    public List<GameSessionUserJSON> gameSessionUsers;
}


[System.Serializable]
public class GameSessionUserJSON {
    public string id;
    public string userName;
    public int roles;
}

[System.Serializable]
public class GameSessionExtensionsJSON {
    public int id;
    public string name;
}


