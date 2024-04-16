
using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class PlayingGameState
    : IState
{
    private GameStateMachine gameStateMachine;

    private bool hasSolutionBeenShown = false;
    public PlayingGameState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        hasSolutionBeenShown = false;
        // show pop up for the solution

        Assert.IsNotNull(gameStateMachine.sequenceBar);
        Assert.IsNotNull(gameStateMachine.sequencePopupController);
        // PHASE SOLUTION 
        gameStateMachine.OnStageBegin?.Invoke();
        gameStateMachine.sequencePopupController.ShowAnimateSequence(gameStateMachine.currentCharacter);

    }



    public void Update()
    {

        if (!hasSolutionBeenShown)
        {
            hasSolutionBeenShown = !gameStateMachine.sequencePopupController.IsShowingAnimation();
            ShowCurrentCharacter();
        }
        else
        {
            gameStateMachine.CurrentTime += Time.deltaTime;
            if (gameStateMachine.CurrentTime > gameStateMachine.MaxTime)
            {
                EndGame(false);
            }

            // Cheat modet, win the game
            if (Input.GetKeyDown(KeyCode.W) || gameStateMachine.CurrentCorrectNumber == gameStateMachine.CurrentSequenceLength)
            {
                EndGame(true);
            }

        }




    }


    public void EndGame(bool victory)
    {
        gameStateMachine.CurrentTime = 0;
        
        
        if (victory)
        {

            gameStateMachine.currentCharacter.MakeSmile();
            AudioManager.Get().PlayLaugh(gameStateMachine.LevelIndex);
            gameStateMachine.LevelIndex ++;
            gameStateMachine.StartState(new IdleState(gameStateMachine, 2, new NewGameState(gameStateMachine)));

        }
        else
        {
            gameStateMachine.LevelIndex = 0;
            AudioManager.Get().PlayWhoosh();
            gameStateMachine.StartState(new NewGameState(gameStateMachine));
        }

        if (gameStateMachine.LevelIndex > gameStateMachine.levelDatasSO.characters.Length)
        {
            WinGame();
        }

        //TODO: this pop up won't be called in case of winning
        gameStateMachine.endingPopup.ShowPopup(victory);
        Exit();
    }

    public void Exit()
    {
        
    }


    public void ShowCurrentCharacter()
    {
        gameStateMachine.currentCharacter.gameObject.SetActive(true);
        RectTransform rectTransform = gameStateMachine.currentCharacter.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;
    }

    public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }


    public bool TryToSelectBodyPart(int bodyPartIndex)
    {
        Assert.IsNotNull(gameStateMachine.CorrectSequenceOrder);

        if (gameStateMachine.CurrentCorrectNumber > gameStateMachine.CorrectSequenceOrder.Length)
        {
            UnityEngine.Debug.LogError($"Sequence array dim is: {gameStateMachine.CurrentCorrectNumber} you are trying to read position {gameStateMachine.CurrentCorrectNumber}");
        }

        gameStateMachine.CurrentCorrectNumber = Math.Min(gameStateMachine.CurrentCorrectNumber, gameStateMachine.CorrectSequenceOrder.Length);
        int correctImageIndex = gameStateMachine.CorrectSequenceOrder[gameStateMachine.CurrentCorrectNumber];

        if (bodyPartIndex == correctImageIndex)
        {
            gameStateMachine.CurrentCorrectNumber++;
            gameStateMachine.OnSequenceProgress?.Invoke(true);
            if (gameStateMachine.CurrentCorrectNumber >= gameStateMachine.CurrentSequenceLength)
            {
                EndGame(true);
            }
            return true;
        }

        gameStateMachine.CurrentCorrectNumber = 0;
        gameStateMachine.OnSequenceProgress?.Invoke(false);
        AudioManager.Get().PlayWoorp();
        return false;
    }
}
