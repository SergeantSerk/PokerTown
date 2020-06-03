using System;
using System.Collections.Generic;
using static PokerTown.Games.Helpers.CardHelper;

namespace PokerTown.Games.Helpers
{
    internal class BlackjackHelper : IGameHelper
    {
        Queue<Card> IGameHelper.CreateDeck(bool shuffle)
        {
            return CreateDeck(shuffle);
        }

        public static Queue<Card> CreateDeck(bool shuffle)
        {
            var cards = new Card[52];
            // for every suit
            for (int s = 0; s < 4; ++s)
            {
                // for every value
                for (int v = 0; v < 13; ++v)
                {
                    var suitOffset = s * 13;
                    cards[suitOffset + v] = new Card((Suit)s, v);
                }
            }

            if (shuffle)
            {
                var random = new Random();
                random.Shuffle(cards);
            }

            return new Queue<Card>(cards);
        }
    }
}
