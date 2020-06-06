using System;
using static PokerTown.Games.Helpers.CardHelper;

namespace PokerTown.Games.Blackjack
{
    public class BlackjackCard : Card
    {
        public BlackjackCard(Suit suit, int identifier) : base(suit, identifier)
        {
            if (identifier == 0)
            {
                // 0 == A
                Value = 11;
            }
            else if (identifier >= 1 && identifier <= 9)
            {
                // 1,2,3,4,5,6,7,8,9 =
                // 2,3,4,5,6,7,8,9,10
                Value = (identifier + 1);
            }
            else if (identifier >= 10 && identifier <= 12)
            {
                // 10,11,12 = J,Q,K
                Value = 10;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"No card value matches {identifier}");
            }
        }

        /// <summary>
        /// Convert raw card <paramref name="identifier"/> to letter representation on real cards.
        /// </summary>
        public override string ToString()
        {
            if (Identifier == 0)
            {
                return "A";
            }
            else if (Identifier >= 1 && Identifier <= 9)
            {
                return (Identifier + 1).ToString();
            }
            else if (Identifier >= 10 && Identifier <= 12)
            {
                return Identifier switch
                {
                    10 => "J",
                    11 => "Q",
                    12 => "K",
                    _ => throw new InvalidOperationException($"Unable to find case for {Identifier} to card value"),
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException($"No card value matches {Identifier}");
            }
        }
    }
}
