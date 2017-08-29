using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Strings
{
    public static partial class StringSearch
    {
        /// <summary>
        /// Makes the bad char table. Contains the distance between the last character of the pattern and the rightmost occurrence of the character.
        /// </summary>
        private static Dictionary<char, int> BuildBadCharacterTable(string pattern)
        {
            var badCharTable = new Dictionary<char, int>();
            int patLength = pattern.Length;

            for (int i = 0; i < patLength - 1; i++)
            {
                badCharTable[pattern[i]] = patLength - 1 - i;
            }

            return badCharTable;
        }

        /// <summary>
        /// Searches for the first occurrence of a pattern in a target <see cref="string"/> using Boyer-Moore's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="pattern">The <see cref="string"/> to search for.</param>
        /// <returns>Returns the position of the first occurrence of the pattern. If not found returns -1.</returns>
        public static int BoyerMooreSearchFirst(string target, string pattern)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (pattern == null) throw new ArgumentNullException(nameof(pattern));

            // Build tables
            var badCharTable = BuildBadCharacterTable(pattern);

            // Faster access
            int patternLength = pattern.Length;
            int targetLength = target.Length;
            int endOfSearch = targetLength - patternLength;

            int i = 0;
            while (i <= endOfSearch)
            {
                // Start mathing
                int j = patternLength - 1;
                while (j >= 0 && target[i + j] == pattern[j])
                {
                    j--;
                }

                if (j < 0)
                    return i; // found a match

                // If we didn't find a match advance to next position
                int badChar = badCharTable.ContainsKey(target[i + j]) ? badCharTable[target[i + j]] : 0;
                int offset = badChar - patternLength + 1 + j;
                i += 1 < offset ? offset : 1;
            }

            // We haven't found anything
            return -1;
        }

        /// <summary>
        /// Searches for all occurences of a pattern in a target <see cref="string"/> using Boyer-Moore's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="pattern">The <see cref="string"/> to search for.</param>
        /// <returns>Returns <see cref="IList{T}"/> of <see cref="int"/> values of the positions at which the pattern occurs. <see cref="IList{T}"/> is empty if none found.</returns>
        public static IList<int> BoyerMooreSearchAll(string target, string pattern)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (pattern == null) throw new ArgumentNullException(nameof(pattern));

            // List with matches
            var matches = new List<int>();

            // Build tables
            var badCharTable = BuildBadCharacterTable(pattern);

            // Faster access
            int patternLength = pattern.Length;
            int targetLength = target.Length;
            int endOfSearch = targetLength - patternLength;

            int i = 0;
            while (i <= endOfSearch)
            {
                int j = patternLength - 1;
                while (j >= 0 && target[i + j] == pattern[j])
                {
                    j--;
                }

                if (j < 0)
                {
                    matches.Add(i); // found a match

                    // Compute next position to start matching again
                    if (i + patternLength < targetLength)
                    {
                        int badChar = badCharTable.ContainsKey(target[i + patternLength]) ? badCharTable[target[i + patternLength]] : 0;
                        i += badChar + 1;
                    }
                    else i++;
                }
                else
                {
                    // If we didn't find a match advance to next position
                    int badChar = badCharTable.ContainsKey(target[i + j]) ? badCharTable[target[i + j]] : 0;
                    int offset = badChar - patternLength + 1 + j;
                    i += 1 < offset ? offset : 1;
                }
            }

            return matches;
        }

        /// <summary>
        /// Searches for the first occurrence of multiple patterns in a target <see cref="string"/> using Boyer-Moore's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="patterns">A <see cref="IList{T}"/> of <see cref="string"/> patterns.</param>
        /// <returns>Retruns <see cref="Dictionary{TKey, TValue}"/> with <see cref="string"/> keys of the patterns and <see cref="int"/> values of the position of first occurence.
        /// If a pattern is not found there is no entry in the dictionary.</returns>
        public static Dictionary<string, int> BoyerMooreMultipleSearchFirst(string target, IList<string> patterns)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (patterns == null) throw new ArgumentNullException(nameof(patterns));

            // Dictionary with matches
            var matches = new Dictionary<string, int>();

            for (int i = 0; i < patterns.Count; i++)
            {
                int postition = BoyerMooreSearchFirst(target, patterns[i]);
                if (postition > -1)
                    matches.Add(patterns[i], postition);
            }

            return matches;
        }

        /// <summary>
        /// Searches for all occurrences of multiple patterns in a target <see cref="string"/> using Boyer-Moore's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="patterns">A <see cref="IList{T}"/> of <see cref="string"/> patterns.</param>
        /// <returns>Retruns <see cref="Dictionary{TKey, TValue}"/> with <see cref="string"/> keys of the patterns and <see cref="List{T}"/> of <see cref="int"/> values of the positions at which the pattern occurs.
        /// If a pattern is not found there is no entry in the dictionary.</returns>
        public static Dictionary<string, List<int>> BoyerMooreMultipleSearchAll(string target, IList<string> patterns)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (patterns == null) throw new ArgumentNullException(nameof(patterns));

            // Dictionary with matches
            var matches = new Dictionary<string, List<int>>();

            for (int i = 0; i < patterns.Count; i++)
            {
                var postitions = new List<int>(BoyerMooreSearchAll(target, patterns[i]));
                if (postitions.Count > 0)
                    matches.Add(patterns[i], postitions);
            }

            return matches;
        }
    }
}
