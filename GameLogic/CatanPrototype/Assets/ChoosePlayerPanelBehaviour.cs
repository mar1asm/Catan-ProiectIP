using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayerPanelBehaviour : MonoBehaviour
{

    [SerializeField]
    public PlayerNamesHolderBehaviour playerNamesHolder;

    public Player playerChosen;

    public string nameChosen;

    public IEnumerator ChoosePlayer(List<Player> players) {
        yield return null;
        List<string> names = new List<string>();
        foreach (var player in players)
        {
            names.Add(player.nickname);
        }

        yield return StartCoroutine(playerNamesHolder.WaitToChoosePlayer(names));

        nameChosen = playerNamesHolder.stringChosen;

        foreach (Player player in players)
        {
            if(player.nickname == nameChosen) {
                playerChosen = player;
                break;
            }
        }
    }
}
