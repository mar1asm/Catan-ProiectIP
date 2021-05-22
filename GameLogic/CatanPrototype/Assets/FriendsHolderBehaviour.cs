using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsHolderBehaviour : MonoBehaviour
{
    
    public void AddFriendHolder(string name) {
        foreach (Transform child in transform)
        {
            
            if(!child.gameObject.activeSelf) {
                child.gameObject.SetActive(true);
                //aici o sa fie cod ca sa pun numele sau
                return;
            }
        }
    }
}
