using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents an AVL binary search tree map.
    /// </summary>
    public class AVLTreeMap<TKey, TValue> : BinarySearchTreeMap<TKey, TValue>
    {
        /// <summary>
        /// Gets the tree root of the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        public new AVLTreeMapNode<TKey, TValue> Root
        {
            get { return (AVLTreeMapNode<TKey, TValue>)base.Root; }
            internal set { base.Root = value; }
        }

        /// <summary>
        /// Gets the height of the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        public int Height { get { return NodeHeight(Root); } }

        /// <summary>
        /// Gets the number of elements in the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        public override int Count { get; internal set; }

        /// <summary>
        /// Gets or sets the value associated with the specified key in the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key. If the specified key is not found,
        /// the get operation throws a <see cref="KeyNotFoundException"/>, and
        /// the set operation creates a new element with the specified key.</returns>
        public override TValue this[TKey key]
        {
            get
            {
                TValue value;
                if (TryGetValue(key, out value))
                    return value;
                else
                    throw new KeyNotFoundException("The key \"" + key.ToString() + "\" was not found in the AVLTreeMap.");
            }
            set
            {
                bool updated;
                Root = AddOrUpdate(Root, key, value, out updated);
                if (!updated) Count++;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AVLTreeMap{TKey, TValue}"/> class and uses the default <see cref="IComparer{T}"/> implementation to compare the keys.
        /// </summary>
        public AVLTreeMap() : base() { }

        /// <summary>
        ///  Creates a new instance of the <see cref="AVLTreeMap{TKey, TValue}"/> class and uses the specified <see cref="IComparer{T}"/> implementation to compare the keys.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing keys, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        public AVLTreeMap(IComparer<TKey> comparer) : base(comparer) { }

        /// <summary>
        /// Gets the node height. Returns 0 if node is null.
        /// </summary>
        private int NodeHeight(AVLTreeMapNode<TKey, TValue> node)
        {
            return node?.Height ?? 0;
        }

        /// <summary>
        /// Fixes the node height, calculating it from its children.
        /// </summary>
        private void FixHeight(AVLTreeMapNode<TKey, TValue> node)
        {
            var leftHeight = NodeHeight(node.Left);
            var rightHeight = NodeHeight(node.Right);
            node.Height = (leftHeight > rightHeight ? leftHeight : rightHeight) + 1;
        }

        /// <summary>
        /// Calculates the balance factor.
        /// </summary>
        private int BalanceFactor(AVLTreeMapNode<TKey, TValue> node)
        {
            return NodeHeight(node.Left) - NodeHeight(node.Right);
        }

        /// <summary>
        /// Standard left rotation.
        /// </summary>
        private AVLTreeMapNode<TKey, TValue> RotateLeft(AVLTreeMapNode<TKey, TValue> node)
        {
            AVLTreeMapNode<TKey, TValue> m = node.Right;
            node.Right = m.Left;
            m.Left = node;
            FixHeight(node);
            FixHeight(m);
            return m;
        }

        /// <summary>
        /// Standard right rotation.
        /// </summary>
        private AVLTreeMapNode<TKey, TValue> RotateRight(AVLTreeMapNode<TKey, TValue> node)
        {
            AVLTreeMapNode<TKey, TValue> m = node.Left;
            node.Left = m.Right;
            m.Right = node;
            FixHeight(node);
            FixHeight(m);
            return m;
        }

        /// <summary>
        /// Checks if balancing is needed and performs it.
        /// </summary>
        private AVLTreeMapNode<TKey, TValue> Balance(AVLTreeMapNode<TKey, TValue> node)
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
        /// Adds an element with the specified key and value into the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public override void Add(TKey key, TValue value)
        {
            Root = Add(Root, key, value);
            Count++;
        }

        /// <summary>
        /// Recursive insertion with balancing on every step.
        /// </summary>
        private AVLTreeMapNode<TKey, TValue> Add(AVLTreeMapNode<TKey, TValue> node, TKey key, TValue value)
        {
            if (node == null) return new AVLTreeMapNode<TKey, TValue>(key, value);

            int cmp = comparer.Compare(key, node.Key);
            if (cmp < 0) node.Left = Add(node.Left, key, value);
            else if (cmp > 0) node.Right = Add(node.Right, key, value);
            else throw new ArgumentException("Tried to insert duplicate value!");

            return Balance(node);
        }

        /// <summary>
        /// Recursive add or update used in indexer.
        /// </summary>
        private AVLTreeMapNode<TKey, TValue> AddOrUpdate(AVLTreeMapNode<TKey, TValue> node, TKey key, TValue value, out bool updated)
        {
            updated = false;

            if (node == null) return new AVLTreeMapNode<TKey, TValue>(key, value);

            int cmp = comparer.Compare(key, node.Key);
            if (cmp < 0) node.Left = AddOrUpdate(node.Left, key, value, out updated);
            else if (cmp > 0) node.Right = AddOrUpdate(node.Right, key, value, out updated);
            else
            {
                node.Value = value;
                updated = true; // mark that value is updated
                return node;
            }

            if (updated) // if only updated, no need to balance
                return node;
            else
                return Balance(node);
        }

        /// <summary>
        /// Removes an element with the specified key from the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully removed; otherwise false. Also returns false if element is not found.</returns>
        public override bool Remove(TKey key)
        {
            int curCount = Count;
            Root = Remove(Root, key);
            return curCount != Count;
        }

        /// <summary>
        /// Recursive removal with balancing on every step.
        /// </summary>
        private AVLTreeMapNode<TKey, TValue> Remove(AVLTreeMapNode<TKey, TValue> node, TKey key)
        {
            if (node == null) return node;

            int cmp = comparer.Compare(key, node.Key);
            if (cmp < 0) node.Left = Remove(node.Left, key);
            else if (cmp > 0) node.Right = Remove(node.Right, key);
            else
            {
                Count--;
                AVLTreeMapNode<TKey, TValue> left = node.Left;
                AVLTreeMapNode<TKey, TValue> right = node.Right;

                node.Invalidate();

                if (right == null) return left;

                AVLTreeMapNode<TKey, TValue> min = null;
                AVLTreeMapNode<TKey, TValue> rightSubtreeRoot = FindAndRemoveMin(right, ref min);
                min.Right = rightSubtreeRoot;
                min.Left = left;

                return Balance(min);
            }
            return Balance(node);
        }

        /// <summary>
        /// Finds and removes the min element of the given subtree.
        /// </summary>
        private AVLTreeMapNode<TKey, TValue> FindAndRemoveMin(AVLTreeMapNode<TKey, TValue> node, ref AVLTreeMapNode<TKey, TValue> min)
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
        /// Determines whether an element with the specified key is contained in the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>true if key is found; otherwise false.</returns>
        public override bool ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether an element with the specified value is contained in the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if value is found; otherwise false.</returns>
        public override bool ContainsValue(TValue value)
        {
            return base.ContainsValue(value);
        }

        /// <summary>
        /// Gets the value associated with the specified key in the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When element with the specified key is found, the value of the element; otherwise, the default value of the value type.</param>
        /// <returns>true if the <see cref="AVLTreeMap{TKey, TValue}"/> contains an element with the specified key; otherwise, false.</returns>
        public override bool TryGetValue(TKey key, out TValue value)
        {
            return base.TryGetValue(key, out value);
        }

        /// <summary>
        /// Removes all elements from the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="AVLTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <returns>Returns the elements in ascending order.</returns>
        public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
