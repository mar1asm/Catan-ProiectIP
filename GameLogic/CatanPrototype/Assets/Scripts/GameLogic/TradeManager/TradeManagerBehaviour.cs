using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManagerBehaviour : MonoBehaviour
{
    [SerializeField]
    private BoardManagerBehaviour boardManager;

    [SerializeField]
    private TurnManagerBehaviour turnManager;

    
    public List<Trade> domesticTrades = new List<Trade>();
    public List<Trade> harbourTrades = new List<Trade>();

    void Start()
    {
      
    }



    public void DetermineHarbourTradesOfCurrentPlayer()
    {
        harbourTrades = GetHarbourTradesForPlayer(turnManager.currentPlayer);
    }


    /// <summary>
    /// Functie pentru ca un player sa "ia" un trade
    /// </summary>
    /// <param name="p"></param>
    /// <param name="t"></param>
    /// <param name="resources"></param>
    /// <returns></returns>
    public bool PlayerTakesTrade(Player p, Trade t, List<ResourceTypes> resources)
    {
        if (!TradeIsSatisfiedByResources(t, resources)) return false;

        if(harbourTrades.Contains(t))
        {
            PlayerTakesHarbourTrade(p, t, resources);
            return true;
        }
        if(domesticTrades.Contains(t))
        {
            PlayerTakesDomesticTrade(p, t, resources);
            return true;
        }

        return false;
    }



    /// <summary>
    /// Aceasta functie verifica daca un trade este satisfacut de o lista de resurse
    /// </summary>
    /// <param name="t"></param>
    /// <param name="resources"></param>
    /// <returns></returns>
    public bool TradeIsSatisfiedByResources(Trade t, List<ResourceTypes> resources)
    {
        Dictionary<ResourceTypes, int> groupedTypes = new Dictionary<ResourceTypes, int>();
        foreach(ResourceTypes resource in resources)
        {
            if(!groupedTypes.ContainsKey(resource))
            {
                groupedTypes.Add(resource, 0);
            }
            groupedTypes[resource]++;
        }

        int nbAny = 0;

        foreach (ResourceTypes resource in t.resourcesNeeded) 
        {
            if(resource == ResourceTypes.Any)
            {
                nbAny++;
                continue;
            }
            if (!groupedTypes.ContainsKey(resource)) return false;
            if (groupedTypes[resource] <= 0) return false;
            groupedTypes[resource]--;
        }

        foreach (var resource in groupedTypes)
        {
            if (resource.Value >= nbAny) return true;
        }

        return false;
    }


    /// <summary>
    /// Functie apelata de <see cref="PlayerTakesTrade(Player, Trade, List{ResourceTypes})"/>
    /// atunci cand trade-ul e un trade cu un alt player
    /// </summary>
    /// <param name="p">Player-ul care face trade-ul</param>
    /// <param name="t">Trade-ul pe care il face</param>
    /// <param name="resources">Lista cu resursse de la player</param>
    private void PlayerTakesDomesticTrade(Player p, Trade t, List<ResourceTypes> resources)
    {

        if (p == turnManager.currentPlayer) return;

        p.PayResources(resources);

        p.GetResources(t.resourcesObtained);

        //pentru ca e un trade Domestic, e clar ca jucatorul a carui tura este primeste resursele..

        turnManager.currentPlayer.GetResources(resources);

        if(t.oneTimeUse)
        {
            domesticTrades.Remove(t);
        }
    }


    /// <summary>
    /// Functie apelata de <see cref="PlayerTakesTrade(Player, Trade, List{ResourceTypes})"/>
    /// atunci cand trade-ul e un trade cu port-ul
    /// </summary>
    /// <param name="p">Player-ul care face trade-ul</param>
    /// <param name="t">Trade-ul pe care il face</param>
    /// <param name="resources">Lista cu resursse de la player</param>
    private void PlayerTakesHarbourTrade(Player p, Trade t, List<ResourceTypes> resources)
    {
        p.PayResources(resources);

        //Nu sunt sigur cum facem pentru ResourceTypes.Any.. dar gasim solutii
        
        p.GetResources(t.resourcesObtained);

        if(t.oneTimeUse)
        {
            harbourTrades.Remove(t);
        }
    }

    /// <summary>
    /// Creaza un domestic Trade si il returneaza
    /// Returneaza null daca player-ul curent nu poate sa faca acest trade
    /// </summary>
    /// <param name="resourcesNeeded"></param>
    /// <param name="resourcesObtained"></param>
    /// <returns></returns>
    public Trade CreateDomesticTrade(List<ResourceTypes> resourcesNeeded, List<ResourceTypes> resourcesObtained)
    {
        var playerResources = turnManager.currentPlayer.GetAvailableResources();

        //Verificam daca player-ul POATE sa dea resursele pe care zice ca le da
        foreach(ResourceTypes resource in resourcesObtained)
        {
            if (!playerResources.ContainsKey(resource)) return null;
            if (playerResources[resource] <= 0) return null;
            playerResources[resource]--;
        }

        Trade trade = new Trade(true);
        foreach(ResourceTypes resource in resourcesNeeded)
        {
            trade.AddResourceNeeded(resource);
        }

        foreach(ResourceTypes resource in resourcesObtained)
        {
            trade.AddResourceObtained(resource);
        }

        domesticTrades.Add(trade);

        return trade;
        
    }
    
    /// <summary>
    ///  Elimina trade-urile facute de un jucator in tura sa
    /// </summary>
    public void ClearTrades()
    {
        domesticTrades.Clear();
        harbourTrades.Clear();
    }


    
    

    /// <summary>
    /// Verifica daca Jucatorul poate sa plateasca pentru acest trade.
    /// NU face insasi platirea, doar verifica daca are cum
    /// </summary>
    /// <param name="player">Jucatorul pe care il verificam</param>
    /// <param name="trade">Trade-ul care ne intereseaza</param>
    /// <returns>True daca poate plati, fals altfel</returns>
    public bool PlayerSatisfiesTradeRequirements(Player player, Trade trade)
    {
        var playerResources = player.GetAvailableResources();

        

        //Intai scadem din cele care nu sunt any

        int nbOfAny = 0;

        foreach(ResourceTypes resource in trade.resourcesNeeded)
        {
            if (resource == ResourceTypes.Any)
            {
                //numaram cate resurse de tipul ANY sunt
                nbOfAny++;
                continue;
            }
            if (!playerResources.ContainsKey(resource)) return false;
            if (playerResources[resource] <= 0) return false;
            playerResources[resource]--;
        }

        foreach(KeyValuePair<ResourceTypes, int> pair in playerResources)
        {
            //Aici verificam daca exista o resursa care ar putea sa plateasca pentru Any,
            //dupa ce am eliminat resursele mandatorii
            if(pair.Value >= nbOfAny)
            {
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// Cauta unde are construite asezari pe porturi si aduga si trade-urile basic 4 : 1
    /// Returneaza sub forma de lista
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public List<Trade> GetHarbourTradesForPlayer(Player p)
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
        
        return trades;
    }
    

    


}
