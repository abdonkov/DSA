namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="RedBlackTree{T}"/>.
    /// </summary>
    public class RedBlackTreeNode<T> : BinarySearchTreeNode<T>
    {
        /// <summary>
        /// Gets the left child of the <see cref="RedBlackTreeNode{T}"/>.
        /// </summary>
        public new RedBlackTreeNode<T> Left
        {
            get { return (RedBlackTreeNode<T>)base.Left; }
            internal set { base.Left = value; }
        }

        /// <summary>
        /// Gets the right child of the <see cref="RedBlackTreeNode{T}"/>.
        /// </summary>
        public new RedBlackTreeNode<T> Right
        {
            get { return (RedBlackTreeNode<T>)base.Right; }
            internal set { base.Right = value; }
        }

        /// <summary>
        /// Gets the parent of the <see cref="RedBlackTreeNode{T}"/>.
        /// </summary>
        public RedBlackTreeNode<T> Parent { get; internal set; }

        /// <summary>
        /// Gets the value contained in the <see cref="RedBlackTreeNode{T}"/>.
        /// </summary>
        public override T Value { get; internal set; }

        /// <summary>
        /// Gets a bool indicating the color of the <see cref="RedBlackTreeNode{T}"/>.
        /// </summary>
        public bool IsRed { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedBlackTreeNode{T}"/> class, containing the specified value.
        /// </summary>
        /// <param name="value">The value to contain in the <see cref="RedBlackTreeNode{T}"/>.</param>
        public RedBlackTreeNode(T value) : base(value)
        {
            Value = value;
            IsRed = true;
        }

        /// <summary>
        /// Removes all references the <see cref="RedBlackTreeNode{T}"/> has.
        /// </summary>
        internal override void Invalidate()
        {
            Left = null;
            Right = null;
            Parent = null;
        }
    }
}
