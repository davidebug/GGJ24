
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayingGameState
    : IState
{
    private GameStateMachine gameStateMachine;

    private bool isSolutionShown = false;
    public PlayingGameState(GameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        isSolutionShown = false;
        // show pop up for the solution

        Assert.IsNotNull(gameStateMachine.sequenceBar);
        Assert.IsNotNull(gameStateMachine.sequencePopupController);
        // PHASE SOLUTION 
        gameStateMachine.sequenceBar.UpdateSequence(gameStateMachine.CurrentSequenceLength, gameStateMachine.CurrentCorrectNumber);

    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        if(isSolutionShown)
        {
            gameStateMachine.ShowCurrentCharacter();
        }

        gameStateMachine.CurrentTime += Time.deltaTime;
        if (gameStateMachine.CurrentTime > gameStateMachine.MaxTime)
        {
            EndGame(false);
        }

        // Cheat modet, win the game
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
        {
            WinGame();
        }

    }
}