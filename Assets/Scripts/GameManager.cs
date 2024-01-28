using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;


public enum GameState
{
    SOLUTION,
    PLAYING,
    GAME_OVER,
    VICTORY,
    WON_GAME
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

    public RectTransform bodyRectTransformPlaceHolder;
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
        Assert.IsNotNull(levelDatasSO);
        StartNewGame();
    }

    public void StartNewGame()
    {

        levelIndex = 0;
        LoadStage(levelIndex);

    }

    public void ResetGame()
    {
        
        levelIndex = 0;
        LoadStage(levelIndex);
        gameState = GameState.SOLUTION;
        OnGameStateChanged?.Invoke(gameState);
        //UIManager.Instance.ClearCardsUI();
        //UIManager.Instance.disableGameEndedScreen();


    }


    public void LoadStage(int levelIndex)
    {

        if(currentCharacter != null)
        {
            Destroy(currentCharacter);
        }
        currentCharacter = Instantiate(levelDatasSO.GetCharacter(levelIndex));

        currentCharacter.gameObject.transform.SetParent(bodyRectTransformPlaceHolder, true);
        
        currentCharacter.gameObject.SetActive(false);

        CurrentTime = 0;
        MaxTime = currentCharacter.MaxTime;
        CorrectSequenceOrder = currentCharacter.sequenceOrder;
        currentSequenceLength = CorrectSequenceOrder.Length;
        CurrentCorrectNumber = 0;

        // Show Solution through popup
        gameState = GameState.SOLUTION;
        OnGameStateChanged?.Invoke(gameState);
    }

    public void StartGame()
    {
        gameState = GameState.PLAYING;
        OnGameStateChanged?.Invoke(gameState);

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
            OnCorrectSequence?.Invoke();
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

   

    public void WinGame()
    {
        gameState = GameState.WON_GAME;
        SceneManager.LoadScene("WinScene");
    }

    public void EndGame(bool victory)
    {
        if (victory)
        {

            currentCharacter.MakeSmile();
            AudioManager.Get().PlayLaugh(levelIndex);
            StartCoroutine(WaitForVictory());

        }
        else
        {
            levelIndex = 0;
            gameState = GameState.GAME_OVER;
            OnGameStateChanged?.Invoke(gameState);
        }

        if (levelIndex > levelDatasSO.characters.Length)
        {
            WinGame();
        }

        CurrentTime = 0f;
    }

    IEnumerator WaitForVictory()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(2);

        gameState = GameState.VICTORY;
        OnGameStateChanged?.Invoke(gameState);
        levelIndex++;
    }
    public void NextStage()
    {
        LoadStage(levelIndex);
    }

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

            // Cheat modet, win the game
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.W))
            {
                WinGame();
            }
        }
    }

    internal void UnselectEverything()
    {
        AudioManager.Get().PlayWoorp();
        OnWrongSequence?.Invoke();
    }
}
