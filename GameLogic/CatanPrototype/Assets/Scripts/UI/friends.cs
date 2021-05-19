using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

[Serializable]
public class FriendsListDataJson 
{
    public List<FriendDataJson> friends;
}

[Serializable]
public class FriendDataJson 
{
    public int id;
    public string userName;
    public bool accepted;
}

public class FriendsList
{
    public List<Friend> friends = new List<Friend>();
}

public class Friend
{
    public int id;
    public string userName;
    public bool accepted;
    public GameObject friendUi;
}

public class friends : MonoBehaviour
{
    public GameObject friendPrefab, usernameObj;
    float yPosition = 6;
    FriendsList friendsList = new FriendsList();

    public void getusername(GameObject username)
    {
        this.usernameObj = username;
    }

    void Start()
    { 
        StartCoroutine(getFriends());
    }

    void Update()
    {
        searchFriend();
    }

    IEnumerator getFriends()
    {
        //Preparing the request
        string uri = "https://localhost:5001/api/user/contacts";
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

                    int lastFriendIndex = friendsList.friends.Count-1;
                    addFriendToUi(f.userName, lastFriendIndex);
                }
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }

    void clearUi() 
    {
        yPosition = 6;
        foreach (var friend in friendsList.friends)
        {
            if (friend.friendUi != null)
                Destroy(friend.friendUi);
        }
    }

    void addFriendToUi(string name, int index)
    {
        GameObject friendUi = (GameObject)Instantiate(friendPrefab, transform.GetChild(4).GetChild(0));
        friendsList.friends[index].friendUi = friendUi;

        //Setting the position
        yPosition -= 1.6f;
        friendUi.transform.localPosition = new Vector3(0, yPosition, friendUi.transform.localPosition.z);

        //Setting the name
        GameObject friendName = friendUi.transform.Find("friendName").gameObject;
        friendName.GetComponent<Text>().text = name;
    }

    void searchFriend()
    {
        string username = usernameObj.GetComponent<UnityEngine.UI.InputField>().text;

        if (username != "")
        {
            clearUi();

            for (int i = 0; i < friendsList.friends.Count; i++)
            {
                Friend friend = friendsList.friends[i];
                if (friend.userName == username)
                    addFriendToUi(friend.userName, i); 
            }
        }
        else 
        {
            for (int i = 0; i < friendsList.friends.Count; i++)
            {
                Friend friend = friendsList.friends[i];
                if (friend.friendUi == null)
                    addFriendToUi(friend.userName, i); 
            }
        }
    }
}
