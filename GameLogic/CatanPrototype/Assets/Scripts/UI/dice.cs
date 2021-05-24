using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice : MonoBehaviour
{

    public GameObject unu, doi, trei, patru, cinci, sase, unu2, doi2, trei2, patru2, cinci2, sase2;

    public int RandomDice()
    {
        int dice = Random.Range(1, 7);
        return dice;
    }


    public void Start()

    {
        unu.SetActive(false);
        doi.SetActive(false);
        trei.SetActive(false);
        patru.SetActive(false);
        cinci.SetActive(false);
        sase.SetActive(false);

        unu2.SetActive(false);
        doi2.SetActive(false);
        trei2.SetActive(false);
        patru2.SetActive(false);
        cinci2.SetActive(false);
        sase2.SetActive(false);
    }

    public void Display()
    {
        int dice;
        dice = RandomDice();

        switch (dice)
        {
            case 1:
                {
                    doi.SetActive(false);
                    trei.SetActive(false);
                    patru.SetActive(false);
                    cinci.SetActive(false);
                    sase.SetActive(false);
                    bool isActive = unu.activeSelf;
                    unu.SetActive(!isActive);
                    break;
                }
            case 2:
                {
                        unu.SetActive(false);
                        trei.SetActive(false);
                        patru.SetActive(false);
                        cinci.SetActive(false);
                        sase.SetActive(false);
                        bool isActive = doi.activeSelf;
                        doi.SetActive(!isActive);
                        break;
                }
            case 3:
                {
                    doi.SetActive(false);
                    unu.SetActive(false);
                    patru.SetActive(false);
                    cinci.SetActive(false);
                    sase.SetActive(false);
                    bool isActive = trei.activeSelf;
                    trei.SetActive(!isActive);
                    break;
                }
            case 4:
                {
                    doi.SetActive(false);
                    trei.SetActive(false);
                    unu.SetActive(false);
                    cinci.SetActive(false);
                    sase.SetActive(false);
                    bool isActive = patru.activeSelf;
                    patru.SetActive(!isActive);
                    break;
                }
            case 5:
                {
                    doi.SetActive(false);
                    trei.SetActive(false);
                    patru.SetActive(false);
                    unu.SetActive(false);
                    sase.SetActive(false);
                    bool isActive = cinci.activeSelf;
                    cinci.SetActive(!isActive);
                    break;
                }
            case 6:
                {
                    doi.SetActive(false);
                    trei.SetActive(false);
                    patru.SetActive(false);
                    cinci.SetActive(false);
                    unu.SetActive(false);
                    bool isActive = sase.activeSelf;
                    sase.SetActive(!isActive);
                    break;
                }
        }

        int dice2;
        dice2 = RandomDice();

        switch (dice2)
        {
            case 1:
                {

                    doi2.SetActive(false);
                    trei2.SetActive(false);
                    patru2.SetActive(false);
                    cinci2.SetActive(false);
                    sase2.SetActive(false);
                    bool isActive = unu2.activeSelf;
                    unu2.SetActive(!isActive);

                    break;
                }
            case 2:
                {

                    unu2.SetActive(false);
                    trei2.SetActive(false);
                    patru2.SetActive(false);
                    cinci2.SetActive(false);
                    sase2.SetActive(false);
                    bool isActive = doi2.activeSelf;
                    doi2.SetActive(!isActive);

                    break;
                }
            case 3:
                {

                    doi2.SetActive(false);
                    unu2.SetActive(false);
                    patru2.SetActive(false);
                    cinci2.SetActive(false);
                    sase2.SetActive(false);
                    bool isActive = trei2.activeSelf;
                    trei2.SetActive(!isActive);
                    break;
                }
            case 4:
                {
                    doi2.SetActive(false);
                    trei2.SetActive(false);
                    unu2.SetActive(false);
                    cinci2.SetActive(false);
                    sase2.SetActive(false);
                    bool isActive = patru2.activeSelf;
                    patru2.SetActive(!isActive);
                    break;
                }
            case 5:
                {
                    doi2.SetActive(false);
                    trei2.SetActive(false);
                    patru2.SetActive(false);
                    unu2.SetActive(false);
                    sase2.SetActive(false);
                    bool isActive = cinci2.activeSelf;
                    cinci2.SetActive(!isActive);
                    break;
                }
            case 6:
                {
                    doi2.SetActive(false);
                    trei2.SetActive(false);
                    patru2.SetActive(false);
                    cinci2.SetActive(false);
                    unu2.SetActive(false);
                    bool isActive = sase2.activeSelf;
                    sase2.SetActive(!isActive);
                    break;
                }
        }



    }

}
