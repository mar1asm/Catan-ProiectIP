using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Extension
{
    public class BoardExpansion : Creator
    {
        private  List<Tuple<Tuple<int, int>, string>> tilelist;


        public BoardExpansion(int capacity )
        {
            tilelist = new List<Tuple<Tuple<int, int>, string>>(capacity);
        }

        public BoardExpansion getTileList()
        {
            return this;
        }


        public void addTile(int q, int r, string type)
        {
            Tuple<int, int> coord = new Tuple<int, int>(q, r);
            Tuple<Tuple<int, int>, string> tile = new Tuple<Tuple<int, int>, string>(coord,type);
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
                config = config + "\t\"type\": " + test.Item2 + "\n";

                config = config + "     }";

            }

            config = config + "\t]\n"; /*+ "}";*/
            return config;
        }
    }

    
}
