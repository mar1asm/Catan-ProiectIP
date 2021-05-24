using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmResourcesBehaviour : MonoBehaviour
{
    
    [SerializeField]
    private ItemInteractionHolderBehaviour holderBehaviour;

    [SerializeField]
    private RobberDisplayBehaviour robberDisplay;

    public void CofirmRobberResources() {
        if(holderBehaviour.nbToGiveToRobber != 0) return;

        robberDisplay.SetUserGaveResources();
    }
}
