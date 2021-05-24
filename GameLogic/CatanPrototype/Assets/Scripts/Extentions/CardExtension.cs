using System;


namespace Extension
{
    public class CardExtension
    {
        private int number;
        private Byte[] image;
        private String type;
        private String description;


        public String Type { get => type; set => type = value; }
        public String Description { get => description; set => description = value; }

        public Byte[] Image { get => image; set => image = value; }

        public int Number { get => number; set => number = value; }

        public string toString()
        {
            String config = "{\n";
            config += "\"type\": " + "\"" + type + "\"" + ",\n";
            config += "\"number\": " + number + "\n";
            config += "}";

            return config;
        }
    }

  

}