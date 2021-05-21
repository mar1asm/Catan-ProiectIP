using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class UserInfo    
{    
    private static readonly UserInfo instance = new UserInfo();
    private static string token;
    private static string expiration;

    private static int gameSessionId;
    private static string username;

    static UserInfo(){}    
    private UserInfo(){}  

    public static UserInfo Instance    
    {    
        get    
        {    
            return instance;    

        }    
    }


    public static void SetGameSessionId(int _gameSessionId) {
        gameSessionId = _gameSessionId;
    }

    public static int GetGameSessionId() {
        return gameSessionId;
    }

    public static void SetUsername(string _username) {
        username = _username;
    }

    public static string GetUsername() {
        return username;
    }

    public static string GetToken() 
    {
        return token;
    }

    public static void SetToken(string t)
    {
        token = t;
    }

    public static string GetExpiration() 
    {
        return expiration;
    }

    public static void SetExpiration(string e)
    {
        expiration = e;
    }  
}
