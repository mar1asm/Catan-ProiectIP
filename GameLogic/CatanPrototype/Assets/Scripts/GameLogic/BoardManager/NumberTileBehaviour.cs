using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTileBehaviour : MonoBehaviour
{
    private int _number;
    public int number
    {
        get
        {
            return _number;
        }

        set
        {
            _number = value;
            //Aici trebuie sa facem schimbarea 
            //Pentru fiecare numar, ce Tile sa afisam
            //Pana nu primim de la UI acele cerculete, o sa ne prefacem ca sunt ele acolo.. 

           // transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().text = _number.ToString();
        }
    }

    private void Update()
    {
        transform.position = new Vector3(0, 0.5f, 0);
    }
}
