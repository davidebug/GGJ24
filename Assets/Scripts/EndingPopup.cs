using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPopup : MonoBehaviour
{
    public GameObject WinText;
    public GameObject LoseText;

    void OnEnable()
    {
        
    }

    void ShowPopup(GameState currentState)
    {
        
        if(currentState == GameState.VICTORY)
        {
            this.gameObject.SetActive(true);
            WinText.SetActive(true);
            LoseText.SetActive(false);
        }    
        else if(currentState == GameState.GAME_OVER)
        {
            this.gameObject.SetActive(true);
            WinText.SetActive(false);
            LoseText.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Get().OnGameStateChanged += ShowPopup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
