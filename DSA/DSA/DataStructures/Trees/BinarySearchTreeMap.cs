using System;
using System.Collections;
using System.Collections.Generic;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a binary search tree map.
    /// </summary>
    public class BinarySearchTreeMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// The comparer of the keys in the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        internal IComparer<TKey> comparer;

        /// <summary>
        /// Gets the tree root of the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        public BinarySearchTreeMapNode<TKey, TValue> Root { get; internal set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        public virtual int Count { get; internal set; }

        /// <summary>
        /// Gets or sets the value associated with the specified key in the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found,
        /// the get operation throws a <see cref="KeyNotFoundException"/>, and
        /// the set operation creates a new element with the specified key.</returns>
        public virtual TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (TryGetValue(key, out value))
                    return value;
                else
                    throw new KeyNotFoundException("The key \"" + key.ToString() + "\" was not found in the BinarySearchTreeMap.");
            }
            set
            {
                if (Root == null)
                {
                    Root = new BinarySearchTreeMapNode<TKey, TValue>(key, value);
                    Count++;
                    return;
                }

                var curNode = Root;
                var lastNode = Root;
                bool addedToLeftSide = true;

                while (curNode != null)
                {
                    int cmp = comparer.Compare(key, curNode.Key);

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
                    else // found the key
                    {
                        curNode.Value = value;
                        return;
                    }
                }

                if (addedToLeftSide)
                {
                    lastNode.Left = new BinarySearchTreeMapNode<TKey, TValue>(key, value);
                    Count++;
                }
                else
                {
                    lastNode.Right = new BinarySearchTreeMapNode<TKey, TValue>(key, value);
                    Count++;
                }
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BinarySearchTreeMap{TKey, TValue}"/> class and uses the default <see cref="IComparer{T}"/> implementation to compare the keys.
        /// </summary>
        public BinarySearchTreeMap() : this(null) { }

        /// <summary>
        ///  Creates a new instance of the <see cref="BinarySearchTreeMap{TKey, TValue}"/> class and uses the specified <see cref="IComparer{T}"/> implementation to compare the keys.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing keys, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        public BinarySearchTreeMap(IComparer<TKey> comparer)
        {
            this.comparer = comparer ?? Comparer<TKey>.Default;
            Root = null;
            Count = 0;
        }

        /// <summary>
        /// Adds an element with the specified key and value into the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public virtual void Add(TKey key, TValue value)
        {
            if (Root == null)
            {
                Root = new BinarySearchTreeMapNode<TKey, TValue>(key, value);
                Count++;
                return;
            }

            var curNode = Root;
            var lastNode = Root;
            bool addedToLeftSide = true;

            while (curNode != null)
            {
                int cmp = comparer.Compare(key, curNode.Key);

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
                else throw new ArgumentException("Tried to insert duplicate key!");
            }

            if (addedToLeftSide)
            {
                lastNode.Left = new BinarySearchTreeMapNode<TKey, TValue>(key, value);
                Count++;
            }
            else
            {
                lastNode.Right = new BinarySearchTreeMapNode<TKey, TValue>(key, value);
                Count++;
            }
        }

        /// <summary>
        /// Removes an element with the specified key from the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully removed; otherwise false. Also returns false if element is not found.</returns>
        public virtual bool Remove(TKey key)
        {
            if (Root == null) return false;

            var curNode = Root;
            var lastNode = Root;
            bool lastWasLeftSide = true;

            while (curNode != null)
            {
                int cmp = comparer.Compare(key, curNode.Key);

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
                        BinarySearchTreeMapNode<TKey, TValue> min = null;
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
        private BinarySearchTreeMapNode<TKey, TValue> FindAndRemoveMin(BinarySearchTreeMapNode<TKey, TValue> subtreeRoot, ref BinarySearchTreeMapNode<TKey, TValue> min)
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
        /// Determines whether an element with the specified key is contained in the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>true if key is found; otherwise false.</returns>
        public virtual bool ContainsKey(TKey key)
        {
            if (Root == null) return false;
            var curNode = Root;
            while (curNode != null)
            {
                int cmp = comparer.Compare(key, curNode.Key);
                if (cmp == 0) return true;
                if (cmp < 0) curNode = curNode.Left;
                if (cmp > 0) curNode = curNode.Right;
            }
            return false;
        }

        /// <summary>
        /// Determines whether an element with the specified value is contained in the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if value is found; otherwise false.</returns>
        public virtual bool ContainsValue(TValue value)
        {
            if (Root == null) return false;

            HashSet<BinarySearchTreeMapNode<TKey, TValue>> cleared = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
            Stack<BinarySearchTreeMapNode<TKey, TValue>> stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
            stack.Push(Root);
            while (stack.Count > 0)
            {
                BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

                if (curNode.Left == null || cleared.Contains(curNode.Left))
                {
                    cleared.Add(curNode);
                    stack.Pop();

                    if (object.Equals(value, curNode.Value)) return true;

                    if (curNode.Right != null) stack.Push(curNode.Right);
                }
                else stack.Push(curNode.Left);
            }

            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key in the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When element with the specified key is found, the value of the element; otherwise, the default value of the value type.</param>
        /// <returns>true if the <see cref="BinarySearchTreeMap{TKey, TValue}"/> contains an element with the specified key; otherwise, false.</returns>
        public virtual bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);

            if (Root == null) return false;
            var curNode = Root;
            while (curNode != null)
            {
                int cmp = comparer.Compare(key, curNode.Key);
                if (cmp == 0)
                {
                    value = curNode.Value;
                    return true;
                }
                if (cmp < 0) curNode = curNode.Left;
                if (cmp > 0) curNode = curNode.Right;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        public virtual void Clear()
        {
            if (Root != null)
            {
                HashSet<BinarySearchTreeMapNode<TKey, TValue>> cleared = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
                Stack<BinarySearchTreeMapNode<TKey, TValue>> stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
                stack.Push(Root);
                while (stack.Count > 0)
                {
                    BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

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
        /// Returns an enumerator that iterates through the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <returns>Returns the elements in ascending order.</returns>
        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (Root != null)
            {
                HashSet<BinarySearchTreeMapNode<TKey, TValue>> returned = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
                Stack<BinarySearchTreeMapNode<TKey, TValue>> stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
                stack.Push(Root);
                while (stack.Count > 0)
                {
                    BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

                    if (curNode.Left == null || returned.Contains(curNode.Left))
                    {
                        returned.Add(curNode);
                        stack.Pop();
                        yield return new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value);

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
