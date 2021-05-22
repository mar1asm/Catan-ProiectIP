using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerBehaviour : MonoBehaviour
{
    
    //public  Player [] players;

    public PlayerManager playerManager;
    public  int [] order;
    public int size=0;
    public int currentPlayerIndex =-1;

    public Queue<Player> setupOrder = new Queue<Player>();

    [SerializeField]
    private TradeManagerBehaviour tradeManager;

    [SerializeField]
    private BoardManagerBehaviour boardManager;

    public Player currentPlayer
    {
        get
        {
            return playerManager.players[order[currentPlayerIndex]];
        }
    }


    public void InitSetupOrder() {
        var players = playerManager.players;
        for(int i = 0 ; i < players.Count; ++i) {
            setupOrder.Enqueue(players[i]);
        }
        for(int i = players.Count - 1; i >= 0; --i) {
            setupOrder.Enqueue(players[i]);
        }

        foreach (var player in setupOrder)
        {
            Debug.Log("ordinea de setup: " + player.nickname);
        }
    }

    public Player GetNextPlayerInSetup() {
        if(setupOrder.Count == 0) return null;
        return setupOrder.Dequeue();
    }

    public void DisplayOrder() {
        string orderString = "";
        for(int i = 0; i < order.Length; ++i) {
            //order += players[i].nickname;
            orderString += playerManager.players[i].nickname;
            orderString += " ";
        }
        Debug.Log("ORDINEA JUCATORILOR E " + orderString);
    }

    //da cu zarul random de la 1 la 6
    public int RollDice()
    {
        int dice = Random.Range(1, 7);
        return dice;
    }


    public void SetOrder(int[] newOrder) {
        Debug.Log("Ordinea noua ");
        for(int  i = 0 ; i < newOrder.Length; ++i) {
            Debug.Log("index: " + newOrder[i]);
        }
        this.size = playerManager.players.Count;
        order = new int[size];
        for(int i = 0 ; i < order.Length; ++i) {
            order[i] = newOrder[i];
        }
    }

    //seteaza ordinea aleatorie a jucatorilor din lista
    // public void SetOrder(PlayerManagement p)
    public void SetOrder()
    {
        
        int x;
        this.size = playerManager.players.Count;
        Debug.LogWarning("cati playeri sunt aici nabii? " + playerManager.players.Count);
        this.order = new int[size];
        int [] v=new int[10];
        for(int i=0;i<size;i++)
        {
            v[i] = 0;
        }
        for (int i = 0; i < size; i++)
        { 
            x= Random.Range(0, size);
            while(v[x]==1)
                x= Random.Range(0, size);
            this.order[i] = x;
            v[x] = 1;
        }

        Debug.LogWarning("ba da ce naiba ai bre");
        Debug.LogWarning("marimea order " + order.Length);
    }
   //da cu zarul pentru randul celui de al x-lea jucator
    public void TurnLogic(int x)
    {
        
        Debug.Log("Randul lui" + playerManager.players[x].nickname );
        int dice1,dice2,number;
        //momentan p[x] face mutarea
        dice1 = RollDice();
        dice2=RollDice();
        number = dice1 + dice2; //Zarul 1 +Zarul 2    

        number = 12;

        boardManager.GiveResources(12);
  
       // GiveResources(number);//jucatorii primesc resursele daca au asezari pe regiunea cu numarul picat
    }
    
  
   
    //returneaza numarul jucatorului care urmeaza 
    public int  Next()
    {
        tradeManager.ClearTrades();
        tradeManager.DetermineHarbourTradesOfCurrentPlayer();
        this.currentPlayerIndex++;
        this.currentPlayerIndex = this.currentPlayerIndex % size;
        return order[this.currentPlayerIndex];
    }
   
    
}
