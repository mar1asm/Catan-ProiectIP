using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AddFriendButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    
    private TextMeshProUGUI text;

    public void SetName(string name) {
        text.text = name;
    }

    public void SendInvitation() {

        StartCoroutine(SendInvitationToUser(text.text));
        GameObject  friendsAddedDisplay = GameObject.Find("friendsAddedDisplay");
        gameObject.transform.parent = friendsAddedDisplay.transform;
        Button button = gameObject.GetComponent<Button>();
        Destroy(button);
    }

    IEnumerator SendInvitationToUser(string username) {

        AddUserToGameJson json =  new AddUserToGameJson();
        json.username = username;

        string jsonString = JsonUtility.ToJson(json);
        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(jsonString);

        string uri = "https://localhost:5001/api/GameSessions/"+ UserInfo.GetGameSessionId() +"/user";
        UnityWebRequest request = UnityWebRequest.Post(uri, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawJson);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());
        request.SetRequestHeader("Content-Type", "application/json");


        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError("POST ERROR - Eroare la trimiterea invitatiei de joc");
        else 
        {
            long status = request.responseCode;
            if(status == 200 || status == 201) {
                Debug.Log("L-am adaugat yupi");
            } 
            else {
                Debug.LogError("Eroare nasoala: " + request.downloadHandler.text);
            }
        }
    }
}

[SerializeField]
public class AddUserToGameJson {
    public string username;
}
