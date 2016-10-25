using DSA.DataStructures.Graphs;
using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// Holds the shortest paths in a unweighted graph from a given source vertex. Used as a return type in the unweighted graph shortest paths calculation methods.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    public class UnweightedGraphShortestPaths<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Gets the source vertex for which the paths are calculated.
        /// </summary>
        public TVertex Source { get; internal set; }

        /// <summary>
        /// A dictionary holding a vertex as key and its previous vertex in the path as value.
        /// </summary>
        internal Dictionary<TVertex, TVertex> previousVertices;

        /// <summary>
        /// A dictionary holding a vertex as key and the distance from the source vertex as value.
        /// </summary>
        internal Dictionary<TVertex, int> pathDistance;

        /// <summary>
        /// Creates a new instance of the <see cref="UnweightedEdge{TVertex}"/>.
        /// </summary>
        /// <param name="source">The source vertex for which the shortest paths are calculated.</param>
        /// <param name="previousVertices">The dictionary holding a vertex as key and its previous vertex in the path as value.</param>
        /// <param name="pathDistance">The dictionary holding a vertex as key and the distance from the source vertex as value.</param>
        internal UnweightedGraphShortestPaths(TVertex source, Dictionary<TVertex, TVertex> previousVertices, Dictionary<TVertex, int> pathDistance)
        {
            Source = source;
            this.previousVertices = previousVertices;
            this.pathDistance = pathDistance;
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
            if (!pathDistance.ContainsKey(destination))
                throw new KeyNotFoundException("There is no path between the source vertex and the given destination vertex!");

            return pathDistance[destination];
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
                var previousVertex = previousVertices[curVertex];
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
        /// <returns>A <see cref="IList{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges in the shortest path.</returns>
        public IList<UnweightedEdge<TVertex>> EdgesPathTo(TVertex destination)
        {
            if (!previousVertices.ContainsKey(destination))
                throw new KeyNotFoundException("There is no path between the source vertex and the given destination vertex!");

            var path = new List<UnweightedEdge<TVertex>>();

            var curVertex = destination;

            while (!object.Equals(curVertex, Source))
            {
                var previousVertex = previousVertices[curVertex];
                path.Add(new UnweightedEdge<TVertex>(previousVertex, curVertex));
                curVertex = previousVertex;
            }

            path.Reverse(); // reverse because the items where added backwards
            return path;
        }
    }
}
