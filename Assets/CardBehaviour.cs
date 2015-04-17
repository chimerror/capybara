using UnityEngine;
using System.Collections;
using System.Linq;
using Assets;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class CardBehaviour : MonoBehaviour
{
    private static Regex _cardNameRegex = new Regex(@"card(?<suit>Clubs|Hearts|Diamonds|Spades)(?<rank>[2-9AJKQ]|10)\.png");
    private static Dictionary<string, Sprite> _cardBacks;
    private static Dictionary<Card, Sprite> _cardFronts;
    private SpriteRenderer _spriteRenderer;

    public Card Card = new Card(CardRank.Ace, CardSuit.Spades);
    public bool FaceUp = true;

    public void Start ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_cardBacks == null)
        {
            _cardBacks = Resources.LoadAll<Sprite>("playingCardBacks").ToDictionary(c => c.name);
        }

        if (_cardFronts == null)
        {
            _cardFronts = Resources.LoadAll<Sprite>("playingCards")
                .Where(c => _cardNameRegex.IsMatch(c.name))
                .ToDictionary(c => ConvertSpriteNameToCard(c.name));
        }

        UpdateRenderer();
    }

    public void Update ()
    {
        UpdateRenderer();
    }

    private static Card ConvertSpriteNameToCard(string name)
    {
        Match match = _cardNameRegex.Match(name);
        if (!match.Success)
        {
            Debug.LogErrorFormat("Unable to extract card from sprite name: {0}", name);
            return new Card();
        }

        string rankName = match.Groups["rank"].Value;
        CardRank rank = CardRank.Unknown;
        try
        {
            switch (rankName)
            {
                case "A":
                    rank = CardRank.Ace;
                    break;

                case "K":
                    rank = CardRank.King;
                    break;

                case "Q":
                    rank = CardRank.Queen;
                    break;

                case "J":
                    rank = CardRank.Jack;
                    break;

                default:
                    rank = (CardRank)Enum.Parse(typeof(CardRank), rankName);
                    break;
            }
        }
        catch (ArgumentException)
        {
            Debug.LogErrorFormat("Unknown card rank: {0}", rankName);
        }

        string suitName = match.Groups["suit"].Value;
        CardSuit suit = CardSuit.Unknown;
        try
        {
            suit = (CardSuit) Enum.Parse(typeof(CardSuit), suitName);
        }
        catch (ArgumentException)
        {
            Debug.LogErrorFormat("Unknown card suit: {0}", suitName);
        }

        return new Card(rank, suit);
    }

    private void UpdateRenderer()
    {
        if (FaceUp)
        {
            try
            {
                _spriteRenderer.sprite = _cardFronts[Card];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogErrorFormat("Missing Card: {0} of {1}", Card.Rank, Card.Suit);
            }
        }
        else
        {
            _spriteRenderer.sprite = _cardBacks["cardBack_green2.png"];
        }
    }
}
