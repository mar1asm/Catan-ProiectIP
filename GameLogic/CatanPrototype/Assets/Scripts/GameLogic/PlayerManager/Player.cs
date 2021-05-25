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
    private int _armySize = 0;
    public Player(string name, string id)
    {
        deck = new DeckPlayer();
        nickname = name;
        ID = id;
        // nu board = b;
    }

    public string GetHandInfo()
    {
        string handString = "";
        foreach(var card in deck.Cards)
        {
            handString += card.GetType() + ",";
        }
        return handString;
    }

    public Player(string name, string id, PlayerColor color)
    {
        deck = new DeckPlayer();
        nickname = name;
        ID = id;
        this.color = color;

        // for(int i = 0 ; i < 10; ++i) {
        //     deck.add(new WoodCard(1, ResourceTypes.Wood));
        // }
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

    public void GetCard(Card card)
    {
        deck.add(card);
    }

    public void GetResources(List<ResourceTypes> resources)
    {
        foreach (var resource in resources)
        {
            deck.add(resource);
        }
    }

    /// <summary>
    /// Cate puncte sunt in mana lui?
    /// </summary>
    public int pointsFromHand
    {
        get
        {
            return GetPointsFromHand();
        }
    }

    /// <summary>
    /// Returneaza numarul de carti cu puncte din mana
    /// </summary>
    /// <returns></returns>
    public int GetPointsFromHand()
    {
        int sum = 0;
        foreach(var card in deck.Cards)
        {
            if(card is PointCard)
            {
                sum++;
            }
        }
        return sum;
    }

    public void PlayCard(int index)
    {
        Card card = deck.Cards[index];
        if(!(card is DevelopmentCard))
        {
            return;
        }

        //aici trebuie logica cartilor...

        if(card is SoldierCard)
        {
            _armySize++;
        }

        deck.remove(card);
    } 



    /// <summary>
    /// Cati soldati a jucat? (nu in mana lui)
    /// </summary>
    public int numberOfSoldiers
    {
        get
        {
            return _armySize;
        }
    }


    /// <summary>
    /// Returneaza numarul de soldati jucati
    /// </summary>
    /// <returns></returns>
    public int GetNumberOfSoldiers()
    {
        return _armySize;
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

    public int GetNumberOfResources() {
        int nr = 0;
        foreach (Card card in deck.Cards)
        {
            if(card is ResourceCard) {
                nr++;
            }
        }

        return nr;
    }

    public Card RemoveRandomResourceCard()
    {
        List<Card> resourceCards = new List<Card>();
        foreach (var card in deck.Cards)
        {
            if(card is ResourceCard)
            {
                resourceCards.Add(card);
            }
        }
        if (resourceCards.Count == 0) return null;

        int index = Random.Range(0, resourceCards.Count);
        Card resourceCard = resourceCards[index];
        resourceCards.RemoveAt(index);
        return resourceCard;
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
