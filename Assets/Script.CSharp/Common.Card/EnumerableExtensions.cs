// Copyright(c) Cragon. All rights reserved.

namespace Casinos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class EnumerableExtensions
    {
        //---------------------------------------------------------------------
        // Shuffle algorithm as seen on page 32 in the book "Algorithms" (4th edition) by Robert Sedgewick
        // <param name="source">Collection to shuffle.</param>
        // <typeparam name="T">The generic type parameter of the collection.</typeparam>
        // <returns>The shuffled collection as IEnumerable.</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
        {
            if (source == null)
            {
                //throw new ArgumentNullException(nameof(source));
            }

            var array = source.ToArray();
            var n = array.Length;
            for (var i = 0; i < n; i++)
            {
                // Exchange a[i] with random element in a[i..n-1]                
                var r = i + random.Next(0, n - i);
                var temp = array[i];
                array[i] = array[r];
                array[r] = temp;
            }

            return array;
        }

        //---------------------------------------------------------------------
        public static string CardsToString(this IEnumerable<Card> cards)
        {
            if (cards == null)
            {
                return string.Empty;
            }

            var stringBuilder = new StringBuilder();
            foreach (var card in cards)
            {
                stringBuilder.Append(card).Append(" ");
            }

            return stringBuilder.ToString().Trim();
        }
    }
}
