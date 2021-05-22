using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

[Serializable]
public class UsersListDataJson 
{
    public List<UserDataJson> users;
}

[Serializable]
public class UserDataJson 
{
    public string id;
    public string userName;
    public string firstName;
    public string lastName;
    public string email;
    public string iconPath;
    public int level;
    public int role;
    public int noOfGames;
    public int noOfWonGames;
    public int timeOnPlay;
    public List<UserNotificationsJson> notifications;
    public List<UserContactsJson> contacts;
}

[Serializable]
public class UserNotificationsJson
{
    public int notificationId;
    public string text;
    public string createdAt;
    public bool read;
}

[Serializable]
public class UserContactsJson
{
    public int id;
    public string userName;
    public bool accepted;
}

public class UsersList
{
    public List<User> users = new List<User>();
}

public class User
{
    public string id;
    public string userName;
    public GameObject userUi;
}

public class SearchFriends : MonoBehaviour
{
    public GameObject searchFriendPrefab, usernameObj;
    public Button searchBtn;
    float yPosition = 6;
    UsersList usersList = new UsersList();

    public void getusername(GameObject username)
    {
        this.usernameObj = username;
    }

    void Start()
    { 
        StartCoroutine(getUsers());

        searchBtn.onClick.AddListener( () =>
        {
            searchUser();
        });
    }

    void Update() //de comentat daca da crash (posibil memory leak)
    {
        refreshUi();
    }

    IEnumerator getUsers()
    {
        //Preparing the request
        string uri = "https://localhost:5001/api/Users";
        UnityWebRequest request = UnityWebRequest.Get(uri);
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());

        //Send the request then wait here until it returns
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("GET Error");     
        else 
        {
            Debug.Log("GET USERS OK");
            long status = request.responseCode;
            if (status == 200) 
            {
                string preProccesedJson = "{ \"users\" : " + request.downloadHandler.text + " }";

                //Get data from Json response
                UsersListDataJson jsonData = JsonUtility.FromJson<UsersListDataJson>(preProccesedJson);

                foreach (var u in jsonData.users)
                {
                    if ((u.userName != UserInfo.GetUsername()) && !isFriend(u))
                    {
                        User user = new User();
                        user.id = u.id;
                        user.userName = u.userName;
                        usersList.users.Add(user);    

                        int lastFriendIndex = usersList.users.Count-1;
                        addUserToUi(u.userName, lastFriendIndex);
                    }
                }
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }

    bool isFriend(UserDataJson user)
    {
        foreach (var friend in user.contacts)
        {
            if (friend.userName == UserInfo.GetUsername())
                return true;
        }
        return false;
    }

    void addUserToUi(string name, int index)
    {
        GameObject userUi = (GameObject)Instantiate(searchFriendPrefab, transform.GetChild(2).GetChild(0));
        usersList.users[index].userUi = userUi;

        Button addBtn = userUi.transform.Find("add").GetComponentInChildren<Button>();
        addBtn.onClick.AddListener( () =>
        {
            StartCoroutine(addUser(usersList.users[index]));
        });

        //Setting the position
        yPosition -= 1.6f;
        userUi.transform.localPosition = new Vector3(0, yPosition, userUi.transform.localPosition.z);

        //Setting the name
        GameObject userName = userUi.transform.Find("userName").gameObject;
        userName.GetComponent<Text>().text = name;
    }

    void clearUi() 
    {
        yPosition = 6;
        foreach (var user in usersList.users)
        {
            if (user.userUi != null)
                Destroy(user.userUi);
        }
    }

    void refreshUi()
    {
        for (int i = 0; i < usersList.users.Count; i++)
        {
            User user = usersList.users[i];
            if (user.userUi == null)
            {
                addUserToUi(user.userName, i); 
            }
        }
    }

    void searchUser()
    {
        string username = usernameObj.GetComponent<UnityEngine.UI.InputField>().text;

        if (username != "")
        {
            clearUi();

            for (int i = 0; i < usersList.users.Count; i++)
            {
                User user = usersList.users[i];
                if (user.userName == username)
                    addUserToUi(user.userName, i); 
            }
        }
        else
        {
            refreshUi();
        }
    }

    IEnumerator addUser(User user)
    {
        //Preparing the POST Json Body
        string json = "{\"userName\": \"" + user.userName + "\"}";
        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(json);

        //Preparing the request
        string uri = "https://localhost:5001/api/user/contacts";
        UnityWebRequest request = UnityWebRequest.Post(uri, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawJson);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());

        //Send the request then wait here until it returns
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("POST Error");
        else 
        {
            Debug.Log("POST OK");
            long status = request.responseCode;
            if (status == 200 || status == 201) 
            {
                clearUi();
                usersList.users.Remove(user);
                // refreshUi();
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
