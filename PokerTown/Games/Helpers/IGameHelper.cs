using System;
using System.Collections.Generic;
using System.Text;

namespace PokerTown.Games.Helpers
{
    public interface IGameHelper
    {
        public Queue<Card> CreateDeck(bool shuffle);
    }
}
