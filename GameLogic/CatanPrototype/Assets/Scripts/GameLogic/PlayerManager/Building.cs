using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building 
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    protected CraftingCost cost;

    public  int verifCost(DeckPlayer deck)
    {
        // parcyrgem deckplayer
        // numarm cate oi, cate lemn, ... are ...  -> nrSh,,, nrB, nrW... nrS....

        // verificam daca nr care ne trebuie noua  <= ce are el
        //parcurgem dictionarul ar trebui sa ma uit ce are in dictionar 

        int nrSheep = 0;
        int nrBrick = 0;
        int nrWood = 0;
        int nrStone = 0;
        int nrWheat = 0;

        foreach (Card c in deck.Cards)
        {
            if (c is ResourceCard) // trebuie vazut daca intra vreodata pe if ul asta 
            {
                Debug.Log("intra aicea merge Ieiii bucurie mare !!!!!!");

                switch (((ResourceCard)c).CardType)
                {
                    case ResourceTypes.Sheep: nrSheep++; break;
                    case ResourceTypes.Brick: nrBrick++; break;
                    case ResourceTypes.Wood: nrWood++; break;
                    case ResourceTypes.Wheat: nrWheat++; break;
                    case ResourceTypes.Stone: nrStone++; break;

                }
                                 
            }
        }
        foreach(ResourceTypes resource in cost.resourcesRequired.Keys)
        {
            switch (resource)
            {
                case ResourceTypes.Sheep: 
                    if (cost.resourcesRequired[resource] > nrSheep)
                    {
                        return 0;
                    }
                    break;
                case ResourceTypes.Brick:
                    if (cost.resourcesRequired[resource] > nrBrick)
                    {
                        return 0;
                    }
                    break;
                case ResourceTypes.Wood:
                    if (cost.resourcesRequired[resource] > nrWood)
                    {
                        return 0;
                    }
                    break;
                case ResourceTypes.Wheat:
                    if (cost.resourcesRequired[resource] > nrWheat)
                    {
                        return 0;
                    }
                    break;
                case ResourceTypes.Stone:
                    if (cost.resourcesRequired[resource] > nrStone)
                    {
                        return 0;
                    }
                    break;

            }
        }

        return 1;
    }
    
    public void takeCards(ref DeckPlayer deck)
    {
        if (verifCost(deck) == 1)
        {
            foreach (ResourceTypes resource in cost.resourcesRequired.Keys)
            {
                for(int i=1;i<= cost.resourcesRequired[resource]; i++)
                {
                    deck.removeCard(resource);
                }
            }
        }
    }

}
