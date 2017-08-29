using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Interfaces
{
    /// <summary>
    /// A min heap interface.
    /// </summary>
    /// <typeparam name="T">The stored value type. T implements <see cref="IComparable{T}"/>.</typeparam>
    public interface IMinHeap<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the number of elements in the <see cref="IMinHeap{T}"/>.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Determines whether the <see cref="IMinHeap{T}"/> is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// Heapifies the given <see cref="IEnumerable{T}"/> collection. Overrides the current <see cref="IMinHeap{T}"/>.
        /// </summary>
        /// <param name="collection">The collection of elements to heapify.</param>
        void Heapify(IEnumerable<T> collection);
        
        /// <summary>
        /// Adds an element to the <see cref="IMinHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        void Add(T value);

        /// <summary>
        /// Gets the min element of the <see cref="IMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the min element of the <see cref="IMinHeap{T}"/>.</returns>
        T PeekMin();

        /// <summary>
        /// Removes and returns the min element of the <see cref="IMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns the min element which was removed from the <see cref="IMinHeap{T}"/>.</returns>
        T PopMin();

        /// <summary>
        /// Replaces the min element with the given value and rebalancing the <see cref="IMinHeap{T}"/>.
        /// </summary>
        /// <param name="value">The value to replace the min element of the <see cref="IMinHeap{T}"/>.</param>
        void ReplaceMin(T value);

        /// <summary>
        /// Removes the min element from the <see cref="IMinHeap{T}"/>.
        /// </summary>
        void RemoveMin();

        /// <summary>
        /// Removes all elements from the <see cref="IMinHeap{T}"/>.
        /// </summary>
        void Clear();

        /// <summary>
        /// Copies the elements of the <see cref="IMinHeap{T}"/> to a new array.
        /// </summary>
        /// <returns>An array containing copies of the elements of the <see cref="IMinHeap{T}"/>.</returns>
        T[] ToArray();

        /// <summary>
        /// Returns a new <see cref="IMaxHeap{T}"/> containing the elements of the <see cref="IMinHeap{T}"/>.
        /// </summary>
        /// <returns>Returns a new <see cref="IMaxHeap{T}"/> containing the elements of the <see cref="IMinHeap{T}"/>.</returns>
        IMaxHeap<T> ToMaxHeap();
    }
}
