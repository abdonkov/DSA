using System;
using System.Collections;
using System.Collections.Generic;

namespace DSA.DataStructures.Lists
{
    /// <summary>
    /// Represents a singly linked list.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Gets the first node of the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        public SinglyLinkedListNode<T> First { get; internal set; }

        /// <summary>
        /// Gets the last node of the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        public SinglyLinkedListNode<T> Last { get; internal set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="SinglyLinkedList{T}"/> class that is empty.
        /// </summary>
        public SinglyLinkedList()
        {
            Count = 0;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SinglyLinkedList{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection whose elements are copied to the new <see cref="SinglyLinkedList{T}"/>.</param>
        public SinglyLinkedList(IEnumerable<T> collection)
        {
            Count = 0;
            SinglyLinkedListNode<T> last = null;

            foreach (var item in collection)
            {
                if (First == null)
                {
                    First = new SinglyLinkedListNode<T>(item, this);
                    last = First;
                }
                else
                {
                    var newNode = new SinglyLinkedListNode<T>(item, this);
                    last.Next = newNode;
                    last = newNode;
                }

                Count++;
            }

            Last = last;
        }
        
        /// <summary>
        /// Adds a new node containing the specified value at the start of the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="value">The value to add at the start of the <see cref="SinglyLinkedList{T}"/>.</param>
        /// <returns>The new <see cref="SinglyLinkedListNode{T}"/> containing the value.</returns>
        public SinglyLinkedListNode<T> AddFirst(T value)
        {
            var newNode = new SinglyLinkedListNode<T>(value);

            AddFirst(newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified new node at the start of the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The new <see cref="SinglyLinkedListNode{T}"/> to add at the start of the <see cref="SinglyLinkedList{T}"/>.</param>
        public void AddFirst(SinglyLinkedListNode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != null) throw new InvalidOperationException("node belongs to another list");

            node.List = this;
            node.Next = First;
            First = node;
            Count++;

            if (First.Next == null) Last = First;
        }

        /// <summary>
        /// Adds a new node containing the specified value after the specified existing node in the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="SinglyLinkedListNode{T}"/> after which to insert a new <see cref="SinglyLinkedListNode{T}"/> containing the value.</param>
        /// <param name="value">The value to add to the <see cref="SinglyLinkedList{T}"/>.</param>
        /// <returns>The new <see cref="SinglyLinkedListNode{T}"/> containing the value.</returns>
        public SinglyLinkedListNode<T> AddAfter(SinglyLinkedListNode<T> node, T value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");

            var newNode = new SinglyLinkedListNode<T>(value);

            AddAfter(node, newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified new node after the specified existing node in the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="SinglyLinkedListNode{T}"/> after which to insert newNode.</param>
        /// <param name="newNode">The new <see cref="SinglyLinkedListNode{T}"/> to add to the <see cref="SinglyLinkedList{T}"/>.</param>
        public void AddAfter(SinglyLinkedListNode<T> node, SinglyLinkedListNode<T> newNode)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");
            if (newNode == null) throw new ArgumentNullException(nameof(newNode));
            if (newNode.List != null) throw new InvalidOperationException("newNode belongs to another list");

            newNode.List = this;
            newNode.Next = node.Next;
            node.Next = newNode;
            Count++;

            if (newNode.Next == null) Last = newNode;
        }

        /// <summary>
        /// Adds a new node containing the specified value before the specified existing node in the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="SinglyLinkedListNode{T}"/> before which to insert a new <see cref="SinglyLinkedListNode{T}"/> containing the value.</param>
        /// <param name="value">The value to add to the <see cref="SinglyLinkedList{T}"/>.</param>
        /// <returns>The new <see cref="SinglyLinkedListNode{T}"/> containing the value.</returns>
        public SinglyLinkedListNode<T> AddBefore(SinglyLinkedListNode<T> node, T value)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");

            if (node == First) return AddFirst(value);

            var newNode = new SinglyLinkedListNode<T>(value);

            AddBefore(node, newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified new node before the specified existing node in the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="SinglyLinkedListNode{T}"/> before which to insert newNode.</param>
        /// <param name="newNode">The new <see cref="SinglyLinkedListNode{T}"/> to add to the <see cref="SinglyLinkedList{T}"/>.</param>
        public void AddBefore(SinglyLinkedListNode<T> node, SinglyLinkedListNode<T> newNode)
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

            var curNode = First;

            while (curNode.Next != node)
            {
                curNode = curNode.Next;
            }

            newNode.List = this;
            curNode.Next = newNode;
            newNode.Next = node;
            Count++;
        }

        /// <summary>
        /// Adds a new node containing the specified value at the end of the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="value">The value to add at the end of the <see cref="SinglyLinkedList{T}"/>.</param>
        public SinglyLinkedListNode<T> AddLast(T value)
        {
            if (Count == 0) return AddFirst(value);

            var newNode = new SinglyLinkedListNode<T>(value);

            AddLast(newNode);

            return newNode;
        }

        /// <summary>
        /// Adds the specified new node at the end of the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The new <see cref="SinglyLinkedListNode{T}"/> to add at the end of the <see cref="SinglyLinkedList{T}"/>.</param>
        public void AddLast(SinglyLinkedListNode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != null) throw new InvalidOperationException("node belongs to another list");

            if (Count == 0)
            {
                AddFirst(node);
                return;
            }

            node.List = this;
            Last.Next = node;
            Last = node;
            Count++;
        }

        /// <summary>
        /// Removes the first occurrence of the specified value from the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove from the <see cref="SinglyLinkedList{T}"/>.</param>
        /// <returns>true if the value is successfully removed; otherwise false. It also returns false if the value was not found in the <see cref="SinglyLinkedList{T}"/>.</returns>
        public bool Remove(T value)
        {
            if (Count == 0) return false;

            if (object.Equals(First.Value, value))
            {
                RemoveFirst();
                return true;
            }

            var curNode = First.Next;
            var lastNode = First;

            while(curNode != null)
            {
                if (object.Equals(curNode.Value, value))
                {
                    lastNode.Next = curNode.Next;
                    if (lastNode.Next == null) Last = lastNode;

                    curNode.Invalidate();
                    Count--;

                    return true;
                }

                lastNode = curNode;
                curNode = curNode.Next;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified node from the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="SinglyLinkedListNode{T}"/> to remove from the <see cref="SinglyLinkedList{T}"/>.</param>
        public void Remove(SinglyLinkedListNode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");

            if (node == First)
            {
                RemoveFirst();
                return;
            }

            var curNode = First;

            while (curNode.Next != node)
            {
                curNode = curNode.Next;
            }

            curNode.Next = node.Next;
            if (curNode.Next == null) Last = curNode;

            node.Invalidate();
            Count--;
        }

        /// <summary>
        /// Removes the node after the specified node from the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="node">The <see cref="SinglyLinkedListNode{T}"/> after which to remove a node.</param>
        public void RemoveAfter(SinglyLinkedListNode<T> node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.List != this) throw new InvalidOperationException("node doesn't belong to this list");

            if (node.Next == null) return;

            var nodeToRemove = node.Next;

            node.Next = nodeToRemove.Next;
            if (node.Next == null) Last = node;

            nodeToRemove.Invalidate();
            Count--;
        }

        /// <summary>
        /// Removes the node at the start of the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        public void RemoveFirst()
        {
            if (Count == 0) throw new InvalidOperationException("The SinglyLinkedList doesn't contain any elements.");

            var oldFirst = First;
            First = First.Next;

            oldFirst.Invalidate();
            Count--;

            if (Count == 0) Last = null;
        }

        /// <summary>
        /// Removes the node at the end of the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        public void RemoveLast()
        {
            if (Count == 0) throw new InvalidOperationException("The SinglyLinkedList doesn't contain any elements.");

            if (Count == 1)
            {
                First.Invalidate();
                First = null;
                Last = null;
                Count--;
                return;
            }

            var curNode = First;

            while (curNode.Next != Last)
            {
                curNode = curNode.Next;
            }

            curNode.Next = null;
            Last.Invalidate();
            Last = curNode;
            Count--;
        }

        /// <summary>
        /// Determines whether an value is in the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <param name="value">The value to search in the <see cref="SinglyLinkedList{T}"/>.</param>
        /// <returns>returns true if the value is found; otherwise false.</returns>
        public bool Contains(T value)
        {
            if (Count == 0) return false;

            var curNode = First;

            while(curNode != null)
            {
                if (object.Equals(curNode.Value, value)) return true;

                curNode = curNode.Next;
            }

            return false;
        }

        /// <summary>
        /// Removes all nodes from the <see cref="SinglyLinkedList{T}"/>.
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
        /// Returns an enumerator that iterates throught the <see cref="SinglyLinkedList{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="SinglyLinkedList{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var curNode = First;

            while(curNode != null)
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
