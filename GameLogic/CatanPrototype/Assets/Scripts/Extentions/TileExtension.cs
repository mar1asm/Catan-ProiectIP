using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Extension
{
    public class TileExtension : MonoBehaviour, Creator
    {
        public Color color;
        private int resourceId;
        public int ResourceId { get => resourceId; set => resourceId = value; }

        private Boolean isResourceAvailable;
        public Boolean IsResourceAvailable
        {
            get => isResourceAvailable;
            set => isResourceAvailable = value;
        }

        public int q, r;
        public int Q { get => q ; set => q = value; }
        public int R { get => r; set => r = value; }

        public String type;
        public String Type { get => type; set => type = value; }

        public void SetType()
        {
            this.type = BoardExpansion.currentType;
            print(type);
            Button b = this.GetComponent<Button>();
            b.image.color = BoardExpansion.currentColor ;

        }

        private Byte[] image;
        public Byte[] Image { get => image; set => image = value; }

        private int number;
        public int Number { get => number; set => number = value; }

        public string toString()
        {
            String config = "{\n";
            config += "\"type\": \"" + type + "\",\n";
            config += "\"number\": " + number + "\n";
            config += "}";

            return config;
        }
    }
}