using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards Container", menuName = "Cards/CardsContainer")]
public class CardsContainer : ScriptableObject
{
    public List<Card> cards;
}