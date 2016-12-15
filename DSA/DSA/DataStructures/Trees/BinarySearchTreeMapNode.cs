namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
    /// </summary>
    public class BinarySearchTreeMapNode<TKey, TValue>
    {
        /// <summary>
        /// Gets the left child of the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public BinarySearchTreeMapNode<TKey, TValue> Left { get; internal set; }

        /// <summary>
        /// Gets the right child of the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public BinarySearchTreeMapNode<TKey, TValue> Right { get; internal set; }

        /// <summary>
        /// Gets the key contained in the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public virtual TKey Key { get; internal set; }

        /// <summary>
        /// Gets the value contained in the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public virtual TValue Value { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/> class, containing the specified key and value.
        /// </summary>
        /// <param name="key">The key to contain in the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/>.</param>
        /// <param name="value">The value to contain in the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/>.</param>
        public BinarySearchTreeMapNode(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Removes all references the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/> has.
        /// </summary>
        internal virtual void Invalidate()
        {
            Left = null;
            Right = null;
        }
    }
}
