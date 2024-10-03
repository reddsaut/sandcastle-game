using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]

public class Card : ScriptableObject
{
    public string cardName;
    public int cost;
    public CardType cardType;

    public SubType subType;

    public string abilityText;

    public enum CardType
    {
        Resource,
        Building,
        Special
    }

    public enum SubType
    {
        Money1,
        House
    }
}
