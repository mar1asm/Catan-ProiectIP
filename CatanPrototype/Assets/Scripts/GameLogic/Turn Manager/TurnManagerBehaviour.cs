using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerBehaviour : MonoBehaviour
{
    
    public  Player [] p;
    public  int [] order;
    public int size=0;
    public int currentPlayer =-1;
   
    void Start()
    {
        p = new Player[3];
        size = 3;
        p[0] = new Player();
        p[1]= new Player();
        p[2] = new Player();
        p[0].nickname = "Ana";
        p[1].nickname = "Ion";
        p[2].nickname = "Alex";
        p[0].ID = "12A";
        p[1].ID = "12B";
        p[2].ID = "12C";
        SetOrder(p);//seteaza ordinea random a jucatorilor
        TurnLogic(Next()); //face o miscare (aruncarea zarului si distribuitul resurselor) pentru urmatorul jucator
      

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
        
        Debug.Log("Randul lui " + p[x].nickname );
        int dice1,dice2,number;
        //momentan p[x] face mutarea
        dice1 = RollDice();
        dice2=RollDice();
        number = dice1 + dice2; //Zarul 1 +Zarul 2     
        Debug.Log(p[x].nickname + " a dat " + number+ " .");
       //GiveResources(number);//jucatorii primesc resursele daca au asezari pe regiunea cu numarul picat
    }
   
    //returneaza numarul jucatorului care urmeaza 
    public int  Next()
    {
        this.currentPlayer++;
        this.currentPlayer = this.currentPlayer % size;
        return order[this.currentPlayer];
    }
   
    
}
