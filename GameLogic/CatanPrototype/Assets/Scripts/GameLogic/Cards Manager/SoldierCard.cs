using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCard : DevelopmentCard
{
    /*private int _numberSoldierCard; // 14 r trebui

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
    
    public SoldierCard(int nr)
    {
        _numberSoldierCard=nr;
    }
    */
    public SoldierCard(string s)
    {
        type = s;
    }
    public override string getType()
    {
        return "soldier";
    }
}
