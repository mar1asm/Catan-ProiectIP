using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyDeck : Deck
{
    // Start is called before the first frame update
    public MonopolyDeck(string name1)
    {
        type = name1;
        package = new List<Card>();
    }
}
