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


    public List<Port> ports = new List<Port>();

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

    
    public Port PlacePort(Corner c1, Corner c2, ResourceTypes resource, int resourcesNeeded, int resourcesObtained)
    {
        if(!(corners.ContainsKey(c1.coordinate) && corners.ContainsKey(c2.coordinate))) {
            return null;
        }

        Port port = new Port(corners[c1.coordinate], corners[c2.coordinate], resource, 
            new Vector2Int(resourcesNeeded, resourcesObtained));

        ports.Add(port);
        return port;
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
        Debug.Log("Random tile chosen : " + tileType);
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

    public List<Corner> GetAvailableCorners(PlayerColor color)
    {
        //made by jon
        List<Corner> listOfCorners= new List<Corner>();
        foreach (var it in playerRoadNetworks[(int)color])
        {
            bool ok = true;
            BoardCoordinate bc=it.Key;
            List<BoardCoordinate> bclist= cornerLattice[bc];
            foreach (var b in bclist)
                if (corners[b].settlement != null) // daca este asezare in vecini atunci "bc" nu este disponibil
                    ok = false;
            if(ok) listOfCorners.Add(corners[bc]);
        }

        return listOfCorners;
    }

    public List<KeyValuePair<Corner,Corner>> GetAvailableConnectors(PlayerColor color)
    {
        List<KeyValuePair<Corner, Corner>> listOfConnectors = new List<KeyValuePair<Corner, Corner>>();

        foreach (var it in playerRoadNetworks[(int)color])
        {
            bool ok = true;
            BoardCoordinate bc = it.Key; //bc este una dintre coordonate
            List<BoardCoordinate> bclist = cornerLattice[bc];//list de vcini ai lui bc
            foreach (var b in bclist)
            {
                for (int i = 0; i < (int)PlayerColor.NbOfColors; i++)
                    if (playerRoadNetworks[i].ContainsKey(b)) //daca un vecin face parte din graful unui player
                        if (playerRoadNetworks[i].ContainsKey(bc)) // si bc face si l part din graful aceluias player 
                            ok = false;// perechea (b,bc) nu este buna pt ca acolo deja exista un drum
                if(ok)
                {
                    KeyValuePair<Corner, Corner> item = new KeyValuePair<Corner, Corner>(corners[b], corners[bc]);
                    listOfConnectors.Add(item);
                }
            }
        }

        return listOfConnectors;

    }

    public Settlement PlaceSettlement(Player p, BoardCoordinate boardCoordinate, string type)
    {
        //made by jon
        
        int colorID = (int)p.color;
        Settlement settlementToPlace = GetSettlementFromString(boardCoordinate, type);
        settlementToPlace.owner = p;


        //if (playerRoadNetworks[colorID].ContainsKey(boardCoordinate))
        //{
        //    List<Corner> availableCorners = GetAvailableCorners(p.color);
        //    foreach (var c in availableCorners)
        //        if (boardCoordinate == c.coordinate)
        //        {
        //            return settlementToPlace;
        //        }
        Debug.Log(boardCoordinate.q + " " + boardCoordinate.r);
        if (corners.ContainsKey(boardCoordinate))
        {

            corners[boardCoordinate].settlement = settlementToPlace;
            return settlementToPlace;
        }

        return null;
    }

    public Connector PlaceConnector(Player p, BoardCoordinate bc1, BoardCoordinate bc2, string type)
    {
        //made by jon

        int colorID = (int)p.color;
        Connector connectorToPlace = GetConnectorFromString(bc1, bc2, type);

        //if (playerRoadNetworks[colorID].ContainsKey(bc1) || playerRoadNetworks[colorID].ContainsKey(bc2))// daca macar una din cele doua coordonate face parte din graful playerului
        //{
        //    List<KeyValuePair<Corner, Corner>> availableConnectors = GetAvailableConnectors(p.color);
        //    foreach(var conn in availableConnectors)
        //    {
        //        if (bc1 == conn.Key.coordinate && bc2 == conn.Value.coordinate ||
        //            bc2 == conn.Key.coordinate && bc1 == conn.Value.coordinate)
        //            return connectorToPlace;//daca coordonatele se gasesc in lista availableConnectors atunci putem pune drumul
        //    }
        //}


        PlaceConnector(p.color, bc1, bc2);
        return connectorToPlace;


        /*
        if(playerRoadNetworks[colorID].ContainsKey(bc1)&& !playerRoadNetworks[colorID].ContainsKey(bc2))
        {
            for(int i=0; i<(int)PlayerColor.NbOfColors;++i)
            {
                if (i == colorID) continue;
                if(playerRoadNetworks[i].ContainsKey(bc2)) return null;
            }
            return connectorToPlace;
        }

        if (playerRoadNetworks[colorID].ContainsKey(bc2) && !playerRoadNetworks[colorID].ContainsKey(bc1))
        {
            for (int i = 0; i < (int)PlayerColor.NbOfColors; ++i)
            {
                if (i == colorID) continue;
                if (playerRoadNetworks[i].ContainsKey(bc1)) return null;
            }
            return connectorToPlace;
        }
        return null;*/
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
    private Settlement GetSettlementFromString(BoardCoordinate boardCoordinate, string type)
    {
        //made by jon
        switch (type)
        {
            case "village": 
                {
                    Corner corner=new Corner(boardCoordinate); 
                    return new Village(corner);
                }
            case "city": 
                {
                    Corner corner = new Corner(boardCoordinate); 
                    return new City(corner);
                }
            default:
                {
                    Corner corner = new Corner(boardCoordinate); 
                    return new TestSettlement(corner);
                }
        }
    }

    private Connector GetConnectorFromString(BoardCoordinate bc1, BoardCoordinate bc2, string type)
    {
        //made by jon
        switch (type)
        {
            case "road":
                {
                    Corner c1 = corners[bc1];
                    Corner c2 = corners[bc2];
                    return new Road(c1,c2);
                }
            case "boat":
                {
                    Corner c1 = corners[bc1];
                    Corner c2 = corners[bc2];
                    return new Boat(c1, c2);
                }
            default:
                {
                    Corner c1 = corners[bc1];
                    Corner c2 = corners[bc2];
                    return new TestConnector(c1,c2);
                }
        }
    }


}
