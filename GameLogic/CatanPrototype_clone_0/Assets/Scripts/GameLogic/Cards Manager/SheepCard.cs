using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepCard : ResourceCard
{
    
   public  override string getResource()
    {
        return "sheep";
    }
    public SheepCard (int number, ResourceTypes type)
    {
        _numberResourceCard = number;
        CardType = type;


    }
}
