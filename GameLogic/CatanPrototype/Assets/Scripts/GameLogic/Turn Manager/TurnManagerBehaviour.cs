using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerBehaviour : MonoBehaviour
{
    
    public  Player [] players;
    public  int [] order;
    public int size=0;
    public int currentPlayerIndex =-1;

    [SerializeField]
    private TradeManagerBehaviour tradeManager;

    void Start()
    {

    }


    public Player currentPlayer
    {
        get
        {
            return players[order[currentPlayerIndex]];
        }
    }


    //da cu zarul random de la 1 la 7
    public int RollDice()
    {
        int dice = Random.Range(1, 7);
        return dice;
    }
    //seteaza ordinea aleatorie a jucatorilor din lista
    // public void SetOrder(PlayerManagement p)
    public void SetOrder(Player [] p)
    {
       
        int x;
        this.order = new int[size];
        int [] v=new int[10];
        for(int i=0;i<size;i++)
        {
            v[i] = 0;
        }
        for (int i = 0; i < size; i++)
        { x= Random.Range(0, size);
            while(v[x]==1)
                x= Random.Range(0, size);
            this.order[i] = x;
            v[x] = 1;
        }
    }
   //da cu zarul pentru randul celui de al x-lea jucator
    public void TurnLogic(int x)
    {
        
        Debug.Log("Randul lui" + players[x].nickname );
        int dice1,dice2,number;
        //momentan p[x] face mutarea
        dice1 = RollDice();
        dice2=RollDice();
        number = dice1 + dice2; //Zarul 1 +Zarul 2     
        number = 5;
       // GiveResources(number);//jucatorii primesc resursele daca au asezari pe regiunea cu numarul picat
    }
    
   
    public void GiveResources(int nr)
    {
        Debug.Log("Jucatorii care au asezari pe" + nr + "primesc resurse.");
        
       /* foreach (KeyValuePair<BoardCoordinate, Tile> entry in board.tiles)
        {
            if (entry.Value.tile.number == nr && entry.Value is ResourceTypes)
                tile.SpecialAction();       
        }*/

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
