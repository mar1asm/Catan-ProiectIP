using System;
using System.Collections.Generic;
using System.Data;

namespace Extension
{
    class Program
    {
        static void Main(string[] args)
        {
            /////////////////////////////////////////////////////////////////////////////////////Board
            BoardExpansion boardExpansion = new BoardExpansion(100);
            boardExpansion.addTile(0, 0, "random");
            boardExpansion.addTile(0, -1, "random"); 
            boardExpansion.addTile(1, -1, "random");
            boardExpansion.addTile(1, 0, "random");
            boardExpansion.addTile(0, 1, "random");
            boardExpansion.addTile(-100, 50, "random");
           /* Console.WriteLine(boardExpansion.toString());*/


            TileExtension tileExtension = new TileExtension();
            tileExtension.Number=5;
            tileExtension.Type = "idk";
            TileExtension tileExtension2 = new TileExtension();
            tileExtension2.Number = 14;
            tileExtension2.Type = "asdfasdf";
           /* Console.WriteLine(tileExtension.toString());*/

            List<TileExtension> list = new List<TileExtension>();
            list.Add(tileExtension);
            list.Add(tileExtension2);


            /////////////////////////////////////////////////////////////////////DECK

            var newDeck = new DeckExtension("Sheep");
            var newDeck2 = new DeckExtension("Development");

            CardExtension card = new CardExtension();
            card.Type = "sheep";
            card.Number = 19;

            CardExtension card2 = new CardExtension();
            card2.Type = "soldier";
            card2.Number = 14;

            CardExtension card3 = new CardExtension();
            card3.Type = "victory";
            card3.Number = 5;

            CardExtension card4 = new CardExtension();
            card4.Type = "road";
            card4.Number = 2;

            CardExtension card5 = new CardExtension();
            card5.Type = "year";
            card5.Number = 2;

            CardExtension card6 = new CardExtension();
            card6.Type = "monopoly";
            card6.Number = 2;

            newDeck.addCards(card);
            newDeck2.addCards(card2);
            newDeck2.addCards(card3);
            newDeck2.addCards(card4);
            newDeck2.addCards(card5);
            newDeck2.addCards(card6);

            DeckControlerExpansion decks = new DeckControlerExpansion();
            decks.addDeck(newDeck);
            decks.addDeck(newDeck2);


           // Console.WriteLine(decks.getJson());
            ///////////////////////////////////////////////////////////////////////////////////Rules
            RuleExtension rules = new RuleExtension();
            rules.PlayerCardNumber = 30;
            rules.TotalCardNumber = 100;
            rules.HexagonNumbers = 15;
            rules.DiceValue = 3;
            rules.SecPerRound = 200;
            rules.MaxTilesBetweenRoad = 3;
            rules.MaxTileBetweenLocation = 4;
            rules.DefaultTradeRatio = 2;

            /////////////////////////////////////////////////////////////////////////////////////Conectors
            ConectorCreator conector1 = new ConectorCreator();
            conector1.Name = "boat";
            conector1.Color = "red";
            ConectorCreator conector2 = new ConectorCreator();
            conector2.Name = "boat";
            conector2.Color = "black";
            ConectorCreator conector3 = new ConectorCreator();
            conector3.Name = "boat";
            conector3.Color = "pink";
            ConectorCreator conector4 = new ConectorCreator();
            conector4.Name = "road";
            conector4.Color = "green";
            List <ConectorCreator> conectors= new List<ConectorCreator>();
            conectors.Add(conector1);
            conectors.Add(conector2);
            conectors.Add(conector3);
            conectors.Add(conector4);



            ////////////////////////////////////////////////////////////////////////////////////Manager
            Manager manager = new Manager();
            manager.Board = boardExpansion;
            manager.Tiles = list;
            manager.Deck = decks;
            manager.Rules = rules;
            manager.Conectors = conectors;
            manager.toFile();
        }
    }
}
