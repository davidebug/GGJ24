using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int maxHp = 10;
    
    public int healthPoints = 10;


    public void DecreaseHp(int damage)
    {
        healthPoints -= damage;
    }
    public void RestoreHp(int toRestore)
    {
        if(healthPoints < maxHp)
            healthPoints += toRestore;
    }

    public virtual void PlayCard(Card card, Player otherPlayer)
    {
            card.ApplyEffect(this, otherPlayer);
    }
}
 