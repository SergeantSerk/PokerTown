using PokerTown.Games.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;
using static PokerTown.Games.Blackjack.BlackjackPlayer;

namespace PokerTown.Games.Blackjack
{
    internal struct PlayerData
    {
        internal BlackjackPlayer Player;
        internal Position Position;

        internal PlayerData(BlackjackPlayer player, Position position)
        {
            Player = player;
            Position = position;
        }
    }

    internal class BlackjackTable
    {
        private const int DealingDurationMilliseconds = 100;

        private Blackjack game;
        private Position dealerPosition;
        private Position playerPosition;

        public string MatchMessage { get; private set; }
        public readonly Queue<PlayerData> Players;

        public BlackjackTable(Blackjack game, Position dealerPosition)
        {
            this.game = game;
            this.dealerPosition = dealerPosition;
            playerPosition = dealerPosition;
            MatchMessage = string.Empty;

            Players = new Queue<PlayerData>();
        }

        public BlackjackPlayer AddPlayer(string name)
        {
            var player = new BlackjackPlayer(name, false);
            // derive new player's position using old player's position
            var position = new Position(playerPosition.X, playerPosition.Y + CardHelper.CardHeight + 2);
            playerPosition = position;
            var tuple = new PlayerData(player, position);
            Players.Enqueue(tuple);
            return player;
        }

        public void RemovePlayer(string name)
        {
            // loop through all players
            for (int i = 0; i < Players.Count; ++i)
            {
                var player = Players.Dequeue();
                // if player's name does not match parameter
                if (!player.Player.Name.Equals(name))
                {
                    // add back into queue
                    Players.Enqueue(player);
                }
                else
                {
                    // found, break loop
                    break;
                }
            }
        }

        public void Play()
        {
            if (Players.Count == 0)
            {
                Console.WriteLine("Dealer cannot play on its own.");
                return;
            }

            // enqueue the dealer
            //var dealer = new BlackjackPlayer("Dealer", true);
            Players.Enqueue(new PlayerData(new BlackjackPlayer("Dealer", true), dealerPosition));

            // initialisation (hand out cards in sequence)
            var players = Players.ToArray();
            var playerCount = players.Length;

            // dish out everyone 2 cards (dealer first card turned)
            var deck = BlackjackHelper.CreateDeck(shuffle: true);
            for (int i = 0; i < 2; ++i)
            {
                foreach (var player in players)
                {
                    // if player is dealer and has no cards in hand, turn over card
                    DealToPlayer(player.Player, deck.Dequeue(), player.Player.Dealer && player.Player.Hand.Cards.Count == 0);
                    PrintTable(players);
                }
            }
            // everyone should have 2 cards at this stage (dealer has one turned over)

            var results = new Dictionary<BlackjackPlayer, PlayerResult>();
            // check for premature win conditions (can never have hand value > 21)
            for (int i = 0; i < playerCount; ++i)
            {
                var data = Players.Dequeue();
                if (data.Player.Hand.IsBlackjack())
                {
                    results.Add(data.Player, PlayerResult.Win);
                }
                else
                {
                    Players.Enqueue(data);
                }
            }

            if (results.Count == playerCount)
            {
                // there are same amount of winners as there with players, everyone won
                var names = new string[playerCount];
                for (int i = 0; i < names.Length; ++i)
                {
                    names[i] = players[i].Player.Name;
                }
                MatchMessage = $"{string.Join(", ", names)} (everyone) has Blackjack!";
            }
            else
            {
                // loop through queue to imitate dequeuing and prevent replaying of
                // impossible turns
                // 1 player must be left (which is dealer)
                do
                {
                    var dataPair = Players.Dequeue();
                    // if current player has already won
                    if (results.ContainsKey(dataPair.Player))
                    {
                        // don't queue
                        continue;
                    }
                    else if (dataPair.Player.Dealer && Players.Count == 0)
                    {
                        // player count == 0 since last player was dequeued
                        // turn over dealer cards
                        // flip over dealer's cards
                        var dealer = dataPair;
                        foreach (var card in dealer.Player.Hand.Cards)
                        {
                            card.Turned = false;
                        }

                        // find the highest hand value amongst all players
                        var max = 0;
                        foreach (var player in players)
                        {
                            // find the highest value hand while not losing the game (between 0 and 21 inclusive)
                            max = Math.Max(max, Math.Min(21, player.Player.Hand.Value));
                        }

                        // TO-DO: dealer logic
                        // begin dealer drawing (we should have one more player remaining, dealer)
                        while (dealer.Player.Hand.Value < 21)
                        {
                            DealToPlayer(dealer.Player, deck.Dequeue(), false);
                            PrintTable(players);

                            if (dealer.Player.Hand.IsBlackjack() || dealer.Player.Hand.IsOverStacked() || dealer.Player.Hand.Value == 17)
                            {
                                // dealer has at least push or dealer stands on soft 17
                                break;
                            }
                            else if (!dealer.Player.Hand.IsWinning())
                            {
                                // dealer is no longer winning, is bust
                                results.Add(dealer.Player, PlayerResult.Bust);
                                break;
                            }
                        }

                        // compare dealer hand with other hands
                        // check each player's hand against dealer's
                        foreach (var player in players)
                        {
                            if (player.Player.Dealer)
                            {
                                // don't compare dealer to dealer
                                continue;
                            }
                            else if (results[player.Player] == PlayerResult.Bust)
                            {
                                // this player is already declared bust, continue
                                continue;
                            }
                            else
                            {
                                if (dealer.Player.Hand.Value > 21 && player.Player.Hand.Value <= 21)
                                {
                                    // dealer is bust, player is not
                                    results[player.Player] = PlayerResult.Win;
                                }
                                else
                                {
                                    // this player was declared won, challenge
                                    if (player.Player.Hand.Value > dealer.Player.Hand.Value)
                                    {
                                        // player does have better hand value than dealer
                                        results[player.Player] = PlayerResult.Win;
                                    }
                                    else if (player.Player.Hand.Value == dealer.Player.Hand.Value)
                                    {
                                        // player has same hand value as dealer
                                        results[player.Player] = PlayerResult.Push;
                                    }
                                    else
                                    {
                                        // player has less hand value than the dealer
                                        results[player.Player] = PlayerResult.Bust;
                                    }
                                }
                            }
                        }
                    }
                    else if (dataPair.Player.Dealer)
                    {
                        // if dealer, send to the end of the queue
                        // and skip since this will be dealt with in the end
                        Players.Enqueue(dataPair);
                        continue;
                    }
                    else
                    {
                        // while the hand is winning and player has not chosen stand or double down
                        do
                        {
                            do
                            {
                                PrintTable(players);
                                dataPair.Player.Choice = BlackjackHelper.AskPlayerChoice(dataPair.Player);
                            } while (dataPair.Player.Choice == PlayerChoice.Invalid);

                            switch (dataPair.Player.Choice)
                            {
                                case PlayerChoice.Hit:
                                case PlayerChoice.DoubleDown:
                                    DealToPlayer(dataPair.Player, deck.Dequeue(), false);
                                    PrintTable(players);
                                    break;
                                case PlayerChoice.Stand:
                                case null:
                                default:
                                    // player chooses stand or does not have any available choices left (like previously
                                    // chose stand or double down)
                                    break;
                            }

                            PlayerResult result;
                            var stop = false;
                            if (dataPair.Player.Hand.IsBlackjack() || dataPair.Player.Hand.IsOverStacked() || dataPair.Player.Hand.Value == 21)
                            {
                                // player has blackjack or is overstacked (cards >= 7 and value <= 21), break this loop
                                // assume win for now and calculate if push later
                                result = PlayerResult.Win;
                                stop = true;
                            }
                            else if (!dataPair.Player.Hand.IsWinning())
                            {
                                // player is no longer winning, break this loop
                                // a bust is a loss 
                                result = PlayerResult.Bust;
                                stop = true;
                            }
                            else
                            {
                                result = PlayerResult.Push;
                            }

                            // if cannot add key/value pair (it exists)
                            if (!results.TryAdd(dataPair.Player, result))
                            {
                                // modify current result
                                results[dataPair.Player] = result;
                            }

                            if (stop)
                            {
                                break;
                            }
                        } while (dataPair.Player.Choice != PlayerChoice.Stand && dataPair.Player.Choice != PlayerChoice.DoubleDown);
                    }
                } while (Players.Count > 0);
            }

            // declare winners, pushes and losers
            var winners = new List<string>();
            var pushes = new List<string>();
            var losers = new List<string>();
            foreach (var result in results)
            {
                if (result.Value == PlayerResult.Win)
                {
                    winners.Add(result.Key.Name);
                }
                else if (result.Value == PlayerResult.Push)
                {
                    pushes.Add(result.Key.Name);
                }
                else if (result.Value == PlayerResult.Bust)
                {
                    losers.Add(result.Key.Name);
                }
            }

            // post-game conditions, if no one won, push or lost
            if (winners.Count == 0)
            {
                winners.Add("None");
            }
            if (pushes.Count == 0)
            {
                pushes.Add("none");
            }
            if (losers.Count == 0)
            {
                losers.Add("none");
            }
            MatchMessage = $"{string.Join(", ", winners)} won, {string.Join(", ", pushes)} push and {string.Join(", ", losers)} lost.";

            // clean-up
            for (int i = 0; i < Players.Count; ++i)
            {
                // remove dealer from queue
                var player = Players.Dequeue();
                if (!player.Player.Dealer)
                {
                    // player isn't dealer, clear hand add it back into queue
                    player.Player.Hand.Cards.Clear();
                    Players.Enqueue(player);
                }
            }
        }

        private void PrintTable(PlayerData[] players)
        {
            foreach (var player in players)
            {
                Console.SetCursorPosition(player.Position.X, player.Position.Y);
                PrintHand(player.Player);
            }
        }

        private void DealToPlayer(BlackjackPlayer player, Card card, bool turned)
        {
            card.Turned = turned;
            Thread.Sleep(DealingDurationMilliseconds);
            player.Hand.Add(card);
            //PrintHand(player);
        }

        private void PrintHand(BlackjackPlayer player)
        {
            //Console.SetCursorPosition(player.Position.X, player.Position.Y);
            int valueOffset = 0;
            if (player.Dealer)
            {
                foreach (var card in player.Hand.Cards)
                {
                    if (card.Turned)
                    {
                        valueOffset += card.Value;
                    }
                }
            }
            Console.WriteLine($"{Environment.NewLine}{(!player.Dealer ? $"[{player.Balance}] " : "")}{player.Name} ({player.Hand.Value - valueOffset}):");
            CardHelper.PrintCards(player.Hand.Cards, CardHelper.SpacedCardOffset);
        }
    }
}