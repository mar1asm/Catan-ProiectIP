using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
        string uri = "https://localhost:5001/api/Authenticate/register";
        WWWForm form = new WWWForm();
        form.AddField("firstName", "Johnny");
        form.AddField("lastName", "Depp");
        form.AddField("username", username);
        form.AddField("email", email);
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
    }
}
