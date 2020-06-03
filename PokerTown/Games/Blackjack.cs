using System;
using static PokerTown.Card;

namespace PokerTown.Games
{
    internal class Blackjack : IGame
    {
        public string Name => Resource.Blackjack_enGB;
        public string ShortDescription => Resource.Blackjack_enGB_SD;
        public string LongDescription => Resource.Blackjack_enGB_LD;

        public void Execute()
        {
            Console.Clear();
            Console.Title = "Blackjack";
            Console.WriteLine("Welcome to the dealer's table.");

            var cards = new Tuple<Suit, string>[]
            {
                new Tuple<Suit, string>(Suit.Diamonds, "J"),
                new Tuple<Suit, string>(Suit.Clubs, "10"),
                new Tuple<Suit, string>(Suit.Hearts, "A"),
                new Tuple<Suit, string>(Suit.Spades, "7")
            };

            PrintCards(cards);
            Console.ReadLine();
        }

        private void PrintCards(Tuple<Suit, string>[] cards)
        {
            // Print top of the cards
            for (int i = 0; i < cards.Length; ++i)
            {
                Console.Write("┌─────┐");
                Console.Write(i == cards.Length - 1 ? Environment.NewLine : " ");
            }

            // Print top values
            for (int i = 0; i < cards.Length; ++i)
            {
                Console.Write("│");
                var previous = Console.ForegroundColor;
                switch (cards[i].Item1)
                {
                    case Suit.Clubs:
                    case Suit.Spades:
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case Suit.Diamonds:
                    case Suit.Hearts:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        break;
                }
                Console.Write(cards[i].Item2.PadRight(2));
                Console.ForegroundColor = previous;
                Console.Write("   │");
                Console.Write(i == cards.Length - 1 ? Environment.NewLine : " ");
            }

            // Print suits
            for (int i = 0; i < cards.Length; ++i)
            {
                Console.Write("│  ");
                var previous = Console.ForegroundColor;
                switch (cards[i].Item1)
                {
                    case Suit.Clubs:
                    case Suit.Spades:
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case Suit.Diamonds:
                    case Suit.Hearts:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        break;
                }
                Console.Write(cards[i].Item1.ToSuitString());
                Console.ForegroundColor = previous;
                Console.Write("  │");
                Console.Write(i == cards.Length - 1 ? Environment.NewLine : " ");
            }

            // Print bottom values
            for (int i = 0; i < cards.Length; ++i)
            {
                Console.Write("│   ");
                var previous = Console.ForegroundColor;
                switch (cards[i].Item1)
                {
                    case Suit.Clubs:
                    case Suit.Spades:
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case Suit.Diamonds:
                    case Suit.Hearts:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    default:
                        break;
                }
                Console.Write(cards[i].Item2.PadLeft(2));
                Console.ForegroundColor = previous;
                Console.Write("│");
                Console.Write(i == cards.Length - 1 ? Environment.NewLine : " ");
            }

            // Print bottom of the cards
            for (int i = 0; i < cards.Length; ++i)
            {
                Console.Write("└─────┘");
                Console.Write(i == cards.Length - 1 ? Environment.NewLine : " ");
            }
        }

        private string PrintCard(Suit s, string v)
        {

            return $"┌─────┐{Environment.NewLine}" +
                   $"│{v,-2}   │{Environment.NewLine}" +
                   $"│  {s.ToSuitString()}  │{Environment.NewLine}" +
                   $"│   {v,2}│{Environment.NewLine}" +
                   $"└─────┘";

        }
    }
}
