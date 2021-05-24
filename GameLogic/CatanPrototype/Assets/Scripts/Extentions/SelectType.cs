using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extension;
using UnityEngine.UI;

public class SelectType : MonoBehaviour
{
    
    //public Sprite sprite;
    public Color color;
    

    public void Start()
    {
       // Sprite sprite = Resources.Load<Sprite>("Assets/Resources/UI/characters/Icon8R.png");
       Button b = this.GetComponent<Button>();
        b.image.color = color;
             
        

    }

    public void SetType(string type)
    {
        BoardExpansion.currentType = type;
        BoardExpansion.currentColor = color;

        
    }
}
