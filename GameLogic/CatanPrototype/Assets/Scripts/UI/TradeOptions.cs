using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeOptions : MonoBehaviour
{
    
    public GameObject bankTrade, userTrade, pop_up_quit, pop_up_refresh, trade_menu, trade_accept;

    public void closeAcceptOffer()
    {
        trade_accept.SetActive(false);
    }

    public void acceptTradewithBank()
    {
        bankTrade.SetActive(false);
    }


    public void closePopUpRefresh()
    {
        pop_up_refresh.SetActive(false);
    }

    public void closePopUpQuit()
    {
        pop_up_quit.SetActive(false);
    }

    public void closeQuitTrade()
    {
        pop_up_quit.SetActive(false);
        trade_menu.SetActive(false);
        bankTrade.SetActive(false);

    }

   
    public void openBank()
    {
        pop_up_quit.SetActive(false);
        pop_up_refresh.SetActive(false);
        userTrade.SetActive(false);

        bool isActive = bankTrade.activeSelf;

        bankTrade.SetActive(!isActive);
    }

    public void openUser()
    {
        pop_up_quit.SetActive(false);
        pop_up_refresh.SetActive(false);
        bankTrade.SetActive(false);

        bool isActive = userTrade.activeSelf;

        userTrade.SetActive(!isActive);
    }

    public void popUpQuit()
    {
        bankTrade.SetActive(false);
        pop_up_refresh.SetActive(false);

        bool isActive = pop_up_quit.activeSelf;

        pop_up_quit.SetActive(!isActive);
    }

    public void popUpQuitBank()
    {
        
        pop_up_refresh.SetActive(false);

        bool isActive = pop_up_quit.activeSelf;

        pop_up_quit.SetActive(!isActive);
    }

    public void popUpNewRefreshBank()
    {
        bankTrade.SetActive(true);
        pop_up_refresh.SetActive(true);
    }
    
    public void trade()
    {
       
        trade_accept.SetActive(false);

        bool isActive = trade_menu.activeSelf;

        trade_menu.SetActive(!isActive);
    }

    public void quitTrade()
    {
        
        trade_menu.SetActive(false);
        trade_accept.SetActive(false);
    }
    public void goTrade()
    {
        bool isActive = trade_accept.activeSelf;

        trade_accept.SetActive(!isActive);
    }

    public void popUpRefresh()
    {
        pop_up_quit.SetActive(false);
        bankTrade.SetActive(false);

        bool isActive = pop_up_refresh.activeSelf;

        pop_up_refresh.SetActive(!isActive);
    }

}
