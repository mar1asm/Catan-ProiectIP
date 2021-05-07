using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public  class Deck 
{
    protected string type;    
    protected List<Card> package;
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
    public int _type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;

        }
    }
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
    public Deck(string type)
    {
        //this.number = number;
        this.type = type;
    }
    public Deck()
    {
        Debug.Log("Se apeleaza constr implicit");
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
