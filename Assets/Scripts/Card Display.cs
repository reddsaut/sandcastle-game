using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardDisplay : MonoBehaviour
{
    public Card cardData; 
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text abilityText;
    public Sprite[] backgrounds;
    public Image background;


    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        background.sprite = backgrounds[(int) cardData.cardType];
        nameText.text = cardData.name;
        costText.text = cardData.cost.ToString();
        abilityText.text = cardData.abilityText;

        
    }
}
