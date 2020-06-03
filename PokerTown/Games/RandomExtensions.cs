using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Solution by Matt Howells (and community) -> https://stackoverflow.com/a/110570/4339019
/// </summary>
namespace PokerTown.Games
{
    /// <summary>
    /// Extension to the <see cref="Random"/> class to help in poker mechanics.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Shuffles <paramref name="array"/> using Fisher-Yates algorithm aka the Knuth Shuffle.
        /// </summary>
        public static void Shuffle<T>(this Random random, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = random.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
