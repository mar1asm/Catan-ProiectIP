using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevelopmentCard : Card
{
    private int _numberDevelopmentCard; // 25 ar trebui
  
    public int numberDevelopmentCard
    {
        get
        {
            return _numberDevelopmentCard;
        }
        set
        {
            _numberDevelopmentCard = value;

        }
    }
    public DevelopmentCard(int nr)
    {
        _numberDevelopmentCard = nr ;
    }
    public DevelopmentCard()
    {

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
}
