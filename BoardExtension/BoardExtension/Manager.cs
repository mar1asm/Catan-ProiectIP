using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace BoardExtension
{
    public sealed class Manager//Patronu'
    {
        /*
                private RulesExpansion rules = new RulesExpansion();*/
        private BoardExpansion board = new BoardExpansion(100);
/*        private List<SettlementExpansion> settlements = new List<SettlementExpansion>();
        private List<ConnectorCreator> connectors = new List<ConnectorCreator>();
        private List<CardExpansion> cards = new List<CardExpansion>();
        private List<DeckExpansion> decks = new List<DeckExpansion>();*/
        private List<TileExtension> tiles = new List<TileExtension>();


        public Manager()
        {
        }

        private static readonly object padlock = new object();
        private static Manager instance = null;
        public static Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        instance = new Manager();
                    }
                }
                return instance;
            }
        }


        // private List<string> _TheList = new List<string>();
        // public List<string> TheList
        // {
        //     get { return _TheList; }
        //     set { _TheList = value; }
        // }

        /*   public RulesExpansion Rules
           {
               get { return rules; }
               set { rules = value; }
           }*/


        public BoardExpansion Board
        {
            get { return board; }
            set { board = value; }
            
        }


        /*public List<SettlementExpansion> Settlements
        {
            get { return settlements; }
            set { settlements = value; }
        }

        List<ConnectorCreator> Connectors
        {
            get { return connectors; }
            set { connectors = value; }
        }*/

        public List<TileExtension> Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }
        /* public List<DeckExpansion> Decks
         {
             get { return decks; }
             set { decks = value; }
         }

         public List<CardExtension> Cards
         {
             get { return cards; }
             set { cards = value; }
         }*/


        public string toStringAvailableTiles()
        {
            string output = "\"availableTiles\": [\n";

            // {
            //     "type": "sheep",
            //     "number": 4
            // }
            foreach (TileExtension currentTile in tiles)
            {
                if (output.Substring(output.Length - 1).First() == '}')
                {
                    output += ",\n";
                }

                output += currentTile.toString();
               

            }

            output += "\n]";

            return output;
        }

        public void toFile()
        {

            String name = @"C:\Users\marti\Desktop\Visual Studio\BoardExtension\output.txt";
            // scriem in fisier ce returneaza`toStringAvailableTiles`
            string output = "";
            output = "{\n";
            output += toStringAvailableTiles() + ",\n";
            output += board.toString();
            output += "\n}";
            /*File.WriteAllText("board.txt", output);*/
            Console.WriteLine(output);

            if (!File.Exists(name))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(name))
                {
                    
                    sw.Write(name, output);
                    
                }
            }
            else
            {
                File.WriteAllText(name, output);
            }

        }
    }
}