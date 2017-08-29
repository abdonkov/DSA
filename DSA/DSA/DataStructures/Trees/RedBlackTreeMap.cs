using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a red-black binary search tree map.
    /// </summary>
    public class RedBlackTreeMap<TKey, TValue> : BinarySearchTreeMap<TKey, TValue>
    {
        /// <summary>
        /// Gets the tree root of the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        public new RedBlackTreeMapNode<TKey, TValue> Root
        {
            get { return (RedBlackTreeMapNode<TKey, TValue>)base.Root; }
            internal set { base.Root = value; }
        }

        /// <summary>
        /// Gets the number of elements in the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        public override int Count { get; internal set; }

        /// <summary>
        /// Gets or sets the value associated with the specified key in the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
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
                    throw new KeyNotFoundException("The key \"" + key.ToString() + "\" was not found in the RedBlackTreeMap.");
            }
            set
            {
                if (Root == null)
                {
                    Root = new RedBlackTreeMapNode<TKey, TValue>(key, value) { IsRed = false };
                    Count = 1;
                    return;
                }

                RedBlackTreeMapNode<TKey, TValue> parent = null;
                RedBlackTreeMapNode<TKey, TValue> current = Root;

                int cmp = 0;
                while (current != null)
                {
                    cmp = comparer.Compare(key, current.Key);

                    parent = current;

                    if (cmp < 0) current = current.Left;
                    else if (cmp > 0) current = current.Right;
                    else
                    {
                        current.Value = value;
                        return;
                    }
                }

                //ready to create new node
                RedBlackTreeMapNode<TKey, TValue> newNode = new RedBlackTreeMapNode<TKey, TValue>(key, value);
                newNode.Parent = parent;
                Count++;

                if (cmp < 0) parent.Left = newNode;
                else parent.Right = newNode;

                BalanceAfterAdd(newNode);
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="RedBlackTreeMap{TKey, TValue}"/> class and uses the default <see cref="IComparer{T}"/> implementation to compare the keys.
        /// </summary>
        public RedBlackTreeMap() : base() { }

        /// <summary>
        ///  Creates a new instance of the <see cref="RedBlackTreeMap{TKey, TValue}"/> class and uses the specified <see cref="IComparer{T}"/> implementation to compare the keys.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing keys, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        public RedBlackTreeMap(IComparer<TKey> comparer) : base(comparer) { }

        private RedBlackTreeMapNode<TKey, TValue> RotateLeft(RedBlackTreeMapNode<TKey, TValue> node)
        {
            RedBlackTreeMapNode<TKey, TValue> m = node.Right;
            node.Right = m.Left;
            if (m.Left != null)
            {
                m.Left.Parent = node;
            }

            if (node.Parent != null)
            {
                if (node == node.Parent.Left) node.Parent.Left = m;
                else if (node == node.Parent.Right) node.Parent.Right = m;
            }
            else
            {
                Root = m;
            }

            m.Parent = node.Parent;

            m.Left = node;

            node.Parent = m;

            return m;
        }

        private RedBlackTreeMapNode<TKey, TValue> RotateRight(RedBlackTreeMapNode<TKey, TValue> node)
        {
            RedBlackTreeMapNode<TKey, TValue> m = node.Left;
            node.Left = m.Right;
            if (m.Right != null)
            {
                m.Right.Parent = node;
            }

            if (node.Parent != null)
            {
                if (node == node.Parent.Left) node.Parent.Left = m;
                else if (node == node.Parent.Right) node.Parent.Right = m;
            }
            else
            {
                Root = m;
            }

            m.Parent = node.Parent;

            m.Right = node;

            node.Parent = m;

            return m;
        }

        /// <summary>
        /// Adds an element with the specified key and value into the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public override void Add(TKey key, TValue value)
        {
            if (Root == null)
            {
                Root = new RedBlackTreeMapNode<TKey, TValue>(key, value) { IsRed = false };
                Count = 1;
                return;
            }

            RedBlackTreeMapNode<TKey, TValue> parent = null;
            RedBlackTreeMapNode<TKey, TValue> current = Root;

            int cmp = 0;
            while (current != null)
            {
                cmp = comparer.Compare(key, current.Key);

                parent = current;

                if (cmp < 0) current = current.Left;
                else if (cmp > 0) current = current.Right;
                else throw new ArgumentException("Tried to insert duplicate value!");
            }

            //ready to create new node
            RedBlackTreeMapNode<TKey, TValue> newNode = new RedBlackTreeMapNode<TKey, TValue>(key, value);
            newNode.Parent = parent;
            Count++;

            if (cmp < 0) parent.Left = newNode;
            else parent.Right = newNode;

            BalanceAfterAdd(newNode);
        }

        private void BalanceAfterAdd(RedBlackTreeMapNode<TKey, TValue> current)
        {
            while (current != Root && current.Parent.IsRed)
            {
                var parent = current.Parent;
                var grandParent = current.Parent.Parent;
                RedBlackTreeMapNode<TKey, TValue> uncle;
                if (grandParent.Left == null || grandParent.Right == null)
                    uncle = new RedBlackTreeMapNode<TKey, TValue>(default(TKey), default(TValue)) { IsRed = false };
                else
                    uncle = grandParent.Left == parent ? grandParent.Right : grandParent.Left;


                if (uncle.IsRed) //case 1: uncle is red
                {
                    parent.IsRed = false;
                    uncle.IsRed = false;
                    grandParent.IsRed = true;

                    current = grandParent;
                }
                else //case 2: uncle is black
                {
                    if (parent == grandParent.Left) //Right Rotation is needed
                    {
                        if (current == parent.Right) //Left Rotation is needed
                        {
                            RotateLeft(parent);
                            parent = current;
                        }

                        RotateRight(grandParent);
                        //swap colors of parent and grandparent
                        grandParent.IsRed = true;
                        parent.IsRed = false;
                    }
                    else if (parent == grandParent.Right) //Left Rotation is needed
                    {
                        if (current == parent.Left) //Right Rotation is needed
                        {
                            RotateRight(parent);
                            parent = current;
                        }

                        RotateLeft(grandParent);
                        //swap colors of parent and grandparent
                        grandParent.IsRed = true;
                        parent.IsRed = false;
                    }
                }
                Root.IsRed = false;
            }
        }

        /// <summary>
        /// Removes an element with the specified key from the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully removed; otherwise false. Also returns false if element is not found.</returns>
        public override bool Remove(TKey key)
        {
            if (Root == null) return false;

            var curNode = Root;

            while (curNode != null)
            {
                int cmp = comparer.Compare(key, curNode.Key);

                if (cmp < 0)
                {
                    curNode = curNode.Left;
                }
                else if (cmp > 0)
                {
                    curNode = curNode.Right;
                }
                else
                {
                    if (curNode.Left != null)
                    {
                        // Finding the largest element in the left subtree
                        var subtreeNode = curNode.Left;
                        while (subtreeNode.Right != null) subtreeNode = subtreeNode.Right;
                        // Swap the key and value of this node with the one to delete
                        TKey xKey = curNode.Key;
                        TValue xValue = curNode.Value;
                        curNode.Key = subtreeNode.Key;
                        curNode.Value = subtreeNode.Value;
                        subtreeNode.Key = xKey;
                        subtreeNode.Value = xValue;
                        // Now the subtreeNode is the one to be deleted.
                        // This is done in order to have a node to delete with only one child
                        RemoveNodeAndBalanceTree(nodeForRemoval: subtreeNode, isNodeForRemovalNull: false);

                        subtreeNode.Invalidate();

                        // Create copy node of the current, place it on its position
                        // and invalidate current node. Used to invalidate references for
                        // the removed node the user can have
                        var copyNode = new RedBlackTreeMapNode<TKey, TValue>(curNode.Key, curNode.Value)
                        {
                            Parent = curNode.Parent,
                            Left = curNode.Left,
                            Right = curNode.Right,
                            IsRed = curNode.IsRed
                        };
                        if (copyNode.Left != null) copyNode.Left.Parent = copyNode;
                        if (copyNode.Right != null) copyNode.Right.Parent = copyNode;
                        if (curNode.Parent != null)
                        {
                            if (copyNode.Parent.Left == curNode)
                                copyNode.Parent.Left = copyNode;
                            else
                                copyNode.Parent.Right = copyNode;
                        }
                        else Root = copyNode;

                        curNode.Invalidate();
                    }
                    else if (curNode.Right != null)
                    {
                        // Finding the smallest element in the right subtree
                        var subtreeNode = curNode.Right;
                        while (subtreeNode.Left != null) subtreeNode = subtreeNode.Left;
                        // Swap the key and value of this node with the one to delete
                        TKey xKey = curNode.Key;
                        TValue xValue = curNode.Value;
                        curNode.Key = subtreeNode.Key;
                        curNode.Value = subtreeNode.Value;
                        subtreeNode.Key = xKey;
                        subtreeNode.Value = xValue;
                        // Now the subtreeNode is the one to be deleted.
                        // This is done in order to have a node to delete with only one child
                        RemoveNodeAndBalanceTree(nodeForRemoval: subtreeNode, isNodeForRemovalNull: false);

                        subtreeNode.Invalidate();

                        // Create copy node of the current, place it on its position
                        // and invalidate current node. Used to invalidate references for
                        // the removed node the user can have
                        var copyNode = new RedBlackTreeMapNode<TKey, TValue>(curNode.Key, curNode.Value)
                        {
                            Parent = curNode.Parent,
                            Left = curNode.Left,
                            Right = curNode.Right,
                            IsRed = curNode.IsRed
                        };
                        if (copyNode.Left != null) copyNode.Left.Parent = copyNode;
                        if (copyNode.Right != null) copyNode.Right.Parent = copyNode;
                        if (curNode.Parent != null)
                        {
                            if (copyNode.Parent.Left == curNode)
                                copyNode.Parent.Left = copyNode;
                            else
                                copyNode.Parent.Right = copyNode;
                        }
                        else Root = copyNode;

                        curNode.Invalidate();
                    }
                    else
                    {
                        // The node for deletion has no childs, so after "finding" the min/max element
                        // in the right/left subtree we swap the value with a null.
                        curNode.IsRed = false;
                        RemoveNodeAndBalanceTree(nodeForRemoval: curNode, isNodeForRemovalNull: true);

                        curNode.Invalidate();
                    }

                    Count--;
                    return true;
                }
            }
            return false;
        }

        private void RemoveNodeAndBalanceTree(RedBlackTreeMapNode<TKey, TValue> nodeForRemoval, bool isNodeForRemovalNull)
        {
            RedBlackTreeMapNode<TKey, TValue> child;
            bool isChildNull;

            if (nodeForRemoval == Root)
            {
                Root = null;
                return;
            }

            if (!isNodeForRemovalNull)
            {
                if (nodeForRemoval.Left != null)
                {
                    isChildNull = false;
                    child = nodeForRemoval.Left;

                    if (nodeForRemoval.Parent.Left == nodeForRemoval)
                        nodeForRemoval.Parent.Left = child;
                    else
                        nodeForRemoval.Parent.Right = child;

                    child.Parent = nodeForRemoval.Parent;

                    // Case 1: If one of the nodes is red, the replaced child is marked with black
                    // and no more balancing is needed
                    if (child.IsRed || nodeForRemoval.IsRed)
                    {
                        child.IsRed = false;
                        return;
                    }
                }
                else if (nodeForRemoval.Right != null)
                {
                    isChildNull = false;
                    child = nodeForRemoval.Right;

                    if (nodeForRemoval.Parent.Left == nodeForRemoval)
                        nodeForRemoval.Parent.Left = child;
                    else
                        nodeForRemoval.Parent.Right = child;

                    child.Parent = nodeForRemoval.Parent;

                    // Case 1: If one of the nodes is red, the replaced child is marked with black
                    // and no more balancing is needed
                    if (child.IsRed || nodeForRemoval.IsRed)
                    {
                        child.IsRed = false;
                        return;
                    }
                }
                else// nodeForRemoval has no children
                {
                    // Case 1: If one of the nodes is red, the replaced child is marked with black
                    // and no more balancing is needed
                    if (nodeForRemoval.IsRed)
                    {
                        // Note that null nodes are considered black so everything is ok
                        if (nodeForRemoval.Parent.Left == nodeForRemoval)
                            nodeForRemoval.Parent.Left = null;
                        else
                            nodeForRemoval.Parent.Right = null;

                        nodeForRemoval.Parent = null;
                        return;
                    }

                    isChildNull = true;
                    child = nodeForRemoval;
                    child.IsRed = false;
                }
            }
            else
            {
                isChildNull = true;
                child = nodeForRemoval;
                child.IsRed = false;
            }


            // Case 2: If we are here then both of the nodes are black.
            // In this case the child is considered "double black"

            var curNode = child;
            bool isCurNodeDoubleBlack = true;

            // While the current node is "double black" and is not the root we are doing the following:
            while (isCurNodeDoubleBlack && curNode != Root)
            {
                RedBlackTreeMapNode<TKey, TValue> sibling;
                bool siblingIsLeftChild;
                if (curNode.Parent.Left == curNode)
                {
                    sibling = curNode.Parent.Right;
                    siblingIsLeftChild = false;
                }
                else
                {
                    sibling = curNode.Parent.Left;
                    siblingIsLeftChild = true;
                }


                if (sibling == null)// if sibling is null it is considered black
                                    // and both its children are black. See case 2.2
                {
                    if (curNode.Parent.IsRed)
                    {
                        curNode.Parent.IsRed = false;
                        isCurNodeDoubleBlack = false;
                    }
                    else
                    {
                        curNode = curNode.Parent;
                    }
                }
                else if (!sibling.IsRed)
                {
                    var leftChildIsRed = sibling.Left?.IsRed ?? false;
                    var rightchildIsRed = sibling.Right?.IsRed ?? false;

                    // Case 2.1: If sibling is black and at least one
                    // of sibling’s children is red we need to perform rotations

                    if (leftChildIsRed || rightchildIsRed)// if one of the childs are red
                    {

                        if (siblingIsLeftChild)
                        {
                            if (rightchildIsRed && !leftChildIsRed)// Left Right Case
                            {
                                RotateLeft(sibling);
                                RotateRight(curNode.Parent);
                            }
                            else// Left Left Case
                            {
                                RotateRight(curNode.Parent);
                            }
                        }
                        else
                        {
                            if (leftChildIsRed && !rightchildIsRed)// Right Left Case
                            {
                                RotateRight(sibling);
                                RotateLeft(curNode.Parent);
                            }
                            else// Right Right Case
                            {
                                RotateLeft(curNode.Parent);
                            }
                        }

                        //After rotation everything is ok
                        isCurNodeDoubleBlack = false;
                    }
                    else// Case 2.2: if both children are black
                    {
                        // recoloring sibling
                        sibling.IsRed = true;

                        // if parent is red we recolor it and end the balancing
                        // because red + double black = single black
                        if (curNode.Parent.IsRed)
                        {
                            curNode.Parent.IsRed = false;
                            isCurNodeDoubleBlack = false;
                        }
                        else// if parent is black it is now double black(black + black = double black)
                        {
                            curNode = curNode.Parent; // so we continue the balancing for it
                        }

                        //here we stop using child anymore so
                        //if it was null node we need to remove it
                        if (isChildNull)
                        {
                            if (child.Parent.Left == child)
                                child.Parent.Left = null;
                            else
                                child.Parent.Right = null;

                            child.Invalidate();

                            isChildNull = false;
                        }
                    }

                }
                else// Case 2.3: if sibling is red
                {
                    //recolor sibling and parent
                    sibling.IsRed = false;
                    curNode.Parent.IsRed = true;

                    if (siblingIsLeftChild)// Left Left Case
                    {
                        RotateRight(curNode.Parent);
                    }
                    else// Right Right Case
                    {
                        RotateLeft(curNode.Parent);
                    }
                }
            }

            //removing child node if it was considered a null node
            if (isChildNull)
            {
                if (child.Parent.Left == child)
                    child.Parent.Left = null;
                else
                    child.Parent.Right = null;

                child.Invalidate();

                isChildNull = false;
            }
        }

        /// <summary>
        /// Determines whether an element with the specified key is contained in the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>true if key is found; otherwise false.</returns>
        public override bool ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether an element with the specified value is contained in the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if value is found; otherwise false.</returns>
        public override bool ContainsValue(TValue value)
        {
            return base.ContainsValue(value);
        }

        /// <summary>
        /// Gets the value associated with the specified key in the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When element with the specified key is found, the value of the element; otherwise, the default value of the value type.</param>
        /// <returns>true if the <see cref="RedBlackTreeMap{TKey, TValue}"/> contains an element with the specified key; otherwise, false.</returns>
        public override bool TryGetValue(TKey key, out TValue value)
        {
            return base.TryGetValue(key, out value);
        }

        /// <summary>
        /// Removes all elements from the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="RedBlackTreeMap{TKey, TValue}"/>.
        /// </summary>
        /// <returns>Returns the elements in ascending order.</returns>
        public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
