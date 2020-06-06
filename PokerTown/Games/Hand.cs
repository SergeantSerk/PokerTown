using System.Collections.Generic;

namespace PokerTown.Games
{
    public abstract class Hand
    {
        public List<Card> Cards { get; private set; }

        public int Value { get; protected set; }

        public Hand(List<Card> cards)
        {
            Cards = cards;
        }

        public virtual void Add(Card card)
        {
            Cards.Add(card);
        }
    }
}
