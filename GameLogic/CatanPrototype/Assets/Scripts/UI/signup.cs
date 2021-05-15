using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class SignupJson 
{
    public string username;
    public string email;
    public string password;
}

public class signup : MonoBehaviour
{
    GameObject usernameObj, emailObj, passwordObj, retypePassObj;


    public void getUsername(GameObject username)
    {
        this.usernameObj = username;
    }

    public void getEmail(GameObject email)
    {
        this.emailObj = email;
    }

    public void getPass(GameObject password)
    {
        this.passwordObj = password;
    }

    public void getRePass(GameObject retypePassword)
    {
        this.retypePassObj = retypePassword;
    }

    public void verifySignup()
    {
        string username = usernameObj.GetComponent<UnityEngine.UI.InputField>().text;
        string email = emailObj.GetComponent<UnityEngine.UI.InputField>().text;
        string password = passwordObj.GetComponent<UnityEngine.UI.InputField>().text;
        string passwordAgain = retypePassObj.GetComponent<UnityEngine.UI.InputField>().text;

        if (username != "" && email != "" && password != "" && passwordAgain != "")
        {
            if (password == passwordAgain)
            {
                StartCoroutine(postSignup(username, email, password));
            }
            else
            {
                Debug.Log("Password does not match");
            }
        }
        else
        {
            Debug.Log("Empty Field(s)");
        }
    }

    IEnumerator postSignup(string username, string email, string password)
    {
        //Preparing the POST Json Body
        SignupJson signupJson = new SignupJson();
        signupJson.username = username;
        signupJson.email = email;
        signupJson.password = password;
        string json = JsonUtility.ToJson(signupJson);
        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(json);

        //Preparing the request
        string uri = "https://localhost:5001/api/Authenticate/register";
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
            Debug.Log("POST OK");
            long status = request.responseCode;
            if (status == 200) 
            {
                SceneManager.LoadScene(sceneName:"mainMenu");
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
