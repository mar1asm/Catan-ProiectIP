using System;

namespace IPCard
{
    class Program
    {
        static void Main(string[] args)
        {
            CardExtension card = new CardExtension();
            card.Type="sheep";
            card.Number = 10;

            CardExtension card2 = new CardExtension();
            card2.Type = "wheat";
            card2.Number = 10;

            Console.WriteLine(card.toString());
            Console.WriteLine(card2.toString());
        }
    }
}
