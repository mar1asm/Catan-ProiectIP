using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerListenerBehaviour : MonoBehaviour
{
 
    public static string notificationUriPath  = "https://localhost:5001/api/Notifications";
    void Start()
    {
        StartCoroutine(ListenForComands());
    }


    public IEnumerator ListenForComands() {
        while(true) {
            UnityWebRequest request = UnityWebRequest.Get(notificationUriPath);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("GET ERROR -  Eroare la ascultare");     
            else 
            {
                long status = request.responseCode;
                if(status == 200 || status == 201) {
                    string preProccesedJson = "{ \"notifications\" : " + request.downloadHandler.text + " }";
                    var notifications = JsonUtility.FromJson<NotificationListJson>(preProccesedJson);

                    Debug.Log("AM primit " + notifications.notifications.Count + " notificari");
                    foreach (var notification in notifications.notifications)
                    {
                        Debug.Log(notification.notificationId + ": " + notification.text +  " " + notification.read);
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}




[System.Serializable]
public class NotificationListJson {
    public List<NotificationJson> notifications;
}

[System.Serializable]
public class NotificationJson {
    public int notificationId;
    public string text;
    public string createdAt;

    public bool read;

}
