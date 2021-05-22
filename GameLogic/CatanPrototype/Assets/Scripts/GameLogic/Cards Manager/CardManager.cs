using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private List<Deck> decks;

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
        // decks = DeckInitializer.InitializeDeckFromFile("GameLogic/card1");


        // ShuffleDecks();
       
        
       // ia lista de deckuri 
       // sa extrg carti din deckuri 

    }

    /// <summary>
    /// Ia prima carte din pachetul cu numele nameOfDeck. 
    /// Returneaza null daca nu exista pachetul respectiv
    /// </summary>
    /// <param name="nameOfDeck"></param>
    /// <returns></returns>
    public Card TakeCardFromDeck(string nameOfDeck)
    {
        foreach (var deck in decks)
        {
            if (deck.type == nameOfDeck) return deck.TakeFirstCard();
        }

        return null;
    }


    /// <summary>
    /// Functie care amesteca toate pachetele
    /// </summary>
    public void ShuffleDecks()
    {
        foreach (var deck in decks)
        {
            deck.Shuffle();
        }
    }
}
