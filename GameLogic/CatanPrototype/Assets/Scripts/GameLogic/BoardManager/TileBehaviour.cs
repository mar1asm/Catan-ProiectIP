using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{


    /*Tile-ul care decide ce fel de hexagon este
    * am adaugat getter si setter special
    *pentru sintaxa de get si set in c#: https://stackoverflow.com/questions/17881091/getter-and-setter-declaration-in-net
    */


    private Tile _tile;
    public Tile tile
    {
        get
        {
            return _tile;
        }

        set
        {
            _tile = value;
            CleanVFX();
            //Debug.Log(_tile.GetTypeAsString());
            _tile.AddVFX2Object(transform.GetChild(0).gameObject);
        }
    }

    public void ActivateSpecialAction()
    {
        _tile.SpecialAction();
    }

    private void CleanVFX()
    {
        foreach(Transform child in transform.GetChild(0))
        {
           child.gameObject.SetActive(false);
        }
    }
    void Awake()
    {
        //distrug hexagonul care tine locul
        CleanVFX();
    }
}
