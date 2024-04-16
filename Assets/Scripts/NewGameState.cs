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
        LoadStage(context.LevelIndex);
        context.endingPopup.HidePopup();

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
    public void LoadStage(int levelIndex)
    {
        if (levelIndex >= context.levelDatasSO.characters.Length)
            return;

        
        if (context.currentCharacter != null)
        {
            Object.Destroy(context.currentCharacter);
        }

        context.currentCharacter = Object.Instantiate(context.levelDatasSO.GetCharacter(levelIndex));
        context.currentCharacter.gameObject.transform.SetParent(context.bodyRectTransformPlaceHolder, false);
        context.currentCharacter.gameObject.SetActive(false);

        context.CurrentTime = 0;
        context.MaxTime = context.currentCharacter.MaxTime;
        context.CorrectSequenceOrder = context.currentCharacter.sequenceOrder;
        context.CurrentSequenceLength = context.CorrectSequenceOrder.Length;
        context.CurrentCorrectNumber = 0;

    }

}
