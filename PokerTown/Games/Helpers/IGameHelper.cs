using System.Collections.Generic;

namespace PokerTown.Games.Helpers
{
    public interface IGameHelper
    {
        public Queue<Card> CreateDeck(bool shuffle);
    }
}
