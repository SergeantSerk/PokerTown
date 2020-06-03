using PokerTown.Games.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace PokerTown.Games.Blackjack
{
    internal class Blackjack : IGame
    {
        public string Name => Resource.Blackjack_enGB;
        public string ShortDescription => Resource.Blackjack_enGB_SD;
        public string LongDescription => Resource.Blackjack_enGB_LD;

        private const int DealingDurationMilliseconds = 1000;


        public void Execute()
        {
            Console.Clear();
            Console.Title = "Blackjack";

            var playing = true;
            do
            {
                #region Introduction
                Console.WriteLine("Welcome to the dealer's table." + Environment.NewLine);
                var deck = BlackjackHelper.CreateDeck(shuffle: true);

                Console.WriteLine("Dealer:");
                var dealerHand = new List<Card>();
                var dealerPos = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
                Console.SetCursorPosition(Console.CursorLeft, dealerPos.Item2 + CardHelper.CardHeight + 1);

                Console.WriteLine("You: ");
                var playerHand = new List<Card>();
                var playerPos = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
                #endregion

                DealToPlayer(playerPos, playerHand, deck.Dequeue());
                DealToPlayer(dealerPos, dealerHand, deck.Dequeue());
                DealToPlayer(playerPos, playerHand, deck.Dequeue());

                Console.SetCursorPosition(0, Console.WindowHeight - 5);
                bool? choice;
                do
                {
                    choice = Program.AskBinary("Continue playing?");
                    if (choice == true)
                    {
                        Console.WriteLine("Starting another round.");
                        playing = true;
                    }
                    else if (choice == false)
                    {
                        Console.WriteLine("Leaving so soon?");
                        playing = false;
                    }
                } while (choice == null);
                Thread.Sleep(1000);
                Console.Clear();
            } while (playing);
        }
        
        private void DealToPlayer(Tuple<int, int> position, ICollection<Card> hand, Card card)
        {
            hand.Add(card);
            Console.SetCursorPosition(position.Item1, position.Item2);
            CardHelper.PrintCards(hand, CardHelper.PackedCardOffset);
            Thread.Sleep(1000);
        }
    }
}
