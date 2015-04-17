namespace Assets
{
    public struct Card
    {
        public readonly CardRank Rank;
        public readonly CardSuit Suit;

        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public bool IsValidCard
        {
            get
            {
                return Rank != CardRank.Unknown && Suit != CardSuit.Unknown;
            }
        }
    }
}
