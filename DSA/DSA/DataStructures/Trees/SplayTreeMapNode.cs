namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="SplayTreeMap{TKey, TValue}"/>.
    /// </summary>
    public class SplayTreeMapNode<TKey, TValue> : BinarySearchTreeMapNode<TKey, TValue>
    {
        /// <summary>
        /// Gets the left child of the <see cref="SplayTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public new SplayTreeMapNode<TKey, TValue> Left
        {
            get { return (SplayTreeMapNode<TKey, TValue>)base.Left; }
            internal set { base.Left = value; }
        }

        /// <summary>
        /// Gets the right child of the <see cref="SplayTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public new SplayTreeMapNode<TKey, TValue> Right
        {
            get { return (SplayTreeMapNode<TKey, TValue>)base.Right; }
            internal set { base.Right = value; }
        }

        /// <summary>
        /// Gets the key contained in the <see cref="SplayTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public override TKey Key { get; internal set; }

        /// <summary>
        /// Gets the value contained in the <see cref="SplayTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public override TValue Value { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplayTreeMapNode{TKey, TValue}"/> class, containing the specified key and value.
        /// </summary>
        /// <param name="key">The key to contain in the <see cref="SplayTreeMapNode{TKey, TValue}"/>.</param>
        /// <param name="value">The value to contain in the <see cref="SplayTreeMapNode{TKey, TValue}"/>.</param>
        public SplayTreeMapNode(TKey key, TValue value) : base(key, value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Removes all references the <see cref="SplayTreeMapNode{TKey, TValue}"/> has.
        /// </summary>
        internal override void Invalidate()
        {
            base.Invalidate();
        }
    }
}
