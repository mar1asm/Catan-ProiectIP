using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerSenderBehaviour : MonoBehaviour
{

    public static string notificationUriPath  = "https://localhost:5001/api/Authenticate/register";
    public IEnumerator Send2Server(string message) {
        byte[] rawMessage = new System.Text.UTF8Encoding().GetBytes(message);

        UnityWebRequest request = UnityWebRequest.Post(notificationUriPath, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawMessage);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || 
        request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log("GET Error");     
        else 
        {
            
        }
    }
}
