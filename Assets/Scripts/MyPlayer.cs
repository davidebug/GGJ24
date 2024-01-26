using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayer : Player
{

    public int actionPoints = 1;

    private int requiredActionPoints = 0;
    private int totalActionPoints = 1;

    public List<Card> deck;
    public List<Card> trash;
    public List<Card> hand;



    public void Awake()
    {
        if (deck.Count == 0)
            deck = new List<Card>();

        trash = new List<Card>();
        hand = new List<Card>();

    }


    public void onTurnStart(int cardsPerTurn)
    {
        IncrementActionPoints();
        actionPoints = totalActionPoints;
        DrawMultipleCards(cardsPerTurn);
    }

    internal void initValues()
    {
        actionPoints = 1;
        totalActionPoints = 1;
        requiredActionPoints = 0;
        healthPoints = 10;
        MoveAllToTrash();
        deck.AddRange(trash);
        hand.Clear();
        trash.Clear();

    }

    public int GetActionPoints()
    {
        return actionPoints;
    }

    public void IncrementActionPoints()
    {
        actionPoints++;
        totalActionPoints++;
    }

    public Card DrawCardFromDeck()
    {
        if (deck.Count <= 0)
        {
            deck.AddRange(trash);
            trash.Clear();
        }
        int index = UnityEngine.Random.Range(0, deck.Count);
        Card drawedCard = deck[index];
        deck.RemoveAt(index);
        hand.Add(drawedCard);
        if (requiredActionPoints > drawedCard.cost)
            requiredActionPoints = drawedCard.cost;

        return drawedCard;
    }

    private void IstanceCurrentHand()
    {
        int handPosition = 0;
        foreach (Card card in hand)
        {
            UIManager.Instance.IstanceCardObject(card, handPosition);
            handPosition++;
        }
    }
    public bool CanStillPlay()
    {
        return hand.Count != 0 &&
            (actionPoints >= requiredActionPoints);
    }

    public void MoveToTrash(Card card)
    {
        trash.Add(card);
        card.isInTrash = true;
        hand.Remove(card);
        if (hand.Count != 0)
            requiredActionPoints = hand.Min(card => card.cost);
    }
    public void MoveAllToTrash()
    {
        List<Card> currentHand = new List<Card>();
        currentHand.AddRange(hand);
        foreach (Card card in currentHand)
        {
            MoveToTrash(card);
        }
    }

    public void DrawMultipleCards(int cardsToDraw)
    {
        for (int i = 0; i < cardsToDraw; i++)
        {
            DrawCardFromDeck();
        }
        if (!hand.Exists(x => x.cost <= totalActionPoints))
        {
            deck.AddRange(hand);
            hand.Clear();
            DrawMultipleCards(cardsToDraw);
        }
        IstanceCurrentHand();
    }

    public override void PlayCard(Card card, Player otherPlayer)
    {

        card.ApplyEffect(this, otherPlayer);
        MoveToTrash(card);
        actionPoints -= card.cost;
    }
}
