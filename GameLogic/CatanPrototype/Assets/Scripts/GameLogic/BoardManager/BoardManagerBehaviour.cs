using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BoardManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;

    [SerializeField]
    private GameObject cornerPrefab;
    [SerializeField]
    private GameObject settlementPrefab; //made by jon
    [SerializeField]
    private GameObject connectorPrefab; //made by jon

    [SerializeField]
    private GameObject portPrefab;

    [SerializeField]
    private GameObject thiefPrefab;

    [SerializeField]
    private Board board;

    private GameObject thiefGameObject;


  
    private float deltaY = 0.002003f * 5751.438f * 0.3f;



    void Start()
    {
        //deltaY = 0.002003f * 5751.438f * 0.3f;
    
        InitializeBoardFromFile("GameLogic/inimioara");
        /*board.PlacePort(new Corner(new BoardCoordinate(1.33f, -2.66f)),
                        new Corner(new BoardCoordinate(0.66f, -2.33f)),
                        ResourceTypes.Any, 3, 1);
        */
        InstantiateBoard();

        
    }

    public List<Vector3> PlacesWithoutThief()
    {
        List<Vector3> toReturn = new List<Vector3>();
        foreach (var pair in board.tiles)
        {
            if (pair.Value.coordinate == board.thiefPosition) continue;
            Vector3 position = pair.Value.coordinate.ToWorldSpace();
            position.y += deltaY;
            toReturn.Add(position);
        }
        return toReturn;
    }


    public List<GameObject> GetAvailableCornersForSettlement(Player p)
    {
        var corners = board.GetAvailableCorners(p.color);
        List<GameObject> toReturn = new List<GameObject>();
        foreach (var  corner in corners)
        {
            toReturn.Add(corner.inGameObject);
        }

        return toReturn;
    }

    public List<KeyValuePair<GameObject,GameObject>> GetAvailablePlacesForConnector(Player p)
    {
        var pairs = board.GetAvailableConnectors(p.color);
        List<KeyValuePair<GameObject, GameObject>> toReturn = new List<KeyValuePair<GameObject, GameObject>>();
        foreach (var pair in pairs)
        {
            toReturn.Add( new  KeyValuePair<GameObject, GameObject>( pair.Key.inGameObject,pair.Value.inGameObject));
        }

        return toReturn;
    }

    public List<ResourceTypes> GetResourcesFromCorner(Corner corner)
    {
        return board.ResourcesFromCorner(corner);
    }


    /// <summary>
    /// Returneaza culoarea jucatorului cu cel mai lung drum de cel putin 5
    /// </summary>
    /// <param name="oldValue"></param>
    /// <returns></returns>
    public PlayerColor GetPlayerWithLongestRoad(int oldValue = 4)
    {
        return board.CheckLongestRoad(oldValue);
    }


    public int GetLongestLenghtOfPlayer(Player player)
    {
        return board.PlayerLongestRoad((int)player.color);
    }





    /// <summary>
    /// Returneaza numarul de puncte din asezari
    /// </summary>
    /// <returns></returns>
    public int GetPlayerPointsFromSettlements(Player p)
    {
        int sum = 0;
        foreach (var pair in board.corners)
        {
            if(pair.Value.settlement != null)
            {
                if(pair.Value.settlement.owner == p)
                {
                    sum += pair.Value.settlement.GetNumberOfPoints();
                }
            }
        }
        return sum;
    }


    

    public void GiveResources(int nr)
    {
        // Debug.Log("Jucatorii care au asezari pe" + nr + "primesc resurse.");

        foreach (KeyValuePair<BoardCoordinate, Tile> entry in board.tiles)
        {
            if (entry.Key == board.thiefPosition) continue;
            if (entry.Value is ResourceTile)
                if(((ResourceTile)entry.Value).numberTileValue == nr)
                  entry.Value.SpecialAction();
        }

    }



    /// <summary>
    /// Lista de trade-uri posibile din port-urile construite de jucator
    /// </summary>
    /// <param name="player">Jucatorul pentru care vrem sa aflam trade-urile</param>
    /// <returns>Lista de trade-uri din porturi</returns>
    /// <returns>Lista de trade-uri din porturi</returns>
    public List<Trade> GetTradesForPlayerFromPorts(Player player)
    {
        List<Trade> trades = new List<Trade>();
        foreach(Port port in board.ports)
        {
            for (int i = 0; i < 2; ++i)
            {
                Settlement settlement = port.corners[i].settlement;
                if(settlement != null)
                {
                    if(settlement.owner == player)
                    {
                     
                        Trade trade = new Trade(true);
                        for (int j = 0; j < port.resourcesObtained; ++j)
                        {
                            trade.AddResourceObtained(ResourceTypes.Any);
                        }
                        for (int j = 0; j < port.resourcesNeeded; ++j)
                        {
                            trade.AddResourceNeeded(port.resourceToTrade);
                        }
                        trades.Add(trade);
                    }
                }
            }

        }

        return trades;
    }

    /// <summary>
    /// Functia determina cel mai lung drum 
    /// </summary>
    /// <returns></returns>



    /// <summary>
    /// Adauga un tile la tabla si il instantiaza in scena
    /// </summary>
    /// <param name="coordinate">Pozitia in boardCoordinates unde trebuie pus hexagonul</param>
    /// <param name="type">Tipul de hexagon</param>
    public void AddTile(BoardCoordinate coordinate, string type)
    {
        Tile tile= board.PlaceTile(coordinate, type);
        Vector3 position = tile.coordinate.ToWorldSpace();
        GameObject gameobj = Instantiate(tilePrefab, position, Quaternion.identity, transform);
        gameobj.GetComponent<TileBehaviour>().tile = tile;
    }

    /// <summary>
    /// Adauga un settlement la board si il instantiaza in scena
    /// </summary>
    /// <param name="p">Player-ul care pune settlement-ul</param>
    /// <param name="c">Coltul in care sa puna</param>
    /// <param name="type">Tipul de settlement:  </param>
    public void AddSettlement(Player p, Corner c, string type)
    {
        //made by jon
        //ar trebui sa apeleze AddSettlement(Player, BoardCoordinate, string)
        BoardCoordinate bc = c.coordinate;
        AddSettlement(p, bc, type);
    }


    /// <summary>
    /// Adauga un settlement si il instantiaza in scena
    /// </summary>
    /// <param name="p"></param>
    /// <param name="coordinare"></param>
    public void AddSettlement(Player p, BoardCoordinate coordinate, string type)
    {
        //made by jon
        Settlement settlement = board.PlaceSettlement(p,coordinate, type);
        Vector3 position = settlement.corner.coordinate.ToWorldSpace();
        position.y += deltaY;
        GameObject gameobj = Instantiate(settlementPrefab, position, Quaternion.identity, transform);
        settlement.owner = p;
        gameobj.GetComponent<SettlementBehaviour>().settlement = settlement;
        

    }


    public void AddConnector(Player player, BoardCoordinate middle, string type) {
        foreach (var cornerPair1 in board.corners)
        {
            Corner c1 = cornerPair1.Value;
            var neighbours = board.cornerLattice[c1.coordinate];
            foreach (var c2 in neighbours)
            {
                BoardCoordinate auxMiddle = (c1.coordinate + c2) / 2;
                if(auxMiddle == middle) {
                    AddConnector(player, c1.coordinate, c2, type);
                    return;
                }
            }
        }
    }
    
    /// <summary>
    /// Adauga un connector la tabla si il instantiaza in scena
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    public void AddConnector(Player p, Corner c1, Corner c2, string type)
    {
        //made by jon
        //ar trebui sa apeleze AddConnector(Player, BoardCoordinate, BoardCoordinate)
        BoardCoordinate bc1 = c1.coordinate;
        BoardCoordinate bc2 = c2.coordinate;
        AddConnector(p, bc1, bc2, type);
    }


    /// <summary>
    /// Adauga conector la board si il instantiaza in scena
    /// </summary>
    /// <param name="bc1"></param>
    /// <param name="bc2"></param>
    public void AddConnector(Player p, BoardCoordinate bc1, BoardCoordinate bc2, string type)
    {
        //made by jon

        //Debug.LogWarning("(" + bc1.q + "," + bc1.r + ") => (" + bc2.q + "," + bc2.r + ")");

        Connector connector = board.PlaceConnector(p,bc1,bc2,type);

        Vector3 position = connector.middle.ToWorldSpace();



        position.y += deltaY; 
        Quaternion rotation = connector.rotation;
        GameObject gameobj = Instantiate(connectorPrefab, position, rotation, transform);
        connector.owner = p;
        gameobj.GetComponent<ConnectorBehaviour>().connector = connector;
    }




    public void InstantiateBoard()
    {
        foreach(KeyValuePair<BoardCoordinate, Tile> entry in board.tiles)
        {
            Vector3 position = entry.Key.ToWorldSpace();
            //Debug.Log(entry.Value.GetTypeAsString());
            GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
            tile.GetComponent<TileBehaviour>().tile = entry.Value;
            if(entry.Value is ResourceTile)
            {
                ((ResourceTile)entry.Value).numberTileValue = 12;
            }
        }

        foreach(KeyValuePair<BoardCoordinate, Corner> entry in board.corners)
        {
           // Debug.Log(entry.Key.q + " " + entry.Key.r);
            Vector3 position = entry.Key.ToWorldSpace();
            position.y += deltaY;
            GameObject corner = Instantiate(cornerPrefab, position, Quaternion.identity, transform);
            corner.GetComponent<CornerBehaviour>().corner = entry.Value;
        }

        foreach(Port port in board.ports)
        {
            Vector3 position = port.middle.ToWorldSpace();
            Quaternion rotation = port.rotation;
            GameObject portGO = Instantiate(portPrefab, position, rotation, transform);
            portGO.GetComponent<PortBehaviour>().port = port;
        }


        Vector3 thiefPosition = board.thiefPosition.ToWorldSpace();
        thiefPosition.y += deltaY;

        thiefGameObject = Instantiate(thiefPrefab, thiefPosition,
                                      Quaternion.identity, transform);

    }


    /// <summary>
    /// Muta hotul la coordonata data
    /// Functia asta nu realizeaza si furtul! 
    /// </summary>
    /// <param name="corner"></param>
    public void MoveThief(Corner corner)
    {
        MoveThief(corner.coordinate);
    }
    
    /// <summary>
    /// Muta hotul la coordonata data
    /// Functia asta nu realizeaza si furtul!
    /// </summary>
    /// <param name="boardCoordinate"></param>
    public void MoveThief(BoardCoordinate boardCoordinate)
    {
        board.SetThiefPosition(boardCoordinate);
        Vector3 newThiefPosition = board.thiefPosition.ToWorldSpace();
        newThiefPosition.y += deltaY;
        thiefGameObject.transform.position = newThiefPosition;
    }
    public void InitializeBoardFromFile(string filePath)
    {
        board = BoardInitializer.InitializeBoardFromFile(filePath);
    }

}
