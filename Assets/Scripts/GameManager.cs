using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int money;
    public DeckManager deckManager;
    public HandManager handManager;
    public GameObject housePrefab;

    public TMP_Text moneyText;

    void Start()
    {
        moneyText.text = "" + money;
    }
    public bool Buy(int cost)
    {
        if(cost > money)
        {
            return false;
        }
        else
        {
            money -= cost;
            moneyText.text = "" + this.money;
            return true;
        }
    }

    public void IncMoney(int money)
    {
        this.money += money;
        moneyText.text = "" + this.money;
    }

    public int GetMoney()
    {
        return money;
    }

    public void Play(GameObject card)
    {
        if(card == null || card.GetComponent<CardDisplay>() == null)
        {
            return;
        }
        Card.SubType subType = card.GetComponent<CardDisplay>().cardData.subType;
        switch (subType)
        {
            case Card.SubType.Money1:
                deckManager.discardPile.Add(card.GetComponent<CardDisplay>().cardData);
                IncMoney(1); // add 1 coin
                handManager.RemoveCardFromHand(card);
                StartCoroutine(CardFade(card));
                break;
            case Card.SubType.House:
                if(Buy(card.GetComponent<CardDisplay>().cardData.cost)) {
                    deckManager.discardPile.Add(card.GetComponent<CardDisplay>().cardData);
                    Instantiate(housePrefab, new Vector3(0,0,0), Quaternion.identity); // house builder mode
                    handManager.RemoveCardFromHand(card);
                    StartCoroutine(CardFade(card));
                }
                break;
        }
    }

    public void AdvanceTurn()
    {
        money = 0;
        moneyText.text = "" + money;
        // shuffle if needed and draw new cards
        Debug.Log("ooh that tickles");
    }

    IEnumerator CardFade(GameObject card)
    {
        Destroy(card.GetComponent<CardMovement>());
        for(int i = 0; i < 20; i++)
        {
            card.transform.Rotate(0, 0, 360f/20);
            card.transform.localScale /= 1.1f;
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(card);
    }
}
