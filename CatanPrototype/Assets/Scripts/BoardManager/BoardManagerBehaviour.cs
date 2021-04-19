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
    private Board board;

    //Dictionary<Vector2, GameObject> board = new Dictionary<Vector2, GameObject>();

    void Start()
    {
        InitializeBoardFromFile("GameLogic/inimioara");
        InstantiateBoard();
       
       
        //bounds = hex.GetComponent<Collider>().bounds.size;
            //InstantiateBoard();
        //PlaceHex(0, 0);
        //PlaceHex(-1, 0);
    }



    public List<GameObject> GetAvailableCorners()
    {
        return null;
    }



    /// <summary>
    /// Functia determina cel mai lung drum 
    /// </summary>
    /// <returns></returns>
    public Player CheckLongestRoad()
    {
        return null;
    }


    /// <summary>
    /// Adauga un tile la tabla si il instantiaza in scena
    /// </summary>
    /// <param name="coordinate">Pozitia in boardCoordinates unde trebuie pus hexagonul</param>
    /// <param name="type">Tipul de hexagon</param>
    public void AddTile(BoardCoordinate coordinate, string type)
    {

    }

    /// <summary>
    /// Adauga un settlement la board si il instantiaza in scena
    /// </summary>
    /// <param name="p"></param>
    /// <param name="c"></param>
    public void AddSettlement(Player p, Corner c, string type)
    {
        //ar trebui sa apeleze AddSettlement(Player, BoardCoordinate, string)
    }


    /// <summary>
    /// Adauga un settlement si il instantiaza in scena
    /// </summary>
    /// <param name="p"></param>
    /// <param name="coordinare"></param>
    public void AddSettlement(Player p, BoardCoordinate coordinate, string type)
    {
        
    }


    /// <summary>
    /// Adauga un connector la tabla si il instantiaza in scena
    /// </summary>
    /// <param name="c1"></param>
    /// <param name="c2"></param>
    public void AddConnector(Player player, Corner c1, Corner c2)
    {
        //ar trebui sa apeleze AddConnector(Player, BoardCoordinate, BoardCoordinate)
    }


    /// <summary>
    /// Adauga conector la board si il instantiaza in scena
    /// </summary>
    /// <param name="bc1"></param>
    /// <param name="bc2"></param>
    public void AddConnector(Player player, BoardCoordinate bc1, BoardCoordinate bc2)
    {
    
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
    }
    public void InitializeBoardFromFile(string filePath)
    {
        board = BoardInitializer.InitializeBoardFromFile(filePath);
    }

}
