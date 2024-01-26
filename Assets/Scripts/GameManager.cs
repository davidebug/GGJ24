using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PLAYER_TURN,
    ENEMY_TURN,
    GAME_OVER,
    VICTORY,
    PLAYING_CARD
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public CardsContainer cardsDatabase;

    public GameState gameState;

    [SerializeField]
    private MyPlayer myPlayer;

    [SerializeField]
    private Player enemyPlayer;

    [SerializeField]
    private int cardsPerTurn = 3;

    public int turnCount = 1;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartNewGame();
    }

    public void initGameValues()
    {
        myPlayer.initValues();
        turnCount = 1;
        gameState = GameState.PLAYER_TURN;
        enemyPlayer.healthPoints = 10;
        UIManager.Instance.ClearCardsUI();
        UIManager.Instance.disableGameEndedScreen();
    }


    public IEnumerator TryPlayCard(CardUI cardObject)
    {
        if (cardObject.card.cost <= myPlayer.GetActionPoints() && gameState == GameState.PLAYER_TURN)
        {
            gameState = GameState.PLAYING_CARD;
            myPlayer.PlayCard(cardObject.card, enemyPlayer);
            UIManager.Instance.Playcard(cardObject);
            yield return UIManager.Instance.showEventNotification(true, cardObject.card);

            Destroy(cardObject.gameObject);
            gameState = GameState.PLAYER_TURN;

            if (enemyPlayer.healthPoints <= 0)
                callGameEnded(true);
            else if (!myPlayer.CanStillPlay())
            {
                myPlayer.MoveAllToTrash();
                UIManager.Instance.ClearCardsUI();
                StartCoroutine(NextTurn());
            }

        }
    }

    private IEnumerator NextTurn()
    {

        StartCoroutine(UIManager.Instance.changeTurn(gameState == GameState.PLAYER_TURN));
        yield return new WaitForSeconds(2);

        if (gameState == GameState.PLAYER_TURN)
        {
            gameState = GameState.ENEMY_TURN;
            StartCoroutine(PlayEnemyTurn());
        }
        else
        {
            gameState = GameState.PLAYER_TURN;
            myPlayer.onTurnStart(cardsPerTurn);
        }


    }

    private void callGameEnded(bool victory)
    {
        if (victory)
            gameState = GameState.VICTORY;
        else
            gameState = GameState.GAME_OVER;

        UIManager.Instance.showGameEndedScreen(victory);
    }

    public void StartNewGame()
    {
        initGameValues();
        myPlayer.DrawMultipleCards(cardsPerTurn);

    }

    private Card DrawRandomcard()
    {
        int index = UnityEngine.Random.Range(0, cardsDatabase.cards.Count);
        return cardsDatabase.cards[index];
    }

    private IEnumerator PlayEnemyTurn()
    {
        Card cardToPlay = DrawRandomcard();
        enemyPlayer.PlayCard(cardToPlay, myPlayer);
        yield return UIManager.Instance.showEventNotification(false, cardToPlay);

        if (myPlayer.healthPoints <= 0)
            callGameEnded(false);
        else
        {
            turnCount++;
            StartCoroutine(NextTurn());
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
