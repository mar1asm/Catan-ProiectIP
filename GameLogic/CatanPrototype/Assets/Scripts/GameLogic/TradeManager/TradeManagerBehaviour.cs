using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private BoardManagerBehaviour boardManager;
    void Start()
    {
        
    }



    public List<Trade> GetTradesForPlayer(Player p)
    {
        var trades = boardManager.GetTradesForPlayerFromPorts(p);

        //Adaugam trade-urile de 4 : 1
        for(int i = 0; i < (int)ResourceTypes.NbTypes; ++i)
        {
            Trade trade = new Trade();
            trade.AddResourceNeeded((ResourceTypes)i);
            trade.AddResourceNeeded((ResourceTypes)i);
            trade.AddResourceNeeded((ResourceTypes)i);
            trade.AddResourceNeeded((ResourceTypes)i);
            trade.AddResourceObtained(ResourceTypes.Any);
            trades.Add(trade);
        }
        Debug.Log("Ajung aici "  + trades.Count);
        return trades;
    }
    

    


}
