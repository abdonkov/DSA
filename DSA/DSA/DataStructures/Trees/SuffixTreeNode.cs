using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="SuffixTree"/>.
    /// </summary>
    public class SuffixTreeNode
    {
        /// <summary>
        /// Retrurns a dictionary containing the children of the <see cref="SuffixTreeNode"/>.
        /// </summary>
        internal Dictionary<char, SuffixTreeNode> children;

        /// <summary>
        /// Gets the char contained in the <see cref="SuffixTreeNode"/>.
        /// </summary>
        public char Key { get; internal set; }

        /// <summary>
        /// Determines whether the <see cref="SuffixTreeNode"/> is a terminal node.
        /// </summary>
        public bool IsTerminal { get; internal set; }

        /// <summary>
        /// Returns a read only dictionary containing the children of the <see cref="SuffixTreeNode"/>.
        /// </summary>
        public ReadOnlyDictionary<char, SuffixTreeNode> Children
        {
            get { return new ReadOnlyDictionary<char, SuffixTreeNode>(children); }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SuffixTreeNode"/> class, containing the specified char.
        /// </summary>
        /// <param name="key">The char to contain in the <see cref="SuffixTreeNode"/></param>
        /// <param name="isTerminal">Determines whether the node is terminal or not.</param>
        public SuffixTreeNode(char key, bool isTerminal)
        {
            Key = key;
            IsTerminal = isTerminal;
            children = new Dictionary<char, SuffixTreeNode>();
        }
    }
}
