using System;
using System.Collections.Generic;
using BoardExtension;

namespace BoardExtension
{
    class Program
    {
        static void Main(string[] args)
        {
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


            Manager manager = new Manager();
            manager.Board = boardExpansion;
            manager.Tiles = list;
            manager.toFile();



        }
    }
}
