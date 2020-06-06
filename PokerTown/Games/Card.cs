using static PokerTown.Games.Helpers.CardHelper;

namespace PokerTown.Games
{
    public abstract class Card
    {
        internal int Identifier { get; }

        public Suit Suit { get; }

        public int Value { get; protected set; }

        public bool Turned { get; set; }

        public Card(Suit suit, int identifier)
        {
            Identifier = identifier;
            Suit = suit;
            Turned = false;
        }
    }
}
