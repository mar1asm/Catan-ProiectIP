using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ResourceCard
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    public override string getResource()
    {
        return "brick";
    }
    public Brick(int number, ResourceTypes type)
    {
        _numberResourceCard = number;
        CardType = type;


    }
}
