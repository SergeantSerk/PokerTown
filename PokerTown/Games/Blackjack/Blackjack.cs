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

                Console.WriteLine("Dealer:");
                var dealer = new BlackjackPlayer(new Position(Console.CursorLeft, Console.CursorTop), new BlackjackHand());
                Console.SetCursorPosition(Console.CursorLeft, dealer.Position.Y + CardHelper.CardHeight + 1);

                Console.WriteLine("You: ");
                var player = new BlackjackPlayer(new Position(Console.CursorLeft, Console.CursorTop), new BlackjackHand());
                #endregion

                DealToPlayer(player, deck.Dequeue(), false);
                DealToPlayer(dealer, deck.Dequeue(), true);
                DealToPlayer(player, deck.Dequeue(), false);
                DealToPlayer(dealer, deck.Dequeue(), false);

                // check if dealer has 21
                string matchMessage = string.Empty;
                if (dealer.Hand.Value == 21)
                {
                    // flip over dealer's cards and display loss
                    foreach (var card in dealer.Hand.Cards)
                    {
                        card.Turned = false;
                    }
                    PrintHand(dealer.Position, dealer.Hand.Cards);

                    // check if player also has 21
                    if (player.Hand.Value == 21)
                    {
                        matchMessage = "Dealer has Blackjack, you too, it's a push.";
                        Console.Title = $"{Name} - Push";
                    }
                    else
                    {
                        matchMessage = "Dealer has Blackjack, you don't, you lost!";
                        Console.Title = $"{Name} - Lost";
                    }
                }
                else if (player.Hand.Value == 21)
                {
                    // player has blackjack, dealer doesn't
                    matchMessage = "You've got the Blackjack, you won!";
                    Console.Title = $"{Name} - Won";
                }
                else
                {
                    bool inProgress = true;
                    bool playerTurn = true;

                    do
                    {
                        if (playerTurn)
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

                            player.Choice = Program.AskPlayerChoice(choices.ToArray());
                            switch (player.Choice)
                            {
                                case PlayerChoice.Hit:
                                    DealToPlayer(player, deck.Dequeue(), false);
                                    break;
                                case PlayerChoice.DoubleDown:
                                    break;
                                case PlayerChoice.Stand:
                                    break;
                                default:
                                    break;
                            }

                            // player completes turn
                            playerTurn = false;
                        }
                        else
                        {
                            // dealer always hits
                            DealToPlayer(dealer, deck.Dequeue(), false);

                            playerTurn = true;
                        }
                    } while (inProgress);
                }

                bool? postChoice;
                do
                {
                    postChoice = Program.AskBinary($"{matchMessage} Continue playing?");
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
            PrintHand(player.Position, player.Hand.Cards);
        }

        private void PrintHand(Position position, ICollection<Card> cards)
        {
            Console.SetCursorPosition(position.X, position.Y);
            CardHelper.PrintCards(cards, CardHelper.SpacedCardOffset);
            Thread.Sleep(DealingDurationMilliseconds);
        }
    }
}
