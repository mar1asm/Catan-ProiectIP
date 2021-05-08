using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class DevelopmentCard : Card
{
    private int _numberDevelopmentCard; // 25 ar trebui
    protected string type;
  
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
    public abstract string getType();
    /*
    public DevelopmentCard(int nr)
    {
        _numberDevelopmentCard = nr ;
    }
    public DevelopmentCard()
    {

    }
    */
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
