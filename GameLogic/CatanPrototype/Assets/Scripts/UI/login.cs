using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class LoginJson 
{
    public string username;
    public string password;
}

//Store the response from API
[Serializable]
public class LoginRespJson 
{
    public string token;
    public string expiration;
}

//Singleton class for user auth data


public class login : MonoBehaviour
{
    GameObject usernameObj, passwordObj;


    public void getusername(GameObject username)
    {
        this.usernameObj = username;
    }

    public void getpassword(GameObject password)
    {
        this.passwordObj = password;
    }

    public void verifyLogin()
    {
        string username = usernameObj.GetComponent<UnityEngine.UI.InputField>().text;
        string password = passwordObj.GetComponent<UnityEngine.UI.InputField>().text;

        if (username != "" && password != "")
        {
            StartCoroutine(postLogin(username, password));
        }
        else
        {
            Debug.Log("Empty Field(s)");
        }
    }

    IEnumerator postLogin(string username, string password)
    {
        //Preparing the POST Json Body
        LoginJson loginJson = new LoginJson();
        loginJson.username = username;
        loginJson.password = password;
        string json = JsonUtility.ToJson(loginJson);
        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(json);

        //Preparing the request
        string uri = "https://localhost:5001/api/Authenticate/login";
        UnityWebRequest request = UnityWebRequest.Post(uri, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawJson);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || 
            request.result == UnityWebRequest.Result.ProtocolError)
                Debug.Log("POST Error");
        else 
        {
            
            long status = request.responseCode;
            if (status == 200) 
            {
                //Get data from Json response
                LoginRespJson jsonData = JsonUtility.FromJson<LoginRespJson>(request.downloadHandler.text);
                //Debug.Log(jsonData.token);
                //Debug.Log(jsonData.expiration);
                UserInfo.SetToken(jsonData.token);
                UserInfo.SetExpiration(jsonData.expiration);
                UserInfo.SetUsername(username);

                SceneManager.LoadScene(sceneName:"mainMenu");
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}