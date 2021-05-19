using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonopolyCard :DevelopmentCard
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
    public MonopolyCard(string s)
    {
        type = s;
    }
    public override string getType()
    {
        return "monopoly";
    }
}
