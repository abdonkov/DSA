using System;
using System.Collections.Generic;


namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents an AVL binary search tree.
    /// </summary>
    public class AVLTree<T> : BinarySearchTree<T>
    {
        /// <summary>
        /// Gets the tree root of the <see cref="AVLTree{T}"/>.
        /// </summary>
        public new AVLTreeNode<T> Root
        {
            get { return (AVLTreeNode<T>)base.Root; }
            internal set { base.Root = value; }
        }

        /// <summary>
        /// Gets the height of the <see cref="AVLTree{T}"/>.
        /// </summary>
        public int Height { get { return NodeHeight(Root); } }

        /// <summary>
        /// Gets the number of elements in the <see cref="AVLTree{T}"/>.
        /// </summary>
        public override int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="AVLTree{T}"/> class and uses the default <see cref="IComparer{T}"/> implementation to compare elements.
        /// </summary>
        public AVLTree() : base() { }

        /// <summary>
        ///  Creates a new instance of the <see cref="AVLTree{T}"/> class and uses the specified <see cref="IComparer{T}"/> implementation to compare elements.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing elements, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        public AVLTree(IComparer<T> comparer) : base(comparer) { }

        /// <summary>
        /// Gets the node height. Returns 0 if node is null.
        /// </summary>
        private int NodeHeight(AVLTreeNode<T> node)
        {
            return node?.Height ?? 0;
        }

        /// <summary>
        /// Fixes the node height, calculating it from its children.
        /// </summary>
        private void FixHeight(AVLTreeNode<T> node)
        {
            var leftHeight = NodeHeight(node.Left);
            var rightHeight = NodeHeight(node.Right);
            node.Height = (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;
        }

        /// <summary>
        /// Calculates the balance factor.
        /// </summary>
        private int BalanceFactor(AVLTreeNode<T> node)
        {
            return NodeHeight(node.Left) - NodeHeight(node.Right);
        }

        /// <summary>
        /// Standard left rotation.
        /// </summary>
        private AVLTreeNode<T> RotateLeft(AVLTreeNode<T> node)
        {
            AVLTreeNode<T> m = node.Right;
            node.Right = m.Left;
            m.Left = node;
            FixHeight(node);
            FixHeight(m);
            return m;
        }

        /// <summary>
        /// Standard right rotation.
        /// </summary>
        private AVLTreeNode<T> RotateRight(AVLTreeNode<T> node)
        {
            AVLTreeNode<T> m = node.Left;
            node.Left = m.Right;
            m.Right = node;
            FixHeight(node);
            FixHeight(m);
            return m;
        }

        /// <summary>
        /// Checks if balancing is needed and performs it.
        /// </summary>
        private AVLTreeNode<T> Balance(AVLTreeNode<T> node)
        {
            FixHeight(node);
            // if balance factor is -2 the left subtree is smaller than the right
            // so we need to perform a left rotation
            if (BalanceFactor(node) == -2)
            {

                if (BalanceFactor(node.Right) > 0)// Right Left Case if true, else Right Right Case
                    node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
            // if balance factor is 2 the right subtree is smaller than the left
            // so we need to perform a right rotation
            if (BalanceFactor(node) == 2)
            {
                if (BalanceFactor(node.Left) < 0)// Left Right Case if true, else Left Left Case
                    node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            return node;
        }

        /// <summary>
        /// Adds an element to the <see cref="AVLTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public override void Add(T value)
        {
            Root = Add(Root, value);
            Count++;
        }

        /// <summary>
        /// Recursive insertion with balancing on every step.
        /// </summary>
        private AVLTreeNode<T> Add(AVLTreeNode<T> node, T value)
        {
            if (node == null) return new AVLTreeNode<T>(value);

            int cmp = comparer.Compare(value, node.Value);
            if (cmp < 0) node.Left = Add(node.Left, value);
            else if (cmp > 0) node.Right = Add(node.Right, value);
            else throw new ArgumentException("Tried to insert duplicate value!");

            return Balance(node);
        }

        /// <summary>
        /// Removes an element from the <see cref="AVLTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>true if the item is successfully removed; otherwise false. Also returns false if item is not found.</returns>
        public override bool Remove(T value)
        {
            int curCount = Count;
            Root = Remove(Root, value);
            return curCount != Count;
        }

        /// <summary>
        /// Recursive removal with balancing on every step.
        /// </summary>
        private AVLTreeNode<T> Remove(AVLTreeNode<T> node, T value)
        {
            if (node == null) return node;

            int cmp = comparer.Compare(value, node.Value);
            if (cmp < 0) node.Left = Remove(node.Left, value);
            else if (cmp > 0) node.Right = Remove(node.Right, value);
            else
            {
                Count--;
                AVLTreeNode<T> left = node.Left;
                AVLTreeNode<T> right = node.Right;

                node.Invalidate();

                if (right == null) return left;

                AVLTreeNode<T> min = null;
                AVLTreeNode<T> rightSubtreeRoot = FindAndRemoveMin(right, ref min);
                min.Right = rightSubtreeRoot;
                min.Left = left;

                return Balance(min);
            }
            return Balance(node);
        }

        /// <summary>
        /// Finds and removes the min element of the given subtree.
        /// </summary>
        private AVLTreeNode<T> FindAndRemoveMin(AVLTreeNode<T> node, ref AVLTreeNode<T> min)
        {
            if (node.Left == null)
            {
                min = node;
                return node.Right;
            }
            node.Left = FindAndRemoveMin(node.Left, ref min);
            return Balance(node);
        }

        /// <summary>
        /// Determines whether a value is in the <see cref="AVLTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if item is found; otherwise false.</returns>
        public override bool Contains(T value)
        {
            return base.Contains(value);
        }

        /// <summary>
        /// Removes all elements from the <see cref="AVLTree{T}"/>.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="AVLTree{T}"/>.
        /// </summary>
        /// <returns>Returns the elements in ascending order.</returns>
        public override IEnumerator<T> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
