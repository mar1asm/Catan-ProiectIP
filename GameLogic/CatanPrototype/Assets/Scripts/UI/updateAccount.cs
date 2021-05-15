using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class UpdateAccountJson 
{
    // public string username;
    // public string email;
    public string oldPassword;
    public string newPassword;
}

public class updateAccount : MonoBehaviour
{
    GameObject usernameObj, emailObj, oldPasswordObj, newPasswordObj;

    public void getUsername(GameObject username)
    {
        this.usernameObj = username;
    }

    public void getEmail(GameObject email)
    {
        this.emailObj = email;
    }

    public void getOldPass(GameObject oldPassword)
    {
        this.oldPasswordObj = oldPassword;
    }

    public void getNewPass(GameObject newPassword)
    {
        this.newPasswordObj = newPassword;
    }

    public void verifyUpdate()
    {
        string username = usernameObj.GetComponent<UnityEngine.UI.InputField>().text;
        string email = emailObj.GetComponent<UnityEngine.UI.InputField>().text;
        string oldPassword = oldPasswordObj.GetComponent<UnityEngine.UI.InputField>().text;
        string newPassword = newPasswordObj.GetComponent<UnityEngine.UI.InputField>().text;

        if (username != "" && email != "" && oldPassword != "" && newPassword != "")
        {
            StartCoroutine(postUpdate(username, email, oldPassword, newPassword));
        }
        else
        {
            Debug.Log("Empty Field(s)");
        }
    }

    IEnumerator postUpdate(string username, string email, string oldPassword, string newPassword)
    {
        //Preparing the POST Json Body
        UpdateAccountJson updateAccountJson = new UpdateAccountJson();
        // updateAccountJson.username = username;
        // updateAccountJson.email = email;
        updateAccountJson.oldPassword = oldPassword;
        updateAccountJson.newPassword = newPassword;
        string json = JsonUtility.ToJson(updateAccountJson);
        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(json);

        //Preparing the request
        //TODO: Add API support for updateAccount
        string uri = "https://localhost:5001/api/Authenticate/change";
        UnityWebRequest request = UnityWebRequest.Post(uri, "POST");
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawJson);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", UserAuth.GetToken());

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
