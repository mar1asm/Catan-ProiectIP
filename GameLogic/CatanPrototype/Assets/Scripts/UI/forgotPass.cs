using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class ForgotPasswordJson 
{
    public string email;
}

public class forgotPass : MonoBehaviour
{
    GameObject emailObj;


    public void getEmail(GameObject email)
    {
        this.emailObj = email;
    }

    public void recoverPassword()
    {
        string email = emailObj.GetComponent<UnityEngine.UI.InputField>().text;
        if (email != "" )
        {
            StartCoroutine(postForgot(email));
        }
        else
        {
            Debug.Log("Empty Field");
        }
    }

    IEnumerator postForgot(string email)
    {   
        //Preparing the POST Json Body
        ForgotPasswordJson forgotPasswordJson = new ForgotPasswordJson();
        forgotPasswordJson.email = email;
        string json = JsonUtility.ToJson(forgotPasswordJson);
        byte[] rawJson = new System.Text.UTF8Encoding().GetBytes(json);

        //Preparing the request
        //TODO: Add API support for password recovery
        string uri = "https://localhost:5001/api/Authenticate/register";
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
                SceneManager.LoadScene(sceneName:"login");
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }
}
