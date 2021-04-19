using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCard : Card
{
    // Start is called before the first frame update

    private int _numberResourceCard; // 95 ar trebui
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
    ResourceCard(int number, ResourceTypes type)
    {
        _numberResourceCard = number;
        CardType = type;


    }
    ResourceCard( ResourceTypes type)
    {
    
        CardType = type;
    }

    /* void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }
     */
}
