using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadDeck : Deck
{
    // Start is called before the first frame update
    public RoadDeck(string name1)
    {
        type = name1;
        package = new List<Card>();
    }
}
