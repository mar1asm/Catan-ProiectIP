using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingCosts : MonoBehaviour
{
    public int bricks=0, lumber=0, wool=0, grain=0, ore=0;

    /**primeste path,settlement,city sau development si retine costul in variabilele bricks, lumber, wool, grain, ore*/
    public CraftingCosts(string construction)
    {
        switch (construction)
        {            
            case "path":
                lumber = 1;
                bricks = 1;
                break;

            case "settlement":
                lumber = 1;
                bricks = 1;
                grain = 1;
                wool = 1;
                break;

            case "city":
                grain = 2;
                ore = 3;
                break;

            case "development":
                grain = 1;
                wool = 1;
                ore = 1;
                break;
        }
    }
}
