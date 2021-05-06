using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class updateAccount : MonoBehaviour
{
    GameObject username, email, newPassword, oldPassword;
    // Start is called before the first frame update
    public void getUsername(GameObject username)
    {
        this.username = username;
    }

    public void getEmail(GameObject email)
    {
        this.email = email;
    }
    public void getOldPass(GameObject oldPassword)
    {
        this.oldPassword = oldPassword;
    }
    public void getNewPass(GameObject newPassword)
    {
        this.newPassword = newPassword;
    }

    
    public void verifyUpdate()
    {
        if (username.GetComponent<UnityEngine.UI.InputField>().text != "" &&
            email.GetComponent<UnityEngine.UI.InputField>().text != "" &&
            newPassword.GetComponent<UnityEngine.UI.InputField>().text != "" &&
            oldPassword.GetComponent<UnityEngine.UI.InputField>().text != "")
        {
            print(username.GetComponent<UnityEngine.UI.InputField>().text + " " +
                email.GetComponent<UnityEngine.UI.InputField>().text + " " +
                oldPassword.GetComponent<UnityEngine.UI.InputField>().text + " " +
                newPassword.GetComponent<UnityEngine.UI.InputField>().text);
            StartCoroutine(postUpdate());
            // StartCoroutine(getUpdate());
            // get when is correct or not si poate trece mai departe la MainMenu
        }
        else
        {
            print("null here");
        }
    }

    public class UpdateData
    {
        // nume de parametrii din json
        public string username;
        public string email;
        public string newPassword;
        public string oldPassword;
        public string status;
    }

    IEnumerator getUpdate()
    {
        print("a intrat aci");
        string uri = "http://64.225.52.232:8921/alabala23";
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            print("erorar");
        }
        else
        {
            try
            {
                // aici iau datele din json si vad fiecare parametru in parte
                UpdateData updateData = JsonUtility.FromJson<UpdateData>(www.downloadHandler.text);
                print(updateData.status);
                // aici verifica daca statusul este ok si trece la mainmenu
                //SceneManager.LoadScene("mainMenu");
                // daca nu, afiseaza ceva mesaj
            }
            catch
            {
                print("catch");
            }
        }

    }

    IEnumerator postUpdate()
    {
        /////////////// put correct 
        string uri = "https://jsonplaceholder.typicode.com/posts";
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username=" + username + "&email=" + email +
            "&newPassword=" + newPassword + "&oldPassword=" + oldPassword));

        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            print("eroare");
        }
        else
        {
            try
            {
                // aici iau datele din json si vad fiecare parametru in parte
                //UpdateData UpdateData = JsonUtility.FromJson<UpdateData>(www.downloadHandler.text);
                print(www.downloadHandler.text);
            }
            catch (System.Exception error)
            {
                print("catch" + error.ToString());
            }
        }
    }
}
