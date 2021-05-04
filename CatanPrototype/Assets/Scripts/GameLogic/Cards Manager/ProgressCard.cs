using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressCard : DevelopmentCard
{
    private int _numberProgressCard; // 25 ar trebui

    public int numberProgressCard
    {
        get
        {
            return _numberProgressCard;
        }
        set
        {
            _numberProgressCard = value;

        }
    }
    // Start is called before the first frame update
    /* void Start()
     {

     }

     // Update is called once per frame
     void Update()
     {

     }
     */
     public ProgressCard(int nr)
    {
        _numberProgressCard = nr;
    }
}
