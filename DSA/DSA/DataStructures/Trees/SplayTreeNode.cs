using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a node in the <see cref="SplayTree{T}"/>.
    /// </summary>
    /// <typeparam name="T">T implements <see cref="IComparable{T}">.</typeparam>
    public class SplayTreeNode<T> : BinarySearchTreeNode<T>
        where T : IComparable<T>
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
        public new T Value { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SplayTreeNode{T}"/> class, containing the specified value.
        /// </summary>
        /// <param name="value">The value to contain in the <see cref="SplayTreeNode{T}"/>.</param>
        public SplayTreeNode(T value) : base(value)
        {
            Value = value;
        }
    }
}
