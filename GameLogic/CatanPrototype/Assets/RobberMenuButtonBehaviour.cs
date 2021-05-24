using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobberMenuButtonBehaviour : MonoBehaviour
{
    
    public void TakeItem() {
        transform.parent.gameObject.GetComponent<ItemInteractionBehaviour>().GiveResourceToRobber();
    }
}
