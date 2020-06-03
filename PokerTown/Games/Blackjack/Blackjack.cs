using System;
using System.Collections.Generic;
using static PokerTown.CardHelper;

namespace PokerTown.Games.Blackjack
{
    internal class Blackjack : IGame
    {
        public string Name => Resource.Blackjack_enGB;
        public string ShortDescription => Resource.Blackjack_enGB_SD;
        public string LongDescription => Resource.Blackjack_enGB_LD;

        public void Execute()
        {
            Console.Clear();
            Console.Title = "Blackjack";
            Console.WriteLine("Welcome to the dealer's table.");

            var playing = true;
            do
            {
                int cardCount = 10;
                HashSet<Card> cards = new HashSet<Card>();
                for (int i = 0; i < cardCount; ++i)
                {
                    cards.Add(GetRandomCard());
                }

                PrintCards(cards, 8);
                Console.ReadLine();
                Console.Clear();
            } while (playing);
        }
    }
}
