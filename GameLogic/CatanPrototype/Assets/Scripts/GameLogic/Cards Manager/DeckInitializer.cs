using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInitializer 
{
    public static List<Deck> InitializeDeckFromFile(string filePath)
    {
        TextAsset txtData = (TextAsset)Resources.Load(filePath);
        string jsonString = txtData.text;
        List<Deck> lst = new List<Deck>();
       
       
       
        
        //ResourceCard r = new ResourceCard();
        //DevelopmentCard dv = new DevelopmentCard();

       // int nrDev = 0;
        CardDescriber cards = JsonUtility.FromJson<CardDescriber>(jsonString);
        foreach (AvailableCard  available in cards.availableCard)
        {

           
                switch (available.name) {
                    case "sheep":
                 
                     foreach (DeckDescriber dd in available.d)
                     {
                         
                         ResourceCard s = new SheepCard(dd.number, ResourceTypes.Sheep);

                        Deck d = new SheepDeck("sheep");
                        for (int i = 0; i < dd.number; i++)
                            d.add(s);
                        lst.Add(d);
                    }

                    break;
                case "stone":
                    foreach (DeckDescriber dd in available.d)
                    {
                        ResourceCard st = new StoneCard(dd.number, ResourceTypes.Stone);
                        Deck d1 = new StoneDeck("stone");

                        for (int i = 0; i < dd.number; i++)
                            d1.add(st);

                        lst.Add(d1);
                    }
                    break;
                    
                case "wood":
                    foreach (DeckDescriber dd in available.d)
                    {
                        ResourceCard w = new WoodCard(dd.number, ResourceTypes.Wood);
                        Deck d2 = new WoodDeck("wood");
                        for (int i = 0; i < dd.number; i++)
                            d2.add(w);

                        lst.Add(d2);
                    }
                    break;
                  
                case "wheat":
                    foreach (DeckDescriber dd in available.d)
                    {
                        ResourceCard wh = new WheatCard(dd.number, ResourceTypes.Wheat);
                        Deck d3 = new WheatDeck("wheat");
                        for (int i = 0; i < dd.number; i++)
                            d3.add(wh);
                        lst.Add(d3);
                    }
                    break;
                   
              case "brick":
                    foreach (DeckDescriber dd in available.d)
                    {
                        ResourceCard b = new BrickCard(dd.number, ResourceTypes.Brick);
                        Deck d4 = new BrickDeck("brick");
                        for (int i = 0; i < dd.number; i++)
                            d4.add(b);

                        lst.Add(d4);
                    }
                  break;
                    
             case "development":
                    foreach (DeckDescriber dd in available.d)
                    {
                        if (dd.type == "soldier")
                        {
                           DevelopmentCard b = new SoldierCard("soldier");
                            Deck d5 = new SoldierDeck("soldier");
                            for (int i = 0; i < dd.number; i++)
                                d5.add(b);

                            // nrDev = nrDev + dd.number;
                            lst.Add(d5);
                        }
                       else if (dd.type == "victory")
                        {
                            DevelopmentCard b = new PointCard("point");
                            Deck d6 = new PointDeck("point");
                            for (int i = 0; i < dd.number; i++)
                                d6.add(b);

                            //nrDev = nrDev + dd.number;
                            lst.Add(d6);
                        }
                      else  if (dd.type == "road")
                        {
                            DevelopmentCard b = new RoadCard("road");
                            Deck d9 = new RoadDeck("road");
                            for (int i = 0; i < dd.number; i++)
                                d9.add(b);
                            lst.Add(d9);
                            // nrDev = nrDev + dd.number;
                        }
                        else if (dd.type == "year")
                        {
                            DevelopmentCard b = new YearCard("year");
                            Deck d7 = new YearDeck("year");
                            for (int i = 0; i < dd.number; i++)
                                d7.add(b);
                            lst.Add(d7);

                        }
                        else if (dd.type == "monopoly")
                        {
                            DevelopmentCard b = new MonopolyCard("monopoly");
                            Deck d8 = new MonopolyDeck("monopoly");
                            for (int i = 0; i < dd.number; i++)
                                d8.add(b);

                            // nrDev = nrDev + dd.number;
                            lst.Add(d8);
                        }

                    }
                    break;

                    /*
                     
                 DevelopmentCard p = new ProgressCard(available.d.Number);
                 Deck d5 = new ProgressDeck("progress");
                 for (int i = 0; i < available.d.Number; i++)
                     d5.add(p);
                 nrDev = nrDev + available.d.Number;

                 break;
             case "soldier":
                 DevelopmentCard sd = new SoldierCard(available.d.Number);
                 Deck d6 = new SoldierDeck("soldier");
                 for (int i = 0; i < available.d.Number; i++)
                     d6.add(sd);
                 nrDev = nrDev + available.d.Number;

                 break;
             case "victory":
                 DevelopmentCard v = new PointCard(available.d.Number);
                 Deck d7 = new PointDeck("point");
                 for (int i = 0; i < available.d.Number; i++)
                     d7.add(v);
                 nrDev = nrDev + available.d.Number;

                 break;
                 */

            }

        }
       return lst;
    }


}
