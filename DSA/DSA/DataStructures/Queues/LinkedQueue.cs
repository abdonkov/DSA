using System;
using System.Collections;
using System.Collections.Generic;

namespace DSA.DataStructures.Queues
{
    /// <summary>
    /// Represents a linked list based queue structure.
    /// </summary>
    /// <typeparam name="T">The stored data type.</typeparam>
    public class LinkedQueue<T> : IEnumerable<T>
    {
        /// <summary>
        /// The head (first) node of the linked queue.
        /// </summary>
        internal Node Head { get; set; }

        /// <summary>
        /// The tail (last) node of the linked queue.
        /// </summary>
        internal Node Tail { get; set; }

        /// <summary>
        /// Gets the number of elements in the <see cref="LinkedQueue{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="LinkedQueue{T}"/> class that is empty and has default capacity.
        /// </summary>
        public LinkedQueue() { }

        /// <summary>
        /// Creates a new instance of the <see cref="LinkedQueue{T}"/> class that contains the elements from the specified collection.
        /// </summary>
        /// <param name="collection">The collection to copy elements from.</param>
        public LinkedQueue(IEnumerable<T> collection)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            Count = 0;
            Node current = null;

            foreach (var item in collection)
            {
                if (Count++ == 0)
                {
                    Head = new Node(item, null);
                    current = Head;
                }
                else
                {
                    var next = new Node(item, null);
                    current.Next = next;
                    current = next;
                }
            }

            Tail = current;
        }

        /// <summary>
        /// Returns the item at the beginning of the <see cref="LinkedQueue{T}"/> without removing it.
        /// </summary>
        /// <returns>The item at the beginning of the <see cref="LinkedQueue{T}"/>.</returns>
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty!");

            return Head.Data;
        }

        /// <summary>
        /// Removes and returns the item at the beginning of the <see cref="LinkedQueue{T}"/>.
        /// </summary>
        /// <returns>The item removed from the beginning of the <see cref="LinkedQueue{T}"/>.</returns>
        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty!");

            Count--;
            T item = Head.Data;

            Head = Head.Next;
            if (Head == null) Tail = null;

            return item;
        }

        /// <summary>
        /// Adds an item at the end of the <see cref="LinkedQueue{T}"/>.
        /// </summary>
        /// <param name="item">The item to add at the end of the <see cref="LinkedQueue{T}"/>. The value can be null for reference types.</param>
        public void Enqueue(T item)
        {
            Count++;

            if (Tail == null)
            {
                Head = new Node(item, null);
                Tail = Head;
            }
            else
            {
                var newNode = new Node(item, null);
                Tail.Next = newNode;
                Tail = newNode;
            }
        }

        /// <summary>
        /// Determines whether an item is in the <see cref="LinkedQueue{T}"/>.
        /// </summary>
        /// <param name="item">The item to search in the <see cref="LinkedQueue{T}"/>.</param>
        /// <returns>returns true if the item is found; otherwise false.</returns>
        public bool Contains(T item)
        {
            Node current = Head;
            while (current != null)
            {
                if (object.Equals(current.Data, item)) return true;
                current = current.Next;
            }
            return false;
        }

        /// <summary>
        /// Removes all elements from the <see cref="LinkedQueue{T}"/>.
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        /// <summary>
        /// Copies the elements of the <see cref="LinkedQueue{T}"/> to a new array in first-in-first-out (FIFO) order.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="LinkedQueue{T}"/>.</returns>
        public T[] ToArray()
        {
            T[] array = new T[Count];
            int index = 0;
            Node current = Head;
            while (current != null)
            {
                array[index++] = current.Data;
                current = current.Next;
            }
            return array;
        }

        /// <summary>
        /// Returns an enumerator that iterates throught the <see cref="LinkedQueue{T}"/>.
        /// </summary>
        /// <returns>Enumerator for the <see cref="LinkedQueue{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Repesents a node in the <see cref="LinkedQueue{T}"/>.
        /// </summary>
        internal class Node
        {
            /// <summary>
            /// The data stored in the <see cref="Node"/>.
            /// </summary>
            public T Data { get; set; }

            /// <summary>
            /// The next <see cref="Node"/>.
            /// </summary>
            public Node Next { get; set; }

            /// <summary>
            /// Creates a new node with the given data and pointing to the given next node.
            /// </summary>
            /// <param name="data">The data stored in the node.</param>
            /// <param name="next">The next node.</param>
            public Node(T data, Node next)
            {
                Data = data;
                Next = next;
            }
        }
    }
}
