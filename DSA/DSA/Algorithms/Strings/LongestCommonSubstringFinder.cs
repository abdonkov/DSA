using System.Collections.Generic;

namespace DSA.Algorithms.Strings
{
    /// <summary>
    /// A static class containing methods for finding the longest common substring
    /// </summary>
    public static class LongestCommonSubstringFinder
    {
        /// <summary>
        /// Finds the length of the longest common substring of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <returns>The length of the longest common substring.</returns>
        public static int LCSLength(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return 0;

            // Faster access
            int s1Length = s1.Length;
            int s2Length = s2.Length;

            // Create two rows for computation
            var rows = new int[2][];
            rows[0] = new int[s2Length];
            rows[1] = new int[s2Length];

            // Row for computation
            int curRow = 0;
            // Max length
            int maxLength = 0;

            for (int i = 0; i < s1Length; i++)
            {
                int lastRow = curRow ^ 1; // compute lastRow ( 0^1=1 and 1^1=0)

                for (int j = 0; j < s2Length; j++)
                {
                    if (s1[i] == s2[j]) // we have a match
                    {
                        // Set current position one higher than the previous
                        if (i == 0 || j == 0)
                            rows[curRow][j] = 1;
                        else
                            rows[curRow][j] = rows[lastRow][j - 1] + 1;

                        // Update max length found
                        if (rows[curRow][j] > maxLength)
                            maxLength = rows[curRow][j];
                    }
                    else // we don't have a match
                        rows[curRow][j] = 0; // set to 0 to remove anything when used as a lastRow
                }

                curRow = lastRow;// change row for computation
            }

            return maxLength;
        }

        /// <summary>
        /// Finds all longest common substrings of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <returns>A <see cref="SortedSet{T}"/> containing the longest common substring with T being a <see cref="string"/> representing each substring.</returns>
        public static SortedSet<string> GetAllLCS(string s1, string s2)
        {
            int length;
            return GetAllLCS(s1, s2, out length);
        }

        /// <summary>
        /// Finds all longest common substrings of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <param name="length">Contains the length of the longest common substing.</param>
        /// <returns>A <see cref="SortedSet{T}"/> containing the longest common substring with T being a <see cref="string"/> representing each substring.</returns>
        public static SortedSet<string> GetAllLCS(string s1, string s2, out int length)
        {
            length = 0;

            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return new SortedSet<string>();

            // Faster access
            int s1Length = s1.Length;
            int s2Length = s2.Length;

            // Create two rows for computation
            var rows = new int[2][];
            rows[0] = new int[s2Length];
            rows[1] = new int[s2Length];

            // Row for computation
            int curRow = 0;
            // Max length
            int maxLength = 0;
            // Set with all LCS for the current length
            var curLCSs = new SortedSet<string>();

            for (int i = 0; i < s1Length; i++)
            {
                int lastRow = curRow ^ 1; // compute lastRow ( 0^1=1 and 1^1=0)

                for (int j = 0; j < s2Length; j++)
                {
                    if (s1[i] == s2[j]) // we have a match
                    {
                        // Set current position one higher than the previous
                        if (i == 0 || j == 0)
                            rows[curRow][j] = 1;
                        else
                            rows[curRow][j] = rows[lastRow][j - 1] + 1;

                        // Update max length found
                        if (rows[curRow][j] > maxLength)
                        {
                            maxLength = rows[curRow][j];
                            // Create new sorted set and add current LCS
                            curLCSs = new SortedSet<string>();
                            curLCSs.Add(s2.Substring(j + 1 - maxLength, maxLength));
                        }
                        else if (rows[curRow][j] == maxLength) // if current substring length == maxLength
                        {
                            // Add the substings to LCS set
                            curLCSs.Add(s2.Substring(j + 1 - maxLength, maxLength));
                        }
                    }
                    else // we don't have a match
                        rows[curRow][j] = 0; // set to 0 to remove anything when used as a lastRow
                }

                curRow = lastRow;// change row for computation
            }

            length = maxLength;
            return curLCSs;
        }

        /// <summary>
        /// Finds one longest common substring of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <returns>A <see cref="string"/> representing a longest common substring.</returns>
        public static string GetOneLCS(string s1, string s2)
        {
            int length;
            return GetOneLCS(s1, s2, out length);
        }

        /// <summary>
        /// Finds one longest common substring of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <param name="length">Contains the length of the longest common substing.</param>
        /// <returns>A <see cref="string"/> representing a longest common substring.</returns>
        public static string GetOneLCS(string s1, string s2, out int length)
        {
            length = 0;

            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return string.Empty;

            // Faster access
            int s1Length = s1.Length;
            int s2Length = s2.Length;

            // Create two rows for computation
            var rows = new int[2][];
            rows[0] = new int[s2Length];
            rows[1] = new int[s2Length];

            // Row for computation
            int curRow = 0;
            // Max length
            int maxLength = 0;
            // current LCS string
            string curLCS = string.Empty;

            for (int i = 0; i < s1Length; i++)
            {
                int lastRow = curRow ^ 1; // compute lastRow ( 0^1=1 and 1^1=0)

                for (int j = 0; j < s2Length; j++)
                {
                    if (s1[i] == s2[j]) // we have a match
                    {
                        // Set current position one higher than the previous
                        if (i == 0 || j == 0)
                            rows[curRow][j] = 1;
                        else
                            rows[curRow][j] = rows[lastRow][j - 1] + 1;

                        // Update max length found
                        if (rows[curRow][j] > maxLength)
                        {
                            maxLength = rows[curRow][j];
                            curLCS = s2.Substring(j + 1 - maxLength, maxLength);
                        }
                    }
                    else // we don't have a match
                        rows[curRow][j] = 0; // set to 0 to remove anything when used as a lastRow
                }

                curRow = lastRow;// change row for computation
            }

            length = maxLength;
            return curLCS;
        }
    }
}
