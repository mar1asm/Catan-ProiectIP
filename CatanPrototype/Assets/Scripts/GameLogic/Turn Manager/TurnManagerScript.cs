using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerScript : MonoBehaviour
{
    
    //public  static PlayerManagement p;
    public static string[] p = { "Ana", "Ioana", "Dan" };
    public static int [] order;
    public static int size;
    public static int currentPlayer =-1;
  
    //da cu zarul random de la 1 la 7
    public int RollDice()
    {
        
        int dice = Random.Range(1, 7);
        return dice;
    }
    //seteaza ordinea aleatorie a jucatorilor din lista
    // public void SetOrder(PlayerManagement p)
    public void SetOrder(string[] p)
    {
        size = p.Length;
        int x;
       /* while (p.playerslist !=NULL)
        {
            p.playerslist = p.playerslist->next;
            size++;
        }*/
        int [] v=new int[10];
        for(int i=0;i<size;i++)
        {
            v[i] = 0;
        }
        for (int i = 0; i < size; i++)
        { x= Random.Range(0, size);
            while(v[x]==1)
                x= Random.Range(0, size);
            order[i] = x;
            v[x] = 1;
        }
    }
   //da cu zarul pentru randul celui de al x-lea jucator
    public void Turn(int x)
    {
        //int i = 0;
        int dice1,dice2,number;
        /*while(x>i)
        {
            p.playerlist = p.playerlist->next;
            i++;
        }*/
        //momentan p[x] face mutarea
        //p.playerlist.nickname va face mutarea dupa ce implementam PlayerManager
        dice1 = RollDice();
        dice2=RollDice();
        number = dice1 + dice2; //Zarul 1 +Zarul 2     
        number = 5;
       // GiveResources(number);//jucatorii primesc resursele daca au asezari pe regiunea cu numarul picat
    }
    
    /*generez resurse pentru tile ul cu numarul nr
    public void GiveResources(int nr)
    {
       
        foreach (KeyValuePair<BoardCoordinate, Tile> entry in board.tiles)
        {
            if (entry.Value.tile.number == nr && entry.Value is ResourceTypes)
                tile.SpecialAction();       
        }

    }
    */
    //returneaza numarul jucatorului care urmeaza 
    public int  Next()
    {
        currentPlayer++;
        currentPlayer = currentPlayer % size;
        return order[currentPlayer];
    }
   
    
}
