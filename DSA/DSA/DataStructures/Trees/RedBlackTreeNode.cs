using System;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="RedBlackTree{T}"/>.
    /// </summary>
    /// <typeparam name="T">T implements <see cref="IComparable{T}">.</typeparam>
    public class RedBlackTreeNode<T> : BinarySearchTreeNode<T>
        where T : IComparable<T>
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
        public new T Value { get; internal set; }

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
    }
}
