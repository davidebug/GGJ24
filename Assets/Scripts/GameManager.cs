using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;

public enum GameState
{
    START_MENU,
    SOLUTION,
    PLAYING,
    GAME_OVER,
    VICTORY
}
public class GameManager : Manager<GameManager> 
{
    //public LevelDataAsset dataAsset;

    public GameState gameState;
    public LevelDataAssetScriptableObject levelDatasSO;

    public int levelIndex = 1;

    public Action OnCorrectSequence;
    public Action OnWrongSequence;
    public Action<GameState> OnGameStateChanged;

    public Action OnStageBegin;

    public int CurrentCorrectNumber;
    public int TotalSequenceLength
    {
        get { return CorrectSequenceOrder.Length; }
    }
    public int[] CorrectSequenceOrder;

    public float CurrentTime;
    public float MaxTime;

    private UIManager uiManagerInstance;
    public Character currentCharacter;
    private int currentSequenceLength;
    [SerializeField]
    private bool SkipMainMenu;
    void Start()
    {
        uiManagerInstance = UIManager.Get();
        Assert.IsNotNull(uiManagerInstance);
        Assert.IsNotNull(levelDatasSO);
        if(SkipMainMenu)
        {
            StartNewGame();

        }
        else
        {
            gameState = GameState.START_MENU;   
            OnGameStateChanged?.Invoke(gameState);
        }
        
    }

    public void initGameValues()
    {
        CurrentTime = 0;
        LoadCurrentCharacter(levelIndex);
        gameState = GameState.SOLUTION;
        OnGameStateChanged?.Invoke(gameState);
        //UIManager.Instance.ClearCardsUI();
        //UIManager.Instance.disableGameEndedScreen();

        
    }

    public void ResetGame()
    {
        CurrentTime = 0;
        levelIndex = 0;
        LoadCurrentCharacter(levelIndex);
        gameState = GameState.SOLUTION;
        OnGameStateChanged?.Invoke(gameState);
        //UIManager.Instance.ClearCardsUI();
        //UIManager.Instance.disableGameEndedScreen();


    }


    public void LoadCurrentCharacter(int levelIndex)
    {
        int currentCharacterIndex = Math.Min(levelIndex, levelDatasSO.characters.Length);
        currentCharacter = Instantiate(levelDatasSO.GetCharacter(currentCharacterIndex));

        currentCharacter.gameObject.transform.SetParent(uiManagerInstance.bodyRectTransformPlaceHolder, true);
        currentCharacter.gameObject.SetActive(false);
        MaxTime = currentCharacter.MaxTime;
        CorrectSequenceOrder = currentCharacter.sequenceOrder;
        currentSequenceLength = CorrectSequenceOrder.Length;
        ShowCurrentCharacter();
    }

    public void ShowCurrentCharacter()
    {
        currentCharacter.gameObject.SetActive(true);
        RectTransform rectTransform = currentCharacter.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector3.zero;  
    }

    //Return true if its the correct body part, false otherwise
    public bool TryToSelectBodyPart(int bodyPartIndex)
    {
        Assert.IsNotNull(CorrectSequenceOrder);

        if(CurrentCorrectNumber > CorrectSequenceOrder.Length)
        {
            Debug.LogError($"Sequence array dim is: {CurrentCorrectNumber} you are trying to read position {CurrentCorrectNumber}");
        }

         CurrentCorrectNumber = Math.Min(CurrentCorrectNumber, CorrectSequenceOrder.Length);
        int correctImageIndex = CorrectSequenceOrder[CurrentCorrectNumber];

        if(bodyPartIndex == correctImageIndex)
        {
            CurrentCorrectNumber++;
            if(CurrentCorrectNumber >= currentSequenceLength)
            {
                EndGame(true);
            }
            return true;
        }

        CurrentCorrectNumber = 0;
        OnWrongSequence?.Invoke();
        return false;
    }

    //    //}
    //}

    //private IEnumerator NextTurn()
    //{

    //    //StartCoroutine(UIManager.Instance.changeTurn(gameState == GameState.PLAYER_TURN));
    //    //yield return new WaitForSeconds(2);

    //    //if (gameState == GameState.PLAYER_TURN)
    //    //{
    //    //    gameState = GameState.ENEMY_TURN;
    //    //    StartCoroutine(PlayEnemyTurn());
    //    //}
    //    //else
    //    //{
    //    //    gameState = GameState.PLAYER_TURN;
    //    //    myPlayer.onTurnStart(cardsPerTurn);
    //    //}


    //}

    public void EndGame(bool victory)
    {
        if (victory)
        {
            gameState = GameState.VICTORY;
            OnGameStateChanged?.Invoke(gameState);
            levelIndex++;
        }
        else
        {
            gameState = GameState.GAME_OVER;
            OnGameStateChanged?.Invoke(gameState);
        }

        ResetGameState();
    }
    public void ResetGameState()
    {
        MaxTime = CurrentTime = 0f;
    }
    public void StartNewGame()
    {
        levelIndex = 0;
        initGameValues();

    }

    public void NextStage()
    {
        gameState = GameState.SOLUTION;
        OnGameStateChanged?.Invoke(gameState);
        LoadCurrentCharacter(levelIndex);
        ShowCurrentCharacter();

    }

    public void StartTimer()
    {
        gameState = GameState.PLAYING;
        OnGameStateChanged?.Invoke(gameState);

    }



    //private IEnumerator PlayEnemyTurn()
    //{
    //    Card cardToPlay = DrawRandomcard();
    //    enemyPlayer.PlayCard(cardToPlay, myPlayer);
    //    yield return UIManager.Instance.showEventNotification(false, cardToPlay);

    //    if (myPlayer.healthPoints <= 0)
    //        callGameEnded(false);
    //    else
    //    {
    //        turnCount++;
    //        StartCoroutine(NextTurn());
    //    }

    //}

    public void QuitGame()
    {
        Application.Quit();
    }


    public void Update()
    {
        if(gameState == GameState.PLAYING)
        {
            CurrentTime += Time.deltaTime;
            if (CurrentTime > MaxTime)
            {
                EndGame(false);
            }

        }
    }

    internal void UnselectEverything()
    {
        OnWrongSequence?.Invoke();
    }
}
