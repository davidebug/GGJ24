using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{

    //const string GAME_VICTORY_TEXT = "PLAYER WINS THE MATCH!";
    //const string GAME_OVER_TEXT = "GAME OVER!";

    //public static UIManager Instance;

    //public MyPlayer player;
    //public Player enemyPlayer;

    //public Text enemyHpCount;
    //public Slider enemyHpSlider;

    //public Text playerHpCount;
    //public Slider playerHpSlider;

    //public Text deckCount;
    //public Text trashcount;
    //public Text actionPointsCount;
    //public Text playerTurn;
    //public Text currentTurn;

    //public List<RectTransform> cardsPositions;
    //private List<GameObject> cardsObjects = new List<GameObject>();
    //public EventNotificator eventNotificator;

    //public Text gameEndedText;
    //public GameObject gameEndedScreen;
    //private CanvasGroup playerTurnCanvas;

    private void Awake()
    {
        //playerTurnCanvas = playerTurn.GetComponent<CanvasGroup>();
        //Instance = this;
    }

    void Update()
    {
        //enemyHpCount.text = enemyPlayer.healthPoints.ToString();
        //enemyHpSlider.value = enemyPlayer.healthPoints;

        //playerHpCount.text = player.healthPoints.ToString();
        //playerHpSlider.value = player.healthPoints;

        //actionPointsCount.text = player.actionPoints.ToString();
        //deckCount.text = player.deck.Count.ToString();
        //trashcount.text = player.trash.Count.ToString();

        //currentTurn.text = GameManager.Instance.turnCount.ToString();

    }

    public IEnumerator showEventNotification(bool isMyPlayer, Card card)
    {

        //yield return eventNotificator.showEventNotification(isMyPlayer, card);
    }

    public IEnumerator changeTurn(bool isEnemyTurn)
    {
        //if (isEnemyTurn)
        //{
        //    playerTurn.text = "Enemy Turn";
        //    playerTurn.color = Color.red;
        //}
        //else
        //{
        //    playerTurn.text = "Your Turn";
        //    playerTurn.color = Color.green;
        //}
        //playerTurnCanvas.alpha = 0;
        //Vector3 startPosition = playerTurn.transform.position;
        //playerTurn.transform.position = new Vector3(Screen.width / 2, Screen.height / 1.5F, 0);
        //playerTurn.transform.localScale *= 3;
        //playerTurnCanvas.DOFade(1, 0.5F);
        //playerTurn.transform.DOScale(1F, 0.5F);
        //yield return new WaitForSeconds(1);
        //playerTurn.transform.DOMove(startPosition, 0.5F);
 
        
  
        
    }

    public void Playcard(CardUI cardObject)
    {
      //  cardObject.PlayCardAnimation();
 
    }

    public void IstanceCardObject(Card cardToIstance, int position)
    {
        //GameObject cardPrefab = Instantiate((GameObject)Resources.Load("Prefabs/CardPrefab"),
        //    cardsPositions[position].transform.position,
        //    Quaternion.identity,
        //    cardsPositions[position].transform);

        //CardUI cardUI = cardPrefab.GetComponent<CardUI>();
        //cardUI.card = cardToIstance;
        //cardsObjects.Add(cardPrefab);

    }

    //public void showGameEndedScreen(bool victory)
    //{
    //    if (victory)
    //        gameEndedText.text = GAME_VICTORY_TEXT;
    //    else
    //        gameEndedText.text = GAME_OVER_TEXT;
    //    gameEndedScreen.SetActive(true);
    //}
    //public void disableGameEndedScreen()
    //{
    //    gameEndedScreen.SetActive(false);
    //}

    //public void ClearCardsUI()
    //{
    //    foreach (GameObject obj in cardsObjects)
    //    {
    //        Destroy(obj);
    //    }
    //}
}
