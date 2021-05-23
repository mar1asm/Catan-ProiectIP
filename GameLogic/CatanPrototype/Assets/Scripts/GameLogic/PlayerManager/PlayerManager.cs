using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{

    [SerializeField]
    private BoardManagerBehaviour boardManager;

    [SerializeField]
    private TurnManagerBehaviour turnManager;

    [SerializeField]
    private MasterHighlighterBehaviour masterHighlighter;

    [SerializeField]
    private BannerHolderBehaviour bannerHolder;

    public List<Player> players = new List<Player>();


    public CraftingCost roadCraftingCost, villageCraftingCost;

    public Player clientPlayer {
        get {
            foreach (var player in players)
            {
                if(player.nickname == UserInfo.GetUsername()) return player;
            }

            return null;
        }
    }

    public Player longestRoadHolder = null;
    public int longestRoadLenght = 4;
    public Player biggestArmyHolder = null;
    public int biggestArmySize = 2;


    [SerializeField]
    private int pointsToWin = 10;
    

    void Start()
    {
        roadCraftingCost = new CraftingCost();
        roadCraftingCost.resourcesRequired.Add(ResourceTypes.Brick, 1);
        roadCraftingCost.resourcesRequired.Add(ResourceTypes.Wood, 1);


        villageCraftingCost = new CraftingCost();
        villageCraftingCost.resourcesRequired.Add(ResourceTypes.Wood, 1);
        villageCraftingCost.resourcesRequired.Add(ResourceTypes.Sheep, 1);
        villageCraftingCost.resourcesRequired.Add(ResourceTypes.Brick, 1);
        villageCraftingCost.resourcesRequired.Add(ResourceTypes.Wheat, 1);
        
    }


    public void GiveResourceToPlayer(string username, List<ResourceTypes> resources) {
        Player p = GetPlayerWithUsername(username);
        p.GetResources(resources);
    }
    public Player GetPlayerWithUsername(string username) {
        foreach (var player in players)
        {
            if(player.nickname == username) {
                return player;
            }
        }

        return null;
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



    public IEnumerator PlayerMovesThief(Player p)
    {
        
        masterHighlighter.SpawnHighlighters(boardManager.PlacesWithoutThief());
        //masterHighlighter.waiting = true;
        yield return StartCoroutine(masterHighlighter.WaitForUserInput());

        //yield return new WaitUntil(() => masterHighlighter.waiting);
        BoardCoordinate coordinate = BoardCoordinate.ToBoardCoordinate(masterHighlighter.positionPressed);

        boardManager.MoveThief(coordinate);
        
    }

    

    public void playerAddsRoad(Player p, BoardCoordinate boardCoordinate, string type)
    {
        
        if (!roadCraftingCost.verifCost(p)) return;

        roadCraftingCost.takeCards(p);


        boardManager.AddConnector(p, boardCoordinate, type);

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
    public void playerAddsSettlement(Player p, BoardCoordinate bc)
    {
        //c.takeCards(p);
        if(villageCraftingCost.verifCost(p))
        {
            villageCraftingCost.takeCards(p);
        }

        boardManager.AddSettlement(p, bc, "village");
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
        
        int score= DeterminePointsOfPlayer(current);

        if(score >= pointsToWin)
        {
            Debug.LogWarning("BRAVOOOOOOOOOOO " + current.nickname + " AI CASTIGAT UHUUUUUUUUU");
        }
    }


    private int DeterminePointsOfPlayer(Player p, bool considerHand = true) {
        int score = 0;
        if (longestRoadHolder == p) score += 2;
        if (biggestArmyHolder == p) score += 2;

        score += boardManager.GetPlayerPointsFromSettlements(p);
        if(considerHand)
            score += p.GetPointsFromHand();
        return score;
    }

    public void UpdateScoreDisplays() 
    {
        foreach (var player in players)
        {
            //aici punem sa nu ia in considerare mana
            int score = DeterminePointsOfPlayer(player, false);
            bannerHolder.UpdateScore(player.nickname, score);
        }
    }

}
