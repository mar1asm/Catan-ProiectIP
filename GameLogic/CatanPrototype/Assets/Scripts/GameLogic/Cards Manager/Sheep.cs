using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : ResourceCard
{
    
   public  override string getResource()
    {
        return "sheep";
    }
    public Sheep (int number, ResourceTypes type)
    {
        _numberResourceCard = number;
        CardType = type;


    }
}
