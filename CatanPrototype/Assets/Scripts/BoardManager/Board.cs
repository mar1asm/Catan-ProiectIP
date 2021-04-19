using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Board 
{
    //private static GameObject tilePrefab = (GameObject)Resources.Load("GameLogic/Prefabs/TilePrefab");
    //private static GameObject cornerPrefab = (GameObject)Resources.Load("GameLogic/Prefabs/CornerPrefab");

    public List<string> availableTileTypes;
    public Dictionary<BoardCoordinate, Tile> tiles {
         get; private set;
    }
    public Dictionary<BoardCoordinate, Corner> corners {
        get; private set;
    }
    public Dictionary<BoardCoordinate, List<BoardCoordinate>> cornerLattice {
        get; private set;
    }


    public Dictionary<BoardCoordinate, List<BoardCoordinate>>[] playerRoadNetworks
    {
        get; private set;
    }

    public Board()
    {
        tiles = new Dictionary<BoardCoordinate, Tile>();
        corners = new Dictionary<BoardCoordinate, Corner>();
        cornerLattice = new Dictionary<BoardCoordinate, List<BoardCoordinate>>();
        availableTileTypes = new List<string>();
        playerRoadNetworks = new Dictionary<BoardCoordinate, List<BoardCoordinate>>[(int)PlayerColor.NbOfColors];
        for(int i = 0; i < (int)PlayerColor.NbOfColors; ++i)
        {
            playerRoadNetworks[i] = new Dictionary<BoardCoordinate, List<BoardCoordinate>>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="color"></param>
    /// <param name="corner1"></param>
    /// <param name="corner2"></param>
    public void PlaceConnector(PlayerColor color, Corner corner1, Corner corner2)
    {
        BoardCoordinate bc1 = corner1.coordinate;
        BoardCoordinate bc2 = corner2.coordinate;
        PlaceConnector(color, bc1, bc2);
    }
    /// <summary>
    /// Functia care adauga un conector intre doua puncte de pe harta
    /// O sa trebuiasca un pic modificat(checkuri daca nu e deja alt drum acolo, daca nu se incearca adaugarea unui
    /// drum care nu poate sa existe etc
    /// </summary>
    /// <param name="color"></param>
    /// <param name="bc1"></param>
    /// <param name="bc2"></param>
    private void PlaceConnector(PlayerColor color, BoardCoordinate bc1, BoardCoordinate bc2)
    {
        int networkIndex = (int)color;
        var network = playerRoadNetworks[networkIndex];
        
        if(!(corners.ContainsKey(bc1) && corners.ContainsKey(bc2)))
        {
            return;
        }

        if(!network.ContainsKey(bc1))
        {
            network.Add(bc1, new List<BoardCoordinate>());
        }
        if(!network.ContainsKey(bc2))
        {
            network.Add(bc2, new List<BoardCoordinate>());
        }

        if(!network[bc1].Contains(bc2))
        {
            network[bc1].Add(bc2);
        }

        if(!network[bc2].Contains(bc1))
        {
            network[bc2].Add(bc1);
        }
    }
    private string ExtractRandomTileType()
    {
        int index = Random.Range(0, availableTileTypes.Count);
        string tileType = availableTileTypes[index];
        availableTileTypes.RemoveAt(index);
        return tileType;
    }

    public void AddAvailableTileType(string tileType, int numberOfTiles)
    {
        for(int i = 0; i < numberOfTiles; ++i)
        {
            AddAvailableTileType(tileType);
        }
    }
    public void AddAvailableTileType(string tileType)
    {
        availableTileTypes.Add(tileType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="q"></param>
    /// <param name="r"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Tile PlaceTile(float q, float r, string type)
    {
        return PlaceTile(new BoardCoordinate(q, r), type);
    }

    /*
     * Function that creates and adds an Hex to the board
     * Return the tile that was just placed
     */
    public Tile PlaceTile(BoardCoordinate boardCoordinate, string type)
    {
        if (tiles.ContainsKey(boardCoordinate)) return null;

        Tile tileToPlace = GetTileFromString(boardCoordinate, type);

        tiles.Add(boardCoordinate, tileToPlace);


        return tileToPlace;
    }

    /*
     * Function that creates and adds the corners coresponding to a Tile
     * returns the Corners that were created
     */
    public List<Corner> PlaceCorners(Tile tile)
    {
        List<Corner> cornersPlaced = new List<Corner>();
        for(int index = (int)Tile.Corners.TOP; index <= (int)Tile.Corners.TOP_LEFT; ++index)
        {
            BoardCoordinate boardCoordinate = tile.coordinate + Tile.cornerOffsets[index];

            BoardCoordinate nextCorner = tile.coordinate + Tile.cornerOffsets[(index + 1) % 6];

            AddConnection2Lattice(boardCoordinate, nextCorner);

            if (corners.ContainsKey(boardCoordinate))
            {
                tile.corners[index] = corners[boardCoordinate];
                continue;
            }

            Corner newCorner = new Corner(boardCoordinate);
            tile.corners[index] = newCorner;
            corners.Add(boardCoordinate, newCorner);

  

            cornersPlaced.Add(newCorner);
        }

        return cornersPlaced;
    }

    private void AddConnection2Lattice(BoardCoordinate bc1, BoardCoordinate bc2)
    {
        if (!cornerLattice.ContainsKey(bc1))
        {
            cornerLattice.Add(bc1, new List<BoardCoordinate>());
        }
        if (!cornerLattice.ContainsKey(bc2))
        {
            cornerLattice.Add(bc2, new List<BoardCoordinate>());
        }


        if(!cornerLattice[bc1].Contains(bc2)) cornerLattice[bc1].Add(bc2);
        if(!cornerLattice[bc2].Contains(bc1)) cornerLattice[bc2].Add(bc1);
    }
    private Tile GetTileFromString(BoardCoordinate boardCoordinate, string type)
    {
        switch(type)
        {
            case "random": return GetTileFromString(boardCoordinate, ExtractRandomTileType());
            case "desert": return new DesertTile(boardCoordinate);
            case "brick": return new BrickTile(boardCoordinate);
            case "mountain": return new MountainTile(boardCoordinate);
            case "forest": return new ForestTile(boardCoordinate);
            case "sheep": return new SheepTile(boardCoordinate);
            case "wheat": return new WheatTile(boardCoordinate);
            default: return new TestTile(boardCoordinate);
        }
    }
    

}
