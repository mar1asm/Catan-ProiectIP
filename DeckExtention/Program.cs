using System;

namespace DeckExtention
{
    class Program
    {
        static void Main(string[] args)
        {
            var newDeck = new DeckExtention("Test");

            CardExtension card = new CardExtension();
            card.Type = "sheep";
            card.Number = 10;

            CardExtension card2 = new CardExtension();
            card2.Type = "wheat";
            card2.Number = 10;

            newDeck.addCards(card);
            newDeck.addCards(card2);

            Console.WriteLine(newDeck.toString());
        }
    }
}