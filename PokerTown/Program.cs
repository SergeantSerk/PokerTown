using PokerTown.Games;
using System;
using System.Text;

namespace PokerTown
{
    public static class Program
    {
        private static readonly IGame[] games = new IGame[]
        {
            new Blackjack()
        };

        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "PokerTown";
            Console.WriteLine("Welcome to PokerTown, please select a gamemode to begin playing:");

            do
            {
                Console.WriteLine($"0. Exit");
                for (int i = 0; i < games.Length; ++i)
                {
                    Console.WriteLine($"{i + 1}. {games[i].Name}");
                }

                Console.WriteLine();
                Console.Write("Option: ");

                var response = Console.ReadLine();
                if (int.TryParse(response, out int result))
                {
                    if (result == 0)
                    {
                        break;
                    }

                    if (result > 0 && result <= games.Length)
                    {
                        var game = games[result - 1];
                        game.Execute();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option selected.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid integer entered.");
                }

                Console.Clear();
            } while (true);
        }
    }
}
