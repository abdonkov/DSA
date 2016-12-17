using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a red-black binary search tree.
    /// </summary>
    public class RedBlackTree<T> : BinarySearchTree<T>
    {
        /// <summary>
        /// Gets the tree root of the <see cref="RedBlackTree{T}"/>.
        /// </summary>
        public new RedBlackTreeNode<T> Root
        {
            get { return (RedBlackTreeNode<T>)base.Root; }
            internal set { base.Root = value; }
        }

        /// <summary>
        /// Gets the number of elements in the <see cref="RedBlackTree{T}"/>.
        /// </summary>
        public override int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="RedBlackTree{T}"/> class and uses the default <see cref="IComparer{T}"/> implementation to compare elements.
        /// </summary>
        public RedBlackTree() : base() { }

        /// <summary>
        ///  Creates a new instance of the <see cref="RedBlackTree{T}"/> class and uses the specified <see cref="IComparer{T}"/> implementation to compare elements.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing elements, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        public RedBlackTree(IComparer<T> comparer) : base(comparer) { }

        private RedBlackTreeNode<T> RotateLeft(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> m = node.Right;
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

        private RedBlackTreeNode<T> RotateRight(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> m = node.Left;
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
        /// Adds an element to the <see cref="RedBlackTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public override void Add(T value)
        {
            if (Root == null)
            {
                Root = new RedBlackTreeNode<T>(value) { IsRed = false };
                Count = 1;
                return;
            }

            RedBlackTreeNode<T> parent = null;
            RedBlackTreeNode<T> current = Root;

            int cmp = 0;
            while(current != null)
            {
                cmp = comparer.Compare(value, current.Value);

                parent = current;

                if (cmp < 0) current = current.Left;
                else if (cmp > 0) current = current.Right;
                else throw new ArgumentException("Tried to insert duplicate value!");
            }

            //ready to create new node
            RedBlackTreeNode<T> newNode = new RedBlackTreeNode<T>(value);
            newNode.Parent = parent;
            Count++;

            if (cmp < 0) parent.Left = newNode;
            else parent.Right = newNode;

            BalanceAfterAdd(newNode);
        }

        private void BalanceAfterAdd(RedBlackTreeNode<T> current)
        {
            while (current != Root && current.Parent.IsRed)
            {
                var parent = current.Parent;
                var grandParent = current.Parent.Parent;
                RedBlackTreeNode<T> uncle;
                if (grandParent.Left == null || grandParent.Right == null)
                    uncle = new RedBlackTreeNode<T>(default(T)) { IsRed = false };
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
        /// Removes an element from the <see cref="RedBlackTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>true if the item is successfully removed; otherwise false. Also returns false if item is not found.</returns>
        public override bool Remove(T value)
        {
            if (Root == null) return false;

            var curNode = Root;

            while (curNode != null)
            {
                int cmp = comparer.Compare(value, curNode.Value);

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
                        // Swap the values of this node with the one to delete
                        T x = curNode.Value;
                        curNode.Value = subtreeNode.Value;
                        subtreeNode.Value = x;
                        // Now the subtreeNode is the one to be deleted.
                        // This is done in order to have a node to delete with only one child
                        RemoveNodeAndBalanceTree(nodeForRemoval: subtreeNode, isNodeForRemovalNull: false);

                        subtreeNode.Invalidate();

                        // Create copy node of the current, place it on its position
                        // and invalidate current node. Used to invalidate references for
                        // the removed node the user can have
                        var copyNode = new RedBlackTreeNode<T>(curNode.Value)
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
                        // Swap the values of this node with the one to delete
                        T x = curNode.Value;
                        curNode.Value = subtreeNode.Value;
                        subtreeNode.Value = x;
                        // Now the subtreeNode is the one to be deleted.
                        // This is done in order to have a node to delete with only one child
                        RemoveNodeAndBalanceTree(nodeForRemoval: subtreeNode, isNodeForRemovalNull: false);

                        subtreeNode.Invalidate();

                        // Create copy node of the current, place it on its position
                        // and invalidate current node. Used to invalidate references for
                        // the removed node the user can have
                        var copyNode = new RedBlackTreeNode<T>(curNode.Value)
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

        private void RemoveNodeAndBalanceTree(RedBlackTreeNode<T> nodeForRemoval, bool isNodeForRemovalNull)
        {
            RedBlackTreeNode<T> child;
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
                RedBlackTreeNode<T> sibling;
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
        /// Determines whether a value is in the <see cref="RedBlackTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if item is found; otherwise false.</returns>
        public override bool Contains(T value)
        {
            return base.Contains(value);
        }

        /// <summary>
        /// Removes all elements from the <see cref="RedBlackTree{T}"/>.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="RedBlackTree{T}"/>.
        /// </summary>
        /// <returns>Returns the elements in ascending order.</returns>
        public override IEnumerator<T> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
