using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;


[Serializable]
public class PlayersDataJson
{
    public List<PlayerDataJson> players;
}

[Serializable]
public class PlayerDataJson
{
    //De adaugat obiectele(datele) de la API
    //   Exemplu
    public string username;
    public string email;
    public string icon;
    public int level;

}

public class playersBanner : MonoBehaviour
{
    public GameObject bannerObj; //obiectele din joc care trebuiesc modificate
    public Text name_player1, name_player2,name_player3;

    void Start()
    {
        StartCoroutine(getPlayersData());
    }

    IEnumerator getPlayersData()
    {
        //Preparing the request
        string uri = "https://localhost:5001/api/GameSessions/{id}"; //de modificat cu url-ul de la api
        UnityWebRequest request = UnityWebRequest.Get(uri);
        request.SetRequestHeader("Authorization", "Bearer " + UserInfo.GetToken());

        //Send the request then wait here until it returns
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log("GET Error");
        else
        {
            Debug.Log("GET OK");
            long status = request.responseCode;
            if (status == 200)
            {
                // string preProccesedJson = "{ \"friends\" : " + request.downloadHandler.text + " }";
                string preProccesedJson = request.downloadHandler.text; //posibil de prelucrat json-ul

                //Get data from Json response
                PlayersDataJson userData = JsonUtility.FromJson<PlayersDataJson>(preProccesedJson);
                updateBanners(userData);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
    }

    public void updateBanners(PlayersDataJson userData)
    {
        //de folosit datele primite pentru a modifica ui-ul

        // userData.players[0].username - usernameul primului player
        // userData.players[0].email - emailul primului player
        // ... etc

        if(userData.players.Count==3)
        {
            name_player1 = GameObject.Find("name-user1").GetComponent<Text>();
            name_player2 = GameObject.Find("name-user2").GetComponent<Text>();

            name_player1.text = userData.players[0].username;
            name_player2.text = userData.players[1].username;
        }
        else
        {
            name_player1 = GameObject.Find("name-user1").GetComponent<Text>();
            name_player2 = GameObject.Find("name-user2").GetComponent<Text>();
            name_player3 = GameObject.Find("name-user3").GetComponent<Text>();

            name_player1.text = userData.players[0].username;
            name_player2.text = userData.players[1].username;
            name_player3.text = userData.players[2].username;
        }

        
          

    }
}
