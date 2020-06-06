using System;
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
                PrintCard(card, xOffset, yPos);
                xOffset += cardOffset;
            }
        }

        public static void PrintCard(Card card, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine($"┌─────┐");

            Console.SetCursorPosition(xOffset, yOffset + 1);
            Console.Write("│");
            if (!card.Turned)
            {
                PrintColouredCharacter(card.Suit, card.ToString().PadRight(2));
                Console.Write("   ");
            }
            else
            {
                Console.Write("▒▒▒▒▒");
            }
            Console.WriteLine("│");

            Console.SetCursorPosition(xOffset, yOffset + 2);
            Console.Write("│");
            if (!card.Turned)
            {
                Console.Write("  ");
                PrintColouredCharacter(card.Suit, SuitToSymbol(card.Suit));
                Console.Write("  ");
            }
            else
            {
                Console.Write("▒▒▒▒▒");
            }
            Console.WriteLine("│");

            Console.SetCursorPosition(xOffset, yOffset + 3);
            Console.Write("│");
            if (!card.Turned)
            {
                Console.Write("   ");
                PrintColouredCharacter(card.Suit, card.ToString().PadLeft(2));
            }
            else
            {
                Console.Write("▒▒▒▒▒");
            }
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
