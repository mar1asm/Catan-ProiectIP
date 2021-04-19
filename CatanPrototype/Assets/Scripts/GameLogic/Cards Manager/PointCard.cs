using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCard : DevelopmentCard
{
    private int _numberPointCard; // 25 ar trebui

    public int numberPointCard
    {
        get
        {
            return _numberPointCard;
        }
        set
        {
            _numberPointCard = value;

        }
    }
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    public PointCard(int nr)
    {
        _numberPointCard=nr;
    }
}
