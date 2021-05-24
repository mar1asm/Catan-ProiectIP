using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;

public class SaveScript : MonoBehaviour
{
    public GameObject board, tiles;
    
    public void Save()
    {   
       
        BoardExpansion boardExpansion = board.GetComponent<BoardExpansion>();
    
        foreach (Transform transform in tiles.transform)
        {
            
            TileExtension t = transform.gameObject.GetComponent<TileExtension>();
            boardExpansion.addTile(t);
        }
        print(boardExpansion.toString());
    }
}
