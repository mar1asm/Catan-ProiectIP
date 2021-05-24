using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteractionBehaviour : MonoBehaviour
{

    public ResourceTypes resourceType = ResourceTypes.Brick;
    [SerializeField]
    private TextMeshProUGUI givenToRobberText;

    [SerializeField]
    private TextMeshProUGUI availableResourcesText;

    public int nbOfResource = 0;
   
    public int nbGivenToRobber = 0;

    private ItemInteractionHolderBehaviour  holderBehaviour;
    void Awake() {
        holderBehaviour = transform.parent.gameObject.GetComponent<ItemInteractionHolderBehaviour>();
    }


    private void SetRobberText() {
        if(nbGivenToRobber == 0) {
            givenToRobberText.text = "" ;
        }
        else {
            givenToRobberText.text = nbGivenToRobber.ToString();
        }
    }

    private void SetResourcesText() {
        availableResourcesText.text = nbOfResource.ToString();
    }


    private void UpdateTexts() {
        SetRobberText();
        SetResourcesText();
    }
    public void Initialize(int nb) {
        nbGivenToRobber = 0;
        nbOfResource = nb;
        UpdateTexts();
    }

    public void GiveResourceToRobber() {
        if(nbOfResource == 0) return;
        if(holderBehaviour.nbToGiveToRobber == 0) return;
        nbOfResource--;
        nbGivenToRobber++;
        UpdateTexts();
        holderBehaviour.Substract();

    }

    public void TakeResourceFromRobber() {
        if(nbGivenToRobber == 0) return;
        nbGivenToRobber--;
        nbOfResource++;
        UpdateTexts();
        holderBehaviour.Add();
    }
}
