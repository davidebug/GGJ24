using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewGameState: IState
{  
    private GameStateMachine context;
   
    public NewGameState(GameStateMachine inGameStateMachine)
    {
        context = inGameStateMachine; 
    }

    public void Enter()
    {
        context.LevelIndex = 0;
        context.LoadStage(context.LevelIndex);
    }

    public void Update()
    {
        if (context.currentCharacter)
        {
            Exit();
            context.StartState(new PlayingGameState(context));
        }
    }

    public void Exit() 
    { 
        // clean the starting state here
    }

   
}
