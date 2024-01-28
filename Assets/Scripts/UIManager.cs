using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : Manager<UIManager>
{

    public MainMenuPanel mainMenuPanel;
    public GameObject[] gameplayPanels;
    public GameObject background;
    public void Start()
    {

        //GameManager.Get().OnGameStateChanged += ManagePanels;
    }

    private void ManagePanels(GameState state)
    {
        //switch (state)
        //{
        //    case GameState.START_MENU:
        //        mainMenuPanel.gameObject.SetActive(true);
        //        foreach(var panel in gameplayPanels)
        //        {
        //            panel.gameObject.SetActive(false);
        //        }
        //        break;
        //    default:
        //        mainMenuPanel.gameObject.SetActive(false);
        //        break;

        //}
    }


   
}
