using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
        //TODO: Update API to support password recovery
        string uri = "https://localhost:5001/api/Authenticate/register";
        WWWForm form = new WWWForm();
        form.AddField("email", email);

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
    }
}
