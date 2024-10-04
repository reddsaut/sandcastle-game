using System;
using UnityEngine;

public class CoinPlay : MonoBehaviour, CardPlay
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        if (gameManager == null)
        {
            throw new System.ArgumentNullException("game manager not found");
        }
    }
    public void Play()
    {
        gameManager.IncMoney(1);
    }
}
