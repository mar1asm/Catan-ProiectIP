using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCard : DevelopmentCard
{
    private int _numberSoldierCard; // 14 r trebui

    public int numberSoldierCard
    {
        get
        {
            return _numberSoldierCard;
        }
        set
        {
            _numberSoldierCard = value;

        }
    }
}
