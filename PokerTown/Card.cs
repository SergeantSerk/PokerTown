using System;
using System.Collections.Generic;
using System.Text;

namespace PokerTown
{
    internal static class Card
    {
        internal enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        internal static string ToSuitString(this Suit suit)
        {
            return suit switch
            {
                Suit.Clubs => "♣",
                Suit.Diamonds => "♦",
                Suit.Hearts => "♥",
                Suit.Spades => "♠",
                _ => string.Empty,
            };
        }
    }
}
