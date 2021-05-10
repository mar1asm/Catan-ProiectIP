using System;

namespace Sprint2
{
    class Program
    {
        static void Main(string[] args)
        {
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


            Console.WriteLine(decks.getJson());
        }
    }
}
