namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="BinarySearchTree{T}"/>.
    /// </summary>
    public class BinarySearchTreeNode<T>
    {
        /// <summary>
        /// Gets the left child of the <see cref="BinarySearchTreeNode{T}"/>.
        /// </summary>
        public BinarySearchTreeNode<T> Left { get; internal set; }

        /// <summary>
        /// Gets the right child of the <see cref="BinarySearchTreeNode{T}"/>.
        /// </summary>
        public BinarySearchTreeNode<T> Right { get; internal set; }

        /// <summary>
        /// Gets the value contained in the <see cref="BinarySearchTreeNode{T}"/>.
        /// </summary>
        public virtual T Value { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BinarySearchTreeNode{T}"/> class, containing the specified value.
        /// </summary>
        /// <param name="value">The value to contain in the <see cref="BinarySearchTreeNode{T}"/>.</param>
        public BinarySearchTreeNode(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Removes all references the <see cref="BinarySearchTreeNode{T}"/> has.
        /// </summary>
        internal virtual void Invalidate()
        {
            Left = null;
            Right = null;
        }
    }
}
