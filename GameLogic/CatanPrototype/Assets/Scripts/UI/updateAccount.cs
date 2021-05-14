using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
        string uri = "https://localhost:5001/api/Authenticate/change";
        WWWForm form = new WWWForm();
        //TODO: update API to support updateAccount
        // form.AddField("username", username);
        // form.AddField("email", email);
        form.AddField("oldPassword", oldPassword);
        form.AddField("newPassword", newPassword);


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
