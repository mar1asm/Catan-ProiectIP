using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class getUserInfo : MonoBehaviour
{
    Text username, level, resources, short_path, item;
    void getInfo()
    {
        username = GameObject.Find("username").GetComponent<Text>();
        level = GameObject.Find("level").GetComponent<Text>();
        resources = GameObject.Find("resources").GetComponent<Text>();
        short_path = GameObject.Find("short-path").GetComponent<Text>();
        item = GameObject.Find("item").GetComponent<Text>();

       // Debug.Log(username);
       // Debug.Log(level);
       // Debug.Log(resources);
       // Debug.Log(short_path);
       // Debug.Log(item);

    }

}
