using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class signup : MonoBehaviour
{
    GameObject username, email, password, retypePass;
    // Start is called before the first frame update
    public void getUsername(GameObject username)
    {
        this.username = username;
    }

    public void getEmail(GameObject email)
    {
        this.email = email;
    }

    public void getPass(GameObject password)
    {
        this.password = password;
    }

    public void getRePass(GameObject retypePassword)
    {
        this.retypePass = retypePassword;
    }
    public void verifySignup()
    {
        if (username.GetComponent<UnityEngine.UI.InputField>().text != "" && 
            email.GetComponent<UnityEngine.UI.InputField>().text != "" &&
            password.GetComponent<UnityEngine.UI.InputField>().text != "" &&
            retypePass.GetComponent<UnityEngine.UI.InputField>().text != "")
        {
            print(username.GetComponent<UnityEngine.UI.InputField>().text + " " + 
                email.GetComponent<UnityEngine.UI.InputField>().text + " " +
                password.GetComponent<UnityEngine.UI.InputField>().text + " " +
                retypePass.GetComponent<UnityEngine.UI.InputField>().text);
            StartCoroutine(postSignup());
            // StartCoroutine(getSignup());
            // get when is correct or not si poate trece mai departe la MainMenu
        }
        else
        {
            print("null here");
        }
    }

    public class SignupData
    {
        // nume de parametrii din json
        public string username;
        public string email;
        public string password;
        public string retypePass;
        public string status;
    }

    IEnumerator getSignup()
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
                SignupData SignupData = JsonUtility.FromJson<SignupData>(www.downloadHandler.text);
                print(SignupData.status);
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

    IEnumerator postSignup()
    {
        /////////////// put correct 
        string uri = "https://jsonplaceholder.typicode.com/posts";
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("username=" + username + "&email=" + email + 
            "&password=" + password + "&retypePass=" + retypePass));

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
                //SignupData SignupData = JsonUtility.FromJson<SignupData>(www.downloadHandler.text);
                print(www.downloadHandler.text);
            }
            catch (System.Exception error)
            {
                print("catch" + error.ToString());
            }
        }
    }
}
