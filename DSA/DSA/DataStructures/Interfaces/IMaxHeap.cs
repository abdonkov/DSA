using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Interfaces
{
    /// <summary>
    /// A max heap interface.
    /// </summary>
    /// <typeparam name="T">The stored value type. T implements <see cref="IComparable{T}"/>.</typeparam>
    public interface IMaxHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the number of elements in the <see cref="IMaxHeap{T}"/>.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines whether the <see cref="IMaxHeap{T}"/> is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Heapifies the given <see cref="IEnumerable{T}"/> collection. Overrides the current <see cref="IMaxHeap{T}"/>.
        /// </summary>
        /// <param name="collection">The collection of elements to heapify.</param>
        void Heapify(IEnumerable<T> collection);

        /// <summary>
        /// Adds an element to the <see cref="IMaxHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        void Add(T value);

        /// <summary>
        /// Gets the max element of the <see cref="IMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the max element of the <see cref="IMaxHeap{T}"/>.</returns>
        T PeekMax();

        /// <summary>
        /// Removes and returns the max element of the <see cref="IMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the max element which was removed from the <see cref="IMaxHeap{T}"/>.</returns>
        T PopMax();

        /// <summary>
        /// Replaces the max element with the given value and rebalancing the <see cref="IMaxHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to replace the max element of the <see cref="IMaxHeap{T}"/>.</param>
        void ReplaceMax(T value);

        /// <summary>
        /// Removes the max element from the <see cref="IMaxHeap{T}"/>.
        /// </summary>
        void RemoveMax();

        /// <summary>
        /// Removes all elements from the <see cref="IMaxHeap{T}"/>.
        /// </summary>
        void Clear();

        /// <summary>
        /// Copies the elements of the <see cref="IMaxHeap{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="IMaxHeap{T}"/>.</returns>
        T[] ToArray();

        /// <summary>
        /// Returns a new <see cref="IMinHeap{T}"/> containing the elements of the <see cref="IMaxHeap{T}"/>.
        /// </summary>
        /// <returns>Returns a new <see cref="IMinHeap{T}"/> containing the elements of the <see cref="IMaxHeap{T}"/>.</returns>
        IMinHeap<T> ToMinHeap();
    }
}
