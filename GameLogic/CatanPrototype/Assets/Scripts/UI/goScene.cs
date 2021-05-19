using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goScene : MonoBehaviour
{
    public void loadScene(string sceen)
    {
        print(sceen + "a intrat aici");
        SceneManager.LoadScene(sceen);
    }
}
