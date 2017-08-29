using System;
using System.Collections;
using System.Collections.Generic;

namespace DSA.DataStructures.Lists
{
    /// <summary>
    /// Represents a skip list.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class SkipList<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// A randomizer used for generating the level of the newly added <see cref="SkipListNode{T}"/>.
        /// </summary>
        internal Random randomizer { get; set; }

        /// <summary>
        /// The probability of increasing the level of the newly added <see cref="SkipListNode{T}"/>.
        /// </summary>
        internal readonly double probability = 0.5;

        /// <summary>
        /// Gets the head node of the <see cref="SkipList{T}"/>.
        /// Note that the head node of the <see cref="SkipList{T}"/> is a dummy node and it's value is not contained in the <see cref="SkipList{T}"/>.
        /// </summary>
        public SkipListNode<T> Head { get; internal set; }

        /// <summary>
        /// Gets the height of the tallest <see cref="SkipListNode{T}"/> in the <see cref="SkipList{T}"/>.
        /// </summary>
        public int Height { get { return Head.Height; } }

        /// <summary>
        /// Gets the number of elements in the <see cref="SkipList{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="DoublyLinkedList{T}"/> class that is empty.
        /// </summary>
        public SkipList()
        {
            Head = new SkipListNode<T>(default(T), 1);
            Count = 0;
            randomizer = new Random();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SkipList{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="SkipList{T}"/>.</param>
        public SkipList(IEnumerable<T> collection) : base()
        {
            foreach (var item in collection)
            {
                Add(item);
            }
        }

        /// <summary>
        /// Adds a value in the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <param name="value">The value to add in the <see cref="SkipList{T}"/>.</param>
        public void Add(T value)
        {
            // Gets the nodes which have to be updated(the nodes before the one we want to insert)
            var nodesForUpdate = new SkipListNode<T>[Height];
            var curNode = Head;
            
            for (int i = Height - 1; i >= 0; i--)
            {
                while(curNode[i] != null && value.CompareTo(curNode[i].Value) > 0)
                {
                    curNode = curNode[i];
                }

                nodesForUpdate[i] = curNode;
            }

            // Check if inserting a duplicate
            var nodeBeforeNodeForInsertion = nodesForUpdate[0];

            if (nodeBeforeNodeForInsertion[0] != null && value.CompareTo(nodeBeforeNodeForInsertion[0].Value) == 0)
                throw new ArgumentException("Tried to insert duplicate value!");

            // Generates the height of the new node
            int height = 1;
            while (randomizer.NextDouble() < probability && height < Head.Height + 1)
                height++;

            // Creating the new node
            var newNode = new SkipListNode<T>(value, height);
            
            if (height > Head.Height)// If the new node is higher that the head node
            {
                // Icrease the head level
                Head.IncrementHeight();
                Head[Head.Height - 1] = newNode;
            }

            // Adding the new node in the list
            for (int i = 0; i < newNode.Height; i++)
            {
                // For every level which have to be updated
                if (i < nodesForUpdate.Length)
                {
                    // The new node starts to point where the node before it points.
                    newNode[i] = nodesForUpdate[i][i];
                    // The old node starts to point to the new inserted node.
                    nodesForUpdate[i][i] = newNode;

                    // Note:
                    // nodesForUpdate is an array of the nodes before the one we add.
                    // nodesForUpdate[i] is the node on the level i before our new node.
                    // nodesForUpdate[i][i] is the reference to the next node on level i.
                }
            }

            Count++;
        }

        /// <summary>
        /// Removes a value from the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove from the <see cref="SkipList{T}"/>.</param>
        /// <returns>true if the value is removed successfully; otherwise false.</returns>
        public bool Remove(T value)
        {
            if (Count == 0) return false;

            // Gets the nodes which have to be updated(the nodes before the one we will delete)
            var nodesForUpdate = new SkipListNode<T>[Height];
            var curNode = Head;

            for (int i = Height - 1; i >= 0; i--)
            {
                while (curNode[i] != null && value.CompareTo(curNode[i].Value) > 0)
                {
                    curNode = curNode[i];
                }

                nodesForUpdate[i] = curNode;
            }

            var nodeToDelete = nodesForUpdate[0][0];

            if (nodeToDelete != null && value.CompareTo(nodeToDelete.Value) == 0)
            {
                // Removing references to the node for deletion and changing them to the node after it
                for (int i = 0; i < Head.Height; i++)
                {
                    if (nodesForUpdate[i][i] != nodeToDelete)
                        break;
                    else
                        nodesForUpdate[i][i] = nodeToDelete[i];
                        // the node now points to the node after the node for removal

                    // Note:
                    // nodesForUpdate is an array of the nodes before the one we are deleting.
                    // nodesForUpdate[i] is the node on the level i before the node for removal.
                    // nodesForUpdate[i][i] is the reference to the next node on level i.
                }

                // Removing references of deleted node
                nodeToDelete.Invalidate();

                // Check if we have to decrease the list level
                if (Head[Head.Height - 1] == null)
                {
                    Head.DecrementHeight();
                }

                Count--;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Determines whether a value is in the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <param name="value">The value to search in the <see cref="SkipList{T}"/>.</param>
        /// <returns>returns true if the value is found; otherwise false.</returns>
        public bool Contains(T value)
        {
            var curNode = Head;

            for (int i = Height - 1; i >= 0; i--)
            {
                while (curNode[i] != null)
                {
                    int cmp = value.CompareTo(curNode[i].Value);

                    if (cmp == 0) return true;
                    //if the value is lower that the next node we need to search on the lower level
                    if (cmp < 0) break;
                    //if the node is higher that the next node we continue comparing on the same level
                    if (cmp > 0) curNode = curNode[i];
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="SkipList{T}"/>.
        /// </summary>
        public void Clear()
        {
            var curNode = Head;
            var lastNode = Head;

            while (curNode != null)
            {
                curNode = curNode[0];
                lastNode.Invalidate();
                lastNode = curNode;
            }

            Head = new SkipListNode<T>(default(T), 1);
            Count = 0;
            randomizer = new Random();
        }

        /// <summary>
        /// Returns an enumerator that iterates throught the <see cref="SkipList{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="SkipList{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var curNode = Head[0];

            while (curNode != null)
            {
                yield return curNode.Value;
                curNode = curNode[0];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
