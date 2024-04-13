using UnityEngine;

public class IdleState
    : IState
{
    private float timeElaspsed;
    private float timeToWaitInSeconds;
    private GameStateMachine gameStateMachine;
    private IState nextState;
    public IdleState(GameStateMachine inGameStateMachine, float inTimeToWaitInSeconds, IState nextState)
    {
        this.timeToWaitInSeconds = inTimeToWaitInSeconds;
        this.gameStateMachine = inGameStateMachine;
        this.nextState = nextState;
    }

    void IState.Enter()
    {
        timeElaspsed = 0;
    }

    void IState.Exit()
    {
        
    }

    void IState.Update()
    {
        timeElaspsed += Time.deltaTime;

        if(timeElaspsed >= timeToWaitInSeconds)
        {
            gameStateMachine.StartState(nextState);
       
        }
    }
}