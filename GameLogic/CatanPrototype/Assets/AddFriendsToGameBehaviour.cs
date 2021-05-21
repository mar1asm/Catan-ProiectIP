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

        Debug.Log("aici ajunge");
        string uri = "https://localhost:5001/api/user/contacts";
        UnityWebRequest request = UnityWebRequest.Get(uri);
        DownloadHandler downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());
        request.SetRequestHeader("Content-Type", "application/json");
        Debug.Log("aici ajunge");
        yield return request.SendWebRequest();
        Debug.Log("aici ajunge");
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.LogError("Eroare la determinarea prietenilor!");
        else 
        {
            Debug.Log("Coaijdmoiajdimoasjdmoajsdmioa");
            long status = request.responseCode;
            if(status == 200) {
                string preProccesedJson = "{ \"friends\" : " + request.downloadHandler.text + " }";
                FriendsListDataJson jsonData = JsonUtility.FromJson<FriendsListDataJson>(preProccesedJson);

                foreach (var friend in jsonData.friends)
                {
                    Debug.Log("HEHEHEHHEI");
                    //var gameObject = Instantiate(addFriendPrefab, Vector3.zero, Quaternion.identity, friendsHolder.transform);
                    var gameObject = Instantiate(addFriendPrefab, friendsHolder.transform);
                    gameObject.GetComponent<AddFriendButtonBehaviour>().SetName(friend.userName);
                    //fa ceva cu ele imd..
                }
            }
            else {
                Debug.LogError("AULEOOOO");
            }
        }
    }


}
