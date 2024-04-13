
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;

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
        gameStateMachine.sequenceBar.UpdateSequence(gameStateMachine.CurrentSequenceLength, gameStateMachine.CurrentCorrectNumber);
        gameStateMachine.sequencePopupController.ShowAnimateSequence(gameStateMachine.currentCharacter);

    }



    public void Update()
    {

        if (!hasSolutionBeenShown)
        {
            hasSolutionBeenShown = !gameStateMachine.sequencePopupController.IsShowingAnimation();
            gameStateMachine.ShowCurrentCharacter();
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
            gameStateMachine.StartState(new IdleState(gameStateMachine, 2, new NewGameState(gameStateMachine)));
            gameStateMachine.LevelIndex ++;

        }
        else
        {
            gameStateMachine.LevelIndex = 0;
            AudioManager.Get().PlayWhoosh();
            gameStateMachine.StartState(new NewGameState(gameStateMachine));
        }

        if (gameStateMachine.LevelIndex > gameStateMachine.levelDatasSO.characters.Length)
        {
            gameStateMachine.WinGame();
        }

        Exit();
    }

    public void Exit()
    {
        
    }
}
