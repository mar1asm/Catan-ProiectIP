using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingCost
{
   //public List<ResourceTypes> resourcesRequired;

   public Dictionary<ResourceTypes,int> resourcesRequired;
    public CraftingCost()
    {
        resourcesRequired = new Dictionary<ResourceTypes, int>();
    }
    public bool verifCost(Player p)
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

        foreach (Card c in p.deck.Cards)
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
        foreach (ResourceTypes resource in resourcesRequired.Keys)
        {
            switch (resource)
            {
                case ResourceTypes.Sheep:
                    if (resourcesRequired[resource] > nrSheep)
                    {
                        return false;
                    }
                    break;
                case ResourceTypes.Brick:
                    if (resourcesRequired[resource] > nrBrick)
                    {
                        return false;
                    }
                    break;
                case ResourceTypes.Wood:
                    if (resourcesRequired[resource] > nrWood)
                    {
                        return false;
                    }
                    break;
                case ResourceTypes.Wheat:
                    if (resourcesRequired[resource] > nrWheat)
                    {
                        return false;
                    }
                    break;
                case ResourceTypes.Stone:
                    if (resourcesRequired[resource] > nrStone)
                    {
                        return false;
                    }
                    break;

            }
        }

        return true;
    }

    public void takeCards(Player p)
    {
        if (verifCost(p))
        {
            foreach (ResourceTypes resource in resourcesRequired.Keys)
            {
                for (int i = 1; i <= resourcesRequired[resource]; i++)
                {
                    p.deck.removeCard(resource);
                }
            }
        }
    }
}
