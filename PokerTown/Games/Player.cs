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
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}