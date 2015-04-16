using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets
{
    public static class Decks
    {
        public const int NumberOfDecks = 6;
        public const int CardsPerDeck = 52;
        public const int TotalNumberOfCards = NumberOfDecks * CardsPerDeck;

        public static Stack<Card> GetDecks(Random rng = null)
        {
            if (rng == null)
            {
                rng = new Random();
            }

            List<Card> decks = new List<Card>();
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    for (int currCard = 0; currCard < NumberOfDecks; currCard++)
                    {
                        decks.Add(new Card(rank, suit));
                    }
                }
            }

            return new Stack<Card>(decks.OrderBy(c => rng.NextDouble()));
        }
    }
}
