using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public enum GameState
{
    SOLUTION,
    PLAYING,
    GAME_OVER,
    VICTORY,
    WON_GAME
}
public class GameManager : Manager<GameManager> 
{    
    public GameState gameState;
    public GameStateMachine gameStateMachine;
   

    void Start()
    {  

        Assert.IsNotNull(gameStateMachine);
        gameStateMachine.StartNewGame(); 
    }

    public void StartNewGame()
    {
        gameStateMachine.StartNewGame();

    }

    public void ResetGame()
    {
        gameStateMachine.StartNewGame();

    }

   

    //Return true if its the correct body part, false otherwise
    

    

  

   
   
    public void QuitGame()
    {
        Application.Quit();
    }


    internal void UnselectEverything()
    {
        OnWrongSequence?.Invoke();
       
    }
}
