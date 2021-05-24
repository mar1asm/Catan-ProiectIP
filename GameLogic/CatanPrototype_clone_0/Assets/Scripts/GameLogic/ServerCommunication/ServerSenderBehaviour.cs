using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerSenderBehaviour : MonoBehaviour
{

    public static string notificationUriPath  = "https://localhost:5001/api/Notifications/session";


    public void Send(string message) {
        StartCoroutine(Send2Server(message));
    }
    public IEnumerator Send2Server(string message) {

        SendNotificationJSON sendNotification = new SendNotificationJSON();

        sendNotification.gameSessionId = UserInfo.GetGameSessionId();
        sendNotification.text = message;

        string jsonString = JsonUtility.ToJson(sendNotification);

        byte[] rawMessage = new System.Text.UTF8Encoding().GetBytes(jsonString);

        UnityWebRequest request = UnityWebRequest.Post(notificationUriPath, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawMessage);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || 
        request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("POST ERROR - Eroare la trimirea de notificare");     
        else 
        {
            long status = request.responseCode;
            if(status == 200) {
                Debug.LogWarning("am trimis catre server!");
            }
            else {
                Debug.LogError("EROARE NASPA LA TRIMITEREA CATRE SERVER");
            }
        }
    }
}


[System.Serializable]
public class SendNotificationJSON {
    public int gameSessionId;
    public string text;
}
