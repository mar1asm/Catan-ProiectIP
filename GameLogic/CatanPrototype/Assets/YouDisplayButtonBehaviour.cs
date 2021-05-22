using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YouDisplayButtonBehaviour : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI text;
    void Start()
    {
        text.text = UserInfo.GetUsername();
    }
}
