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

    public Sprite[] images;
    public Image image;


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

        image.sprite = images[(int) cardData.subType];

        if(cardData.cardType == Card.CardType.Resource) { // resource (money) cards have no cost
            costText.text = "";
        }
    }
}
