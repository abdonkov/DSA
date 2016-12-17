namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
    /// </summary>
    public class RedBlackTreeMapNode<TKey, TValue> : BinarySearchTreeMapNode<TKey, TValue>
    {
        /// <summary>
        /// Gets the left child of the <see cref="RedBlackTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public new RedBlackTreeMapNode<TKey, TValue> Left
        {
            get { return (RedBlackTreeMapNode<TKey, TValue>)base.Left; }
            internal set { base.Left = value; }
        }

        /// <summary>
        /// Gets the right child of the <see cref="RedBlackTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public new RedBlackTreeMapNode<TKey, TValue> Right
        {
            get { return (RedBlackTreeMapNode<TKey, TValue>)base.Right; }
            internal set { base.Right = value; }
        }

        /// <summary>
        /// Gets the parent of the <see cref="RedBlackTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public RedBlackTreeMapNode<TKey, TValue> Parent { get; internal set; }

        /// <summary>
        /// Gets the key contained in the <see cref="RedBlackTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public override TKey Key { get; internal set; }

        /// <summary>
        /// Gets the value contained in the <see cref="RedBlackTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public override TValue Value { get; internal set; }

        /// <summary>
        /// Gets a bool indicating the color of the <see cref="RedBlackTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public bool IsRed { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTreeMapNode{TKey, TValue}"/> class, containing the specified key and value.
        /// </summary>
        /// <param name="key">The key to contain in the <see cref="RedBlackTreeMapNode{TKey, TValue}"/>.</param>
        /// <param name="value">The value to contain in the <see cref="RedBlackTreeMapNode{TKey, TValue}"/>.</param>
        public RedBlackTreeMapNode(TKey key, TValue value) : base(key, value)
        {
            Key = key;
            Value = value;
            IsRed = true;
        }

        /// <summary>
        /// Removes all references the <see cref="RedBlackTreeMapNode{TKey, TValue}"/> has.
        /// </summary>
        internal override void Invalidate()
        {
            Left = null;
            Right = null;
            Parent = null;
        }
    }
}
