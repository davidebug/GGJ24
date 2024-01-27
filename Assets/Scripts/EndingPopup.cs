using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPopup : MonoBehaviour
{
    public GameObject LoseText;
    public GameObject Buttons;
    public GameObject NextChar;

    void OnEnable()
    {
        
    }

    void ShowPopup(GameState currentState)
    {
        
        if(currentState == GameState.VICTORY)
        {
            this.gameObject.SetActive(true);
            LoseText.SetActive(false);
            Buttons.SetActive(false);
            NextChar.SetActive(true);  
        }    
        else if(currentState == GameState.GAME_OVER)
        {
            this.gameObject.SetActive(true);
            LoseText.SetActive(true);
            Buttons.SetActive(true);
            NextChar.SetActive(false);
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
