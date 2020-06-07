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

        private const int DealingDurationMilliseconds = 750;

        public void Execute()
        {
            Console.Clear();
            var playing = true;
            do
            {
                #region Introduction
                Console.Title = Name;
                Console.WriteLine("Welcome to the table." + Environment.NewLine);
                var table = new Table();
                var deck = BlackjackHelper.CreateDeck(shuffle: true);

                // reserve space for names
                var dealer = new BlackjackPlayer("Dealer", new Position(Console.CursorLeft, Console.CursorTop + 1), new BlackjackHand());
                Console.WriteLine($"{dealer.Name}:");
                Console.SetCursorPosition(Console.CursorLeft, dealer.Position.Y + CardHelper.CardHeight + 1);

                // reserve space for names
                var player = new BlackjackPlayer("You", new Position(Console.CursorLeft, Console.CursorTop + 1), new BlackjackHand());
                Console.WriteLine($"{player.Name}: ");
                #endregion

                string matchMessage = string.Empty;
                var inProgress = true;
                var playerTurn = true;

                do
                {
                    if (playerTurn)
                    {
                        if (player.Hand.Cards.Count < 2)
                        {
                            DealToPlayer(player, deck.Dequeue(), false);
                            // player completes draw
                            playerTurn = false;
                        }
                        else
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

                            do
                            {
                                player.Choice = Program.AskPlayerChoice(choices.ToArray());
                            } while (player.Choice == PlayerChoice.Invalid);

                            switch (player.Choice)
                            {
                                case PlayerChoice.Hit:
                                case PlayerChoice.DoubleDown:
                                    DealToPlayer(player, deck.Dequeue(), false);
                                    break;
                                case PlayerChoice.Stand:
                                case null:
                                default:
                                    // player chooses stand or does not have any available choices left (like previously
                                    // chose stand or double down)
                                    playerTurn = false;
                                    break;
                            }
                        }

                        if (player.Hand.Value == 21)
                        {
                            if (player.Hand.Cards.Count == 2)
                            {
                                // player has blackjack, dealer doesn't
                                matchMessage = "You've got 21, you won!";
                                Console.Title = $"{Name} - Won";
                            }
                            else
                            {
                                // player has 21, dealer doesn't
                                matchMessage = "You've got 21, you won!";
                                Console.Title = $"{Name} - Won";
                            }
                            inProgress = false;
                        }
                        else if (player.Hand.Value > 21)
                        {
                            // player has blackjack, dealer doesn't
                            matchMessage = "Unfortunate, you've gone over 21, you lost.";
                            Console.Title = $"{Name} - Bust";
                            inProgress = false;
                        }
                    }
                    else
                    {
                        // dealer always hits
                        // if hand is empty
                        if (dealer.Hand.Cards.Count < 2)
                        {
                            // draw a turned over card
                            DealToPlayer(dealer, deck.Dequeue(), dealer.Hand.Cards.Count == 0);
                            playerTurn = true;
                        }
                        else
                        {
                            DealToPlayer(dealer, deck.Dequeue(), false);
                        }

                        if (dealer.Hand.Value == 21)
                        {
                            // check if it is a straight blackjack
                            if (dealer.Hand.Cards.Count == 2)
                            {
                                // check if player also has 21
                                if (player.Hand.Value == 21)
                                {
                                    matchMessage = "Dealer has instant Blackjack, you too, it's a push.";
                                    Console.Title = $"{Name} - Push";
                                }
                                else
                                {
                                    matchMessage = "Dealer has instant Blackjack, you don't, you lost!";
                                    Console.Title = $"{Name} - Lost";
                                }
                            }
                            else
                            {
                                matchMessage = "Dealer has 21, you lost!";
                                Console.Title = $"{Name} - Lost";
                            }
                            inProgress = false;
                        }
                        else if (dealer.Hand.Value > 21)
                        {
                            matchMessage = "Dealer is bust, you won!";
                            Console.Title = $"{Name} - Won";
                            inProgress = false;
                        }
                    }
                } while (inProgress);

                // match should not be in progress
                // flip over dealer's cards
                foreach (var card in dealer.Hand.Cards)
                {
                    card.Turned = false;
                }
                PrintHand(dealer);
                PrintHand(player);

                bool? postChoice;
                do
                {
                    postChoice = Program.AskBinary($"{matchMessage} Play again?");
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

        private void DealToPlayer(Player player, Card card, bool turned)
        {
            card.Turned = turned;
            player.Hand.Add(card);
            PrintHand(player);
        }

        private void PrintHand(Player player)
        {
            Console.SetCursorPosition(player.Position.X, player.Position.Y);
            CardHelper.PrintCards(player.Hand.Cards, CardHelper.SpacedCardOffset);
            Thread.Sleep(DealingDurationMilliseconds);
        }
    }
}
