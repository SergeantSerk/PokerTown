using System;

namespace PokerTown.Games
{
    public struct Position
    {
        public int X;
        public int Y;

        public Position(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }

    public class Player
    {
        public Position Position { get; set; }

        public Hand Hand { get; private set; }

        public Player(Position position, Hand hand)
        {
            Position = position;
            Hand = hand;
        }
    }
}