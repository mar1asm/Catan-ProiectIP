using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class StartGameBehaviour : MonoBehaviour
{

    public void CreateGame() {
        StartCoroutine(CreateGameSession());
    }

    IEnumerator CreateGameSession() { 
         //Preparing the POST Json Body
        

        GameSessionStarterJSON json = new GameSessionStarterJSON();

        json.extensions.Add(0);

        string jsonString = JsonUtility.ToJson(json);

        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(jsonString);

        //Preparing the request
        string uri = "https://localhost:5001/api/GameSessions";
        UnityWebRequest request = UnityWebRequest.Post(uri, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawJson);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());

        //Send the request then wait here until it returns
        Debug.Log("inainte de request");
        yield return request.SendWebRequest();
        Debug.Log("ajung aici!");
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("POST Error");
        else 
        {
            Debug.Log("POST OK");
            long status = request.responseCode;
            Debug.Log("Statusul : " + status);
            if (status == 201) 
            {
                jsonString = request.downloadHandler.text;
                GameSessionStarterResponseJSON response = 
                    JsonUtility.FromJson<GameSessionStarterResponseJSON>(jsonString) ;
                UserInfo.SetGameSessionId(response.id);
                Debug.Log(
                    "MERGEEEE " +  response.id
                );
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
