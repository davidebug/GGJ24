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
    public Action OnCorrectSequence;
    public Action OnWrongSequence;
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


    public void LoadStage(int levelIndex)
    {
        if (levelIndex >= levelDatasSO.characters.Length)
            return;

        if (currentCharacter != null)
        {
            Destroy(currentCharacter);
        }

        currentCharacter = Instantiate(levelDatasSO.GetCharacter(levelIndex));
        currentCharacter.gameObject.transform.SetParent(bodyRectTransformPlaceHolder, false);
        currentCharacter.gameObject.SetActive(false);

        CurrentTime = 0;
        MaxTime = currentCharacter.MaxTime;
        CorrectSequenceOrder = currentCharacter.sequenceOrder;
        currentSequenceLength = CorrectSequenceOrder.Length;
        CurrentCorrectNumber = 0;

    }

    public void ShowCurrentCharacter()
    {
        currentCharacter.gameObject.SetActive(true);
        RectTransform rectTransform = currentCharacter.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;
    }

 

    private IEnumerator WaitForVictory()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(2);
        levelIndex++;
        StartNewGame();
    }

    public void NextStage()
    {
        LoadStage(levelIndex);
    }


    public void WinGame()
    {
        SceneManager.LoadScene("WinScene");
    }
}
