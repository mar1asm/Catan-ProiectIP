using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    
    public List<Player> players;

    void Start()
    {
        Player test = new Player("test", "abc");
        test.color = PlayerColor.Blue;

        players.Add(test);

        test.deck.add(new SheepCard(1, ResourceTypes.Sheep));
        test.deck.add(new SheepCard(1, ResourceTypes.Sheep));
        test.deck.add(new SheepCard(1, ResourceTypes.Sheep));
        


        Trade t = new Trade(true);
        t.AddResourceNeeded(ResourceTypes.Sheep);
        t.AddResourceNeeded(ResourceTypes.Sheep);
        //t.AddResourceNeeded(ResourceTypes.Stone);

        TradeManagerBehaviour tmb = GameObject.Find("Trade Manager").GetComponent<TradeManagerBehaviour>();


        Debug.Log(tmb.PlayerSatisfiesTradeRequirements(test, t));

        test.PayResources(t.resourcesNeeded);

        Debug.Log("Cate carti are?" + test.deck.Cards.Count);
        //StartCoroutine(WaitForBoardToFinish());
        
 
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
        
    }
    public void AddPlayer(Player p)
    {
        players.Add(p);
    }
    public void RemovePlayer(Player p)
    {
        players.Remove(p);
    }
   

}
