using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeickInitializaer 
{
    public static Deck InitializeDeckFromFile(string filePath)
    {
        TextAsset txtData = (TextAsset)Resources.Load(filePath);
        string jsonString = txtData.text;

        Deck d = new Deck();
       
       
        
        //ResourceCard r = new ResourceCard();
        DevelopmentCard dv = new DevelopmentCard();

        int nrDev = 0;
        CardDescriber cards = JsonUtility.FromJson<CardDescriber>(jsonString);
        foreach (AvailableCard  available in cards.availableCard)
        {
           
                switch (available.type) {
                    case "sheep":
                        ResourceCard s = new Sheep(available.number, ResourceTypes.Sheep);
                        
                        d.add(s);
                        break;
                case "stone":
                    ResourceCard st = new Stone(available.number, ResourceTypes.Sheep);
                   
                    d.add(st);
                    break;
                case "wood":
                    ResourceCard w = new Sheep(available.number, ResourceTypes.Sheep);
                   
                    d.add(w);
                    break;
                case "wheat":
                    ResourceCard wh = new Sheep(available.number, ResourceTypes.Sheep);
                    
                    d.add(wh);
                    break;
                case "brick":
                    ResourceCard b = new Sheep(available.number, ResourceTypes.Sheep);
                   
                    d.add(b);
                    break;
                case "progress":
                    DevelopmentCard p = new ProgressCard(available.number);
                    d.add(p);
                    nrDev = nrDev + available.number;

                    break;
                case "soldier":
                    DevelopmentCard sd = new SoldierCard(available.number);
                    d.add(sd);
                    nrDev = nrDev + available.number;

                    break;
                case "victory":
                    DevelopmentCard v = new PointCard(available.number);
                    d.add(v);
                    nrDev = nrDev + available.number;

                    break;

            }
        }
        return d;
    }


}
