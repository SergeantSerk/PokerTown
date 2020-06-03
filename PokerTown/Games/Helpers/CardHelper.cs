using System;
using System.Collections;
using System.Collections.Generic;

namespace PokerTown.Games.Helpers
{
    public static class CardHelper
    {
        public const int CardHeight = 5;
        public const int PackedCardOffset = 4;
        public const int SpacedCardOffset = 7;

        public enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public static Card GetRandomCard()
        {
            var random = new Random();
            var s = random.Next(0, 4);
            var suit = (Suit)s;
            var value = random.Next(0, 13);
            return new Card(suit, value);
        }

        public static void PrintCards(ICollection<Card> cards, int cardOffset)
        {
            if (cards == null)
            {
                throw new ArgumentNullException("Cannot print null cards");
            }
            else if (cardOffset < 0)
            {
                throw new ArgumentOutOfRangeException("Cannot offset cards by negative amounts");
            }

            int xOffset = 0;
            int yPos = Console.CursorTop;
            foreach (var card in cards)
            {
                PrintCard(card.Suit, card.Value, xOffset, yPos);
                xOffset += cardOffset;
            }
        }

        public static void PrintCard(Suit s, int v, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine($"┌─────┐");

            Console.SetCursorPosition(xOffset, yOffset + 1);
            Console.Write("│");
            PrintColouredCharacter(s, CardValueToString(v).PadRight(2));
            Console.WriteLine("   │");

            Console.SetCursorPosition(xOffset, yOffset + 2);
            Console.Write("│  ");
            PrintColouredCharacter(s, SuitToSymbol(s));
            Console.WriteLine("  │");

            Console.SetCursorPosition(xOffset, yOffset + 3);
            Console.Write("│   ");
            PrintColouredCharacter(s, CardValueToString(v).PadLeft(2));
            Console.WriteLine("│");

            Console.SetCursorPosition(xOffset, yOffset + 4);
            Console.Write($"└─────┘");
        }

        /// <summary>
        /// Prints <paramref name="value"/> (with colour) based on <paramref name="suit"/>.
        /// </summary>
        private static void PrintColouredCharacter(Suit suit, string value)
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor = SuitToColour(suit);
            Console.Write(value);
            Console.ForegroundColor = previous;
        }

        /// <summary>
        /// Convert raw card <paramref name="value"/> to letter representation on real cards.
        /// </summary>
        private static string CardValueToString(int value)
        {
            if (value == 0)
            {
                return "A";
            }
            else if (value >= 1 && value <= 9)
            {
                return (value + 1).ToString();
            }
            else if (value >= 10 && value <= 12)
            {
                return value switch
                {
                    10 => "J",
                    11 => "Q",
                    12 => "K",
                    _ => throw new InvalidOperationException($"Unable to find case for {value} to card value"),
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException($"No card value matches {value}");
            }
        }

        /// <summary>
        /// Derive card <paramref name="suit"/> to string representation.
        /// </summary>
        private static string SuitToSymbol(Suit suit)
        {
            return suit switch
            {
                Suit.Clubs => "♣",
                Suit.Diamonds => "♦",
                Suit.Hearts => "♥",
                Suit.Spades => "♠",
                _ => string.Empty,
            };
        }

        /// <summary>
        /// Derive card colour from <paramref name="suit"/>.
        /// </summary>
        private static ConsoleColor SuitToColour(Suit suit)
        {
            switch (suit)
            {
                case Suit.Clubs:
                case Suit.Spades:
                    return ConsoleColor.Black;
                case Suit.Diamonds:
                case Suit.Hearts:
                    return ConsoleColor.Red;
                default:
                    return Console.ForegroundColor;
            }
        }
    }
}
