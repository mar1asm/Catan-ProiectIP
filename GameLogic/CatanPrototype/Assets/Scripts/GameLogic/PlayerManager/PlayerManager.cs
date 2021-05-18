using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private BoardManagerBehaviour boardManager;

    [SerializeField]
    private TurnManagerBehaviour turnManager;

    public List<Player> players;

    public Player clientPlayer = new Player("test", "1");

    public Player longestRoadHolder = null;
    public int longestRoadLenght = 4;
    public Player biggestArmyHolder = null;
    public int biggestArmySize = 2;


    [SerializeField]
    private int pointsToWin = 1;
    

    void Start()
    {
        
    }


    public void Setup()
    {
        SetupTurnManagerPlayers();
        turnManager.SetOrder();
        turnManager.currentPlayerIndex = 0;
    }


    private void SetupTurnManagerPlayers()
    {
        turnManager.size = players.Count;
        turnManager.players = new Player[players.Count];
        for (int i = 0; i < players.Count; ++i)
        {
            turnManager.players[i] = players[i];
        }
    }

    public void SetPointGoal(int value)
    {
        pointsToWin = value;
    }

    /// <summary>
    /// Corutina asta este doar pentru test, a nu se folosi!!
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForBoardToFinish()
    {
        yield return new WaitForSeconds(1);
        BoardManagerBehaviour bb = GameObject.Find("Board Manager").GetComponent<BoardManagerBehaviour>();
        bb.AddSettlement(players[0], new BoardCoordinate(1.33f, -2.66f), "village");
        Debug.Log("am pus");
        TradeManagerBehaviour tmb = GameObject.Find("Trade Manager").GetComponent<TradeManagerBehaviour>();
        var trades = tmb.GetHarbourTradesForPlayer(players[0]);
        Debug.Log("E bine aici " + trades.Count);
        foreach (Trade trade in trades)
        {
            Debug.Log(trade.ToString() + "\n");
        } 
    }

    public void playerAddsRoad(Player p)
    {
        CraftingCost c = new CraftingCost();
        c.resourcesRequired.Add(ResourceTypes.Wood, 1);
        c.resourcesRequired.Add(ResourceTypes.Brick, 1);
        
        if (!c.verifCost(p)) return;

        c.takeCards(p);
        PlayerColor colorLongest = boardManager.GetPlayerWithLongestRoad(longestRoadLenght);
        foreach (var player in players)
        {
            if (player.color == colorLongest)
            {
                longestRoadHolder = player;
                longestRoadLenght = boardManager.GetLongestLenghtOfPlayer(longestRoadHolder);
                break;
            }
        }
        VerifyWinningConditions();
    }
    public void playerAddsCity(Player p)
    {
       CraftingCost c = new CraftingCost();
       c.resourcesRequired.Add(ResourceTypes.Stone, 3);
       c.resourcesRequired.Add(ResourceTypes.Wheat, 2);
       if(c.verifCost(p))
       {
            c.takeCards(p);
       }
       VerifyWinningConditions();
    }
    public void playerAddsSettlement(Player p)
    {
        CraftingCost c = new CraftingCost();
        c.resourcesRequired.Add(ResourceTypes.Wood, 1);
        c.resourcesRequired.Add(ResourceTypes.Brick, 1);
        c.resourcesRequired.Add(ResourceTypes.Sheep, 1);
        c.resourcesRequired.Add(ResourceTypes.Wheat, 1);
        //c.takeCards(p);
        if(c.verifCost(p))
        {
            c.takeCards(p);
        }
        VerifyWinningConditions();
    }
    public void playerAddsDevelopment(Player p)
    {
        CraftingCost c = new CraftingCost();
        c.resourcesRequired.Add(ResourceTypes.Wheat, 1);
        c.resourcesRequired.Add(ResourceTypes.Stone, 1);
        c.resourcesRequired.Add(ResourceTypes.Sheep, 1);
        //c.takeCards(p);
        if(c.verifCost(p))
        {
            c.takeCards(p);
        }
        VerifyWinningConditions();
    }
    public void AddPlayer(Player p)
    {
        players.Add(p);
    }
    public void RemovePlayer(Player p)
    {
        players.Remove(p);
    }

    public void PlayerStealsFromPlayer(Player thief, Player victim)
    {
        Card card = victim.RemoveRandomResourceCard();
        if (card == null) return;
        List<Card> resourcesAux = new List<Card>();
        resourcesAux.Add(card);
        thief.GetCard(card);
    }
   

    public void VerifyWinningConditions()
    {
        Player current = turnManager.currentPlayer;
        int score = 0;
        if (longestRoadHolder == current) score += 2;
        if (biggestArmyHolder == current) score += 2;

        score += boardManager.GetPlayerPointsFromSettlements(current);
        score += current.GetPointsFromHand();
        if(score >= pointsToWin)
        {
            Debug.LogWarning("BRAVOOOOOOOOOOO " + current.nickname + " AI CASTIGAT UHUUUUUUUUU");
        }
    }

}
