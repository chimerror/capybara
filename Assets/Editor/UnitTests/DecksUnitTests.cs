using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assets;

[TestFixture]
public class DecksUnitTests
{
    [Test]
    public void CanCreateDecks()
    {
        Stack<Card> decks = Decks.GetDecks(new Random(13));
        Assert.AreEqual(Decks.TotalNumberOfCards, decks.Count);
        foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
        {
            foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                Assert.AreEqual(Decks.NumberOfDecks,
                    decks.Count(c => c.Suit == suit && c.Rank == rank));
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
