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
            return cards.Count;
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
    }

    public void add(ResourceTypes resource)
    {
        switch (resource)
        {
            case ResourceTypes.Sheep: add(new SheepCard(1, resource));
                break;
            case ResourceTypes.Brick: add(new BrickCard(1, resource));
                break;
            case ResourceTypes.Wood: add(new WoodCard(1, resource));
                break;
            case ResourceTypes.Stone: add(new StoneCard(1, resource));
                break;
            case ResourceTypes.Wheat: add(new WheatCard(1, resource));
                break;
            default: Debug.LogError("Not a correct type of resource");
                break;
        }
    }

    public void remove(Card card)
    {
        if(cards.Contains(card))
        {
            cards.Remove(card);
        }
    }

    public void remove(ResourceTypes resource)
    {
        foreach(Card card in cards)
        {
            if(card is ResourceCard)
            {
                if( ((ResourceCard)card).CardType == resource)
                {
                    remove(card);
                    return;
                }
            }
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
