using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UsernameDisplayBehaviour : MonoBehaviour
{
    [SerializeField]
    private Text text;


    void Start()
    {
        text.text = UserInfo.GetUsername();
    }
}
