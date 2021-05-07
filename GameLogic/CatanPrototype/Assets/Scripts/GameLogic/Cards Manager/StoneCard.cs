using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCard : ResourceCard
{
    // Start is called before the first frame update
    /* void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }*/
    public override string getResource()
    {
        return "stone";
    }
    public StoneCard(int number, ResourceTypes type)
    {
        _numberResourceCard = number;
        CardType = type;


    }
}
