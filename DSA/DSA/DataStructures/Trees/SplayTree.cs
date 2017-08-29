using DSA.DataStructures.Lists;
using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Trees
{
    /// <summary>
    /// Represents a Splay binary search tree.
    /// </summary>
    public class SplayTree<T> : BinarySearchTree<T>
    {
        /// <summary>
        /// Gets the tree root of the <see cref="SplayTree{T}"/>.
        /// </summary>
        public new SplayTreeNode<T> Root
        {
            get { return (SplayTreeNode<T>)base.Root; }
            internal set { base.Root = value; }
        }

        /// <summary>
        /// Gets the number of elements in the <see cref="SplayTree{T}"/>.
        /// </summary>
        public override int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="SplayTree{T}"/> class and uses the default <see cref="IComparer{T}"/> implementation to compare elements.
        /// </summary>
        public SplayTree() : base() { }

        /// <summary>
        ///  Creates a new instance of the <see cref="SplayTree{T}"/> class and uses the specified <see cref="IComparer{T}"/> implementation to compare elements.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer{T}"/> implementation to use when comparing elements, or null to use the default comparer <see cref="Comparer{T}.Default"/>.</param>
        public SplayTree(IComparer<T> comparer) : base(comparer) { }

        /// <summary>
        /// Standard left rotation.
        /// </summary>
        private SplayTreeNode<T> RotateLeft(SplayTreeNode<T> node)
        {
            SplayTreeNode<T> m = node.Right;
            node.Right = m.Left;
            m.Left = node;
            return m;
        }

        /// <summary>
        /// Standard right rotation.
        /// </summary>
        private SplayTreeNode<T> RotateRight(SplayTreeNode<T> node)
        {
            SplayTreeNode<T> m = node.Left;
            node.Left = m.Right;
            m.Right = node;
            return m;
        }

        /// <summary>
        /// Adds an element to the <see cref="SplayTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        public override void Add(T value)
        {
            if (Root == null)
            {
                Root = new SplayTreeNode<T>(value);
                Count++;
                return;
            }

            var curNode = Root;
            bool addToLeftSide = true;
            var traversedNodes = new SinglyLinkedList<SplayTreeNode<T>>();

            while (curNode != null)
            {
                traversedNodes.AddFirst(curNode);

                int cmp = comparer.Compare(value, curNode.Value);

                if (cmp < 0)
                {
                    curNode = curNode.Left;
                    addToLeftSide = true;
                }
                else if (cmp > 0)
                {
                    curNode = curNode.Right;
                    addToLeftSide = false;
                }
                else throw new ArgumentException("Tried to insert duplicate value!");
            }

            if (addToLeftSide)
            {
                var newNode = new SplayTreeNode<T>(value);
                traversedNodes.First.Value.Left = newNode;
                traversedNodes.AddFirst(newNode);
                Count++;
            }
            else
            {
                var newNode = new SplayTreeNode<T>(value);
                traversedNodes.First.Value.Right = newNode;
                traversedNodes.AddFirst(newNode);
                Count++;
            }

            //Have to splay tree after adding
            Splay(traversedNodes);
        }

        /// <summary>
        /// Splays the <see cref="SplayTree{T}"/> given a <see cref="SinglyLinkedList{T}"/> containing the traversed nodes. The first node being the one for splaying and the last being the root.
        /// </summary>
        /// <param name="traversedNodes">A <see cref="SinglyLinkedList{T}"/> containing the traversed nodes.</param>
        internal void Splay(SinglyLinkedList<SplayTreeNode<T>> traversedNodes)
        {
            if (traversedNodes == null) throw new ArgumentNullException(nameof(traversedNodes));
            if (traversedNodes.Last.Value != Root) throw new ArgumentException(nameof(traversedNodes) + "list does not end with the root node!");
            if (traversedNodes.Count == 1) return;

            var nodeToSplay = traversedNodes.First.Value;
            traversedNodes.RemoveFirst();

            while (traversedNodes.Count != 0)
            {
                var parent = traversedNodes.First.Value;
                traversedNodes.RemoveFirst();

                var grandParent = traversedNodes.First?.Value;
                if (grandParent != null) traversedNodes.RemoveFirst();

                var greatGrandParent = traversedNodes.First?.Value;

                if (grandParent == null)
                {
                    //one more rotation to make node root
                    if (parent.Left == nodeToSplay)
                    {
                        Root = RotateRight(parent);
                    }
                    else
                    {
                        Root = RotateLeft(parent);
                    }

                    return;
                }
                else// if grandparent is not null
                {
                    // check is grandparent is left child of great grandparent
                    // NOTE: this also returns false if great grandparent is null
                    bool grandParentIsLeftChild = greatGrandParent?.Left == grandParent;


                    // if parent is left child of grandparent
                    if (grandParent.Left == parent)
                    {
                        // if splay node is left child of its parent
                        if (parent.Left == nodeToSplay)
                        {
                            // Left Left Case - fixed by 2 right rotations
                            RotateRight(grandParent);
                            RotateRight(parent);
                        }
                        else// if splay node is right child of its parent
                        {
                            //Left Right Case - fixed by left and right rotation
                            grandParent.Left = RotateLeft(parent);
                            RotateRight(grandParent);

                        }
                    }
                    else// if parent is right child of grandparent
                    {
                        // if splay node is right child of its parent
                        if (parent.Right == nodeToSplay)
                        {
                            // Right Right Case - fixed by 2 left rotations
                            RotateLeft(grandParent);
                            RotateLeft(parent);
                        }
                        else// if splay node is left child of its parent
                        {
                            //Right Left Case - fixed by right and left rotation
                            grandParent.Right = RotateRight(parent);
                            RotateLeft(grandParent);

                        }
                    }

                    // After the rotations the splay node is on the grandparent
                    // position so now the great grandparent have to point to it
                    if (greatGrandParent == null)
                    {
                        // if there is no great grandparent
                        // then the splay node is the new root
                        Root = nodeToSplay;
                        return;
                    }
                    else// if we have great grand parent
                    {
                        // we update were it points
                        if (grandParentIsLeftChild)
                            greatGrandParent.Left = nodeToSplay;
                        else
                            greatGrandParent.Right = nodeToSplay;
                    }
                }
            }

            // The method returns as soon as splay node becomes the root
            // so this should never happen is the traversed nodes list is correct
            throw new Exception("Splaying was not performed correctly! The traversed nodes list is corrupted!");
        }

        /// <summary>
        /// Removes an element from the <see cref="SplayTree{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>true if the item is successfully removed; otherwise false. Also returns false if item is not found.</returns>
        public override bool Remove(T value)
        {
            if (Root == null) return false;

            var curNode = Root;
            var traversedNodes = new SinglyLinkedList<SplayTreeNode<T>>();
            bool lastWasLeftSide = true;

            while (curNode != null)
            {
                traversedNodes.AddFirst(curNode);

                int cmp = comparer.Compare(value, curNode.Value);

                if (cmp < 0)
                {
                    curNode = curNode.Left;
                    lastWasLeftSide = true;
                }
                else if (cmp > 0)
                {
                    curNode = curNode.Right;
                    lastWasLeftSide = false;
                }
                else// found the value
                {
                    Count--;

                    // remove the current node from the traversed nodes we don't need it
                    traversedNodes.RemoveFirst();

                    // deleting the node and replacing it with the min node in the right subtree
                    if (curNode.Right == null)
                    {
                        if (lastWasLeftSide)
                        {
                            if (curNode == Root)
                            {
                                // if node for removal is the root node
                                // no more operations are required
                                Root = curNode.Left;
                                curNode.Invalidate();
                                return true;
                            }
                            else traversedNodes.First.Value.Left = curNode.Left;
                        }
                        else
                        {
                            traversedNodes.First.Value.Right = curNode.Left;
                        }
                    }
                    else
                    {
                        SplayTreeNode<T> min = null;
                        var rightNode = FindAndRemoveMin(curNode.Right, ref min);
                        min.Right = rightNode;
                        min.Left = curNode.Left;

                        if (lastWasLeftSide)
                        {
                            if (curNode == Root)
                            {
                                // if node for removal is the root node
                                // no more operations are required
                                Root = min;
                                curNode.Invalidate();
                                return true;
                            }
                            else traversedNodes.First.Value.Left = min;
                        }
                        else
                        {
                            traversedNodes.First.Value.Right = min;
                        }
                    }

                    // Note: if the node for deletion was the root we want be here
                    // because everything is ok and no splaying is needed

                    // When removing a node from the tree we have to
                    // splay the tree for its parent
                    Splay(traversedNodes);

                    curNode.Invalidate();

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Finds the min node in the subtree and returns the root of the new subtree
        /// </summary>
        private SplayTreeNode<T> FindAndRemoveMin(SplayTreeNode<T> subtreeRoot, ref SplayTreeNode<T> min)
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
        /// Determines whether a value is in the <see cref="SplayTree{T}"/> and then it splays it.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if item is found; otherwise false.</returns>
        public override bool Contains(T value)
        {
            if (Root == null) return false;

            var curNode = Root;
            var traversedNodes = new SinglyLinkedList<SplayTreeNode<T>>();

            while (curNode != null)
            {
                traversedNodes.AddFirst(curNode);

                int cmp = comparer.Compare(value, curNode.Value);

                if (cmp < 0)
                {
                    curNode = curNode.Left;
                }
                else if (cmp > 0)
                {
                    curNode = curNode.Right;
                }
                else// found the value
                {
                    // have to splay for the searched node
                    Splay(traversedNodes);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="SplayTree{T}"/>.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="SplayTree{T}"/>.
        /// </summary>
        /// <returns>Returns the elements in ascending order.</returns>
        public override IEnumerator<T> GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
}
