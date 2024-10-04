using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    public List<Card> discardPile = new List<Card>();

    private int startingHandSize = 5;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager handManager;
    void Start()
    {
        //Load all card assets from the Resources folder
        Card[] cards = Resources.LoadAll<Card>("Cards");

        //Add the loaded cards to the allCards list
        allCards.AddRange(cards);

        handManager = FindAnyObjectByType<HandManager>();
        maxHandSize = handManager.maxHandSize;
        NewHand();
    }

    public void NewHand()
    {
        StartCoroutine(DrawHand());
    }

    void Update()
    {
        if (handManager != null)
        {
            currentHandSize = handManager.cardsInHand.Count;
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        {
            foreach (Card card in discardPile)
            {
                allCards.Add(card);
            }
            discardPile.Clear();
            Shuffler.Shuffle<Card>(allCards);
        }
        if (currentHandSize < maxHandSize)
        {
            handManager.AddCardToHand(allCards[0]);
            allCards.Remove(allCards[0]);
        }
    }

    IEnumerator DrawHand()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            Debug.Log($"Drawing Card");
            DrawCard(handManager);
            yield return new WaitForSeconds(1f / 10);
        }
    }
}
