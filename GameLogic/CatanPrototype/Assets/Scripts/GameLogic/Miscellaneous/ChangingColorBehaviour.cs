using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingColorBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToColor;


    void Start()
    {
        
    }


    public void UpdateColor(Player p)
    {
        ChangeColorOfObjects(p.color);
    }

    public void UpdateColor(PlayerColor color)
    {
        ChangeColorOfObjects(color);
    }


    /// <summary>
    /// Functie de folosit ca sa obtii culoarea cu valorile RGB
    /// In unity valorile nu sunt intre 0-255, sunt intre 0-1 (nu intrebati de ce)
    /// Asa ca trebuie sa fie convertite
    /// </summary>
    /// <param name="r"></param>
    /// <param name="g"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private Color GetColor(float r, float g, float b)
    {
        return new Color(r / 255f, g / 255f, b / 255f);
    }


    private void ChangeColorOfObjects(PlayerColor color)
    {
        Color c;
        //Culori puse de mine acuma, pot sa fie schimbare
        switch (color)
        {
            case PlayerColor.Red: c = GetColor(255, 0, 0);
                break;
            case PlayerColor.White: c = GetColor(213, 219, 214);
                break;
            case PlayerColor.Orange: c = GetColor(255, 142, 43);
                break;
            case PlayerColor.Blue: c = GetColor(0, 0, 255);
                break;
            case PlayerColor.Brown: c = GetColor(128, 66, 0);
                break;
            case PlayerColor.Green: c = GetColor(0, 82, 0);
                break;       
            default: c = GetColor(0, 0, 0);
                break;
        }


        foreach (GameObject objectToChange in objectsToColor)
        {
            objectToChange.GetComponent<Renderer>().material.SetColor("_Color", c);
        }

    }

}
