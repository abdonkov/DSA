using DSA.DataStructures.Graphs;
using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// Holds the shortest paths in a weighted graph from a given source vertex. Used as a return type in the weighted graph shortest paths calculation methods.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    /// <typeparam name="TWeight">The data type of the weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
    public class WeightedGraphShortestPaths<TVertex, TWeight>
        where TVertex : IComparable<TVertex>
        where TWeight : IComparable<TWeight>
    {
        /// <summary>
        /// Gets the source vertex for which the paths are calculated.
        /// </summary>
        public TVertex Source { get; internal set; }

        /// <summary>
        /// A dictionary holding a vertex as key and as value a key-value pair of its previous vertex in the path(being the key) and the weight of the edge connecting them(being the value).
        /// </summary>
        internal Dictionary<TVertex, KeyValuePair<TVertex, TWeight>> previousVertices;

        /// <summary>
        /// A dictionary holding a vertex as key and as value a key-value pair holding the weight of the path from the source vertex(beign the key) and the distance from the source vertex(being the value).
        /// </summary>
        internal Dictionary<TVertex, KeyValuePair<TWeight, int>> weightAndDistance;

        /// <summary>
        /// Creates a new instance of the <see cref="UnweightedEdge{TVertex}"/>.
        /// </summary>
        /// <param name="source">The source vertex for which the shortest paths are calculated.</param>
        /// <param name="previousVertices">The dictionary holding a vertex as key and as value a key-value pair of its previous vertex in the path(being the key) and the weight of the edge connecting them(being the value).</param>
        /// <param name="weightAndDistance">The dictionary holding a vertex as key and as value a key-value pair holding the weight of the path from the source vertex(being the key) and the distance from the source vertex(being the value).</param>
        internal WeightedGraphShortestPaths(TVertex source, Dictionary<TVertex, KeyValuePair<TVertex, TWeight>> previousVertices, Dictionary<TVertex, KeyValuePair<TWeight, int>> weightAndDistance)
        {
            Source = source;
            this.previousVertices = previousVertices;
            this.weightAndDistance = weightAndDistance;
        }

        /// <summary>
        /// Determines if there is a path from the source vertex to the given destination vertex.
        /// </summary>
        /// <param name="destination">The destination vertex.</param>
        /// <returns>Returns true if there is a path from the source to the destination vertex; otherwise false.</returns>
        public bool HasPathTo(TVertex destination)
        {
            return previousVertices.ContainsKey(destination);
        }

        /// <summary>
        /// Returns the shortest path distance from the source vertex to the destination vertex.
        /// </summary>
        /// <param name="destination">The destination vertex.</param>
        /// <returns>A integer representing the number of nodes in the path between the source and the destination vertex, including the destination and excluding the source.</returns>
        public int DistanceTo(TVertex destination)
        {
            if (!weightAndDistance.ContainsKey(destination))
                throw new KeyNotFoundException("There is no path between the source vertex and the given destination vertex!");

            return weightAndDistance[destination].Value;
        }

        /// <summary>
        /// Returns the weight of the shortest path from the source vertex to the destination vertex.
        /// </summary>
        /// <param name="destination">The destination vertex.</param>
        /// <returns>The weight of the shortest path between the source and destination vertices.</returns>
        public TWeight PathWeightTo(TVertex destination)
        {
            if(!weightAndDistance.ContainsKey(destination))
                throw new KeyNotFoundException("There is no path between the source vertex and the given destination vertex!");

            return weightAndDistance[destination].Key;
        }

        /// <summary>
        /// Returns the vertices in the shortest path between the source and the destination vertices.
        /// </summary>
        /// <param name="destination">The destination vertex.</param>
        /// <returns>A <see cref="IList{T}"/> of the vertices in the shortest path.</returns>
        public IList<TVertex> VerticesPathTo(TVertex destination)
        {
            if (!previousVertices.ContainsKey(destination))
                throw new KeyNotFoundException("There is no path between the source vertex and the given destination vertex!");

            var path = new List<TVertex>();

            var curVertex = destination;

            path.Add(curVertex);

            while (!object.Equals(curVertex, Source))
            {
                var previousVertex = previousVertices[curVertex].Key;
                path.Add(previousVertex);
                curVertex = previousVertex;
            }

            path.Reverse(); // reverse because the items where added backwards
            return path;
        }

        /// <summary>
        /// Returns the edges in the shortest path between the source and the destination vertices.
        /// </summary>
        /// <param name="destination">The destination vertex.</param>
        /// <returns>A <see cref="IList{T}"/> of <see cref="WeightedEdge{TVertex, TWeight}"/> representing the edges in the shortest path.</returns>
        public IList<WeightedEdge<TVertex, TWeight>> EdgesPathTo(TVertex destination)
        {
            if (!previousVertices.ContainsKey(destination))
                throw new KeyNotFoundException("There is no path between the source vertex and the given destination vertex!");

            var path = new List<WeightedEdge<TVertex, TWeight>>();

            var curVertex = destination;

            while (!object.Equals(curVertex, Source))
            {
                var kvpPreviousVertex = previousVertices[curVertex];
                path.Add(new WeightedEdge<TVertex, TWeight>(kvpPreviousVertex.Key, curVertex, kvpPreviousVertex.Value));
                curVertex = kvpPreviousVertex.Key;
            }

            path.Reverse(); // reverse because the items where added backwards
            return path;
        }
    }
}
