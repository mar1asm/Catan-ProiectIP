using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    class DeckControlerExpansion
    {
        private List<DeckExtension> listdeck = new List<DeckExtension>();
        public string getJson()
        {
            String objectString = "{\n";
            objectString += "\"availableCard\": [\n";

            foreach(DeckExtension deck in listdeck)
            {
                objectString += deck.toString();
                objectString += ",\n";
            }
            objectString = objectString.Remove(objectString.Length - 2);
            objectString += "\n]\n}";

            return objectString;
        }

        public void addDeck(DeckExtension deck)
        {
            listdeck.Add(deck);
        }
    }


}
