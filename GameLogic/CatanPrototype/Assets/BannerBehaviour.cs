using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject textObject;

    public void SetText(string text)
    {
        textObject.GetComponent<Text>().text = text;
    }


}
