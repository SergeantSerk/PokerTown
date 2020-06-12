using System.Collections.Generic;

namespace PokerTown.Games.Blackjack
{
    public class BlackjackHand : Hand
    {
        public BlackjackHand() : base(new List<Card>())
        {

        }

        public bool IsOverStacked()
        {
            // 7 or more cards with sum value less than or equal to 21 trigger automatic win 
            return Cards.Count >= 7 && Value <= 21;
        }

        public bool IsWinning()
        {
            // must have at least 2 cards (sanity check) and must be 21 or less
            return Cards.Count >= 2 && Value <= 21;
        }

        public bool IsValidWin()
        {
            // true if blackjack or (7 or more cards and value still 21 or less)
            return IsBlackjack() || (Cards.Count >= 7 && Value <= 21);
        }

        public bool IsBlackjack()
        {
            return Cards.Count == 2 && Value == 21;
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
