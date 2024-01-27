using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;

public enum GameState
{
    START_MENU,
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
    public Action OnStageBegin;
    public Action<bool> OnGameOver;

    public int CurrentCorrectNumber;
    public int TotalSequenceLength
    {
        get { return CorrectSequenceOrder.Length; }
    }
    public int[] CorrectSequenceOrder;

    public int CurrentTime;
    public int MaxTime;

    private UIManager UIManager;
    public Character currentCharacter;
    void Start()
    {
        UIManager = UIManager.Get();
        Assert.IsNotNull(UIManager);
        Assert.IsNotNull(levelDatasSO);
        StartNewGame();
        
    }

    public void initGameValues()
    {

        gameState = GameState.PLAYING;
        CurrentTime = 0;
        LoadCurrentCharacter(levelIndex);
        OnStageBegin?.Invoke();
        //UIManager.Instance.ClearCardsUI();
        //UIManager.Instance.disableGameEndedScreen();

        
    }

    public void LoadCurrentCharacter(int levelIndex)
    {
        int currentCharacterIndex = Math.Min(levelIndex, levelDatasSO.characters.Length);
        currentCharacter = Instantiate(levelDatasSO.GetCharacter(currentCharacterIndex), UIManager.transform);

        // put character in the UI
        MaxTime = currentCharacter.MaxTime;
        CorrectSequenceOrder = currentCharacter.sequenceOrder;

    }

    public void SelectBodyPart(int bodyPartIndex)
    {

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

    public void GameOver(bool victory)
    {
        if (victory)
        {
            gameState = GameState.VICTORY;
            levelIndex++;
        }
        else
        {
            gameState = GameState.GAME_OVER;
          
        }

    }
    public void CleanGameScene()
    {
        GameObject.Destroy(currentCharacter);
        MaxTime = CurrentTime = 0;

    }
    public void StartNewGame()
    {
        levelIndex = 0;
        initGameValues();

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

}
