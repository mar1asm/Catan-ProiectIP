using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class refreshResources : MonoBehaviour
{
    Text twood, trock, tbrick, tsheep, twheat, twood2, trock2, tbrick2, tsheep2, twheat2;

    public void display()
    {
        twood = GameObject.Find("twood").GetComponent<Text>();
        trock = GameObject.Find("trock").GetComponent<Text>();
        tbrick = GameObject.Find("tbrick").GetComponent<Text>();
        tsheep = GameObject.Find("tsheep").GetComponent<Text>();
        twheat = GameObject.Find("twheat").GetComponent<Text>();
        twood2 = GameObject.Find("twood2").GetComponent<Text>();
        trock2 = GameObject.Find("trock2").GetComponent<Text>();
        tbrick2 = GameObject.Find("tbrick2").GetComponent<Text>();
        tsheep2 = GameObject.Find("tsheep2").GetComponent<Text>();
        twheat2 = GameObject.Find("twheat2").GetComponent<Text>();

        twood.text = "0";
        trock.text = "0";
        tbrick.text = "0";
        tsheep.text = "0";
        twheat.text = "0";
        twood2.text = "0";
        trock2.text = "0";
        tbrick2.text = "0";
        tsheep2.text = "0";
        twheat2.text = "0";

    }
}