using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class GameStateMachine : StateMachine
{


#region EndingPopUp

    public GameObject LoseText;
    public GameObject Buttons;
    public GameObject NextChar;
    public GameObject MainMenu2;
    public GameObject endingPopUp;

    public Sprite[] levelProgressSprites;
    public Image winImage;
    public float animationDuration = 0.5f;
    public float victoryPopupDuration = 4f;
    #endregion


    #region GameStates

    private NewGameState newGameState;
    private PlayingGameState playingGameState;

    #endregion
    public void StartNewGame()
    {

       if (newGameState == null)
       {
            newGameState = new NewGameState(GameManager.Get(), this);
       }

        StartState(newGameState);

    }

    public override void StartState(IState state)
    {
        base.StartState(state);

        state.Enter();
    }


    void Update()
    {
        if (currentState != null)
        {
            currentState.Update();

        }
    }
}
