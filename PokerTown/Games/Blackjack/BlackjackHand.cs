using System.Collections.Generic;

namespace PokerTown.Games.Blackjack
{
    public class BlackjackHand : Hand
    {
        public BlackjackHand() : base(new List<Card>())
        {

        }

        public override void Add(Card card)
        {
            Cards.Add(card);
            Value = GetHandValue(Cards);
        }

        // Solution with inspiration from https://brilliant.org/wiki/programming-blackjack/
        private int GetHandValue(ICollection<Card> hand)
        {
            int sum = 0;
            int aceCount = 0;
            foreach (var card in hand)
            {
                sum += card.Value;
                if (card.Identifier == 0)
                {
                    ++aceCount;
                }
            }

            while (aceCount > 0)
            {
                if (sum > 21)
                {
                    sum -= 10;
                    --aceCount;
                }
                else
                {
                    break;
                }
            }

            return sum;
        }
    }
}
