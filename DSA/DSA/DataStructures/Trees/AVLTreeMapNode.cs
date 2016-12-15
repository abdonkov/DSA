namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="AVLTreeMap{TKey, TValue}"/>.
    /// </summary>
    public class AVLTreeMapNode<TKey, TValue> : BinarySearchTreeMapNode<TKey, TValue>
    {
        /// <summary>
        /// Gets the left child of the <see cref="AVLTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public new AVLTreeMapNode<TKey, TValue> Left
        {
            get { return (AVLTreeMapNode<TKey, TValue>)base.Left; }
            internal set { base.Left = value; }
        }

        /// <summary>
        /// Gets the right child of the <see cref="AVLTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public new AVLTreeMapNode<TKey, TValue> Right
        {
            get { return (AVLTreeMapNode<TKey, TValue>)base.Right; }
            internal set { base.Right = value; }
        }

        /// <summary>
        /// Gets the key contained in the <see cref="AVLTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public override TKey Key { get; internal set; }

        /// <summary>
        /// Gets the value contained in the <see cref="AVLTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public override TValue Value { get; internal set; }

        /// <summary>
        /// Gets the height of the <see cref="AVLTreeMapNode{TKey, TValue}"/>.
        /// </summary>
        public int Height { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AVLTreeMapNode{TKey, TValue}"/> class, containing the specified key and value.
        /// </summary>
        /// <param name="key">The key to contain in the <see cref="AVLTreeMapNode{TKey, TValue}"/>.</param>
        /// /// <param name="value">The value to contain in the <see cref="AVLTreeMapNode{TKey, TValue}"/>.</param>
        public AVLTreeMapNode(TKey key, TValue value) : base(key, value)
        {
            Key = key;
            Value = value;
            Height = 1;
        }

        /// <summary>
        /// Removes all references the <see cref="AVLTreeMapNode{TKey, TValue}"/> has.
        /// </summary>
        internal override void Invalidate()
        {
            base.Invalidate();
        }
    }
}
