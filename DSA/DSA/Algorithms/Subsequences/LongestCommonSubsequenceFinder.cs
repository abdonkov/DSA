using System.Collections.Generic;

namespace DSA.Algorithms.Subsequences
{
    /// <summary>
    /// A static class containing methods for finding the longest common subsequence
    /// </summary>
    public static class LongestCommonSubsequenceFinder
    {
        /// <summary>
        /// Computes the longest common subsequence table of two <see cref="IList{T}"/> objects.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="a">An <see cref="IList{T}"/> object representing the first sequence.</param>
        /// <param name="b">An <see cref="IList{T}"/> object representing the second sequence.</param>
        /// <returns>A int matrix representing the LCS table.</returns>
        internal static int[,] ComputeLCSTable<T>(IList<T> a, IList<T> b)
        {
            int height = a.Count + 1;
            int width = b.Count + 1;

            var lcsTable = new int[height, width];

            for (int i = 1; i < height; i++)
            {
                for (int j = 1; j < width; j++)
                {
                    if (object.Equals(a[i - 1], b[j - 1]))
                        lcsTable[i, j] = lcsTable[i - 1, j - 1] + 1;
                    else
                    {
                        if (lcsTable[i, j - 1] > lcsTable[i - 1, j])
                            lcsTable[i, j] = lcsTable[i, j - 1];
                        else
                            lcsTable[i, j] = lcsTable[i - 1, j];
                    }
                }
            }

            return lcsTable;
        }

        /// <summary>
        /// Computes the longest common subsequence table of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <returns>A int matrix representing the LCS table.</returns>
        internal static int[,] ComputeLCSTable(string s1, string s2)
        {
            int height = s1.Length + 1;
            int width = s2.Length + 1;

            var lcsTable = new int[height, width];

            for (int i = 1; i < height; i++)
            {
                for (int j = 1; j < width; j++)
                {
                    if (object.Equals(s1[i - 1], s2[j - 1]))
                        lcsTable[i, j] = lcsTable[i - 1, j - 1] + 1;
                    else
                    {
                        if (lcsTable[i, j - 1] > lcsTable[i - 1, j])
                            lcsTable[i, j] = lcsTable[i, j - 1];
                        else
                            lcsTable[i, j] = lcsTable[i - 1, j];
                    }
                }
            }

            return lcsTable;
        }

        /// <summary>
        /// Finds all LCS of two <see cref="IList{T}"/> objects from their LCS table.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="lcsTable">The LCS table of the given sequences.</param>
        /// <param name="a">An <see cref="IList{T}"/> object representing the first sequence.</param>
        /// <param name="b">An <see cref="IList{T}"/> object representing the second sequence.</param>
        /// <param name="i">A index representing the row in the LCS table.</param>
        /// <param name="j">A index representing the column in the LCS table.</param>
        /// <returns>A <see cref="List{T}"/> containing the longest common subsequences with T being a <see cref="List{T}"/> representing each subsequence.</returns>
        internal static List<List<T>> BacktrackAll<T>(int[,] lcsTable, IList<T> a, IList<T> b, int i, int j)
        {
            if (i == 0 || j == 0)
                return new List<List<T>> { new List<T>() };
            else if (object.Equals(a[i - 1], b[j - 1]))
            {
                var current = BacktrackAll(lcsTable, a, b, i - 1, j - 1);
                var temp = new List<List<T>>();
                if (current.Count == 0)
                {
                    temp.Add(new List<T>() { a[i - 1] });
                }
                else
                {
                    for (int k = 0; k < current.Count; k++)
                    {
                        var list = new List<T>(current[k]);
                        list.Add(a[i - 1]);
                        temp.Add(list);
                    }
                }

                return temp;
            }
            else
            {
                var result = new List<List<T>>();
                bool hadToGoUp = false;
                if (lcsTable[i - 1, j] >= lcsTable[i, j - 1])
                {
                    hadToGoUp = true;
                    var current = BacktrackAll(lcsTable, a, b, i - 1, j);
                    for (int k = 0; k < current.Count; k++)
                    {
                        result.Add(current[k]);
                    }
                }

                if (lcsTable[i, j - 1] >= lcsTable[i - 1, j])
                {
                    var current = BacktrackAll(lcsTable, a, b, i, j - 1);

                    if (!hadToGoUp)
                    {
                        for (int k = 0; k < current.Count; k++)
                        {
                            result.Add(current[k]);
                        }
                    }
                    else // if we had to go up in the table. We need to check if we are adding duplicates
                    {
                        for (int k = 0; k < current.Count; k++)
                        {
                            bool isDuplicate = true;
                            for (int l = 0; l < result.Count; l++)
                            {
                                isDuplicate = true;
                                for (int p = 0; p < result[l].Count; p++)
                                {
                                    if (!object.Equals(current[k][p], result[l][p]))
                                    {
                                        isDuplicate = false;
                                        break;
                                    }
                                }
                                if (isDuplicate) break;
                            }

                            if (!isDuplicate)
                            {
                                result.Add(current[k]);
                            }
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Finds all LCS of two <see cref="string"/> objects from their LCS table.
        /// </summary>
        /// <param name="lcsTable">The LCS table of the given sequences.</param>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <param name="i">A index representing the row in the LCS table.</param>
        /// <param name="j">A index representing the column in the LCS table.</param>
        /// <returns>A <see cref="SortedSet{T}"/> containing the longest common subsequences with T being a <see cref="string"/> representing each subsequence.</returns>
        internal static SortedSet<string> BacktrackAll(int[,] lcsTable, string s1, string s2, int i, int j)
        {
            if (i == 0 || j == 0)
                return new SortedSet<string>() { "" };
            else if (object.Equals(s1[i - 1], s2[j - 1]))
            {
                var current = BacktrackAll(lcsTable, s1, s2, i - 1, j - 1);
                var temp = new SortedSet<string>();
                if (current.Count == 0)
                {
                    temp.Add(s1[i - 1].ToString());
                }
                else
                {
                    foreach (var str in current)
                    {
                        temp.Add(str + s1[i - 1]);
                    }
                }

                return temp;
            }
            else
            {
                var result = new SortedSet<string>();
                if (lcsTable[i - 1, j] >= lcsTable[i, j - 1])
                {
                    var current = BacktrackAll(lcsTable, s1, s2, i - 1, j);
                    foreach (var s in current)
                    {
                        result.Add(s);
                    }
                }

                if (lcsTable[i, j - 1] >= lcsTable[i - 1, j])
                {
                    var current = BacktrackAll(lcsTable, s1, s2, i, j - 1);
                    foreach (var s in current)
                    {
                        result.Add(s);
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Finds one LCS of two <see cref="IList{T}"/> objects from their LCS table.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="lcsTable">The LCS table of the given sequences.</param>
        /// <param name="a">An <see cref="IList{T}"/> object representing the first sequence.</param>
        /// <param name="b">An <see cref="IList{T}"/> object representing the second sequence.</param>
        /// <param name="i">A index representing the row in the LCS table.</param>
        /// <param name="j">A index representing the column in the LCS table.</param>
        /// <returns>A <see cref="List{T}"/> representing a longest common subseqeunce.</returns>
        internal static List<T> BacktrackOne<T>(int[,] lcsTable, IList<T> a, IList<T> b, int i, int j)
        {
            if (i == 0 || j == 0)
                return new List<T>();
            else if (object.Equals(a[i - 1], b[j - 1]))
            {
                var current = BacktrackOne(lcsTable, a, b, i - 1, j - 1);
                var temp = new List<T>(current);
                temp.Add(a[i - 1]);
                return temp;
            }
            else
            {
                var up = new List<T>();
                var left = new List<T>();
                if (lcsTable[i - 1, j] >= lcsTable[i, j - 1])
                {
                    var current = BacktrackOne(lcsTable, a, b, i - 1, j);
                    up = new List<T>(current);
                }

                if (lcsTable[i, j - 1] >= lcsTable[i - 1, j])
                {
                    var current = BacktrackOne(lcsTable, a, b, i, j - 1);
                    left = new List<T>(current);
                }

                if (up.Count >= left.Count)
                    return new List<T>(up);
                else
                    return new List<T>(left);
            }
        }

        /// <summary>
        /// Finds one LCS of two <see cref="string"/> objects from their LCS table.
        /// </summary>
        /// <param name="lcsTable">The LCS table of the given sequences.</param>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <param name="i">A index representing the row in the LCS table.</param>
        /// <param name="j">A index representing the column in the LCS table.</param>
        /// <returns>A <see cref="string"/> representing a longest common subseqeunce.</returns>
        internal static string BacktrackOne(int[,] lcsTable, string s1, string s2, int i, int j)
        {
            if (i == 0 || j == 0)
                return string.Empty;
            else if (object.Equals(s1[i - 1], s2[j - 1]))
                return BacktrackOne(lcsTable, s1, s2, i - 1, j - 1) + s1[i - 1];
            else
            {
                var up = string.Empty;
                var left = string.Empty;
                if (lcsTable[i - 1, j] >= lcsTable[i, j - 1])
                    up = BacktrackOne(lcsTable, s1, s2, i - 1, j);

                if (lcsTable[i, j - 1] >= lcsTable[i - 1, j])
                    left = BacktrackOne(lcsTable, s1, s2, i, j - 1);

                if (up.Length >= left.Length)
                    return up;
                else
                    return left;
            }
        }

        /// <summary>
        /// Finds the length of the longest common subsequence of two <see cref="IList{T}"/> objects.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="a">An <see cref="IList{T}"/> object representing the first sequence.</param>
        /// <param name="b">An <see cref="IList{T}"/> object representing the second sequence.</param>
        /// <returns>The length of the longest common subsequence.</returns>
        public static int LCSLength<T>(IList<T> a, IList<T> b)
        {
            var lcsTable = ComputeLCSTable(a, b);
            return lcsTable[a.Count, b.Count];
        }

        /// <summary>
        /// Finds the length of the longest common subsequence of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <returns>The length of the longest common subsequence.</returns>
        public static int LCSLength(string s1, string s2)
        {
            var lcsTable = ComputeLCSTable(s1, s2);
            return lcsTable[s1.Length, s2.Length];
        }

        /// <summary>
        /// Finds all longest common subsequences of two <see cref="IList{T}"/> objects.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="a">An <see cref="IList{T}"/> object representing the first sequence.</param>
        /// <param name="b">An <see cref="IList{T}"/> object representing the second sequence.</param>
        /// <returns>A <see cref="List{T}"/> containing the longest common subsequences with T being a <see cref="List{T}"/> representing each subsequence.</returns>
        public static List<List<T>> GetAllLCS<T>(IList<T> a, IList<T> b)
        {
            var lcsTable = ComputeLCSTable(a, b);
            return BacktrackAll(lcsTable, a, b, a.Count, b.Count);
        }

        /// <summary>
        /// Finds all longest common subsequences of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <returns>A <see cref="SortedSet{T}"/> containing the longest common subsequences with T being a <see cref="string"/> representing each subsequence.</returns>
        public static SortedSet<string> GetAllLCS(string s1, string s2)
        {
            var lcsTable = ComputeLCSTable(s1, s2);
            return BacktrackAll(lcsTable, s1, s2, s1.Length, s2.Length);
        }

        /// <summary>
        /// Finds one longest common subsequence of two <see cref="IList{T}"/> objects.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="IList{T}"/>.</typeparam>
        /// <param name="a">An <see cref="IList{T}"/> object representing the first sequence.</param>
        /// <param name="b">An <see cref="IList{T}"/> object representing the second sequence.</param>
        /// <returns>A <see cref="List{T}"/> representing a longest common subseqeunce.</returns>
        public static List<T> GetOneLCS<T>(IList<T> a, IList<T> b)
        {
            var lcsTable = ComputeLCSTable(a, b);
            return BacktrackOne(lcsTable, a, b, a.Count, b.Count);
        }

        /// <summary>
        /// Finds one longest common subsequence of two <see cref="string"/> objects.
        /// </summary>
        /// <param name="s1">The first <see cref="string"/> object.</param>
        /// <param name="s2">The second <see cref="string"/> object.</param>
        /// <returns>A <see cref="string"/> representing a longest common subseqeunce.</returns>
        public static string GetOneLCS(string s1, string s2)
        {
            var lcsTable = ComputeLCSTable(s1, s2);
            return BacktrackOne(lcsTable, s1, s2, s1.Length, s2.Length);
        }
    }
}
