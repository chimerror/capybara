using Assets;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

[TestFixture]
public class DecksUnitTests
{
    [Test]
    public void CanCreateDecks()
    {
        Stack<Card> decks = Decks.GetDecks(new Random(13));
        decks.Count.Should<int>().Be(Decks.TotalNumberOfCards);
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                int expectedNumber =
                    suit == CardSuit.Unknown || rank == CardRank.Unknown ? 0 : Decks.NumberOfDecks;
                decks.Count(c => c.Suit == suit && c.Rank == rank).Should<int>().Be(expectedNumber);
            }
        }
    }

    [Test]
    public void CanExhaustDecks()
    {
        Stack<Card> decks = Decks.GetDecks(new Random(13));

        do
        {
            Card currCard = decks.Pop();
            Console.WriteLine("{0} of {1}", currCard.Rank, currCard.Suit);
        } while (decks.Count > 0);
    }
}
