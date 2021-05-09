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
        StartCoroutine(WaitForBoardToFinish());
        /// cod de test  pentru player manager
        DeckPlayer dp = new DeckPlayer();
        ResourceCard sp = new SheepCard(1,ResourceTypes.Sheep);
        ResourceCard sp1 = new SheepCard(1, ResourceTypes.Sheep);
        ResourceCard sp2 = new SheepCard(1, ResourceTypes.Sheep);
        ResourceCard bc = new BrickCard(1, ResourceTypes.Brick);
        ResourceCard bc1 = new  BrickCard(1, ResourceTypes.Brick);
        ResourceCard bc2 = new BrickCard(1, ResourceTypes.Brick);
        ResourceCard wc = new WoodCard(1, ResourceTypes.Wood);
        ResourceCard wc1 = new WoodCard(1, ResourceTypes.Wood);
        ResourceCard wc2 = new WoodCard(1, ResourceTypes.Wood);
        dp.add(sp);
        dp.add(sp1);
        dp.add(sp2);
        dp.add(bc);
        dp.add(bc1);
        dp.add(bc2);
        dp.add(wc);
        dp.add(wc1);
        dp.add(wc2);
        Building rc = new RoadCost();
        rc.takeCards(ref dp);
        Debug.Log("Uite cate au ramas ");
        Debug.Log(dp.nrCards);
    }

    IEnumerator WaitForBoardToFinish()
    {
        yield return new WaitForSeconds(1);
        BoardManagerBehaviour bb = GameObject.Find("Board Manager").GetComponent<BoardManagerBehaviour>();
        bb.AddSettlement(players[0], new BoardCoordinate(1.33f, -2.66f), "village");
        Debug.Log("am pus");
        TradeManagerBehaviour tmb = GameObject.Find("Trade Manager").GetComponent<TradeManagerBehaviour>();
        var trades = tmb.GetTradesForPlayer(players[0]);
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
