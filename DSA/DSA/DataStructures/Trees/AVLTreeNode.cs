namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="AVLTree{T}"/>.
    /// </summary>
    public class AVLTreeNode<T> : BinarySearchTreeNode<T>
    {
        /// <summary>
        /// Gets the left child of the <see cref="AVLTreeNode{T}"/>.
        /// </summary>
        public new AVLTreeNode<T> Left
        {
            get { return (AVLTreeNode<T>)base.Left; }
            internal set { base.Left = value; }
        }
        /// <summary>
        /// Gets the right child of the <see cref="AVLTreeNode{T}"/>.
        /// </summary>
        public new AVLTreeNode<T> Right
        {
            get { return (AVLTreeNode<T>)base.Right; }
            internal set { base.Right = value; }
        }

        /// <summary>
        /// Gets the value contained in the <see cref="AVLTreeNode{T}"/>.
        /// </summary>
        public override T Value { get; internal set; }

        /// <summary>
        /// Gets the height of the <see cref="AVLTreeNode{T}"/>.
        /// </summary>
        public int Height { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AVLTreeNode{T}"/> class, containing the specified value.
        /// </summary>
        /// <param name="value">The value to contain in the <see cref="AVLTreeNode{T}"/>.</param>
        public AVLTreeNode(T value) : base(value)
        {
            Value = value;
            Height = 1;
        }

        /// <summary>
        /// Removes all references the <see cref="AVLTreeNode{T}"/> has.
        /// </summary>
        internal override void Invalidate()
        {
            base.Invalidate();
        }
    }
}
