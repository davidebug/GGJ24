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
        Assert.IsNotNull(levelDatasSO);
        Assert.IsNotNull(gameStateMachine);
        gameStateMachine.StartNewGame(this); 
    }

    public void StartNewGame()
    {
        gameStateMachine.StartNewGame(this);

    }

    public void ResetGame()
    {

        gameStateMachine.StartNewGame(this);
        gameState = GameState.SOLUTION;
        OnGameStateChanged?.Invoke(gameState);

    }


   

    public void StartGame()
    {
        gameState = GameState.PLAYING;
        OnGameStateChanged?.Invoke(gameState);

        ShowCurrentCharacter();
    }

   

    //Return true if its the correct body part, false otherwise
    public bool TryToSelectBodyPart(int bodyPartIndex)
    {
        Assert.IsNotNull(CorrectSequenceOrder);

        if(CurrentCorrectNumber > CorrectSequenceOrder.Length)
        {
            Debug.LogError($"Sequence array dim is: {CurrentCorrectNumber} you are trying to read position {CurrentCorrectNumber}");
        }

        CurrentCorrectNumber = Math.Min(CurrentCorrectNumber, CorrectSequenceOrder.Length);
        int correctImageIndex = CorrectSequenceOrder[CurrentCorrectNumber];

        if(bodyPartIndex == correctImageIndex)
        {
            CurrentCorrectNumber++;
            OnCorrectSequence?.Invoke();
            if(CurrentCorrectNumber >= currentSequenceLength)
            {
                EndGame(true);
            }
            return true;
        }

        CurrentCorrectNumber = 0;
        OnWrongSequence?.Invoke();
        AudioManager.Get().PlayWoorp();
        return false;
    }

    public void WinGame()
    {
        gameState = GameState.WON_GAME;
        SceneManager.LoadScene("WinScene");
    }

  

   
    public void NextStage()
    {
        LoadStage(levelIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void Update()
    {
        if(gameState == GameState.PLAYING)
        {
           
        }
    }

    internal void UnselectEverything()
    {
        OnWrongSequence?.Invoke();
       
    }
}
