using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SheepDeck : Deck
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    public SheepDeck(string name1)
    {
        type = name1;
        package = new List<Card>();
    }
}
