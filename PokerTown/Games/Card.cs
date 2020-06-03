using System;
using System.Collections.Generic;
using System.Text;
using static PokerTown.Games.Helpers.CardHelper;

namespace PokerTown.Games
{
    public class Card
    {
        public Suit Suit { get; private set; }

        public int Value { get; private set; }

        public Card(Suit suit, int value)
        {
            Suit = suit;
            Value = value;
        }
    }
}
