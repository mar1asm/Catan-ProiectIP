using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log("Merge la inceput ");
        Card c;
        c = new SheepCard(5,ResourceTypes.Sheep);
        DeckPlayer p;
        p = new DeckPlayer();
        p.add(c);
        Debug.Log("Merge la sfarsit" + p.nrCards);
        */
        List<Deck> lst = new List<Deck>();
       lst= DeckInitializer.InitializeDeckFromFile("GameLogic/card1");
       // ia lista de deckuri 
       // sa extrg carti din deckuri 

    }

    // Update is called once per frame
  /*  void Update()
    {
        
    }*/
}
