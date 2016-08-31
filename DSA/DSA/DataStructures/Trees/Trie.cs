using DSA.DataStructures.Interfaces;
using DSA.DataStructures.Lists;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a Trie (Prefix Tree).
    /// </summary>
    public class Trie : ITree<string>
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

            while (curListNode != null)
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
        /// Determines whether a word is in the <see cref="AVLTree{T}"/>.
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
        /// Removes all words from the <see cref="Trie"/>.
        /// </summary>
        public void Clear()
        {
            Root = null;
            Count = 0;
        }
    }
}
