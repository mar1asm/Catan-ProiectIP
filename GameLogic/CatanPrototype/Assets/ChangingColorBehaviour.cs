using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColorBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToColor;


    void Start()
    {
        ChangeColorOfObjects(PlayerColor.Blue);
    }


    void ChangeColorOfObjects(PlayerColor color)
    {
        Color c;
        //Culori puse de mine acuma, pot sa fie schimbare
        switch (color)
        {
            case PlayerColor.Red: c = new Color(255, 0, 0);
                break;
            case PlayerColor.White: c = new Color(213, 219, 214);
                break;
            case PlayerColor.Orange: c = new Color(255, 142, 43);
                break;
            case PlayerColor.Blue: c = new Color(0, 0, 255);
                break;
            case PlayerColor.Brown: c = new Color(112, 62, 0);
                break;
            case PlayerColor.Green: c = new Color(0, 255, 0);
                break;       
            default: c = new Color(0, 0, 0);
                break;
        }


        foreach (GameObject objectToChange in objectsToColor)
        {
            objectToChange.GetComponent<Renderer>().material.SetColor("_Color", c);
        }

    }

}
