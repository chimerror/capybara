using UnityEngine;
using System;
using System.Collections;
using Assets;
using System.Collections.Generic;

[IntegrationTest.DynamicTest("DecksIntegrationTests")]
[IntegrationTest.Timeout(60)]
public class DeckCanBeExhaustedBehaviour : MonoBehaviour
{
    private const int _seed = 13;
    private Stack<Card> _decks;
    private bool _deckExhausted = false;
    private CardBehaviour _cardBehaviour;

    void Start()
    {
        _decks = Decks.GetDecks(new System.Random(_seed));
        _cardBehaviour = GameObject.FindGameObjectWithTag("CardPrefab").GetComponentInParent<CardBehaviour>();
        _cardBehaviour.FaceUp = false;
        _cardBehaviour.Card = _decks.Pop();
    }

    void Update()
    {
        if (_decks.Count > 0)
        {
            if (_cardBehaviour.FaceUp)
            {
                _cardBehaviour.FaceUp = false;
                _cardBehaviour.Card = _decks.Pop();
            }
            else
            {
                _cardBehaviour.FaceUp = true;
            }
        }
        else if (!_deckExhausted)
        {
            IntegrationTest.Pass();
            _deckExhausted = true;
        }
    }
}
