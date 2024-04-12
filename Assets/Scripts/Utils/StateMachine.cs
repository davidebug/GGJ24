using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public abstract class StateMachine : MonoBehaviour
{
    public GameManager gameManager;
    protected IState currentState;

    public virtual IState GetCurrentState()
    {
        return currentState;
    }

    public virtual void StartState(IState state) { }
    
}
