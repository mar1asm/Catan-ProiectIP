using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
        string uri = "https://localhost:5001/api/Authenticate/login";
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using(UnityWebRequest request = UnityWebRequest.Post(uri, form))
        {
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
            }       
        }

        // string uri = "https://www.google.com";
        // using(UnityWebRequest request = UnityWebRequest.Get(uri))
        // {
        //     yield return request.SendWebRequest();
        //     if (request.result == UnityWebRequest.Result.ConnectionError || 
        //     request.result == UnityWebRequest.Result.ProtocolError)
        //         Debug.Log("Get Error");
        //     else 
        //     {
        //         Debug.Log("Get OK");
        //         long status = request.responseCode;
        //         if (status == 200) 
        //         {
        //             SceneManager.LoadScene(sceneName:"mainMenu");
        //         }
        //     }       
        // }
    }
}
