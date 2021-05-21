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
            boardExpansion.addTile(0, 0, "forest");
            boardExpansion.addTile(0, -1, "pasture"); 
            boardExpansion.addTile(0, -2, "mountains");
            boardExpansion.addTile(0, -3, "fields");
            boardExpansion.addTile(0, 1, "sea");
            boardExpansion.addTile(0, 2, "pasture");
            boardExpansion.addTile(0, 3, "sea");

            boardExpansion.addTile(1, 0, "pasture");
            boardExpansion.addTile(1, -1, "mountains");
            boardExpansion.addTile(1, -2, "pasture");
            boardExpansion.addTile(1, -3, "fields");
            boardExpansion.addTile(1, 1, "sea");
            boardExpansion.addTile(1, 2, "sea");

            boardExpansion.addTile(2, 0, "sea");
            boardExpansion.addTile(2, 1, "goldfield");
            boardExpansion.addTile(2, -1, "field");
            boardExpansion.addTile(2, -2, "pasture");
            boardExpansion.addTile(2, -3, "hills");

            boardExpansion.addTile(3, 0, "hills");
            boardExpansion.addTile(3, -1, "sea");
            boardExpansion.addTile(3, -2, "sea");
            boardExpansion.addTile(3, -3, "sea");

            boardExpansion.addTile(-1, 0, "forest");
            boardExpansion.addTile(-1, -1, "hills");
            boardExpansion.addTile(-1, -2, "forest");
            boardExpansion.addTile(-1, 1, "sea");
            boardExpansion.addTile(-1, 2, "field"); 
            boardExpansion.addTile(-1, 3, "mountains");

            boardExpansion.addTile(-2, 0, "sea");
            boardExpansion.addTile(-2, -1, "sea");
            boardExpansion.addTile(-2, 1, "sea");
            boardExpansion.addTile(-2, 2, "goldfield");
            boardExpansion.addTile(-2, 3, "sea");

            boardExpansion.addTile(-3, 0, "hills");
            boardExpansion.addTile(-3, 1, "mountain");
            boardExpansion.addTile(-3, 2, "sea");
            boardExpansion.addTile(-3, 3, "sea");
            /* Console.WriteLine(boardExpansion.toString());*/


            TileExtension tileExtension = new TileExtension();
            tileExtension.Number=15;
            tileExtension.Type = "sea";
            TileExtension tileExtension2 = new TileExtension();
            tileExtension2.Number = 4;
            tileExtension2.Type = "fields";
            TileExtension tileExtension3 = new TileExtension();
            tileExtension3.Number = 4;
            tileExtension3.Type = "hills";
            TileExtension tileExtension4 = new TileExtension();
            tileExtension4.Number = 4;
            tileExtension4.Type = "mountains";
            TileExtension tileExtension5 = new TileExtension();
            tileExtension5.Number = 5;
            tileExtension5.Type = "pasture";
            TileExtension tileExtension6 = new TileExtension();
            tileExtension6.Number = 3;
            tileExtension6.Type = "forest";
            TileExtension tileExtension7 = new TileExtension();
            tileExtension7.Number = 2;
            tileExtension7.Type = "goldfield";
            /* Console.WriteLine(tileExtension.toString());*/

            List<TileExtension> list = new List<TileExtension>();
            list.Add(tileExtension);
            list.Add(tileExtension2); 
            list.Add(tileExtension3);
            list.Add(tileExtension4);
            list.Add(tileExtension5);
            list.Add(tileExtension6);
            list.Add(tileExtension7);


            /////////////////////////////////////////////////////////////////////DECK

            var newDeck = new DeckExtension("Resources");
            var newDeck2 = new DeckExtension("Development");
            var newDeck3 = new DeckExtension("BuidingsCosts");
            var newDeck4 = new DeckExtension("SpecialCards");

            CardExtension card = new CardExtension();
            card.Type = "brick";
            card.Number = 19;

            CardExtension card1 = new CardExtension();
            card1.Type = "lumber";
            card1.Number = 19;

            CardExtension card2 = new CardExtension();
            card2.Type = "grain";
            card2.Number = 19;

            CardExtension card3 = new CardExtension();
            card3.Type = "ore";
            card3.Number = 19;

            CardExtension card4 = new CardExtension();
            card4.Type = "wool";
            card4.Number = 19;

            CardExtension card5 = new CardExtension();
            card5.Type = "knight";
            card5.Number = 14;

            CardExtension card6 = new CardExtension();
            card6.Type = "progress";
            card6.Number = 6;

            CardExtension card7 = new CardExtension();
            card7.Type = "victory point";
            card7.Number = 5;

            CardExtension card8 = new CardExtension();
            card8.Type = "cost card";
            card8.Number = 3;

            CardExtension card9 = new CardExtension();
            card9.Type = "longest road";
            card9.Number = 1;

            CardExtension card10 = new CardExtension();
            card10.Type = "longest army";
            card10.Number = 1;






            newDeck.addCards(card);
            newDeck.addCards(card2);
            newDeck.addCards(card3);
            newDeck.addCards(card4);

            newDeck2.addCards(card5);
            newDeck2.addCards(card6);
            newDeck2.addCards(card7);

            newDeck3.addCards(card8);

            newDeck4.addCards(card9);
            newDeck4.addCards(card10);

            DeckControlerExpansion decks = new DeckControlerExpansion();
            decks.addDeck(newDeck);
            decks.addDeck(newDeck2);
            decks.addDeck(newDeck3);
            decks.addDeck(newDeck4);


           // Console.WriteLine(decks.getJson());
            ///////////////////////////////////////////////////////////////////////////////////Rules
            RuleExtension rules = new RuleExtension();
            rules.PlayerCardNumber = 7;
            rules.TotalCardNumber = 125;
            rules.HexagonNumbers = 27;
            rules.DiceValue = 2;
            rules.SecPerRound = 200;
            rules.MaxTilesBetweenRoad = 2;
            rules.MaxTileBetweenLocation = 4;
            rules.DefaultTradeRatio = 2;

            /////////////////////////////////////////////////////////////////////////////////////Conectors
            ConectorCreator conector1 = new ConectorCreator();
            conector1.Name = "boat";
            conector1.Color = "red";
            ConectorCreator conector2 = new ConectorCreator();
            conector2.Name = "boat";
            conector2.Color = "blue";
            ConectorCreator conector3 = new ConectorCreator();
            conector3.Name = "boat";
            conector3.Color = "orange";
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
