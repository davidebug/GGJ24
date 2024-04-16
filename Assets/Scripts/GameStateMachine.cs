using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[SerializeField]
public class GameStateMachine : StateMachine
{
    #region context

    public LevelDataAssetScriptableObject levelDatasSO;
    private int levelIndex = 1;
    /* Action Called every time there is a progress by the player on reconstructing the sequence: true if the next step is correct, false otherwise */
    public Action<bool> OnSequenceProgress;
    public Action<int> OnBodyPartClicked;
    public Action<GameState> OnGameStateChanged;
    public Action OnStageBegin;
    public RectTransform bodyRectTransformPlaceHolder;
    public int CurrentCorrectNumber;
    public int[] CorrectSequenceOrder;
    public float CurrentTime;
    public float MaxTime;
    public Character currentCharacter;

    public int TotalSequenceLength
    {
        get { return CorrectSequenceOrder.Length; }
    }

    public int LevelIndex { get => levelIndex; set => levelIndex = value; }
    public int CurrentSequenceLength { get => currentSequenceLength; set => currentSequenceLength = value; }

    private int currentSequenceLength;
    [SerializeField]
    private bool SkipMainMenu;

    #region SequenceContext

    public SequenceBar sequenceBar;
    public SequencePopupController sequencePopupController;

    #endregion

    #endregion

    #region EndingPopUp

    public EndingPopup endingPopup;
    #endregion


    #region GameStates

    private NewGameState newGameState;
    private PlayingGameState playingGameState;

    #endregion
    public void StartNewGame()
    {

       if (newGameState == null)
       {
            newGameState = new NewGameState(this);
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
