using System;

namespace DSA.DataStructures.Interfaces
{
    /// <summary>
    /// Tree interface.
    /// </summary>
    /// <typeparam name="T">T implements <see cref="IComparable{T}">.</typeparam>
    public interface ITree<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Gets the number of elements in the tree.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds an element to the tree.
        /// </summary>
        /// <param name="value">The value to add.</param>
        void Add(T value);

        /// <summary>
        /// Removes an element from the tree.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>true if the item is successfully removed; otherwise false. Also returns false if item is not found.</returns>
        bool Remove(T value);

        /// <summary>
        /// Checks if the item is contained in the tree.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>true if item is found; otherwise false.</returns>
        bool Contains(T value);

        /// <summary>
        /// Removes all elements from the tree.
        /// </summary>
        void Clear();
    }
}