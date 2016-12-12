using DSA.DataStructures.Lists;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a Trie (Prefix Tree).
    /// </summary>
    public class Trie : IEnumerable<string>
    {
        /// <summary>
        /// Gets the tree root of the <see cref="Trie"/>.
        /// </summary>
        public TrieNode Root { get; internal set; }

        /// <summary>
        /// Gets the number of words in the <see cref="Trie"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="Trie"/> class.
        /// </summary>
        public Trie()
        {
            Root = new TrieNode(default(char), false);
        }

        /// <summary>
        /// Adds a word to the <see cref="Trie"/>.
        /// </summary>
        /// <param name="word">The word to add.</param>
        public void Add(string word)
        {
            var curNode = Root;
            bool wordWasAlreadyAdded = false;

            for (int i = 0; i < word.Length; i++)
            {
                if (curNode.children.ContainsKey(word[i]))
                {
                    if (i == word.Length - 1)
                    {
                        wordWasAlreadyAdded = curNode.children[word[i]].IsTerminal;
                        curNode.children[word[i]].IsTerminal = true;
                    }
                    else
                        curNode = curNode.children[word[i]];
                }
                else
                {
                    if (i == word.Length - 1)
                    {
                        var newNode = new TrieNode(word[i], true);
                        curNode.children.Add(word[i], newNode);
                        //curNode.children[word[i]] = new TrieNode(word[i], true);
                    }
                    else
                    {
                        var newNode = new TrieNode(word[i], false);
                        curNode.children.Add(word[i], newNode);
                        //curNode.children[word[i]] = new TrieNode(word[i], false);
                        curNode = curNode.children[word[i]];
                    }
                }
            }

            if (!wordWasAlreadyAdded) Count++;
        }

        /// <summary>
        /// Removes a word from the <see cref="Trie"/>.
        /// </summary>
        /// <param name="word">The word to remove.</param>
        /// <returns>true if the word is successfully removed; otherwise false. Also returns false if word is not found.</returns>
        public bool Remove(string word)
        {
            var curNode = Root;

            bool removedSuccessfully = true;

            if (!Contains(word)) return false;

            var nodesList = new SinglyLinkedList<TrieNode>();

            nodesList.AddFirst(curNode);

            for (int i = 0; i < word.Length; i++)
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
        /// Determines whether a word is in the <see cref="Trie"/>.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>true if word is found; otherwise false.</returns>
        public bool Contains(string word)
        {
            var curNode = Root;

            for (int i = 0; i < word.Length; i++)
            {
                if (curNode.children.ContainsKey(word[i]))
                {
                    if (i == word.Length - 1)
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
        /// Determines whether a prefix is in the <see cref="Trie"/>.
        /// </summary>
        /// <param name="prefix">The prefix to check.</param>
        /// <returns>true if prefix is found; otherwise false.</returns>
        public bool ContainsPrefix(string prefix)
        {
            var curNode = Root;

            for (int i = 0; i < prefix.Length; i++)
            {
                if (curNode.children.ContainsKey(prefix[i]))
                {
                    if (i == prefix.Length - 1)
                    {
                        return true;
                    }
                    else
                        curNode = curNode.children[prefix[i]];
                }
                else return false;
            }

            return false;
        }

        /// <summary>
        /// Removes all words from the <see cref="Trie"/>.
        /// </summary>
        public void Clear()
        {
            Root = new TrieNode(default(char), false);
            Count = 0;
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of the words in the <see cref="Trie"/> beginning with the given prefix sorted lexicographically.
        /// </summary>
        /// <param name="prefix">The prefix of the requested words.</param>
        /// <returns>Returns the words sorted in lexicographical order.</returns>
        public IEnumerable<string> GetWordsByPrefix(string prefix)
        {
            return GetWordsByPrefix(prefix, Comparer<char>.Default);
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of the words in the <see cref="Trie"/> beginning with the given prefix sorted by the given comparer.
        /// </summary>
        /// <param name="prefix">The prefix of the requested words.</param>
        /// <param name="comparer">The comparer which is used for sorting.</param>
        /// <returns>Returns the words sorted with the given comparer.</returns>
        public IEnumerable<string> GetWordsByPrefix(string prefix, Comparer<char> comparer)
        {
            if (ContainsPrefix(prefix))
            {
                // Stack containing the nodes for traversal
                var nodesStack = new Stack<TrieNode>();
                // Stack containing the level of the current node
                var levelStack = new Stack<int>();

                var curNode = Root;

                var curWord = new StringBuilder();

                // Finding last node of the prefix and adding the characters
                // to the current word
                for (int i = 0; i < prefix.Length; i++)
                {
                    curNode = curNode.children[prefix[i]];
                    curWord.Append(prefix[i]);
                }

                int prefixLength = prefix.Length;

                // Code below is the same as the GetEnumerator() method except we are using
                // the given comparer instead of the default compare method. Normal DFS
                // begging from the last node of the prefix.
                var sortedChildren = new SortedDictionary<char, TrieNode>(curNode.children,
                                                Comparer<char>.Create((x, y) => comparer.Compare(y,x)));

                foreach (var kvp in sortedChildren)
                {
                    nodesStack.Push(kvp.Value);
                    // We set the level of the nodes after the last node of the prefix
                    levelStack.Push(prefixLength + 1);
                }

                while (nodesStack.Count != 0)
                {
                    curNode = nodesStack.Pop();
                    var curLevel = levelStack.Pop();

                    if (curWord.Length >= curLevel)
                        curWord.Length = curLevel - 1;

                    curWord.Append(curNode.Key);

                    if (curNode.IsTerminal)
                        yield return curWord.ToString();

                    sortedChildren = new SortedDictionary<char, TrieNode>(curNode.children,
                                                Comparer<char>.Create((x, y) => comparer.Compare(y, x)));

                    foreach (var kvp in sortedChildren)
                    {
                        nodesStack.Push(kvp.Value);
                        levelStack.Push(curLevel + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of all words in the <see cref="Trie"/> sorted in lexicographical order.
        /// </summary>
        /// <returns>Returns the words sorted in lexicographical order.</returns>
        public IEnumerable<string> GetAllWords()
        {
            return GetAllWords(Comparer<char>.Default);
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/> of all words in the <see cref="Trie"/> sorted by the given comparer.
        /// </summary>
        /// <param name="comparer">The comparer which is used for sorting.</param>
        /// <returns>Returns the words sorted with the given comparer.</returns>
        public IEnumerable<string> GetAllWords(Comparer<char> comparer)
        {
            // Stack containing the nodes for traversal
            var nodesStack = new Stack<TrieNode>();
            // Stack containing the level of the current node
            var levelStack = new Stack<int>();

            var curNode = Root;

            // Creating reversed sorted dictionary for adding into the stack of nodes
            var sortedChildren = new SortedDictionary<char, TrieNode>(curNode.children,
                                            Comparer<char>.Create((x, y) => comparer.Compare(y, x)));

            // Adding nodes in the stack
            foreach (var kvp in sortedChildren)
            {
                nodesStack.Push(kvp.Value);
                levelStack.Push(1);
            }

            var curWord = new StringBuilder();

            while (nodesStack.Count != 0)
            {
                curNode = nodesStack.Pop();
                var curLevel = levelStack.Pop();

                // if the current word lenght is higher that the node level
                // we have to trim the lenght to the previous level
                // Note: this happens when we traverse all the nodes in a level
                // and drop to a lower level again
                if (curWord.Length >= curLevel)
                    curWord.Length = curLevel - 1;

                // Add the current node character to the word
                curWord.Append(curNode.Key);

                // If we have a complete word we output it
                if (curNode.IsTerminal)
                    yield return curWord.ToString();

                // Creating reversed sorted dictionary for adding into the stack of nodes
                sortedChildren = new SortedDictionary<char, TrieNode>(curNode.children,
                                            Comparer<char>.Create((x, y) => comparer.Compare(y, x)));

                // Adding nodes in the stack
                foreach (var kvp in sortedChildren)
                {
                    nodesStack.Push(kvp.Value);
                    levelStack.Push(curLevel + 1);
                }
            }

            // Note: We add the nodes in reversed order because the stack
            // is LIFO(last in first out) data structure so everything is outputted
            // in reversed order. We do this because we need this feature(LIFO)
            // of the stack to create a DFS(depth first search)
        }

        /// <summary>
        /// Returns an enumerator that iterates lexicographically through the <see cref="Trie"/>.
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
