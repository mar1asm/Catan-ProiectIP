using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


namespace Extension
{
   
    public sealed class Manager//Patronu'
    {
        private String strFilePath;
        private RuleExtension rules;
        private BoardExpansion board;
        private DeckControlerExpansion deck;
        private List<ConectorCreator> conectors = new List<ConectorCreator>();
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


        public RuleExtension Rules
        {
            get { return rules; }
            set { rules = value; }
        }


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
        */

        public List<ConectorCreator> Conectors
        {
            get { return conectors; }
            set { conectors = value; }
        }

        public List<TileExtension> Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }
        public DeckControlerExpansion Deck
        {
            get { return deck; }
            set { deck = value; }
        }


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

            strFilePath = Directory.GetCurrentDirectory();
            strFilePath = Directory.GetParent(strFilePath).ToString();
            strFilePath = Directory.GetParent(strFilePath).ToString();
            strFilePath = Directory.GetParent(strFilePath).ToString();
            strFilePath += "\\JsonExtension";


            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }

            toFileBoard();
            toFileDeck();
            toFileRules();
            toFileConectors();
            
        }
    
        public void toFileBoard()
    {

        String name = strFilePath + "\\boardJson.json";
        // scriem in fisier ce returneaza`toStringAvailableTiles`
        string output = "";
        output = "{\n";
        output += toStringAvailableTiles() + ",\n";
        output += board.toString();
        output += "\n}";

        if (!File.Exists(name))
        {
                // Create a file to write to.
            StreamWriter sw = File.CreateText(name);
                sw.Write(output);
                sw.Flush();
               
        }
        else
        {
            File.WriteAllText(name, output);
        }

    }
        public void toFileDeck() {
            String name = strFilePath + "\\deckJson.json";


            if (!File.Exists(name))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(name))
                {
                    sw.Write(deck.getJson());

                }
            }
            else
            {
                File.WriteAllText(name, deck.getJson());
            }

        }
        public void toFileRules()
        {
            String name = strFilePath + "\\rulesJson.json";


            if (!File.Exists(name))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(name))
                {

                    sw.Write(rules.toString());

                }
            }
            else
            {
                File.WriteAllText(name, rules.toString());
            }

        }
        public void toFileConectors()
        {
            String name = strFilePath + "\\conectorsJson.json";

            string output = "\"conectors\" : [ \n";

            foreach(var conector in conectors)
            {
                output += conector.toString() + ",\n";

            }
            output = output.Remove(output.Length - 2, 2);
            output += "\n]";

            if (!File.Exists(name))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(name))
                {

                    sw.Write(output);

                }
            }
            else
            {
                File.WriteAllText(name, output);
            }

        }
    }
}