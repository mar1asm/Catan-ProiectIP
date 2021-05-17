using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userBaner_3players : MonoBehaviour
{

        Text name_player1, name_player2;

        
        name_player1 = GameObject.Find("name-user1").GetComponent<Text>();
        name_player2 = GameObject.Find("name-user2").GetComponent<Text>();

        IEnumerator getFriends()
        {
            //Preparing the request
            string uri = "https://localhost:5001/api/GameSessions/{id}";
            UnityWebRequest request = UnityWebRequest.Get(uri);
            request.SetRequestHeader("Authorization", "Bearer " + UserAuth.GetToken());

            //Send the request then wait here until it returns
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("GET Error");
            else
            {
                Debug.Log("GET OK");
                long status = request.responseCode;
                if (status == 200)
                {
                    string preProccesedJson = "{ \"friends\" : " + request.downloadHandler.text + " }";

                    //Get data from Json response
                    FriendsListDataJson jsonData = JsonUtility.FromJson<FriendsListDataJson>(preProccesedJson);

                    foreach (var f in jsonData.friends)
                    {
                        Friend friend = new Friend();
                        friend.id = f.id;
                        friend.userName = f.userName;
                        friend.accepted = f.accepted;
                        friendsList.friends.Add(friend);

                        int lastFriendIndex = friendsList.friends.Count - 1;
                        addFriendToUi(f.userName, lastFriendIndex);
                    }
                }
                else
                {
                    Debug.Log(request.downloadHandler.text);
                }
            }
        }
}

