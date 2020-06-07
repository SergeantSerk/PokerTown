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
            DoubleDown,
            Invalid
        }

        public PlayerChoice? Choice { get; set; }

        public BlackjackPlayer(string name, Position position, Hand hand) : base(name, position, hand)
        {
            // Null infers the player hasn't chosen yet
            Choice = null;
        }
    }
}
