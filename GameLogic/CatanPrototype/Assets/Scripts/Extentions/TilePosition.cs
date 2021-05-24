using Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosition : MonoBehaviour
{
    int q, r;
    string type = null;
    
    public GameObject Canvas, tile;

    public void display()
    {
        print("hello");
        /* Canvas = GameObject.Find("Canvas");
         BordCreat bordCreat = Canvas.GetComponent<BordCreat>();
         bordCreat.boardExpansion.addTile(0, 0, "forest");
         Debug.Log(bordCreat.boardExpansion.getTileList());
        */
        BoardExpansion boardExpansion = Canvas.GetComponent<BoardExpansion>();
        TileExtension tileExtension = tile.GetComponent<TileExtension>();
        boardExpansion.addTile(tileExtension);
        Debug.Log(boardExpansion.getTileList());

    }
    public void Tile (int q, int r, string type)
    {
        


    }

}
