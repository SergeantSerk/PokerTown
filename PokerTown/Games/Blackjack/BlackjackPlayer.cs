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
            Hit,
            Stand,
            DoubleDown
        }

        public PlayerChoice? Choice { get; set; }

        public BlackjackPlayer(Position position, Hand hand) : base(position, hand)
        {
            // Null infers the player hasn't chosen yet
            Choice = null;
        }
    }
}
