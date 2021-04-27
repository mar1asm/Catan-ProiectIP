using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void loadScene(string sceen)
    {
        print(sceen + "a intrat aici");
        SceneManager.LoadScene(sceen);
    }
}
