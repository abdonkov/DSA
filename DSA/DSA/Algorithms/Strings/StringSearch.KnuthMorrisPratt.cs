using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Strings
{
    public static partial class StringSearch
    {
        private static int[] BuildKMPTable(string pattern)
        {
            var kmpTable = new int[pattern.Length];

            if (kmpTable.Length < 2)
            {
                if (kmpTable.Length > 0)
                    kmpTable[0] = -1;

                return kmpTable;
            }

            int tableIndex = 2; // current position in table for computation
            int patSubstrIndex = 0; // index in the pattern of the current substring

            // First two values are fixed -1 and 0
            kmpTable[0] = -1;

            // Build table
            while (tableIndex < kmpTable.Length)
            {
                // If the substring continues
                if (pattern[tableIndex - 1] == pattern[patSubstrIndex])
                {
                    kmpTable[tableIndex++] = ++patSubstrIndex;
                }
                // It does not but we can fall back
                else if (patSubstrIndex != 0)
                {
                    patSubstrIndex = kmpTable[patSubstrIndex];
                }
                // If we ran out of candidates
                else
                {
                    kmpTable[tableIndex++] = 0;
                }
            }

            return kmpTable;
        }

        /// <summary>
        /// Searches for the first occurrence of a pattern in a target <see cref="string"/> using Knuth–Morris–Pratt's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="pattern">The <see cref="string"/> to search for.</param>
        /// <returns>Returns the position of the first occurrence of the pattern. If not found returns -1.</returns>
        public static int KnuthMorrisPrattSearchFirst(string target, string pattern)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (pattern == null) throw new ArgumentNullException(nameof(pattern));

            // Build KMP table
            var kmpTable = BuildKMPTable(pattern);

            int matchIndex = 0; // position of the current match
            int patternIndex = 0; // position in the pattern
            // Save for faster access
            int targetLength = target.Length;
            int patternLength = pattern.Length;

            while (matchIndex + patternIndex < targetLength)
            {
                if (pattern[patternIndex] == target[matchIndex + patternIndex])
                {
                    patternIndex++;
                    if (patternIndex == patternLength)
                        return matchIndex;
                }
                else // we are not in the middle of a pattern
                {
                    // if we can backtrack
                    if (kmpTable[patternIndex] > -1)
                    {
                        matchIndex = matchIndex + patternIndex - kmpTable[patternIndex];
                        patternIndex = kmpTable[patternIndex];
                    }
                    else // we can't backtrack (the beginning of the word)
                    {
                        matchIndex++;
                        patternIndex = 0;
                    }
                }
            }

            // We haven't found anything
            return -1;
        }

        /// <summary>
        /// Searches for all occurences of a pattern in a target <see cref="string"/> using Knuth–Morris–Pratt's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="pattern">The <see cref="string"/> to search for.</param>
        /// <returns>Returns <see cref="IList{T}"/> of <see cref="int"/> values of the positions at which the pattern occurs. <see cref="IList{T}"/> is empty if none found.</returns>
        public static IList<int> KnuthMorrisPrattSearchAll(string target, string pattern)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (pattern == null) throw new ArgumentNullException(nameof(pattern));

            // List with matches
            var matches = new List<int>();

            // Build KMP table
            var kmpTable = BuildKMPTable(pattern);

            int matchIndex = 0; // position of the current match
            int patternIndex = 0; // position in the pattern
            // Save for faster access
            int targetLength = target.Length;
            int patternLength = pattern.Length;

            while (matchIndex + patternIndex < targetLength)
            {
                if (pattern[patternIndex] == target[matchIndex + patternIndex])
                {
                    patternIndex++;
                    if (patternIndex == patternLength)
                    {
                        matches.Add(matchIndex);

                        // Find where the next match will begin
                        patternIndex--;

                        // if we can backtrack
                        if (kmpTable[patternIndex] > -1)
                        {
                            matchIndex = matchIndex + patternIndex - kmpTable[patternIndex];
                            patternIndex = kmpTable[patternIndex];
                        }
                        else // we can't backtrack (the beginning of the word)
                        {
                            matchIndex++;
                            patternIndex = 0;
                        }
                    }
                }
                else // we are not in the middle of a pattern
                {
                    // if we can backtrack
                    if (kmpTable[patternIndex] > -1)
                    {
                        matchIndex = matchIndex + patternIndex - kmpTable[patternIndex];
                        patternIndex = kmpTable[patternIndex];
                    }
                    else // we can't backtrack (the beginning of the word)
                    {
                        matchIndex++;
                        patternIndex = 0;
                    }
                }
            }

            // We haven't found anything
            return matches;
        }

        /// <summary>
        /// Searches for the first occurrence of multiple patterns in a target <see cref="string"/> using Knuth–Morris–Pratt's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="patterns">A <see cref="IList{T}"/> of <see cref="string"/> patterns.</param>
        /// <returns>Retruns <see cref="Dictionary{TKey, TValue}"/> with <see cref="string"/> keys of the patterns and <see cref="int"/> values of the position of first occurence.
        /// If a pattern is not found there is no entry in the dictionary.</returns>
        public static Dictionary<string, int> KnuthMorrisPrattMultipleSearchFirst(string target, IList<string> patterns)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (patterns == null) throw new ArgumentNullException(nameof(patterns));

            // Dictionary with matches
            var matches = new Dictionary<string, int>();

            for (int i = 0; i < patterns.Count; i++)
            {
                int postition = KnuthMorrisPrattSearchFirst(target, patterns[i]);
                if (postition > -1)
                    matches.Add(patterns[i], postition);
            }

            return matches;
        }

        /// <summary>
        /// Searches for all occurrences of multiple patterns in a target <see cref="string"/> using Knuth–Morris–Pratt's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="patterns">A <see cref="IList{T}"/> of <see cref="string"/> patterns.</param>
        /// <returns>Retruns <see cref="Dictionary{TKey, TValue}"/> with <see cref="string"/> keys of the patterns and <see cref="List{T}"/> of <see cref="int"/> values of the positions at which the pattern occurs.
        /// If a pattern is not found there is no entry in the dictionary.</returns>
        public static Dictionary<string, List<int>> KnuthMorrisPrattMultipleSearchAll(string target, IList<string> patterns)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (patterns == null) throw new ArgumentNullException(nameof(patterns));

            // Dictionary with matches
            var matches = new Dictionary<string, List<int>>();

            for (int i = 0; i < patterns.Count; i++)
            {
                var postitions = new List<int>(KnuthMorrisPrattSearchAll(target, patterns[i]));
                if (postitions.Count > 0)
                    matches.Add(patterns[i], postitions);
            }

            return matches;
        }
    }
}
