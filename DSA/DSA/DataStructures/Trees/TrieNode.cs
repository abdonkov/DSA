using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="Trie"/>.
    /// </summary>
    public class TrieNode
    {
        /// <summary>
        /// Retrurns a dictionary containing the children of the <see cref="TrieNode"/>.
        /// </summary>
        internal Dictionary<char, TrieNode> children;

        /// <summary>
        /// Gets the char contained in the <see cref="TrieNode"/>.
        /// </summary>
        public char Key { get; internal set; }

        /// <summary>
        /// Determines whether the <see cref="TrieNode"/> is a terminal node.
        /// </summary>
        public bool IsTerminal { get; internal set; }

        /// <summary>
        /// Returns a read only dictionary containing the children of the <see cref="TrieNode"/>.
        /// </summary>
        public ReadOnlyDictionary<char, TrieNode> Children
        {
            get { return new ReadOnlyDictionary<char, TrieNode>(children); }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TrieNode"/> class, containing the specified char.
        /// </summary>
        /// <param name="key">The char to contain in the <see cref="TrieNode"/></param>
        /// <param name="isTerminal">Determines whether the node is terminal or not.</param>
        public TrieNode(char key, bool isTerminal)
        {
            Key = key;
            IsTerminal = isTerminal;
            children = new Dictionary<char, TrieNode>();
        }
    }
}
