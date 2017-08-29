using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Strings
{
    /// <summary>
    /// A static class containing methods for string pattern searching.
    /// </summary>
    public static partial class StringSearch
    {
        /// <summary>
        /// Searches for the first occurrence of a pattern in a target <see cref="string"/> using Rabin-Karp's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="pattern">The <see cref="string"/> to search for.</param>
        /// <returns>Returns the position of the first occurrence of the pattern. If not found returns -1.</returns>
        public static int RabinKarpSearchFirst(string target, string pattern)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (pattern == null) throw new ArgumentNullException(nameof(pattern));

            // Save for faster access
            int patternLength = pattern.Length;

            if (target.Length < patternLength) return -1;

            ulong targetHash = 0;
            ulong patternHash = 0;
            ulong alphabetSize = 256; // max char value
            ulong moduloValue = 65537; // custom selected prime number for the hashing
            
            // Calculating hash of pattern and the beggining of target
            for (int i = 0; i < patternLength; i++)
            {
                patternHash = (patternHash * alphabetSize + pattern[i]) % moduloValue;
                targetHash = (targetHash * alphabetSize + target[i]) % moduloValue;
            }

            // Check if pattern is in the beginning
            if (patternHash == targetHash)
                if (string.Equals(target.Substring(0, patternLength), pattern))
                    return 0;

            // Calculate pow value (used in the hashing proccess)
            ulong pow = 1;
            for (int i = 0; i < patternLength - 1; i++)
            {
                pow = (pow * alphabetSize) % moduloValue;
            }

            // Hashing the rest of the target and searching for the pattern
            int endOfSearch = target.Length - patternLength;
                        
            for (int i = 0; i < endOfSearch; i++)
            {
                // Some Rabin-Karp magic
                targetHash = (targetHash + moduloValue - pow * target[i] % moduloValue) % moduloValue;
                targetHash = (targetHash * alphabetSize + target[i + patternLength]) % moduloValue;

                // If the hashes are equal check the string( because collisions are possible) and return if found
                if (targetHash == patternHash)
                    if (string.Equals(target.Substring(i + 1, patternLength), pattern))
                        return i + 1;
            }

            // The pattern was not found
            return -1;
        }

        /// <summary>
        /// Searches for all occurences of a pattern in a target <see cref="string"/> using Rabin-Karp's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="pattern">The <see cref="string"/> to search for.</param>
        /// <returns>Returns <see cref="IList{T}"/> of <see cref="int"/> values of the positions at which the pattern occurs. <see cref="IList{T}"/> is empty if none found.</returns>
        public static IList<int> RabinKarpSearchAll(string target, string pattern)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (pattern == null) throw new ArgumentNullException(nameof(pattern));

            // Save for faster access
            int patternLength = pattern.Length;

            // List with the positions where the pattern was found
            var matches = new List<int>();

            if (target.Length < patternLength) return matches;

            ulong targetHash = 0;
            ulong patternHash = 0;
            ulong alphabetSize = 256; // max char value
            ulong moduloValue = 65537; // custom selected prime number for the hashing

            // Calculating hash of pattern and the beggining of target
            for (int i = 0; i < patternLength; i++)
            {
                patternHash = (patternHash * alphabetSize + pattern[i]) % moduloValue;
                targetHash = (targetHash * alphabetSize + target[i]) % moduloValue;
            }

            // Check if pattern is in the beginning
            if (patternHash == targetHash)
                if (string.Equals(target.Substring(0, patternLength), pattern))
                    matches.Add(0);

            // Calculate pow value (used in the hashing proccess)
            ulong pow = 1;
            for (int i = 0; i < patternLength - 1; i++)
            {
                pow = (pow * alphabetSize) % moduloValue;
            }

            // Hashing the rest of the target and searching for the pattern
            int endOfSearch = target.Length - patternLength;

            for (int i = 0; i < endOfSearch; i++)
            {
                // Some Rabin-Karp magic
                targetHash = (targetHash + moduloValue - pow * target[i] % moduloValue) % moduloValue;
                targetHash = (targetHash * alphabetSize + target[i + patternLength]) % moduloValue;

                // If the hashes are equal check the string( because collisions are possible) and return if found
                if (targetHash == patternHash)
                    if (string.Equals(target.Substring(i + 1, patternLength), pattern))
                        matches.Add(i + 1);
            }

            // Retrun the list with all starting positions of the pattern
            return matches;
        }

        /// <summary>
        /// Searches for the first occurrence of multiple patterns in a target <see cref="string"/> using Rabin-Karp's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="patterns">A <see cref="IList{T}"/> of <see cref="string"/> patterns.</param>
        /// <returns>Retruns <see cref="Dictionary{TKey, TValue}"/> with <see cref="string"/> keys of the patterns and <see cref="int"/> values of the position of first occurence.
        /// If a pattern is not found there is no entry in the dictionary.</returns>
        public static Dictionary<string, int> RabinKarpMultipleSearchFirst(string target, IList<string> patterns)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (patterns == null) throw new ArgumentNullException(nameof(patterns));

            // Dictionary with pattern hashes for all strings
            var patternHashes = new Dictionary<string, ulong>();
            // Dictionary with target hashes for all different string lengths
            var targetHashes = new Dictionary<int, ulong>();
            // Dictionary with pow values for all different string lengths
            var pows = new Dictionary<int, ulong>();
            // Dictionary with all strings with a specific length
            var patternLengths = new Dictionary<int, List<string>>();
            // Dictionary with found positions for every string
            var matches = new Dictionary<string, int>();

            ulong alphabetSize = 256; // max char value
            ulong moduloValue = 65537; // custom selected prime number for the hashing

            // Calculating hash of patterns and all target hashes and pow values
            for (int i = 0; i < patterns.Count; i++)
            {
                // Chech if target hash for current string length has to be computed
                bool hasToComputeTargetHashAndPow = !targetHashes.ContainsKey(patterns[i].Length);

                // Populate pattern lengths dictionary
                if (hasToComputeTargetHashAndPow) patternLengths.Add(patterns[i].Length, new List<string>() { patterns[i] });
                else patternLengths[patterns[i].Length].Add(patterns[i]);

                ulong patternHash = 0;
                ulong targetHash = 0;
                ulong pow = 1;
                for (int j = 0; j < patterns[i].Length; j++)
                {
                    patternHash = (patternHash * alphabetSize + patterns[i][j]) % moduloValue;
                    if (hasToComputeTargetHashAndPow)
                    {
                        targetHash = (targetHash * alphabetSize + target[j]) % moduloValue;
                        if (j != 0) // used to skip one iteration. Pow is calculated with one less iteration
                            pow = (pow * alphabetSize) % moduloValue;
                    }
                }

                // Add hashes in collections
                patternHashes.Add(patterns[i], patternHash);
                if (hasToComputeTargetHashAndPow)
                {
                    targetHashes.Add(patterns[i].Length, targetHash);
                    pows.Add(patterns[i].Length, pow);
                }
            }

            // Check if pattern is in the beginning
            foreach (var patKVP in patternHashes)
            {
                if (patKVP.Value == targetHashes[patKVP.Key.Length])
                    if (string.Equals(target.Substring(0, patKVP.Key.Length), patKVP.Key))
                        matches.Add(patKVP.Key, 0);
            }

            // Hashing the rest of the target and searching for the pattern
            // Patters are grouped by their length
            foreach (var patternsWithSpecificLength in patternLengths)
            {
                int patternLength = patternsWithSpecificLength.Key;
                int endOfSearch = target.Length - patternLength;

                for (int i = 0; i < endOfSearch; i++)
                {
                    ulong targetHash = targetHashes[patternLength];

                    // Some Rabin-Karp magic
                    targetHash = (targetHash + moduloValue - pows[patternLength] * target[i] % moduloValue) % moduloValue;
                    targetHash = (targetHash * alphabetSize + target[i + patternLength]) % moduloValue;

                    targetHashes[patternLength] = targetHash;

                    // Search all patterns for a match
                    foreach (var pat in patternsWithSpecificLength.Value)
                    {
                        if (!matches.ContainsKey(pat))
                        {
                            // If the hashes are equal check the string( because collisions are possible) and return if found
                            if (targetHash == patternHashes[pat])
                                if (string.Equals(target.Substring(i + 1, patternLength), pat))
                                    matches.Add(pat, i + 1);
                        }

                        if (matches.Count == patterns.Count) return matches;
                    }

                    if (matches.Count == patterns.Count) return matches;
                }
            }

            // Return matches
            return matches;
        }

        /// <summary>
        /// Searches for all occurrences of multiple patterns in a target <see cref="string"/> using Rabin-Karp's algorithm.
        /// </summary>
        /// <param name="target">The <see cref="string"/> to search in.</param>
        /// <param name="patterns">A <see cref="IList{T}"/> of <see cref="string"/> patterns.</param>
        /// <returns>Retruns <see cref="Dictionary{TKey, TValue}"/> with <see cref="string"/> keys of the patterns and <see cref="List{T}"/> of <see cref="int"/> values of the positions at which the pattern occurs.
        /// If a pattern is not found there is no entry in the dictionary.</returns>
        public static Dictionary<string, List<int>> RabinKarpMultipleSearchAll(string target, IList<string> patterns)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (patterns == null) throw new ArgumentNullException(nameof(patterns));

            // Dictionary with pattern hashes for all strings
            var patternHashes = new Dictionary<string, ulong>();
            // Dictionary with target hashes for all different string lengths
            var targetHashes = new Dictionary<int, ulong>();
            // Dictionary with pow values for all different string lengths
            var pows = new Dictionary<int, ulong>();
            // Dictionary with all strings with a specific length
            var patternLengths = new Dictionary<int, List<string>>();
            // Dictionary with found positions for every string
            var matches = new Dictionary<string, List<int>>();

            ulong alphabetSize = 256; // max char value
            ulong moduloValue = 65537; // custom selected prime number for the hashing

            // Calculating hash of patterns and all target hashes and pow values
            for (int i = 0; i < patterns.Count; i++)
            {
                // Chech if target hash for current string length has to be computed
                bool hasToComputeTargetHashAndPow = !targetHashes.ContainsKey(patterns[i].Length);

                // Populate matches dictionary and pattern lengths dictionary
                matches.Add(patterns[i], new List<int>());
                if (hasToComputeTargetHashAndPow) patternLengths.Add(patterns[i].Length, new List<string>() { patterns[i] });
                else patternLengths[patterns[i].Length].Add(patterns[i]);

                ulong patternHash = 0;
                ulong targetHash = 0;
                ulong pow = 1;
                for (int j = 0; j < patterns[i].Length; j++)
                {
                    patternHash = (patternHash * alphabetSize + patterns[i][j]) % moduloValue;
                    if (hasToComputeTargetHashAndPow)
                    {
                        targetHash = (targetHash * alphabetSize + target[j]) % moduloValue;
                        if (j != 0) // used to skip one iteration. Pow is calculated with one less iteration
                            pow = (pow * alphabetSize) % moduloValue;
                    }
                }

                // Add hashes in collections
                patternHashes.Add(patterns[i], patternHash);
                if (hasToComputeTargetHashAndPow)
                {
                    targetHashes.Add(patterns[i].Length, targetHash);
                    pows.Add(patterns[i].Length, pow);
                }
            }

            // Check if pattern is in the beginning
            foreach (var patKVP in patternHashes)
            {
                if (patKVP.Value == targetHashes[patKVP.Key.Length])
                    if (string.Equals(target.Substring(0, patKVP.Key.Length), patKVP.Key))
                        matches[patKVP.Key].Add(0);

            }

            // Hashing the rest of the target and searching for the pattern
            // Patters are grouped by their length
            foreach (var patternsWithSpecificLength in patternLengths)
            {
                int patternLength = patternsWithSpecificLength.Key;
                int endOfSearch = target.Length - patternLength;

                for (int i = 0; i < endOfSearch; i++)
                {
                    ulong targetHash = targetHashes[patternLength];

                    // Some Rabin-Karp magic
                    targetHash = (targetHash + moduloValue - pows[patternLength] * target[i] % moduloValue) % moduloValue;
                    targetHash = (targetHash * alphabetSize + target[i + patternLength]) % moduloValue;

                    targetHashes[patternLength] = targetHash;

                    // Search all patterns for a match
                    foreach (var pat in patternsWithSpecificLength.Value)
                    {
                        // If the hashes are equal check the string( because collisions are possible) and return if found
                        if (targetHash == patternHashes[pat])
                            if (string.Equals(target.Substring(i + 1, patternLength), pat))
                                matches[pat].Add(i + 1);
                    }
                }
            }

            // Remove all patterns that are not found
            for (int i = 0; i < patterns.Count; i++)
            {
                if (matches[patterns[i]].Count == 0)
                {
                    matches.Remove(patterns[i]);
                }
            }

            // Return matches
            return matches;
        }
    }
}
