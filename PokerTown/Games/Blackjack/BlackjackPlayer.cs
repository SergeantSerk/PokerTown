using System;
using System.Collections.Generic;
using System.Text;
using static PokerTown.Games.Blackjack.Blackjack;

namespace PokerTown.Games.Blackjack
{
    public class BlackjackPlayer : Player
    {
        public enum PlayerChoice
        {
            Invalid,
            Stand,
            DoubleDown,
            Hit
        }

        public enum PlayerResult
        {
            Bust,
            Push,
            Win
        }

        public bool Dealer { get; private set; }

        public PlayerChoice? Choice { get; set; }

        public BlackjackHand Hand { get; private set; }

        public BlackjackPlayer(string name, bool dealer) : base(name)
        {
            // Null infers the player hasn't chosen yet
            Choice = null;
            Dealer = dealer;
            Hand = new BlackjackHand();
        }
    }
}
