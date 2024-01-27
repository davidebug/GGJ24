using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int levelIndex = 1;

    public Action OnCorrectSequence;
    public Action OnWrongSequence;
    public Action<bool> OnGameOver;

    void Start()
    {
        StartNewGame();
    }

    public void initGameValues()
    {

        gameState = GameState.PLAYING;
        //UIManager.Instance.ClearCardsUI();
        //UIManager.Instance.disableGameEndedScreen();

     
    }


    //public IEnumerator TryPlayCard()
    //{
    //    //if (cardObject.card.cost <= myPlayer.GetActionPoints() && gameState == GameState.PLAYER_TURN)
    //    //{
    //    //    gameState = GameState.PLAYING_CARD;
    //    //    myPlayer.PlayCard(cardObject.card, enemyPlayer);
    //    //    UIManager.Instance.Playcard(cardObject);
    //    //    yield return UIManager.Instance.showEventNotification(true, cardObject.card);

    //    //    Destroy(cardObject.gameObject);
    //    //    gameState = GameState.PLAYER_TURN;

    //    //    if (enemyPlayer.healthPoints <= 0)
    //    //        callGameEnded(true);
    //    //    else if (!myPlayer.CanStillPlay())
    //    //    {
    //    //        myPlayer.MoveAllToTrash();
    //    //        UIManager.Instance.ClearCardsUI();
    //    //        StartCoroutine(NextTurn());
    //    //    }

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

        }
        else
        {
            gameState = GameState.GAME_OVER;
        }

    }

    public void StartNewGame()
    {
        
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
