using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string nickname;
    public string ID;
    public PlayerColor color;
    int score;
    public DeckPlayer deck;
    public Player(string name, string id)
    {
        deck = new DeckPlayer();
        nickname = name;
        ID = id;
        // nu board = b;
    }
    public Player ( string name, string id, DeckPlayer d)
    {
        nickname = name;
        ID = id;
        deck = d;

    }

    /// <summary>
    /// Functie care "plateste" resursele din mana jucatorului
    /// </summary>
    /// <param name="resources"></param>
    public void PayResources(List<ResourceTypes> resources)
    {
        foreach(ResourceTypes resource in resources)
        {
            deck.remove(resource);
        }
    }


    public void GetResources(List<ResourceTypes> resources)
    {
        foreach (var resource in resources)
        {
            deck.add(resource);
        }
    }


    /// <summary>
    /// Returneaza un dictionar care spune cate resurse din fiecare tip are un player
    /// </summary>
    /// <returns></returns>
    public Dictionary<ResourceTypes, int> GetAvailableResources()
    {
        Dictionary<ResourceTypes, int> playerResources = new Dictionary<ResourceTypes, int>();

        foreach (Card card in deck.Cards)
        {
            if (card is ResourceCard)
            {
                ResourceTypes type = ((ResourceCard)card).CardType;
                if (!playerResources.ContainsKey(type))
                {
                    playerResources.Add(type, 0);
                }
                playerResources[type]++;
            }
        }

        return playerResources;
    }

    public void ScoreAdd(int value)
    {
        score = score + value;
    }

    public void ScoreSet(int value)
    {
        score = value;
    }

    //public Settlement PlaceSettlement(BoardCoordinate boardCoordinate, string type)
    //{
    //    board.PlaceSettlement(this, boardCoordinate, type);
    //    return null;
    //}

    //public Connector PlaceConnector(BoardCoordinate bc1, BoardCoordinate bc2, string type)
    //{
    //    board.PlaceConnector(this, bc1, bc2, type);
    //    return null;
    //}
}
