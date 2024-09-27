using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DeckManager : MonoBehavior
{
    public List<Card> allCards = new List<Card>();
    private int currentIndex = 0;
    public DrawCard(HandManager handManager)
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
