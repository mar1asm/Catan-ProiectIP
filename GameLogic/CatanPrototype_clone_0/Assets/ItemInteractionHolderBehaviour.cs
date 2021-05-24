using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteractionHolderBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private TextMeshProUGUI giveResourcesText;


    [SerializeField]
    public int nbToGiveToRobber;


    void Start()
    {

    }


    public void Substract() {
        if(nbToGiveToRobber == 0) return;
        nbToGiveToRobber--;
        SetResourcesToGiveText();
    }


    public void Add() {
        nbToGiveToRobber++;
        SetResourcesToGiveText();
    }
    public void SetResourcesToGiveText() {
        giveResourcesText.text = nbToGiveToRobber.ToString();
        if(nbToGiveToRobber != 0) {
            giveResourcesText.color = Color.red;
        }
        else
        {
            giveResourcesText.color = Color.green;
        }
    }
    

    public void SetDisplay(Player p, int nbToGive) {
        nbToGiveToRobber = nbToGive;
        SetResourcesToGiveText();
        var resources = p.GetAvailableResources();

        foreach (Transform childTransform in transform)
        {
            var itemInteraction = childTransform.gameObject.GetComponent<ItemInteractionBehaviour>();

            itemInteraction.Initialize(0);

            if(resources.ContainsKey(itemInteraction.resourceType)) {
                itemInteraction.Initialize(resources[itemInteraction.resourceType]);
            }

        } 
    }

    public List<ResourceTypes> GetResourcesToBeRemoved() {
        List<ResourceTypes> toReturn = new List<ResourceTypes>();

        foreach (Transform childTransform in transform)
        {
            var itemInteraction = childTransform.gameObject.GetComponent<ItemInteractionBehaviour>();

            for(int i = 0 ; i < itemInteraction.nbGivenToRobber; ++i) {
                toReturn.Add(itemInteraction.resourceType);
            }
        }

        return toReturn;
    }
}
