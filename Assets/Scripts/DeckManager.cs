using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    private int currentIndex = 0;

    private void Start()
    {
        Card[] cards = Resources.LoadAll<Card>("Cards");
        allCards.AddRange(cards);

        HandManager handManager = FindAnyObjectByType<HandManager>();
        for (int i = 0; i < 6; i++)
        {
            DrawCard(handManager);
        }
    }
    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        {
            return;
        }
        Card nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex = (currentIndex + 1) % allCards.Count;
    }
}
