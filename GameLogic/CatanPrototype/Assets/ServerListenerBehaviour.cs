using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerListenerBehaviour : MonoBehaviour
{
 
    public static string notificationUriPath  = "https://localhost:5001/api/Authenticate/register";
    void Start()
    {
        //StartCoroutine(ListenForComands());
    }


    public IEnumerator ListenForComands() {
        while(true) {
            UnityWebRequest request = UnityWebRequest.Get(notificationUriPath);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("GET Error");     
            else 
            {
                //vedem in ce format primi de la server
                //sa luam si sa interpretam comanda
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

}
