using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class openLog : MonoBehaviour
{  
    public GameObject gamelog_emoji, gamelog_chat, gamelog_quit, gamelog_statistics, gamelog_history, gamelog_settings, gamelog_rules, gamelog_info, trade_menu;
    public GameObject bankTrade, pop_up_quit, pop_up_refresh;

    public void openBank()
    {
        pop_up_quit.SetActive(false);
        pop_up_refresh.SetActive(false);

        bool isActive = bankTrade.activeSelf;

        bankTrade.SetActive(!isActive);
    }

    public void popUpQuit()
    {
        bankTrade.SetActive(false);
        pop_up_refresh.SetActive(false);

        bool isActive = pop_up_quit.activeSelf;

        pop_up_quit.SetActive(!isActive);
    }

    public void popUpRefresh()
    {
        pop_up_quit.SetActive(false);
        bankTrade.SetActive(false);

        bool isActive = pop_up_refresh.activeSelf;

        pop_up_refresh.SetActive(!isActive);
    }


    public void emoji()
    {
     
        gamelog_chat.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_rules.SetActive(false);
        gamelog_info.SetActive(false);
        trade_menu.SetActive(false);


        bool isActive = gamelog_emoji.activeSelf;

        gamelog_emoji.SetActive(!isActive);
     

    }

    public void chat()
    {
        gamelog_emoji.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_rules.SetActive(false);
        gamelog_info.SetActive(false);
        trade_menu.SetActive(false);

        bool isActive = gamelog_chat.activeSelf;

        gamelog_chat.SetActive(!isActive);
    }

    public void quit()
    {
        gamelog_emoji.SetActive(false);
        gamelog_chat.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_rules.SetActive(false);
        gamelog_info.SetActive(false);
        trade_menu.SetActive(false);

        bool isActive = gamelog_quit.activeSelf;

        gamelog_quit.SetActive(!isActive);
    }

    public void statistics()
    {
        gamelog_emoji.SetActive(false);
        gamelog_chat.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_rules.SetActive(false);
        gamelog_info.SetActive(false);
        trade_menu.SetActive(false);

        bool isActive = gamelog_statistics.activeSelf;

        gamelog_statistics.SetActive(!isActive);
    }

    public void history()
    {
        gamelog_emoji.SetActive(false);
        gamelog_chat.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_rules.SetActive(false);
        gamelog_info.SetActive(false);
        trade_menu.SetActive(false);

        bool isActive = gamelog_history.activeSelf;

        gamelog_history.SetActive(!isActive);
    }

    public void settings()
    {
        gamelog_emoji.SetActive(false);
        gamelog_chat.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_rules.SetActive(false);
        gamelog_info.SetActive(false);
        trade_menu.SetActive(false);

        bool isActive = gamelog_settings.activeSelf;

        gamelog_settings.SetActive(!isActive);
    }


    public void rules()
    {
        gamelog_emoji.SetActive(false);
        gamelog_chat.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_info.SetActive(false);
        trade_menu.SetActive(false);

        bool isActive = gamelog_rules.activeSelf;

        gamelog_rules.SetActive(!isActive);
    }

    public void info()
    {
        gamelog_emoji.SetActive(false);
        gamelog_chat.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_rules.SetActive(false);
        trade_menu.SetActive(false);

        bool isActive = gamelog_info.activeSelf;

        gamelog_info.SetActive(!isActive);
    }

    public void trade()
    {
        gamelog_emoji.SetActive(false);
        gamelog_chat.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_info.SetActive(false);
        gamelog_rules.SetActive(false);

        bool isActive = trade_menu.activeSelf;

        trade_menu.SetActive(!isActive);
    }

    public void quitTrade()
    {
        gamelog_emoji.SetActive(false);
        gamelog_chat.SetActive(false);
        gamelog_quit.SetActive(false);
        gamelog_statistics.SetActive(false);
        gamelog_history.SetActive(false);
        gamelog_settings.SetActive(false);
        gamelog_info.SetActive(false);
        gamelog_rules.SetActive(false);
        trade_menu.SetActive(false);
    }

}

