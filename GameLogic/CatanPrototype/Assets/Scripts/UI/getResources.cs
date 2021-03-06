using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getResources : MonoBehaviour 
{
    Text nmr_wood, nmr_rock, nmr_brick, nmr_sheep, nmr_wheat;

    [SerializeField]
    private PlayerManager playerManager;


    
    public void updateDisplay()
    {
        nmr_wood = GameObject.Find("nmr-wood").GetComponent<Text>();
        nmr_rock = GameObject.Find("nmr-rock").GetComponent<Text>();
        nmr_brick = GameObject.Find("nmr-brick").GetComponent<Text>();
        nmr_sheep = GameObject.Find("nmr-sheep").GetComponent<Text>();
        nmr_wheat = GameObject.Find("nmr-wheat").GetComponent<Text>();



        Player playerClient = playerManager.clientPlayer;

        var resources = playerClient.GetAvailableResources();

        if (resources.ContainsKey(ResourceTypes.Wood))
        {
            nmr_wood.text  = resources[ResourceTypes.Wood].ToString();
        } 
        else
        {
            nmr_wood.text = "0";
        }



        if(resources.ContainsKey(ResourceTypes.Stone)) {
            nmr_rock.text  = resources[ResourceTypes.Stone].ToString();
        }
        else
        {
            nmr_rock.text = "0";
        }

        if (resources.ContainsKey(ResourceTypes.Brick)) {
            nmr_brick.text  = resources[ResourceTypes.Brick].ToString();
        }
        else
        {
            nmr_brick.text = "0";
        }

        if (resources.ContainsKey(ResourceTypes.Sheep)) {
            nmr_sheep.text  = resources[ResourceTypes.Sheep].ToString();
        }
        else
        {
            nmr_sheep.text = "0";
        }

        if (resources.ContainsKey(ResourceTypes.Wheat)) {
            nmr_wheat.text  = resources[ResourceTypes.Wheat].ToString();
        }
        else
        {
            nmr_wheat.text = "0";
        }

        // Functia display gaseste toate obiectele nmr_wood,nmr_rock,nmr_brick,nmr_sheep,nmr_wheat care reprezinta cele
        //5 resurse, si le afiseaza. Valorile trebuie sa fie returnate in functia care se ocupa cu resursele fiecarui player. 
    }
}
