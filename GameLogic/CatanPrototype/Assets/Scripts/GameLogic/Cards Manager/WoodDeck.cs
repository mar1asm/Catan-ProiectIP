using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDeck : Deck
{
    // Start is called before the first frame update
    /* void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }*/
    public WoodDeck(string name1)
    {
        type = name1;
        package = new List<Card>();
    }
}
