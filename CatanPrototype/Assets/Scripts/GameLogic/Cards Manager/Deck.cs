using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck 
{
    private List<Card> package;
    private int _nr;
    public int nr
    {
        get
        {
            return _nr;
        }
        set
        {
            _nr = value;

        }
    }
    public List<Card> Package
    {
        get
        {
            return package;
        }
        set
        {
            package = value;

        }
    }
    public Deck()
    {
        _nr = 0;
        package = new List<Card>();

    }
    public void add(Card card)
    {
        package.Add(card);
        _nr++;
    }
    public void remove(Card card)
    {
        if (package.Contains(card))
        {
            package.Remove(card);
            _nr--;

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
}
