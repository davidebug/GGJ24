using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EventNotificator : MonoBehaviour
{

    public Text eventText;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private IEnumerator doAnimation()
    {
        canvasGroup.DOFade(0.7F, 0.5F);
        yield return new WaitForSeconds(1.5F);
        canvasGroup.DOFade(0, 0.5F);
    }
    public IEnumerator showEventNotification(bool isMyPlayer, Card card)
    {
        DOTween.Complete(canvasGroup);
        string currentPlayer = isMyPlayer ? "Player" : "Enemy";


        if (card is DamageCard)
        {

            string otherPlayer = !isMyPlayer ? "Player" : "Enemy";
            int damage = ((DamageCard)card).damage;
            eventText.text = currentPlayer + " dealt " + damage.ToString() + " damage to " + otherPlayer;

        }
        else if (card is GreenCard)
        {
            int restorePower = ((GreenCard)card).restorePower;
            eventText.text = currentPlayer + " restored " + restorePower.ToString() + " HP";
        }
        else if (card is YellowCard && isMyPlayer)
        {
            eventText.text = currentPlayer + " earned 1 Bonus Action Point";
        }
        yield return doAnimation();
    }


}
