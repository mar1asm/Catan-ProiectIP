using System;
using System.Collections.Generic;
using System.Text;

namespace Extension
{
    public class TileExtension : Creator
    {
        private int resourceId;
        public int ResourceId { get => resourceId; set => resourceId = value; }

        private Boolean isResourceAvailable;
        public Boolean IsResourceAvailable
        {
            get => isResourceAvailable;
            set => isResourceAvailable = value;
        }

        private String type;
        public String Type { get => type; set => type = value; }

        private Byte[] image;
        public Byte[] Image { get => image; set => image = value; }

        private int number;
        public int Number { get => number; set => number = value; }

        public string toString()
        {
            String config = "{\n";
            config += "type: \"" + type + "\",\n";
            config += "number: " + number + "\n";
            config += "}";

            return config;
        }
    }
}