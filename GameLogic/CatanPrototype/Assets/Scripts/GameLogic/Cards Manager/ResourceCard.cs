using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceCard : Card
{
    // Start is called before the first frame update

    protected int _numberResourceCard; // 95 ar trebui
    protected ResourceTypes cardType;
    public int numberResourceCard
    {
        get
        {
            return _numberResourceCard;
        }
        set
        {
            _numberResourceCard = value;
          
        }
    }
    public ResourceTypes CardType
    {
        get
        {
            return cardType;
        }
        set
        {
            cardType=value;

        }
    }
   public ResourceCard(int number, ResourceTypes type)
    {
        _numberResourceCard = number;
        CardType = type;


    }
   public ResourceCard( ResourceTypes type)
    {
    
        CardType = type;
    }
   public ResourceCard()
    {

        
    }
    public abstract string getResource();

    /* void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }
     */
}
