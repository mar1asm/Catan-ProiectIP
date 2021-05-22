using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerListenerBehaviour : MonoBehaviour
{
 
    [SerializeField]
    private CommandInterpreterBehaviour commandInterpreter;
    public static string notificationUriPath  = "https://localhost:5001/api/Notifications/user";
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

                   
                    //fac asta pentru optimizare
                    List<Coroutine> coroutines  = new List<Coroutine>();
                    foreach (var notification in notifications.notifications)
                    {
                        coroutines.Add(StartCoroutine(removeNotification(notification.notificationId)));;
                        if(commandInterpreter != null) {
                            commandInterpreter.InterpretCommand(notification.text);
                        }
                        
                    }
                    //dupa asteptam sa le stearga pe toate
                    foreach (var coroutine in coroutines)
                    {
                        yield return coroutine;
                    }  
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator removeNotification(int notificationId) {
        RemoveNotificationJson json = new RemoveNotificationJson();
        json.id = notificationId;

        string jsonString = JsonUtility.ToJson(json);

        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(jsonString);


        UnityWebRequest request = UnityWebRequest.Put(notificationUriPath, "PUT");
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(rawJson);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());

        yield return request.SendWebRequest();
         if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("PUT ERROR -  Eroare la stergerea notificarii");     
        else 
        {
            long status = request.responseCode;
            if(status == 200 || status == 201) {
                Debug.Log("Am sters notificarea!");
            } else {
                Debug.Log("Ceva naspa la stergerea notificarii...");
            }
        }

    }
}


[System.Serializable]
public class RemoveNotificationJson {
    public int id;
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
