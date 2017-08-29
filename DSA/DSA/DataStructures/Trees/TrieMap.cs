using DSA.DataStructures.Lists;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a Trie Map(Prefix Tree Map).
    /// </summary>
    public class TrieMap<TValue> : IEnumerable<KeyValuePair<string, TValue>>
    {
        /// <summary>
        /// Gets the tree root of the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        public TrieMapNode<TValue> Root { get; internal set; }

        /// <summary>
        /// Gets the number of words in the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Gets or sets the value associated with the specified word in the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        /// <param name="word">The word of the value to get or set.</param>
        /// <returns>The value associated with the specified word. If the specified word is not found,
        /// the get operation throws a <see cref="KeyNotFoundException"/>, and
        /// the set operation creates a new element with the specified word.</returns>
        public virtual TValue this[string word]
        {
            get
            {
                TValue value;
                if (TryGetValue(word, out value))
                    return value;
                else
                    throw new KeyNotFoundException("The word \"" + word + "\" was not found in the TrieMap.");
            }
            set
            {
                var curNode = Root;
                bool wordWasAlreadyAdded = false;

                for (int i = 0; i < word.Length; i++)
                {
                    if (curNode.children.ContainsKey(word[i]))
                    {
                        if (i == word.Length - 1)
                        {
                            var child = curNode.children[word[i]];
                            wordWasAlreadyAdded = child.IsTerminal;
                            child.IsTerminal = true;
                            child.Value = value;
                        }
                        else
                            curNode = curNode.children[word[i]];
                    }
                    else
                    {
                        if (i == word.Length - 1)
                        {
                            var newNode = new TrieMapNode<TValue>(word[i], value, true);
                            curNode.children.Add(word[i], newNode);
                        }
                        else
                        {
                            var newNode = new TrieMapNode<TValue>(word[i], default(TValue), false);
                            curNode.children.Add(word[i], newNode);
                            curNode = curNode.children[word[i]];
                        }
                    }
                }

                if (!wordWasAlreadyAdded) Count++;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TrieMap{TValue}"/> class.
        /// </summary>
        public TrieMap()
        {
            Root = new TrieMapNode<TValue>(default(char), default(TValue), false);
        }

        /// <summary>
        /// Adds a word and a value to it to the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        /// <param name="word">The word to add.</param>
        /// <param name="value">The value to add.</param>
        public void Add(string word, TValue value)
        {
            var curNode = Root;
            bool wordWasAlreadyAdded = false;

            for (int i = 0; i < word.Length; i++)
            {
                if (curNode.children.ContainsKey(word[i]))
                {
                    if (i == word.Length - 1)
                    {
                        var child = curNode.children[word[i]];
                        wordWasAlreadyAdded = child.IsTerminal;
                        child.IsTerminal = true;
                        child.Value = value;
                    }
                    else
                        curNode = curNode.children[word[i]];
                }
                else
                {
                    if (i == word.Length - 1)
                    {
                        var newNode = new TrieMapNode<TValue>(word[i], value, true);
                        curNode.children.Add(word[i], newNode);
                    }
                    else
                    {
                        var newNode = new TrieMapNode<TValue>(word[i], default(TValue), false);
                        curNode.children.Add(word[i], newNode);
                        curNode = curNode.children[word[i]];
                    }
                }
            }

            if (!wordWasAlreadyAdded) Count++;
        }

        /// <summary>
        /// Removes a word from the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        /// <param name="word">The word to remove.</param>
        /// <returns>true if the word is successfully removed; otherwise false. Also returns false if word is not found.</returns>
        public bool Remove(string word)
        {
            var curNode = Root;

            bool removedSuccessfully = true;

            if (!ContainsWord(word)) return false;

            var nodesList = new SinglyLinkedList<TrieMapNode<TValue>>();

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
        /// Determines whether a word is in the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>true if word is found; otherwise false.</returns>
        public bool ContainsWord(string word)
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
        /// Determines whether a value is in the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if value is found; otherwise false.</returns>
        public bool ContainsValue(TValue value)
        {
            // Stack containing the nodes for traversal
            var nodesStack = new Stack<TrieMapNode<TValue>>();
            // Stack containing the level of the current node
            var levelStack = new Stack<int>();

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

                // if the current word lenght is higher that the node level
                // we have to trim the lenght to the previous level
                // Note: this happens when we traverse all the nodes in a level
                // and drop to a lower level again
                if (curWord.Length >= curLevel)
                    curWord.Length = curLevel - 1;

                // Add the current node character to the word
                curWord.Append(curNode.Key);

                // If we have a complete word we check for its value
                if (curNode.IsTerminal)
                {
                    if (object.Equals(curNode.Value, value)) return true;
                }

                // Adding nodes in the stack
                foreach (var kvp in curNode.children)
                {
                    nodesStack.Push(kvp.Value);
                    levelStack.Push(curLevel + 1);
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified word in the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        /// <param name="word">The word of the value to get.</param>
        /// <param name="value">When element with the specified word is found, the value of the element; otherwise, the default value of the value type.</param>
        /// <returns>true if the <see cref="TrieMap{TValue}"/> contains an element with the specified word; otherwise, false.</returns>
        public virtual bool TryGetValue(string word, out TValue value)
        {
            value = default(TValue);

            var curNode = Root;

            for (int i = 0; i < word.Length; i++)
            {
                if (curNode.children.ContainsKey(word[i]))
                {
                    if (i == word.Length - 1)
                    {
                        if (curNode.children[word[i]].IsTerminal)
                        {
                            value = curNode.children[word[i]].Value;
                            return true;
                        }
                        else return false;
                    }
                    else
                        curNode = curNode.children[word[i]];
                }
                else return false;
            }

            return false;
        }

        /// <summary>
        /// Determines whether a prefix is in the <see cref="TrieMap{TValue}"/>.
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
        /// Removes all words from the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        public void Clear()
        {
            Root = new TrieMapNode<TValue>(default(char), default(TValue), false);
            Count = 0;
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/>(T being <see cref="KeyValuePair{TKey, TValue}"/> with key of type <see cref="string"/> and value of type TValue) of the words in the <see cref="TrieMap{TValue}"/> beginning with the given prefix sorted lexicographically.
        /// </summary>
        /// <param name="prefix">The prefix of the requested words.</param>
        /// <returns>Returns the words sorted in lexicographical order.</returns>
        public IEnumerable<KeyValuePair<string, TValue>> GetWordsByPrefix(string prefix)
        {
            return GetWordsByPrefix(prefix, Comparer<char>.Default);
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/>(T being <see cref="KeyValuePair{TKey, TValue}"/> with key of type <see cref="string"/> and value of type TValue) of the words in the <see cref="TrieMap{TValue}"/> beginning with the given prefix sorted by the given comparer.
        /// </summary>
        /// <param name="prefix">The prefix of the requested words.</param>
        /// <param name="comparer">The comparer which is used for sorting.</param>
        /// <returns>Returns the words sorted with the given comparer.</returns>
        public IEnumerable<KeyValuePair<string, TValue>> GetWordsByPrefix(string prefix, Comparer<char> comparer)
        {
            if (ContainsPrefix(prefix))
            {
                // Stack containing the nodes for traversal
                var nodesStack = new Stack<TrieMapNode<TValue>>();
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
                var sortedChildren = new SortedDictionary<char, TrieMapNode<TValue>>(curNode.children,
                                                Comparer<char>.Create((x, y) => comparer.Compare(y, x)));

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
                        yield return new KeyValuePair<string, TValue>(curWord.ToString(), curNode.Value);

                    sortedChildren = new SortedDictionary<char, TrieMapNode<TValue>>(curNode.children,
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
        /// Returns an <see cref="IEnumerable{T}"/>(T being <see cref="KeyValuePair{TKey, TValue}"/> with key of type <see cref="string"/> and value of type TValue) of all words in the <see cref="TrieMap{TValue}"/> sorted in lexicographical order.
        /// </summary>
        /// <returns>Returns the words sorted in lexicographical order.</returns>
        public IEnumerable<KeyValuePair<string, TValue>> GetAllWords()
        {
            return GetAllWords(Comparer<char>.Default);
        }

        /// <summary>
        /// Returns an <see cref="IEnumerable{T}"/>(T being <see cref="KeyValuePair{TKey, TValue}"/> with key of type <see cref="string"/> and value of type TValue) of all words in the <see cref="TrieMap{TValue}"/> sorted by the given comparer.
        /// </summary>
        /// <param name="comparer">The comparer which is used for sorting.</param>
        /// <returns>Returns the words sorted with the given comparer.</returns>
        public IEnumerable<KeyValuePair<string, TValue>> GetAllWords(Comparer<char> comparer)
        {
            // Stack containing the nodes for traversal
            var nodesStack = new Stack<TrieMapNode<TValue>>();
            // Stack containing the level of the current node
            var levelStack = new Stack<int>();

            var curNode = Root;

            // Creating reversed sorted dictionary for adding into the stack of nodes
            var sortedChildren = new SortedDictionary<char, TrieMapNode<TValue>>(curNode.children,
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
                    yield return new KeyValuePair<string, TValue>(curWord.ToString(), curNode.Value);

                // Creating reversed sorted dictionary for adding into the stack of nodes
                sortedChildren = new SortedDictionary<char, TrieMapNode<TValue>>(curNode.children,
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
        /// Returns an enumerator that iterates lexicographically through the <see cref="TrieMap{TValue}"/>.
        /// </summary>
        /// <returns>Returns the words sorted in lexicographical order.</returns>
        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        {
            return GetAllWords(Comparer<char>.Default).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
