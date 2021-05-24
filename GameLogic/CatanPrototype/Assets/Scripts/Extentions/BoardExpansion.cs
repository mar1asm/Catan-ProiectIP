using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Extension 
{
    public class BoardExpansion : MonoBehaviour, Creator 
    {
        private  List<Tuple<Tuple<int, int>, string>> tilelist;
        public int capacity;
        public static string currentType;
        public static Color currentColor = Color.white;


        public BoardExpansion()
        {
            tilelist = new List<Tuple<Tuple<int, int>, string>>(this.capacity);
        }

        public BoardExpansion getTileList()
        {
            return this;
        }


        public void addTile(TileExtension tile1)
        {
            Tuple<int, int> coord = new Tuple<int, int>(tile1.q, tile1.r);
            Tuple<Tuple<int, int>, string> tile = new Tuple<Tuple<int, int>, string>(coord,tile1.Type);

            tilelist.Add(tile);
        }

        

        public string toString()
        {   
            
            String config = " \"board\" :[\n";
           


            foreach(Tuple<Tuple<int, int>, string> test in tilelist)
            {
             
                if (config.Substring(config.Length - 1).First()== '}')
                {
                    config += ",\n";
                }
                
                config = config + "     {\n";

                config = config + "\t\"q\": " + test.Item1.Item1 + ",\n";
                config = config + "\t\"r\": " + test.Item1.Item2 + ",\n";
                config = config + "\t\"type\": " + "\"" + test.Item2 + "\"" + "\n";

                config = config + "     }";

            }

            config = config + "\t]\n"; /*+ "}";*/
            
            return config;
        }
    }

    
}
