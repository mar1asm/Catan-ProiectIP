using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class login : MonoBehaviour
{
    GameObject username, password;
    // Start is called before the first frame update
    public void getusername(GameObject username)
    {
        this.username = username;
    }

    public void getpassword(GameObject password)
    {
        this.password = password;
    }
    public void verifyLogin()
    {
        if (username.GetComponent<UnityEngine.UI.InputField>().text!= "" && password.GetComponent<UnityEngine.UI.InputField>().text!= "")
        {
            print(username.GetComponent<UnityEngine.UI.InputField>().text + " " + password.GetComponent<UnityEngine.UI.InputField>().text);
            StartCoroutine(postLogin());
            // StartCoroutine(getLogin());
            // get when is correct or not si poate trece mai departe la MainMenu
        }
        else
        {
            print("null here");
        }
    }

    public class LoginData
    {
        // nume de parametrii din json
        public string username;
        public string password;
        public string status;
    }

    IEnumerator getLogin()
    {
        print("a intrat aci");
        string uri = "http://64.225.52.232:8921/alabala23";
        UnityWebRequest www = UnityWebRequest.Get(uri);
        yield return www.SendWebRequest();
        
        if (www.isNetworkError)
        {
            print("erorar");
        } else
        {
            try
            {
                // aici iau datele din json si vad fiecare parametru in parte
                LoginData loginData = JsonUtility.FromJson<LoginData>(www.downloadHandler.text);
                print(loginData.status);
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

    IEnumerator postLogin()
    {
        /////////////// put correct 
        string uri = "https://jsonplaceholder.typicode.com/posts";
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username="+username+"&parola="+password));

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
                //LoginData loginData = JsonUtility.FromJson<LoginData>(www.downloadHandler.text);
                print(www.downloadHandler.text);
            }
            catch (System.Exception error)
            {
                print("catch" + error.ToString());
            }
        }
    }
}
