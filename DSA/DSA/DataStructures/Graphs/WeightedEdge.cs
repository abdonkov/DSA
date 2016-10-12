using DSA.DataStructures.Interfaces;
using System;

namespace DSA.DataStructures.Graphs
{
    /// <summary>
    /// Represents a weighted edge in a graph.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    /// <typeparam name="TWeight">The data type of the weight of the edge. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
    public class WeightedEdge<TVertex, TWeight> : IWeightedEdge<TVertex, TWeight>
        where TVertex : IComparable<TVertex>
        where TWeight : IComparable<TWeight>
    {
        /// <summary>
        /// Deteremines whether the edge is weighted.
        /// </summary>
        public bool IsWeighted { get { return true; } }

        /// <summary>
        /// Gets the source vertex of the <see cref="WeightedEdge{TVertex, TWeight}"/>.
        /// </summary>
        public TVertex Source { get; internal set; }

        /// <summary>
        /// Gets the destination vertex of the <see cref="WeightedEdge{TVertex, TWeight}"/>.
        /// </summary>
        public TVertex Destination { get; internal set; }

        /// <summary>
        /// Gets the weight of the <see cref="WeightedEdge{TVertex, TWeight}"/>.
        /// </summary>
        public TWeight Weight { get; internal set; }

        /// <summary>
        /// Creates a new instance of the <see cref="WeightedEdge{TVertex, TWeight}"/> defined by the given vertices and weight.
        /// </summary>
        /// <param name="source">The source vertex of the <see cref="WeightedEdge{TVertex, TWeight}"/>.</param>
        /// <param name="destination">The destination vertex of the <see cref="WeightedEdge{TVertex, TWeight}"/>.</param>
        /// <param name="weight">The weight of the <see cref="WeightedEdge{TVertex, TWeight}"/>.</param>
        public WeightedEdge(TVertex source, TVertex destination, TWeight weight)
        {
            Source = source;
            Destination = destination;
            Weight = weight;
        }
    }
}
