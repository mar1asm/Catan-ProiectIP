using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    
    public List<Player> players;

    public Player longestRoadHolder;

    void Start()
    {
        Player test = new Player("test", "abc");
        test.color = PlayerColor.Blue;

        players.Add(test);
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

    void Update()
    {

    }


    public void playerAddsRoad(Player p)
    {
        CraftingCost c = new CraftingCost();
        c.resourcesRequired.Add(ResourceTypes.Wood, 1);
        c.resourcesRequired.Add(ResourceTypes.Brick, 1);
        c.takeCards(p); // in take card face si verificarea si nu ia daca nu are cartile necesare 
    }
    public void playerAddsCity(Player p)
    {
       CraftingCost c = new CraftingCost();
       c.resourcesRequired.Add(ResourceTypes.Stone, 3);
       c.resourcesRequired.Add(ResourceTypes.Wheat, 2);
       c.takeCards(p);
    }
    public void playerAddsSettlement(Player p)
    {
        CraftingCost c = new CraftingCost();
        c.resourcesRequired.Add(ResourceTypes.Wood, 1);
        c.resourcesRequired.Add(ResourceTypes.Brick, 1);
        c.resourcesRequired.Add(ResourceTypes.Sheep, 1);
        c.resourcesRequired.Add(ResourceTypes.Wheat, 1);
        c.takeCards(p);
    }
    public void playerAddsDevelopment(Player p)
    {
        CraftingCost c = new CraftingCost();
        c.resourcesRequired.Add(ResourceTypes.Wheat, 1);
        c.resourcesRequired.Add(ResourceTypes.Stone, 1);
        c.resourcesRequired.Add(ResourceTypes.Sheep, 1);
        c.takeCards(p);
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
   



}
