using System;
using System.Collections.Generic;
using System.Text;

namespace Extension
{
    public class ConectorCreator : Creator
    {
        private String color;
        private String name;

        public String Color { get => color; set => color = value; }
        public String Name { get => name; set => name = value; }

        public string toString()
        {
            string result = "{\n";
            result += "\"name\" : \"" + name + "\"\n";
            result += "\"color\" : \"" + color + "\"\n";
            result += "}";

                return result;

        } 
    }



}
