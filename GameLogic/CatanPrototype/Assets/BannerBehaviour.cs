using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BannerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject textObject;


    [SerializeField]
    private TextMeshProUGUI scoreText;

    void Start() {

        GetComponent<Image>().color = new Color(1, 1, 1, 0.65f);
    }

    public void SetScore(string score) {
        scoreText.text = score;
    }
    public void SetText(string text)
    {
        textObject.GetComponent<Text>().text = text;
    }


    public void SetHighlight(bool highlight) {
        if(!highlight) {
            GetComponent<Image>().color = new Color(1, 1, 1, 0.65f);
            //transform.localScale = new Vector3(1, 1, 1);
            textObject.GetComponent<Text>().color = new Color(1, 1, 1);
        } 
        else 
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            textObject.GetComponent<Text>().color = new Color(0, 1, 0);
            //transform.localScale = new Vector3(1.275f, 1.275f, 1.275f);
        }
    }

    public string GetText() {
        return textObject.GetComponent<Text>().text;
    }


}
