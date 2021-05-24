using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandInterpreterBehaviour : MonoBehaviour
{


    [SerializeField]
    private BoardManagerBehaviour boardManager;

    [SerializeField]
    private PlayerManager playerManager;

    [SerializeField]
    private TurnManagerBehaviour turnManager;

    [SerializeField]
    private SetupMasterBehaviour setupMaster;


    [SerializeField]
    private ServerSenderBehaviour serverSender;


    [SerializeField]
    private BannerHolderBehaviour bannerHolder;

    [SerializeField]
    private ClientActionsMasterBehaviour clientActionsMaster;


    [SerializeField]
    private GameObject NextTurnButton, AddRoadButton, PlaceVillageButton;

    [SerializeField]
    private getResources resourcesDisplay;



    private Dictionary<Player, BoardCoordinate> setupLastPlacedSettlement = new Dictionary<Player, BoardCoordinate>();


 

    public void InterpretCommand(string command) {
        
        string[] tokens = command.Split(' ');
        Debug.Log(command);
        switch (tokens[0])
        {
            case "startGame" : {
                if(tokens.Length < 2) return;
                int gameId = int.Parse(tokens[1]);

                UserInfo.SetGameSessionId(gameId);
                SceneManager.LoadScene("PlayingWithBoardScene");
            } break;

            case "players": {
                for(int i = 1 ; i < tokens.Length; ++i) {
                    string[] playerTokens = tokens[i].Split(',');
                    
                    GameObject bannersHolderObject = GameObject.Find("BannersHolder");

                    PlayerColor colorOfPlayer = GetColorFromString(playerTokens[1]);
                    //nu repet bucata asta de cod la host
                    if (!UserInfo.IsHost() && playerManager != null)
                    {
                        playerManager.AddPlayer(new Player(playerTokens[0], "?", colorOfPlayer));
                    }
                    bannersHolderObject.GetComponent<BannerHolderBehaviour>().AddBanner(playerTokens[0], colorOfPlayer);
                }
                
            } break;

            case "placeTile": {
                if(UserInfo.IsHost()) return;

                if(tokens.Length < 4) return;
                
                float q =  float.Parse(tokens[1]);
                float r =  float.Parse(tokens[2]);
                string type = tokens[3];
                int numberTileNumber = -1;
                if(tokens.Length >= 5) {
                    numberTileNumber = int.Parse(tokens[4]);
                }
                
                if(boardManager == null) return;

                boardManager.AddTile(new BoardCoordinate(q, r), type, numberTileNumber);
            } break;

            case "initThief": {
                if(UserInfo.IsHost()) return;
                
                if(tokens.Length < 3) return;

                float q = float.Parse(tokens[1]);
                float r = float.Parse(tokens[2]);
                

                boardManager.MoveThief(new BoardCoordinate(q, r));
            }break;

            case "setOrder": {
                if(UserInfo.IsHost()) return;


                if(tokens.Length < 2) return;


                var indexes = tokens[1].Split(',');
                int[] newOrder = new int[indexes.Length];
                for(int i = 0 ; i < indexes.Length; ++i) {
                    newOrder[i] = int.Parse(indexes[i]);
                }

                turnManager.SetOrder(newOrder);

                turnManager.DisplayOrder();
            } break;

            case "setup": {
                if(tokens.Length < 2) return;

                if(playerManager.clientPlayer.nickname != tokens[1]) return;

                StartCoroutine(setupMaster.PlaceSettlementAndRoad());
            } break;

            case "placeSettlement": {
                if(tokens.Length < 4) return;

                string username = tokens[1];

                string[] coords = tokens[2].Split(';');

                float q = float.Parse(coords[0]);

                float r = float.Parse(coords[1]);

                bool forced = false;

                if(tokens.Length >= 5) {
                    forced = bool.Parse(tokens[4]);
                }


                //asa ne dam seama daca suntem in setup sau in timpul jocului
                if(forced) 
                {
                    boardManager.AddSettlement(playerManager.GetPlayerWithUsername(username),
                                    new BoardCoordinate(q, r),
                                    tokens[3]);

                    //if-ul asta e pentru a tine minte unde  a fost asezat a doua asezare....
                    //ca sa ii dam resursele cum trebuie
                    if(UserInfo.IsHost() && turnManager.isSetupTime) {
                        Player p = playerManager.GetPlayerWithUsername(username);
                        if(!setupLastPlacedSettlement.ContainsKey(p)) {
                            setupLastPlacedSettlement.Add(p, new BoardCoordinate(q, r));
                        }
                        else {
                            setupLastPlacedSettlement[p] = new BoardCoordinate(q, r);
                        }
                    }
                }
                else 
                {
                    //ar fi bine sa fie pasat si token[3], dar am vazut abia acum ca
                    //sunt doua functii diferite, una pentru sate si una pentru oras...
                    //modificati sau gasiti metoda de rezolvare
                    Player p = playerManager.GetPlayerWithUsername(username);
                    playerManager.playerAddsSettlement(p,
                                        new BoardCoordinate(q, r));

                    if(p == playerManager.clientPlayer) {
                        resourcesDisplay.updateDisplay();
                    }

                    playerManager.UpdateScoreDisplays();
                }
            } break;

            case "placeConnector": {
                string username = tokens[1];

                string[] coords = tokens[2].Split(';');

                float q = float.Parse(coords[0]);

                float r = float.Parse(coords[1]);

                bool forced = false;

                if(tokens.Length >= 5) {
                    forced = bool.Parse(tokens[4]);
                }

                if(forced) {
                    boardManager.AddConnector(playerManager.GetPlayerWithUsername(username),
                                new BoardCoordinate(q, r),
                                tokens[3]);
                }
                else 
                {   
                    Player p = playerManager.GetPlayerWithUsername(username);
                    playerManager.playerAddsRoad(p,
                                    new BoardCoordinate(q, r),
                                    tokens[3]);

                    if(p == playerManager.clientPlayer) {
                        resourcesDisplay.updateDisplay();
                    }

                    playerManager.UpdateScoreDisplays();                                    
                }

            } break;

            case "nextSetup": {
                if(!UserInfo.IsHost()) return;

                Player nextInSetup = turnManager.GetNextPlayerInSetup();
                if(nextInSetup == null) {

                    foreach (var pair in setupLastPlacedSettlement)
                    {
                        var resources = boardManager.GetResourcesFromCorner(pair.Value);
                        string getResourcesMess = "getResources " + pair.Key.nickname;
                        string resourcesString = resources[0].ToString();
                        for(int i = 1; i < resources.Count; ++i) {
                            resourcesString += "," + resources[i].ToString();
                        }
                        getResourcesMess += " " + resourcesString;

                        serverSender.Send(getResourcesMess);
                    }

                    string message2Everybody = "turnNext";
                    serverSender.Send(message2Everybody);
                    return;
                }
                string message = "setup " + nextInSetup.nickname;

                serverSender.Send(message);
            } break;

            case "turnNext": {
                turnManager.Next();
                
                Player currentPlayer = turnManager.currentPlayer;
                bannerHolder.SetHighlight(currentPlayer.nickname);
                //Daca e tura jucatorului din acest client
                if(currentPlayer.nickname == UserInfo.GetUsername()) {
                    NextTurnButton.SetActive(true);
                    AddRoadButton.SetActive(true);
                    PlaceVillageButton.SetActive(true);
                }
                else {
                    NextTurnButton.SetActive(false);
                    AddRoadButton.SetActive(false);
                    PlaceVillageButton.SetActive(false);
                }

                if(UserInfo.IsHost()) {
                    //momentan o sa fie 12.... pentru ca numa jetoane cu 12 sunt, trebuie facuta initializarea dupa un fisier calumea a 
                    //jetoanelor... sa rezolvati asta va rog
                    int randomDiceValue = 7;

                    string genString = "genResources " + randomDiceValue;

                    serverSender.Send(genString);
                }
            } break;

            case "genResources": {
                if(tokens.Length < 2) return;

                int randomDiceValue = int.Parse(tokens[1]);

                if(randomDiceValue == 7) {
                    Player clientPlayer = playerManager.clientPlayer;
                    if(clientPlayer.GetNumberOfResources() > 7) {
                        clientActionsMaster.GiveResourcesToThief();
                    }

          
                    if(turnManager.currentPlayer.nickname == UserInfo.GetUsername()) {
                        int nb = 0;
                        foreach (var player in playerManager.players)
                        {
                            if(player.GetNumberOfResources() > 7) {
                                nb++;
                            }
                        }
                        clientActionsMaster.MoveThief(nb);
                    } 
                    return; 
                }

                boardManager.GiveResources(randomDiceValue);

                resourcesDisplay.updateDisplay();
            } break;

            case "getResources": {
                if(tokens.Length < 3) return;

                string[] resourcesStrings = tokens[2].Split(',');

                List<ResourceTypes> resources = new List<ResourceTypes>();

                for(int i = 0 ; i < resourcesStrings.Length; ++i) {
                    resources.Add(GetResourceTypeFromString(resourcesStrings[i]));
                }

                playerManager.GiveResourceToPlayer(tokens[1], resources);

                resourcesDisplay.updateDisplay();
            } break;

            case "playerLosesResourcesToThief": {
                if(tokens.Length < 3) return;

                string[] resourcesStrings = tokens[2].Split(',');

                List<ResourceTypes> resources = new List<ResourceTypes>();

                for(int i = 0 ; i < resourcesStrings.Length; ++i) {
                    resources.Add(GetResourceTypeFromString(resourcesStrings[i]));
                }

                //playerManager.GiveResourceToPlayer(tokens[1], resources);
                playerManager.GetPlayerWithUsername(tokens[1]).PayResources(resources);

                resourcesDisplay.updateDisplay();

                if(turnManager.currentPlayer.nickname == UserInfo.GetUsername()) {
                    clientActionsMaster.PlayerGaveResourcesToRobber();
                }
            } break;
        
            case "moveThief": {
                if(tokens.Length < 3) return;

                Player p = playerManager.GetPlayerWithUsername(tokens[1]);

                string[] coords = tokens[2].Split(';');
                float q = float.Parse(coords[0]);
                float r = float.Parse(coords[1]);

                boardManager.MoveThief(new BoardCoordinate(q, r));

            }break;
        }
    }

    private ResourceTypes GetResourceTypeFromString(string resource) {
        switch(resource) {
            case "Sheep": return ResourceTypes.Sheep;
            case "Brick": return ResourceTypes.Brick;
            case "Wood": return ResourceTypes.Wood;
            case "Stone": return ResourceTypes.Stone;
            case "Wheat": return ResourceTypes.Wheat;
            default: return ResourceTypes.Sheep;
        }
    }
    private PlayerColor GetColorFromString(string color) {
        switch(color) {
            case "Red": return PlayerColor.Red;
            case "Blue": return PlayerColor.Blue;
            case "Green": return PlayerColor.Green;
            case "Orange": return PlayerColor.Orange;
            case "Brown": return PlayerColor.Brown;
            case "White": return PlayerColor.White;
            default: return PlayerColor.Red;
        }
    }
}
