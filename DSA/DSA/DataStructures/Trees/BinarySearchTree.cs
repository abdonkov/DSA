using System;
using System.Collections.Generic;
using System.Collections;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a binary search tree.
    /// </summary>
    public class BinarySearchTree<T> : IEnumerable<T>
    {
        /// <summary>
        /// The comparer of the elements in the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        internal IComparer<T> comparer;

        /// <summary>
        /// Gets the tree root of the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        public BinarySearchTreeNode<T> Root { get; internal set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        public virtual int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="BinarySearchTree{T}"/> class and uses the default <see cref="IComparer{T}"/> implementation to compare elements.
        /// </summary>
        public BinarySearchTree() : this(null) { }

        /// <summary>
        ///  Creates a new instance of the <see cref="BinarySearchTree{T}"/> class and uses the specified <see cref="IComparer{T}"/> implementation to compare elements.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing elements, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        public BinarySearchTree(IComparer<T> comparer)
        {
            this.comparer = comparer ?? Comparer<T>.Default;
            Root = null;
            Count = 0;
        }

        /// <summary>
        /// Adds an element to the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public virtual void Add(T value)
        {
            if (Root == null)
            {
                Root = new BinarySearchTreeNode<T>(value);
                Count++;
                return;
            }

            var curNode = Root;
            var lastNode = Root;
            bool addedToLeftSide = true;

            while (curNode != null)
            {
                int cmp = comparer.Compare(value, curNode.Value);

                if (cmp < 0)
                {
                    lastNode = curNode;
                    curNode = curNode.Left;
                    addedToLeftSide = true;
                }
                else if (cmp > 0)
                {
                    lastNode = curNode;
                    curNode = curNode.Right;
                    addedToLeftSide = false;
                }
                else throw new ArgumentException("Tried to insert duplicate value!");
            }

            if (addedToLeftSide)
            {
                lastNode.Left = new BinarySearchTreeNode<T>(value);
                Count++;
            }
            else
            {
                lastNode.Right = new BinarySearchTreeNode<T>(value);
                Count++;
            }
        }

        /// <summary>
        /// Removes an element from the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>true if the item is successfully removed; otherwise false. Also returns false if item is not found.</returns>
        public virtual bool Remove(T value)
        {
            if (Root == null) return false;

            var curNode = Root;
            var lastNode = Root;
            bool lastWasLeftSide = true;

            while (curNode != null)
            {
                int cmp = comparer.Compare(value, curNode.Value);

                if (cmp < 0)
                {
                    lastNode = curNode;
                    curNode = curNode.Left;
                    lastWasLeftSide = true;
                }
                else if (cmp > 0)
                {
                    lastNode = curNode;
                    curNode = curNode.Right;
                    lastWasLeftSide = false;
                }
                else
                {
                    if (curNode.Right == null)
                    {
                        if (lastWasLeftSide)
                        {
                            if (curNode == Root) Root = curNode.Left;
                            else lastNode.Left = curNode.Left;
                        }
                        else
                        {
                            lastNode.Right = curNode.Left;
                        }
                    }
                    else
                    {
                        BinarySearchTreeNode<T> min = null;
                        var rightNode = FindAndRemoveMin(curNode.Right, ref min);
                        min.Right = rightNode;
                        min.Left = curNode.Left;

                        if (lastWasLeftSide)
                        {
                            if (curNode == Root) Root = min;
                            else lastNode.Left = min;
                        }
                        else
                        {
                            lastNode.Right = min;
                        }
                    }

                    curNode.Invalidate();
                    Count--;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Finds the min node in the subtree and returns the root of the new subtree
        /// </summary>
        private BinarySearchTreeNode<T> FindAndRemoveMin(BinarySearchTreeNode<T> subtreeRoot, ref BinarySearchTreeNode<T> min)
        {
            if (subtreeRoot.Left == null)
            {
                if (subtreeRoot.Right == null)
                {
                    min = subtreeRoot;
                    return null;
                }
                else
                {
                    min = subtreeRoot;
                    return subtreeRoot.Right;
                }
            }

            var curNode = subtreeRoot;
            var lastNode = subtreeRoot;

            while (curNode.Left != null)
            {
                lastNode = curNode;
                curNode = curNode.Left;
            }

            lastNode.Left = curNode.Right;
            min = curNode;

            return subtreeRoot;
        }

        /// <summary>
        /// Determines whether a value is in the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if item is found; otherwise false.</returns>
        public virtual bool Contains(T value)
        {
            if (Root == null) return false;
            var curNode = Root;
            while (curNode != null)
            {
                int cmp = comparer.Compare(value, curNode.Value);
                if (cmp == 0) return true;
                if (cmp < 0) curNode = curNode.Left;
                if (cmp > 0) curNode = curNode.Right;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        public virtual void Clear()
        {
            if (Root != null)
            {
                HashSet<BinarySearchTreeNode<T>> cleared = new HashSet<BinarySearchTreeNode<T>>();
                Stack<BinarySearchTreeNode<T>> stack = new Stack<BinarySearchTreeNode<T>>();
                stack.Push(Root);
                while (stack.Count > 0)
                {
                    BinarySearchTreeNode<T> curNode = stack.Peek();

                    if (curNode.Left == null || cleared.Contains(curNode.Left))
                    {
                        cleared.Add(curNode);
                        stack.Pop();

                        if (curNode.Right != null) stack.Push(curNode.Right);

                        curNode.Invalidate();
                    }
                    else stack.Push(curNode.Left);
                }
            }

            Root = null;
            Count = 0;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="BinarySearchTree{T}"/>.
        /// </summary>
        /// <returns>Returns the elements in ascending order.</returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            if (Root != null)
            {
                HashSet<BinarySearchTreeNode<T>> returned = new HashSet<BinarySearchTreeNode<T>>();
                Stack<BinarySearchTreeNode<T>> stack = new Stack<BinarySearchTreeNode<T>>();
                stack.Push(Root);
                while (stack.Count > 0)
                {
                    BinarySearchTreeNode<T> curNode = stack.Peek();

                    if (curNode.Left == null || returned.Contains(curNode.Left))
                    {
                        returned.Add(curNode);
                        stack.Pop();
                        yield return curNode.Value;

                        if (curNode.Right != null) stack.Push(curNode.Right);
                    }
                    else stack.Push(curNode.Left);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
