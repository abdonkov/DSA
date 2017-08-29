using System;
using System.Collections;
using System.Collections.Generic;

namespace DSA.DataStructures.Lists
{
    /// <summary>
    /// Represents a doubly linked list.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class DoublyLinkedList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets the first node of the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        public DoublyLinkedListNode<T> First { get; internal set; }

        /// <summary>
        /// Gets the last node of the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        public DoublyLinkedListNode<T> Last { get; internal set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="DoublyLinkedList{T}"/> class that is empty.
        /// </summary>
        public DoublyLinkedList()
        {
            Count = 0;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DoublyLinkedList{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="DoublyLinkedList{T}"/>.</param>
        public DoublyLinkedList(IEnumerable<T> collection)
        {
            Count = 0;
            DoublyLinkedListNode<T> last = null;

            foreach (var item in collection)
            {
                if (First == null)
                {
                    First = new DoublyLinkedListNode<T>(item, this);
                    last = First;
                }
                else
                {
                    var newNode = new DoublyLinkedListNode<T>(item, this);
                    last.Next = newNode;
                    newNode.Previous = last;
                    last = newNode;
                }

                Count++;
            }

            Last = last;
        }

        /// <summary>
        /// Adds a new node containing the specified value at the start of the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="value">The value to add at the start of the <see cref="DoublyLinkedList{T}"/>.</param>
        /// <returns>The new <see cref="DoublyLinkedListNode{T}"/> containing the value.</returns>
        public DoublyLinkedListNode<T> AddFirst(T value)
        {
            var newNode = new DoublyLinkedListNode<T>(value);

            AddFirst(newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified new node at the start of the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The new <see cref="DoublyLinkedListNode{T}"/> to add at the start of the <see cref="DoublyLinkedList{T}"/>.</param>
        public void AddFirst(DoublyLinkedListNode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != null) throw new InvalidOperationException("node belongs to another list");

            node.List = this;
            node.Previous = null;
            node.Next = First;
            if (First != null) First.Previous = node;
            First = node;
            Count++;

            if (First.Next == null) Last = First;
        }

        /// <summary>
        /// Adds a new node containing the specified value after the specified existing node in the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="DoublyLinkedListNode{T}"/> after which to insert a new <see cref="DoublyLinkedListNode{T}"/> containing the value.</param>
        /// <param name="value">The value to add to the <see cref="DoublyLinkedList{T}"/>.</param>
        /// <returns>The new <see cref="DoublyLinkedListNode{T}"/> containing the value.</returns>
        public DoublyLinkedListNode<T> AddAfter(DoublyLinkedListNode<T> node, T value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");

            if (node == Last) return AddLast(value);

            var newNode = new DoublyLinkedListNode<T>(value);

            AddAfter(node, newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified new node after the specified existing node in the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="DoublyLinkedListNode{T}"/> after which to insert newNode.</param>
        /// <param name="newNode">The new <see cref="DoublyLinkedListNode{T}"/> to add to the <see cref="DoublyLinkedList{T}"/>.</param>
        public void AddAfter(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> newNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");
            if (newNode == null) throw new ArgumentNullException(nameof(newNode));
            if (newNode.List != null) throw new InvalidOperationException("newNode belongs to another list");

            if (node == Last)
            {
                AddLast(newNode);
                return;
            }

            newNode.List = this;

            node.Next.Previous = newNode;
            newNode.Next = node.Next;

            newNode.Previous = node;
            node.Next = newNode;

            Count++;
        }

        /// <summary>
        /// Adds a new node containing the specified value before the specified existing node in the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="DoublyLinkedListNode{T}"/> before which to insert a new <see cref="DoublyLinkedListNode{T}"/> containing the value.</param>
        /// <param name="value">The value to add to the <see cref="DoublyLinkedList{T}"/>.</param>
        /// <returns>The new <see cref="DoublyLinkedListNode{T}"/> containing the value.</returns>
        public DoublyLinkedListNode<T> AddBefore(DoublyLinkedListNode<T> node, T value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");

            if (node == First) return AddFirst(value);

            var newNode = new DoublyLinkedListNode<T>(value);

            AddBefore(node, newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified new node before the specified existing node in the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="DoublyLinkedListNode{T}"/> before which to insert newNode.</param>
        /// <param name="newNode">The new <see cref="DoublyLinkedListNode{T}"/> to add to the <see cref="DoublyLinkedList{T}"/>.</param>
        public void AddBefore(DoublyLinkedListNode<T> node, DoublyLinkedListNode<T> newNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");
            if (newNode == null) throw new ArgumentNullException(nameof(newNode));
            if (newNode.List != null) throw new InvalidOperationException("newNode belongs to another list");

            if (node == First)
            {
                AddFirst(newNode);
                return;
            }

            newNode.List = this;

            node.Previous.Next = newNode;
            newNode.Previous = node.Previous;

            newNode.Next = node;
            node.Previous = newNode;

            Count++;
        }

        /// <summary>
        /// Adds a new node containing the specified value at the end of the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="value">The value to add at the end of the <see cref="DoublyLinkedList{T}"/>.</param>
        public DoublyLinkedListNode<T> AddLast(T value)
        {
            if (Count == 0) return AddFirst(value);
            
            var newNode = new DoublyLinkedListNode<T>(value);

            AddLast(newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified new node at the end of the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The new <see cref="DoublyLinkedListNode{T}"/> to add at the end of the <see cref="DoublyLinkedList{T}"/>.</param>
        public void AddLast(DoublyLinkedListNode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != null) throw new InvalidOperationException("node belongs to another list");

            if (Count == 0)
            {
                AddFirst(node);
                return;
            }

            node.List = this;
            node.Next = null;
            node.Previous = Last;
            Last.Next = node;
            Last = node;
            Count++;            
        }

        /// <summary>
        /// Removes the first occurrence of the specified value from the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove from the <see cref="DoublyLinkedList{T}"/>.</param>
        /// <returns>true if the value is successfully removed; otherwise false. It also returns false if the value was not found in the <see cref="DoublyLinkedList{T}"/>.</returns>
        public bool Remove(T value)
        {
            if (Count == 0) return false;

            if (object.Equals(First.Value, value))
            {
                RemoveFirst();
                return true;
            }

            var curNode = First.Next;

            while (curNode != null)
            {
                if (object.Equals(curNode.Value, value))
                {
                    curNode.Previous.Next = curNode.Next;

                    if (curNode.Next != null)
                        curNode.Next.Previous = curNode.Previous;
                    else
                        Last = curNode.Previous;

                    curNode.Invalidate();
                    Count--;
                    return true;
                }

                curNode = curNode.Next;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified node from the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="DoublyLinkedListNode{T}"/> to remove from the <see cref="DoublyLinkedList{T}"/>.</param>
        public void Remove(DoublyLinkedListNode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");

            if (node == First)
            {
                RemoveFirst();
                return;
            }

            if (node == Last)
            {
                RemoveLast();
                return;
            }

            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            node.Invalidate();
            Count--;
        }

        /// <summary>
        /// Removes the node at the start of the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        public void RemoveFirst()
        {
            if (Count == 0) throw new InvalidOperationException("The DoublyLinkedList doesn't contain any elements.");

            var oldFirst = First;
            First = First.Next;
            oldFirst.Invalidate();

            if (First != null)
                First.Previous = null;
            else
                Last = null;

            Count--;
        }

        /// <summary>
        /// Removes the node at the end of the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        public void RemoveLast()
        {
            if (Count == 0) throw new InvalidOperationException("The DoublyLinkedList doesn't contain any elements.");

            var oldLast = Last;
            Last = Last.Previous;
            oldLast.Invalidate();

            if (Last != null)
                Last.Next = null;
            else
                First = null;

            Count--;

        }

        /// <summary>
        /// Determines whether an value is in the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <param name="value">The value to search in the <see cref="DoublyLinkedList{T}"/>.</param>
        /// <returns>returns true if the value is found; otherwise false.</returns>
        public bool Contains(T value)
        {
            if (Count == 0) return false;

            var curNode = First;

            while (curNode != null)
            {
                if (object.Equals(curNode.Value, value)) return true;

                curNode = curNode.Next;
            }

            return false;
        }

        /// <summary>
        /// Removes all nodes from the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        public void Clear()
        {
            if (Count == 0) return;

            var curNode = First;
            var lastNode = curNode;
            while (curNode != null)
            {
                curNode = curNode.Next;
                lastNode.Invalidate();
                lastNode = curNode;
            }

            First = null;
            Last = null;
            Count = 0;
        }

        /// <summary>
        /// Returns an enumerator that iterates throught the <see cref="DoublyLinkedList{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="DoublyLinkedList{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var curNode = First;

            while (curNode != null)
            {
                yield return curNode.Value;
                curNode = curNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
