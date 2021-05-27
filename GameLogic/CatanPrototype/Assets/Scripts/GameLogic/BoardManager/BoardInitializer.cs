using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class BoardInitializer
{
    /*
     * Cel mai probabil o sa fie modificat si nu o sa citim dintr-un fisier
     */
    public static List<int> hexagonNumber;
    public static List<Port> ports;
    public static Board InitializeBoardFromFile(string filePath) 
    {

        //Reading the text from the file
        //StreamReader sr = new StreamReader(filePath);
        //string path = "E:\\UnityProjects\\CatanPrototype\\Assets\\Resources\\GameLogic\\inimioara";
        TextAsset txtData = (TextAsset)Resources.Load(filePath);
        string jsonString =  txtData.text;
  
        //Debug.Log(jsonString);
        //Am folosit ceva de la Unity, dar cred ca o sa schimbam cum citim din json..
        //vedem mai tarziu
        GameDescriber game = JsonUtility.FromJson<GameDescriber>(jsonString);
        hexagonNumber = new List<int>();


        Board board = new Board();

        foreach (TileNumberDescriber tileNumberDescriber in game.availableTiles)
        {
            board.AddAvailableTileType(tileNumberDescriber.type, tileNumberDescriber.number);
        }


        foreach (HexDescriber hexDescriber in game.board)
        {
            Tile tile = board.PlaceTile(hexDescriber.q, hexDescriber.r, hexDescriber.type);
            
            board.PlaceCorners(tile);
        }
        foreach(AvailableToken at in game.availableTokens)
        {
            for(int i = 0; i < at.appearance; i++)
            {
                hexagonNumber.Add(at.value);
            }
        }
       
        foreach(Harbour h in game.harbours)
        {
            ResourceTypes rs = ResourceTypes.Any;
            if (h.type == "sheep") rs = ResourceTypes.Sheep;
            else if (h.type == "wood") rs = ResourceTypes.Wood;
            else if (h.type == "wheat") rs = ResourceTypes.Wheat;
            else if (h.type == "stone") rs = ResourceTypes.Stone;
            else if (h.type == "brick") rs = ResourceTypes.Brick;
            board.PlacePort(new Corner(new BoardCoordinate(h.q0, h.r0)),
                      new Corner(new BoardCoordinate(h.q1, h.r1)),
                       rs, h.raportx, h.raporty);
        }
        // facut logica din spate 
       /* Debug.Log("ceva");
        foreach (int nr in hexagonNumber)
        {
            Debug.Log(nr);
        }*/
      
        return board;
    }
}
