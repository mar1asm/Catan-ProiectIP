using System;
using System.Collections.Generic;
using System.Text;

namespace Extension
{
    public class RuleExtension
    {
        private int playerCardNumber;
        private bool changePlayerCardNumber;
        private int totalCardNumber;
        private bool changeTotalCardNumber;
        private int hexagonNumbers;
        private bool changeHexagonNumbers;
        private int diceValue;
        private bool changeDiceValue;
        private int secPerRound;
        private bool changeSecPerRound;
        private int maxTilesBetweenRoad;
        private bool changeMaxTilesBetweenRoad;
        private int maxTileBetweenLocation;
        private bool changeMaxTileBetweenLocation;
        private double defaultTradeRatio;
        private bool changeDefaultTradeRatio;

        public int PlayerCardNumber
        {
            get => playerCardNumber;
            set
            {
                playerCardNumber = value;
                changePlayerCardNumber = true;
            }
        }
        public int TotalCardNumber
        {
            get => totalCardNumber;
            set
            {
                totalCardNumber = value;
                changeTotalCardNumber = true;
            }
        }
        public int HexagonNumbers
        {
            get => hexagonNumbers;
            set
            {
                hexagonNumbers = value;
                changeHexagonNumbers = true;
            }
        }
        public int DiceValue
        {
            get => diceValue;
            set
            {
                diceValue = value;
                changeDiceValue = true;
            }
        }
        public int SecPerRound
        {
            get => secPerRound;
            set
            {
                secPerRound = value;
                changeSecPerRound = true;
            }
        }
        public int MaxTilesBetweenRoad
        {
            get => maxTilesBetweenRoad;
            set
            {
                maxTilesBetweenRoad = value;
                changeMaxTilesBetweenRoad = true;
            }
        }
        public int MaxTileBetweenLocation
        {
            get => maxTileBetweenLocation;
            set
            {
                maxTileBetweenLocation = value;
                changeMaxTileBetweenLocation = true;
            }
        }
        public double DefaultTradeRatio
        {
            get => defaultTradeRatio;
            set
            {
                defaultTradeRatio = value;
                changeDefaultTradeRatio = true;
            }
        }

        public string toString()
        {
            bool comma = false;
            String config = "{\n";
            config += "\"gameSettings\": [";

            if (changePlayerCardNumber)
            {
                config += "\n{\n";
                config += "\"name\": \"PlayerCardNumber\",\n";
                config += "\"number\": ";
                config += playerCardNumber;
                config += "\n}";
                comma = true;
            }
            if (changeTotalCardNumber)
            {
                if (comma) config += ",\n";
                config += "{\n";
                config += "\"name\": \"TotalCardNumber\",\n";
                config += "\"number\": ";
                config += totalCardNumber;
                config += "\n}";
                comma = true;
            }
            if (changeHexagonNumbers)
            {
                if (comma) config += ",\n";
                config += "{\n";
                config += "\"name\": \"HexagonNumbers\",\n";
                config += "\"number\": ";
                config += hexagonNumbers;
                config += "\n}";
                comma = true;
            }
            if (changeDiceValue)
            {
                if (comma) config += ",\n";
                config += "{\n";
                config += "\"name\": \"DiceValue\",\n";
                config += "\"number\": ";
                config += diceValue;
                config += "\n}";
                comma = true;
            }
            if (changeSecPerRound)
            {
                if (comma) config += ",\n";
                config += "{\n";
                config += "\"name\": \"SecPerRound\",\n";
                config += "\"number\": ";
                config += secPerRound;
                config += "\n}";
                comma = true;
            }
            if (changeMaxTilesBetweenRoad)
            {
                if (comma) config += ",\n";
                config += "{\n";
                config += "\"name\": \"MaxTilesBetweenRoad\",\n";
                config += "\"number\": ";
                config += maxTilesBetweenRoad;
                config += "\n}";
                comma = true;
            }
            if (changeMaxTileBetweenLocation)
            {
                if (comma) config += ",\n";
                config += "{\n";
                config += "\"name\": \"MaxTileBetweenLocation\",\n";
                config += "\"number\": ";
                config += maxTileBetweenLocation;
                config += "\n}";
                comma = true;
            }
            if (changeDefaultTradeRatio)
            {
                if (comma) config += ",\n";
                config += "{\n";
                config += "\"name\": \"DefaultTradeRatio\",\n";
                config += "\"number\": ";
                config += defaultTradeRatio;
                config += "\n}";
            }

            config += "\n]\n}";

            return config;
        }
    }
}
