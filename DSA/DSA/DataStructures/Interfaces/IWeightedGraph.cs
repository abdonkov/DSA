using System;
using System.Collections.Generic;

namespace DSA.DataStructures.Interfaces
{
    /// <summary>
    /// The weighted graph interface.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    /// <typeparam name="TWeight">The data type of the weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
    public interface IWeightedGraph<TVertex, TWeight> : IGraph<TVertex>
        where TVertex : IComparable<TVertex>
        where TWeight : IComparable<TWeight>
    {
        /// <summary>
        /// Adds an edge defined by the given vertices, having the given weight. If the vertices are not present in the graph they will be added.
        /// </summary>
        /// <param name="firstVertex">The first vertex. Source of the edge if the graph is directed.</param>
        /// <param name="secondVertex">The second vertex. Destination of the edge if the graph is directed.</param>
        /// <param name="weight">The weight of the edge.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if the edge already exists.</returns>
        bool AddEdge(TVertex firstVertex, TVertex secondVertex, TWeight weight);

        /// <summary>
        /// Returns the incoming edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IList{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> of all incoming edges of the given vertex.</returns>
        new IList<IWeightedEdge<TVertex, TWeight>> IncomingEdges(TVertex vertex);

        /// <summary>
        /// Returns the outgoing edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IList{T}"/> of <see cref="IWeightedEdge{TVertex, TWeight}"/> of all outgoing edges of the given vertex.</returns>
        new IList<IWeightedEdge<TVertex, TWeight>> OutgoingEdges(TVertex vertex);

        /// <summary>
        /// Updates the weight of the edge defined by the given existing vertices.
        /// </summary>
        /// <param name="firstVertex">The first vertex. Source of the edge if the graph is directed.</param>
        /// <param name="secondVertex">The second vertex. Destination of the edge if the graph is directed.</param>
        /// <param name="weight">The new weight of the edge.</param>
        /// <returns>Returns true if the edge weight was updated successfully; otherwise false. Also returns false if the vertices are not present in this graph.</returns>
        bool UpdateEdgeWeight(TVertex firstVertex, TVertex secondVertex, TWeight weight);
    }
}
