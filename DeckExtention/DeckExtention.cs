using System;
using System.Collections.Generic;
using System.Text;

namespace DeckExtention
{
    public class DeckExtention : Creator
    {
        public String Name { get; set; }
        public List<CardExtension> CardExtentionList;
        public DeckExtention(String name)
        {
            CardExtentionList = new List<CardExtension>();
            this.Name = name;
        }

        public void addCards(CardExtension cardExtention) => CardExtentionList.Add(cardExtention);

        public string toString()
        {
            String objectString = "{\n";
            objectString += "\"name\": " + Name +",\n";
            objectString += "\"d\": \n[\n";
            foreach(var card in CardExtentionList)
            {
                objectString += card.toString() + ",\n";
            }
            objectString = objectString.Remove(objectString.Length - 2);
 
            objectString += "\n]";
            objectString += "\n}";

            return objectString;
        }
    }
}