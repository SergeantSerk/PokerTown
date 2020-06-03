using System;
using System.Collections.Generic;
using System.Text;
using static PokerTown.CardHelper;

namespace PokerTown
{
    internal class Card
    {
        internal Suit Suit { get; private set; }

        internal string Value { get; private set; }

        internal Card(Suit suit, string value)
        {
            Suit = suit;
            Value = value;
        }
    }
}
