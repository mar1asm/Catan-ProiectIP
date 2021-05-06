using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class forgotPass : MonoBehaviour
{
    GameObject email;

    public void getEmail(GameObject email)
    {
        this.email = email;
    }
    public void veriftForgot()
    {
        if (email.GetComponent<UnityEngine.UI.InputField>().text != "" )
        {
            print(email.GetComponent<UnityEngine.UI.InputField>().text);
            StartCoroutine(postForgot());
            // StartCoroutine(getLogin());
            // get when is correct or not si poate trece mai departe la MainMenu
        }
        else
        {
            print("null here");
        }
    }

    public class ForgotData
    {
        // nume de parametrii din json
        public string email;
        public string status;
    }

    IEnumerator getForgot()
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
                ForgotData forgotData = JsonUtility.FromJson<ForgotData>(www.downloadHandler.text);
                print(forgotData.status);
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

    IEnumerator postForgot()
    {
        /////////////// put correct 
        string uri = "https://jsonplaceholder.typicode.com/posts";
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("email=" + email));

        UnityWebRequest www = UnityWebRequest.Post(uri, formData);
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
