using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
        string uri = "https://localhost:5001/api/Authenticate/change";
        WWWForm form = new WWWForm();
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
