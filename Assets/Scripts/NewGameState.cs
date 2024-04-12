using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewGameState: IState
{
    private GameManager gameManager;    
    private GameStateMachine gameStateMachine;
   
    public NewGameState(GameManager inGameManager, GameStateMachine inGameStateMachine)
    {
        gameManager = inGameManager;
        gameStateMachine = inGameStateMachine; 
    }

    public void Enter()
    {
        gameManager.levelIndex = 0;
        gameManager.LoadStage(gameManager.levelIndex);
    }

    public void Update()
    {
        if (gameManager.currentCharacter)
        {
            Exit();
            gameStateMachine.StartState(new PlayingGameState(gameManager, gameStateMachine));
        }
    }

    public void Exit() 
    { 
        // clean the starting state here
    }

   
}
