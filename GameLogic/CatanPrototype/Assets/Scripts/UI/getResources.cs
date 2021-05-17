using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getResources : MonoBehaviour{
    Text nmr_wood, nmr_rock, nmr_brick, nmr_sheep, nmr_wheat;

    public void display()
    {
        nmr_wood = GameObject.Find("nmr-wood").GetComponent<Text>();
        nmr_rock = GameObject.Find("nmr-rock").GetComponent<Text>();
        nmr_brick = GameObject.Find("nmr-brick").GetComponent<Text>();
        nmr_sheep = GameObject.Find("nmr-sheep").GetComponent<Text>();
        nmr_wheat = GameObject.Find("nmr-wheat").GetComponent<Text>();


        print(nmr_brick.text);
        print(nmr_rock.text);
        print(nmr_sheep.text);
        print(nmr_wheat.text);
        print(nmr_wood.text);
        // Functia display gaseste toate obiectele nmr_wood,nmr_rock,nmr_brick,nmr_sheep,nmr_wheat care reprezinta cele
        //5 resurse, si le afiseaza. Valorile trebuie sa fie returnate in functia care se ocupa cu resursele fiecarui player. 
    }
}
