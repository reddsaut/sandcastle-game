using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private int money;
    public OptionsManager optionsManager { get; private set; }
    public AudioManager audioManager { get; private set; }
    public DeckManager deckManager { get; private set; }
    public HandManager handManager;

    public GameObject housePrefab;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void InitializeManagers()
    {
        optionsManager = GetComponentInChildren<OptionsManager>();
        audioManager = GetComponentInChildren<AudioManager>();
        deckManager = GetComponentInChildren<DeckManager>();
        handManager = GetComponentInChildren<HandManager>();
        if (optionsManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/OptionsManager");
            if(prefab == null)
            {
                Debug.Log($"OptionsManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                optionsManager = GetComponentInChildren<OptionsManager>();
            }
        }
        if (audioManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/AudioManager");
            if (prefab == null)
            {
                Debug.Log($"AudioManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                audioManager = GetComponentInChildren<AudioManager>();
            }
        }
        if (deckManager == null)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/DeckManager");
            if (prefab == null)
            {
                Debug.Log($"DeckManager prefab not found");
            }
            else
            {
                Instantiate(prefab, transform.position, Quaternion.identity, transform);
                deckManager = GetComponentInChildren<DeckManager>();
            }
        }
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
            return true;
        }
    }

    public void IncMoney(int money)
    {
        this.money += money;
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
        Card.SubType subType= card.GetComponent<CardDisplay>().cardData.subType;
        Debug.Log("" + subType.ToString());
        switch (subType)
        {
            case Card.SubType.Money1:
                IncMoney(1); // add 1 coin
                handManager.RemoveCardFromHand(card);
                Destroy(card);
                break;
            case Card.SubType.House:
                if(Buy(1)) {
                    Instantiate(housePrefab, new Vector3(0,0,0), Quaternion.identity); // house builder mode
                    handManager.RemoveCardFromHand(card);
                    Destroy(card);
                }
                break;
        }
    }
}
