using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class ResetPasswordJson 
{
    public string oldPassword;
    public string newPassword;
}

public class resetPassword : MonoBehaviour
{
    
    GameObject oldPasswordObj, newPasswordObj;

    public void getOldPass(GameObject oldPassword)
    {
        this.oldPasswordObj = oldPassword;
    }

    public void getNewPass(GameObject newPassword)
    {
        this.newPasswordObj = newPassword;
    }

    public void resetPass()
    {
        string oldPassword = oldPasswordObj.GetComponent<UnityEngine.UI.InputField>().text;
        string newPassword = newPasswordObj.GetComponent<UnityEngine.UI.InputField>().text;

        if (oldPassword != "" && newPassword != "")
        {
            StartCoroutine(postUpdate(oldPassword, newPassword));
        }
        else
        {
            Debug.Log("Empty Field(s)");
        }
    }

    IEnumerator postUpdate(string oldPassword, string newPassword)
    {
        //Preparing the POST Json Body
        ResetPasswordJson resetPasswordJson = new ResetPasswordJson();
        resetPasswordJson.oldPassword = oldPassword;
        resetPasswordJson.newPassword = newPassword;
        string json = JsonUtility.ToJson(resetPasswordJson);
        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(json);

        //Preparing the request
        string uri = "https://localhost:5001/api/Authenticate/change";
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
