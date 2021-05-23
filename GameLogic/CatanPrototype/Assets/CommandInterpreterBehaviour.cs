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
