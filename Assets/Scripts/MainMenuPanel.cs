using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    public Button StartButton;
    public Button QuitGame;

  
    void Start()
    {
        StartButton.onClick.AddListener(GameManager.Get().StartNewGame);
        QuitGame.onClick.AddListener(GameManager.Get().QuitGame);   
    }
    
}
