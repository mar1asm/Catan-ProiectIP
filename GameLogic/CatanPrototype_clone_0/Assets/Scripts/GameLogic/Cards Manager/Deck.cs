using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Deck 
{
    public string type;    
    public List<Card> package;
    /*protected int number;
    public int Number
    {
        get
        {
            return number;
        }
       set
        {
            number = value;

        }
        
    }
    */
    //public int _type
    //{
    //    get
    //    {
    //        return type;
    //    }
    //    set
    //    {
    //        type = value;

    //    }
    //}
    public List<Card> Package
    {
        get
        {
            return package;
        }
        set
        {
            package = value;

        }
    }
    /*public int  getNr()
    {
        return _number;
    }*/
    public void add(Card card)
    {
       
        package.Add(card);
       // number++;
    }
    public void remove(Card card)
    {
        if (package.Contains(card))
        {
            package.Remove(card);
          // number--;

        }
    }

    public Card TakeFirstCard()
    {
        if (package.Count <= 0)
        {
            Debug.LogError("[" + type + "] This deck is empty");
            return null;
        }
        Card card = package[0];
        package.RemoveAt(0);
        return card;
    }



    public void Shuffle()
    {
        List<Card> newCards = new List<Card>();

        while(package.Count != 0)
        {
            int index = Random.Range(0, package.Count);
            newCards.Add(package[index]);
            package.RemoveAt(index);
        }

        package = newCards;
    }
    public Deck(string type)
    {
        //this.number = number;
        package = new List<Card>();
        this.type = type;
    }
    public Deck()
    {
        //Debug.Log("Se apeleaza constr implicit");
    }
    /*
    // Start is called before the first frame update
    
     void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }
     */
}
