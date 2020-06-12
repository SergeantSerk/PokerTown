using PokerTown.Games.Helpers;
using PokerTown.Games.Misc;
using System;
using System.Collections.Generic;
using static PokerTown.Games.Blackjack.BlackjackPlayer;
using static PokerTown.Games.Helpers.CardHelper;

namespace PokerTown.Games.Blackjack
{
    internal class BlackjackHelper : IGameHelper
    {
        Queue<Card> IGameHelper.CreateDeck(bool shuffle)
        {
            return CreateDeck(shuffle);
        }

        public static PlayerChoice? AskPlayerChoice(BlackjackPlayer player)
        {
            var choices = new List<PlayerChoice>();
            if (player.Choice == null)
            {
                choices.Add(PlayerChoice.Hit);
                choices.Add(PlayerChoice.Stand);
                choices.Add(PlayerChoice.DoubleDown);
            }
            else if (player.Choice == PlayerChoice.Hit)
            {
                choices.Add(PlayerChoice.Hit);
                choices.Add(PlayerChoice.Stand);
            }
            // Stand = no further actions allowed
            // Double down = no further actions allowed

            var question = string.Join('/', choices);
            Program.Ask(question);
            Console.Write($"[{player.Name}] H/S/D: ");
            var response = Console.ReadKey();
            Console.WriteLine(Environment.NewLine);
            return response.Key switch
            {
                ConsoleKey.H => PlayerChoice.Hit,
                ConsoleKey.S => PlayerChoice.Stand,
                ConsoleKey.D => PlayerChoice.DoubleDown,
                _ => PlayerChoice.Invalid,
            };
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
                    cards[suitOffset + v] = new BlackjackCard((Suit)s, v);
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
