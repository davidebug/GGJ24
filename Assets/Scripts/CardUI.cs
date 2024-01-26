using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CardUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Card card;

    private string id;
    public Text descriptionText;
    public Text costText;
    public Image cardImage;
    private Vector3 originalPosition;
    private CanvasGroup canvasGroup;

    private void Awake()
    {       
        originalPosition = gameObject.transform.position;
        gameObject.transform.position = UIManager.Instance.deckCount.transform.position;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        id = card.id;
        descriptionText.text = card.effectDescription;
        costText.text = card.cost.ToString();
        cardImage.sprite = card.sprite;
        gameObject.transform.DOMove(originalPosition, 0.7F);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

            transform.DOLocalMoveY(300, 0.3F);
            transform.DOScale(1.2F, 0.3F);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
   
            transform.DOLocalMoveY(0, 0.3F);
            transform.DOScale(1F, 0.3F);
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameManager.Instance.gameState == GameState.PLAYER_TURN)
            StartCoroutine(GameManager.Instance.TryPlayCard(this));
    }
    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
    public void PlayCardAnimation()
    {
        transform.DOLocalMoveY(500, 0.5F);
        transform.DOScale(3, 0.5F);
        canvasGroup.DOFade(0, 0.5F);
        
    }
}