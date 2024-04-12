
using System.Diagnostics;

public class PlayingGameState
    : IState
{
    private GameManager gameManager;
    private GameStateMachine gameStateMachine;

    private bool isSolutionShown = false;
    public PlayingGameState(GameManager gameManager, GameStateMachine gameStateMachine)
    {
        this.gameManager = gameManager;
        this.gameStateMachine = gameStateMachine;
    }

    public void Enter()
    {
        isSolutionShown = false;
        // show pop up for the solution
    }

    public void Update()
    {
        if(isSolutionShown)
        {

        }
      
    }
}