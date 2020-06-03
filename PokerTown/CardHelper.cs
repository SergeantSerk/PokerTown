using System;
using System.Collections;
using System.Collections.Generic;

namespace PokerTown
{
    internal static class CardHelper
    {
        internal enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        internal static Card GetRandomCard()
        {
            var random = new Random();
            var s = random.Next(0, 4);
            var v = random.Next(0, 13);
            var suit = (Suit)s;
            var value = GetCardValue(v);
            return new Card(suit, value);
        }

        internal static void PrintCards(ICollection<Card> cards, int cardOffset)
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

        internal static void PrintCard(Suit s, string v, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine($"┌─────┐");

            Console.SetCursorPosition(xOffset, yOffset + 1);
            Console.Write("│");
            PrintColouredCharacter(s, v.PadRight(2));
            Console.WriteLine("   │");

            Console.SetCursorPosition(xOffset, yOffset + 2);
            Console.Write("│  ");
            PrintColouredCharacter(s, SuitToSymbol(s));
            Console.WriteLine("  │");

            Console.SetCursorPosition(xOffset, yOffset + 3);
            Console.Write("│   ");
            PrintColouredCharacter(s, v.PadLeft(2));
            Console.WriteLine("│");

            Console.SetCursorPosition(xOffset, yOffset + 4);
            Console.WriteLine($"└─────┘");

        }

        private static void PrintColouredCharacter(Suit suit, string value)
        {
            var previous = Console.ForegroundColor;
            Console.ForegroundColor = SuitToColour(suit);
            Console.Write(value);
            Console.ForegroundColor = previous;
        }

        private static string GetCardValue(int v)
        {
            if (v == 0)
            {
                return "A";
            }
            else if (v >= 1 && v <= 9)
            {
                return (v + 1).ToString();
            }
            else if (v >= 10 && v <= 12)
            {
                switch (v)
                {
                    case 10:
                        return "J";
                    case 11:
                        return "Q";
                    case 12:
                        return "K";
                    default:
                        throw new InvalidOperationException($"Unable to find case for {v} to card value");
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException($"No card value matches {v}");
            }
        }

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
