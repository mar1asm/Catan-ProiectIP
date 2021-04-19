using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    public List<Player> players;

    public void AddPlayer(Player p)
    {
        players.Add(p);
    }
    public void RemovePlayer(Player p)
    {
        players.Remove(p);
    }

}
