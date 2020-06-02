using System;
using static PokerTown.Card;

namespace PokerTown.Games
{
    internal class Blackjack : IGame
    {
        public string Name => Resource.Blackjack_enGB;
        public string ShortDescription => Resource.Blackjack_enGB_SD;
        public string LongDescription => Resource.Blackjack_enGB_LD;

        public void Execute()
        {
            Console.WriteLine("Welcome to the dealer's table.");
            Console.WriteLine(RenderCard(Suit.Clubs, "10"));
        }

        private string RenderCard(Suit s, string v)
        {

            return $"┌─────┐{Environment.NewLine}" +
                   $"│{v,-2}   │{Environment.NewLine}" +
                   $"│  {s.ToSuitString()}  │{Environment.NewLine}" +
                   $"│   {v,2}│{Environment.NewLine}" +
                   $"└─────┘";

        }
    }
}
