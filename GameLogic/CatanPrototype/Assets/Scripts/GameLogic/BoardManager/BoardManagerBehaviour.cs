using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
    private Board board;

    //E o variabila folosita doar la testare, nu are relevanta in viitor
    

    //Dictionary<Vector2, GameObject> board = new Dictionary<Vector2, GameObject>();

    void Start()
    {
        InitializeBoardFromFile("GameLogic/inimioara");
        board.PlacePort(new Corner(new BoardCoordinate(1.33f, -2.66f)),
                        new Corner(new BoardCoordinate(0.66f, -2.33f)),
                        ResourceTypes.Any, 3, 1);

        InstantiateBoard();

        Player p = new Player("ad", "id",board);
        p.color = PlayerColor.Blue;

        AddConnector(p, new BoardCoordinate(1.33f, -2.66f), new BoardCoordinate(1.66f, -2.66f), "road");

        
       
        //bounds = hex.GetComponent<Collider>().bounds.size;
            //InstantiateBoard();
        //PlaceHex(0, 0);
        //PlaceHex(-1, 0);
    }


    /// <summary>
    /// Lista de trade-uri posibile din port-urile construite de jucator
    /// </summary>
    /// <param name="player">Jucatorul pentru care vrem sa aflam trade-urile</param>
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
    public Player CheckLongestRoad()
    {
        //made by jon
        for(int i=0; i<(int)PlayerColor.NbOfColors; ++i)
        {

        }

        return null;
    }


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
        GameObject gameobj = Instantiate(settlementPrefab, position, Quaternion.identity, transform);
        gameobj.GetComponent<SettlementBehaviour>().settlement = settlement;

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
        Connector connector = board.PlaceConnector(p,bc1,bc2,type);
        Vector3 position = connector.middle.ToWorldSpace();// aici nu trebuie sa fie diferita aceasta adaugare?
        GameObject gameobj = Instantiate(connectorPrefab, position, Quaternion.identity, transform);
        gameobj.GetComponent<ConnectorBehaviour>().connector = connector;

    }




    public void InstantiateBoard()
    {
        foreach(KeyValuePair<BoardCoordinate, Tile> entry in board.tiles)
        {
            Vector3 position = entry.Key.ToWorldSpace();
            GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
            tile.GetComponent<TileBehaviour>().tile = entry.Value;
        }

        foreach(KeyValuePair<BoardCoordinate, Corner> entry in board.corners)
        {
           // Debug.Log(entry.Key.q + " " + entry.Key.r);
            Vector3 position = entry.Key.ToWorldSpace();
            position.y += 0.3f;
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
    }
    public void InitializeBoardFromFile(string filePath)
    {
        board = BoardInitializer.InitializeBoardFromFile(filePath);
    }

}
