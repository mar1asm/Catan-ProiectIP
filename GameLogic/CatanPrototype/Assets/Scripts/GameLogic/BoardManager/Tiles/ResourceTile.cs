using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceTile : Tile
{

    private int _numberTileValue = 0;
    public ResourceTypes resourceType = ResourceTypes.Sheep;


    
    public int numberTileValue
    {
        get
        {
            return _numberTileValue;
        }
        set
        {
            _numberTileValue = value;
            if(inGameNumberTile != null)
            {
                inGameNumberTile.GetComponent<NumberTileBehaviour>().number = _numberTileValue;
            }
        }
    }
    public GameObject numberTilePrefab;
    public GameObject inGameNumberTile;
    protected ResourceTile(float q, float r) : base(q, r)
    {
        LoadNumberTile();
    }


    protected ResourceTile(BoardCoordinate boardCoordinate) : base(boardCoordinate)
    {
        LoadNumberTile();
    }

    private void LoadNumberTile() 
    {
        numberTilePrefab = (GameObject)Resources.Load("GameLogic/Prefabs/NumberTilePrefab");
    }
    //This needs to be modified in order to add the number tile on top
    public override GameObject AddVFX2Object(GameObject parent)
    {
        GameObject tile = base.AddVFX2Object(parent);
        inGameNumberTile = GameObject.Instantiate(numberTilePrefab, new Vector3(0, 0.5f, 0), Quaternion.identity, tile.transform);
        //Debug.Log("Numarul acestui tile este: " + inGameNumberTile.transform.position);
        return tile;
    }


  
    public override void SpecialAction()
    {
        for(int i = 0; i < 6; ++i)
        {
            corners[i].ActivateSettlement();
        }
    }
}
