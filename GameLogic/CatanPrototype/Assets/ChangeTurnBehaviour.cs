using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurnBehaviour : MonoBehaviour
{
    public void PassTurn() {
        GameObject serverSenderObject = GameObject.Find("ServerSender");
        serverSenderObject.GetComponent<ServerSenderBehaviour>().Send("turnNext");
        gameObject.SetActive(false);
    }
}
