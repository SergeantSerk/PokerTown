using PokerTown.Games.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using static PokerTown.Games.Blackjack.BlackjackPlayer;

namespace PokerTown.Games.Blackjack
{
    public class Blackjack : IGame
    {
        public string Name => Resource.Blackjack_enGB;
        public string ShortDescription => Resource.Blackjack_enGB_SD;
        public string LongDescription => Resource.Blackjack_enGB_LD;

        public void Execute()
        {
            // "betting" screen
            Console.Clear();
            Console.Title = Name;
            var playing = true;

            BlackjackTable table;
            do
            {
                table = new BlackjackTable(this, new Position(Console.CursorLeft, Console.CursorTop));
                table.AddPlayer("Player 1");
                table.AddPlayer("Player 2");
                table.AddPlayer("Player 3");
                table.AddPlayer("Player 4");
                table.Play();

                bool? postChoice;
                do
                {
                    postChoice = Program.AskBinary($"{table.MatchMessage}{Environment.NewLine}Play again?");
                    if (postChoice == true)
                    {
                        Console.WriteLine("Starting another round.");
                        playing = true;
                    }
                    else if (postChoice == false)
                    {
                        Console.WriteLine("Leaving so soon?");
                        playing = false;
                    }
                } while (postChoice == null);
                Thread.Sleep(500);
                Console.Clear();
            } while (playing);
        }
    }
}
