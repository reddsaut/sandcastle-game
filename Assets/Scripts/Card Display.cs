using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardDisplay : MonoBehaviour
{
    public Card cardData; 
    public Image cardImage;
    public TMP_Text nameText;
    public TMP_Text costText;
    public Image[] typeImages;
    private Color[] typeColors = {
        Color.yellow,
        Color.blue,
        Color.magenta
    };


    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        cardImage.color = typeColors[(int)cardData.cardType];
        nameText.text = cardData.name;
        costText.text = cardData.cost.ToString();
        
    }
}
