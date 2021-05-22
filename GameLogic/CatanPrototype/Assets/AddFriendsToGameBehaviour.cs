using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AddFriendsToGameBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject friendsHolder;

    [SerializeField]
    private GameObject addFriendPrefab;
    void OnEnable() {
        StartCoroutine(GetFriendsOfUser());
    }

    private IEnumerator GetFriendsOfUser() {

        
        string uri = "https://localhost:5001/api/user/contacts";
        UnityWebRequest request = UnityWebRequest.Get(uri);
        DownloadHandler downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());
        request.SetRequestHeader("Content-Type", "application/json");
        
        yield return request.SendWebRequest();
        
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError("GET ERROR - Eroare la determinarea prietenilor!");
        else 
        {
            long status = request.responseCode;
            if(status == 200) {
                string preProccesedJson = "{ \"friends\" : " + request.downloadHandler.text + " }";
                FriendsListDataJson jsonData = JsonUtility.FromJson<FriendsListDataJson>(preProccesedJson);

                foreach (var friend in jsonData.friends)
                {
                    
                    var gameObject = Instantiate(addFriendPrefab, friendsHolder.transform);
                    gameObject.GetComponent<AddFriendButtonBehaviour>().SetName(friend.userName);
                    
                }
            }
            else {
                Debug.LogError("Eroare nasoala la determinarea prietenilor");
            }
        }
    }


}
