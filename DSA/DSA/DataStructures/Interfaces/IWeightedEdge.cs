using System;

namespace DSA.DataStructures.Interfaces
{
    /// <summary>
    /// The weighted edge interface.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    /// <typeparam name="TWeight">The data type of the weight of the edge. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
    public interface IWeightedEdge<TVertex, TWeight> : IEdge<TVertex>
        where TVertex : IComparable<TVertex>
        where TWeight : IComparable<TWeight>
    {
        /// <summary>
        /// Gets the weight of the edge.
        /// </summary>
        TWeight Weight { get; }
    }
}
