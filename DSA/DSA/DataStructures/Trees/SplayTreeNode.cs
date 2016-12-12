namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="SplayTree{T}"/>.
    /// </summary>
    public class SplayTreeNode<T> : BinarySearchTreeNode<T>
    {
        /// <summary>
        /// Gets the left child of the <see cref="SplayTreeNode{T}"/>.
        /// </summary>
        public new SplayTreeNode<T> Left
        {
            get { return (SplayTreeNode<T>)base.Left; }
            internal set { base.Left = value; }
        }

        /// <summary>
        /// Gets the right child of the <see cref="SplayTreeNode{T}"/>.
        /// </summary>
        public new SplayTreeNode<T> Right
        {
            get { return (SplayTreeNode<T>)base.Right; }
            internal set { base.Right = value; }
        }

        /// <summary>
        /// Gets the value contained in the <see cref="SplayTreeNode{T}"/>.
        /// </summary>
        public override T Value { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplayTreeNode{T}"/> class, containing the specified value.
        /// </summary>
        /// <param name="value">The value to contain in the <see cref="SplayTreeNode{T}"/>.</param>
        public SplayTreeNode(T value) : base(value)
        {
            Value = value;
        }

        /// <summary>
        /// Removes all references the <see cref="SplayTreeNode{T}"/> has.
        /// </summary>
        internal override void Invalidate()
        {
            base.Invalidate();
        }
    }
}
