using DSA.DataStructures.Lists;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a Suffix Tree.
    /// </summary>
    public class SuffixTree : IEnumerable<string>
    {
        internal StringBuilder ReverseStringBuilder(StringBuilder sb)
        {
            var reversedSB = new StringBuilder(sb.Length);
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                reversedSB.Append(sb[i]);
            }

            return reversedSB;
        }

        /// <summary>
        /// Gets the tree root of the <see cref="SuffixTree"/>.
        /// </summary>
        public SuffixTreeNode Root { get; internal set; }

        /// <summary>
        /// Gets the number of words in the <see cref="SuffixTree"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="SuffixTree"/> class.
        /// </summary>
        public SuffixTree()
        {
            Root = new SuffixTreeNode(default(char), false);
        }

        /// <summary>
        /// Adds a word to the <see cref="SuffixTree"/>.
        /// </summary>
        /// <param name="word">The word to add.</param>
        public void Add(string word)
        {
            var curNode = Root;
            bool wordWasAlreadyAdded = false;

            for (int i = word.Length - 1; i >= 0; i--)
            {
                if (curNode.children.ContainsKey(word[i]))
                {
                    if (i == 0)
                    {
                        wordWasAlreadyAdded = curNode.children[word[i]].IsTerminal;
                        curNode.children[word[i]].IsTerminal = true;
                    }
                    else
                        curNode = curNode.children[word[i]];
                }
                else
                {
                    if (i == 0)
                    {
                        var newNode = new SuffixTreeNode(word[i], true);
                        curNode.children.Add(word[i], newNode);
                    }
                    else
                    {
                        var newNode = new SuffixTreeNode(word[i], false);
                        curNode.children.Add(word[i], newNode);

                        curNode = curNode.children[word[i]];
                    }
                }
            }

            if (!wordWasAlreadyAdded) Count++;
        }

        /// <summary>
        /// Removes a word from the <see cref="SuffixTree"/>.
        /// </summary>
        /// <param name="word">The word to remove.</param>
        /// <returns>true if the word is successfully removed; otherwise false. Also returns false if word is not found.</returns>
        public bool Remove(string word)
        {
            var curNode = Root;

            bool removedSuccessfully = true;

            if (!Contains(word)) return false;

            var nodesList = new SinglyLinkedList<SuffixTreeNode>();

            nodesList.AddFirst(curNode);

            for (int i = word.Length - 1; i >= 0; i--)
            {
                curNode = curNode.children[word[i]];
                nodesList.AddFirst(curNode);
            }

            if (nodesList.First.Value.children.Count > 0)
            {
                nodesList.First.Value.IsTerminal = false;
                Count--;
                return true;
            }

            var curListNode = nodesList.First;

            while (curListNode.Next != null)
            {
                if (curListNode.Value.children.Count == 0)
                {
                    if (!curListNode.Next.Value.children.Remove(curListNode.Value.Key))
                        removedSuccessfully = false;

                    curListNode = curListNode.Next;
                }
                else break;
            }

            if (removedSuccessfully) Count--;

            return removedSuccessfully;
        }

        /// <summary>
        /// Determines whether a word is in the <see cref="SuffixTree"/>.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>true if word is found; otherwise false.</returns>
        public bool Contains(string word)
        {
            var curNode = Root;

            for (int i = word.Length - 1; i >= 0; i--)
            {
                if (curNode.children.ContainsKey(word[i]))
                {
                    if (i == 0)
                    {
                        return curNode.children[word[i]].IsTerminal;
                    }
                    else
                        curNode = curNode.children[word[i]];
                }
                else return false;
            }

            return false;
        }

        /// <summary>
        /// Determines whether a suffix is in the <see cref="SuffixTree"/>.
        /// </summary>
        /// <param name="suffix">The suffix to check.</param>
        /// <returns>true if suffix is found; otherwise false.</returns>
        public bool ContainsSuffix(string suffix)
        {
            var curNode = Root;

            for (int i = suffix.Length - 1; i >= 0; i--)
            {
                if (curNode.children.ContainsKey(suffix[i]))
                {
                    if (i == 0)
                    {
                        return true;
                    }
                    else
                        curNode = curNode.children[suffix[i]];
                }
                else return false;
            }

            return false;
        }

        /// <summary>
        /// Removes all words from the <see cref="SuffixTree"/>.
        /// </summary>
        public void Clear()
        {
            Root = new SuffixTreeNode(default(char), false);
            Count = 0;
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of the words in the <see cref="SuffixTree"/> ending with the given suffix sorted lexicographically.
        /// </summary>
        /// <param name="suffix">The suffix of the requested words.</param>
        /// <returns>Returns the words sorted in lexicographical order.</returns>
        public IEnumerable<string> GetWordsBySuffix(string suffix)
        {
            return GetWordsBySuffix(suffix, Comparer<char>.Default);
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of the words in the <see cref="SuffixTree"/> ending with the given suffix sorted by the given comparer.
        /// </summary>
        /// <param name="suffix">The suffix of the requested words.</param>
        /// <param name="comparer">The comparer which is used for sorting.</param>
        /// <returns>Returns the words sorted with the given comparer.</returns>
        public IEnumerable<string> GetWordsBySuffix(string suffix, Comparer<char> comparer)
        {
            // Words in the tree
            var words = new List<string>();

            if (ContainsSuffix(suffix))
            {
                // Stack containing the nodes for traversal
                var nodesStack = new Stack<SuffixTreeNode>();
                // Stack containing the level of the current node
                var levelStack = new Stack<int>();
                
                var curNode = Root;

                var curWord = new StringBuilder();

                // Finding last node of the suffix and adding the characters
                // to the current word. NOTE: current word is in reversed order
                for (int i = suffix.Length - 1; i >= 0; i--)
                {
                    curNode = curNode.children[suffix[i]];
                    curWord.Append(suffix[i]);
                }

                int suffixLength = suffix.Length;

                foreach (var kvp in curNode.children)
                {
                    nodesStack.Push(kvp.Value);
                    // We set the level of the nodes after the last node of the suffix
                    levelStack.Push(suffixLength + 1);
                }

                while (nodesStack.Count != 0)
                {
                    curNode = nodesStack.Pop();
                    var curLevel = levelStack.Pop();

                    if (curWord.Length >= curLevel)
                        curWord.Length = curLevel - 1;

                    curWord.Append(curNode.Key);

                    if (curNode.IsTerminal)
                        words.Add(ReverseStringBuilder(curWord).ToString());

                    foreach (var kvp in curNode.children)
                    {
                        nodesStack.Push(kvp.Value);
                        levelStack.Push(curLevel + 1);
                    }
                }

                // Sorting the words. Sorting is not possible on traversal because the words are saved backwards.
                words.Sort(Comparer<string>.Create((x, y) =>
                {
                    bool isXLengthSmaller = x.Length < y.Length;
                    int smallestLength = isXLengthSmaller ? x.Length : y.Length;

                    for (int i = 0; i < smallestLength; i++)
                    {
                        int cmp = comparer.Compare(x[i], y[i]);
                        if (cmp != 0) return cmp;
                    }

                    if (isXLengthSmaller) return -1;
                    else if (x.Length > y.Length) return 1;
                    else return 0;
                }));
            }
            
            return words;
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of all words in the <see cref="SuffixTree"/> sorted in lexicographical order.
        /// </summary>
        /// <returns>Returns the words sorted in lexicographical order.</returns>
        public IEnumerable<string> GetAllWords()
        {
            return GetAllWords(Comparer<char>.Default);
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of all words in the <see cref="SuffixTree"/> sorted by the given comparer.
        /// </summary>
        /// <param name="comparer">The comparer which is used for sorting.</param>
        /// <returns>Returns the words sorted with the given comparer.</returns>
        public IEnumerable<string> GetAllWords(Comparer<char> comparer)
        {
            // Stack containing the nodes for traversal
            var nodesStack = new Stack<SuffixTreeNode>();
            // Stack containing the level of the current node
            var levelStack = new Stack<int>();

            // Words in the tree
            var words = new List<string>();

            var curNode = Root;

            // Adding nodes in the stack
            foreach (var kvp in curNode.children)
            {
                nodesStack.Push(kvp.Value);
                levelStack.Push(1);
            }

            var curWord = new StringBuilder();

            while (nodesStack.Count != 0)
            {
                curNode = nodesStack.Pop();
                var curLevel = levelStack.Pop();

                // if the current word length is higher that the node level
                // we have to trim the length to the previous level
                // Note: this happens when we traverse all the nodes in a level
                // and drop to a lower level again
                if (curWord.Length >= curLevel)
                    curWord.Length = curLevel - 1;

                // Add the current node character to the word
                curWord.Append(curNode.Key);

                // If we have a complete word we output it
                if (curNode.IsTerminal)
                    words.Add(ReverseStringBuilder(curWord).ToString());

                // Adding nodes in the stack
                foreach (var kvp in curNode.children)
                {
                    nodesStack.Push(kvp.Value);
                    levelStack.Push(curLevel + 1);
                }
            }

            // Sorting the words. Sorting is not possible on traversal because the words are saved backwards.
            words.Sort(Comparer<string>.Create((x, y) =>
            {
                bool isXLengthSmaller = x.Length < y.Length;
                int smallestLength = isXLengthSmaller ? x.Length : y.Length;

                for (int i = 0; i < smallestLength; i++)
                {
                    int cmp = comparer.Compare(x[i], y[i]);
                    if (cmp != 0) return cmp;
                }

                if (isXLengthSmaller) return -1;
                else if (x.Length > y.Length) return 1;
                else return 0;
            }));

            return words;
        }

        /// <summary>
        /// Returns an enumerator that iterates lexicographically through the <see cref="SuffixTree"/>.
        /// </summary>
        /// <returns>Returns the words sorted in lexicographical order.</returns>
        public IEnumerator<string> GetEnumerator()
        {
            return GetAllWords(Comparer<char>.Default).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
