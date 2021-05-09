using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPlayer
{
    private int _nrCards;
    private List<Card> cards;
    public int nrCards
    {
        get
        {
            return _nrCards;
        }
        set
        {
            _nrCards = value;

        }
    }
    public List<Card> Cards
    {
        get
        {
            return cards;
        }
        set
        {
            cards = value;

        }
    }
    public DeckPlayer()
    {
        _nrCards = 0;
        cards = new List<Card>();
       
    }
    public void add(Card card)
    {
        cards.Add(card);
        _nrCards = _nrCards + 1;
    }
    public void remove(Card card)
    {
        if(cards.Contains(card))
        {
            cards.Remove(card);
            _nrCards = _nrCards - 1;

        }
    }
    public void removeCard(ResourceTypes resource)
    {

        foreach (Card c in Cards)
        {
            if (c is ResourceCard) // trebuie vazut daca intra vreodata pe if ul asta 
            {
                if(((ResourceCard)c).CardType == resource)
                {
                    remove(c);
                    break;
                }

            }
        }
    }
    // Start is called before the first frame update
    /*void Start()
      {

      }

      // Update is called once per frame
      void Update()
      {

      }
      */
}       
