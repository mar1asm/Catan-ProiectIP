using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChooseGame : MonoBehaviour
{
    public GameObject pop_up_TheGame, pop_up_Seafarers, pop_up_CitiesKnights, loadingScreen;


    [SerializeField]
    private StartGameBehaviour startGame;
    
    public void TheGame()
    {
        pop_up_Seafarers.SetActive(false);
        pop_up_CitiesKnights.SetActive(false);
        loadingScreen.SetActive(false);

        bool isActive = pop_up_TheGame.activeSelf;

        pop_up_TheGame.SetActive(!isActive);

    }

    public void Seafarers()
    {
        pop_up_TheGame.SetActive(false);
        pop_up_CitiesKnights.SetActive(false);
        loadingScreen.SetActive(false);

        bool isActive = pop_up_Seafarers.activeSelf;

        pop_up_Seafarers.SetActive(!isActive);
    }

    public void CitiesKnights()
    {
        pop_up_TheGame.SetActive(false);
        pop_up_Seafarers.SetActive(false);
        loadingScreen.SetActive(false);

        bool isActive = pop_up_CitiesKnights.activeSelf;

        pop_up_CitiesKnights.SetActive(!isActive);
    }


    public void closeTheGame()
    {
        pop_up_TheGame.SetActive(false);
    }

    public void closeSeafarers()
    {
        pop_up_Seafarers.SetActive(false);
    }

    public void closeCitiesKnights()
    {
        pop_up_CitiesKnights.SetActive(false);
    }

    public void StartGame()
    {
   

        bool isActive = loadingScreen.activeSelf;

        loadingScreen.SetActive(!isActive);


        startGame.CreateGame();

        pop_up_TheGame.SetActive(false);
        pop_up_Seafarers.SetActive(false);
        pop_up_CitiesKnights.SetActive(false);

       
    }

    

}



[System.Serializable]
public class GameSessionStarterJSON {
    public List<int> extensions = new List<int>();
}

[System.Serializable]
public class GameSessionStarterResponseJSON {
    public int id;
    public int status;
    public string createdAt;
}