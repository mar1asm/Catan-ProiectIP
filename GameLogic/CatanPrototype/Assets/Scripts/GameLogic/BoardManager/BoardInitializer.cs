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

       /* Debug.Log("ceva");
        foreach (int nr in hexagonNumber)
        {
            Debug.Log(nr);
        }*/
      
        return board;
    }
}
