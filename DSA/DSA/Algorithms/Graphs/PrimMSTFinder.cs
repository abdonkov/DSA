using DSA.DataStructures.Graphs;
using DSA.DataStructures.Interfaces;
using DSA.DataStructures.Queues;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// A static class containing extension methods for minimum spanning tree finding using Prim's algorithm.
    /// </summary>
    public static class PrimMSTFinder
    {
        /// <summary>
        /// Uses Prim's algorithm to find the minimum spanning tree of the undirected weighted graph. Returns a <see cref="WeightedALGraph{TVertex, TWeight}"/> representing the MST.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <typeparam name="TWeight">The data type of weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/>.</param>
        /// <returns>Returns a <see cref="WeightedALGraph{TVertex, TWeight}"/> representing the MST.</returns>
        public static WeightedALGraph<TVertex, TWeight> PrimMST<TVertex, TWeight>(this IWeightedGraph<TVertex, TWeight> graph)
            where TVertex : IComparable<TVertex>
            where TWeight : IComparable<TWeight>
        {
            if (graph.IsDirected)
                throw new ArgumentException("Graph is directed!");

            var mst = new WeightedALGraph<TVertex, TWeight>();

            // A hash set of the added vertices to the MST
            var addedVertices = new HashSet<TVertex>();
            // Comparer for the kvp in the priority queue
            var kvpComparer = Comparer<KeyValuePair<TWeight, KeyValuePair<TVertex, TVertex>>>.Create((x, y) =>
            {
                int cmp = 0;
                // null checking because TWeight can be null
                if (x.Key == null)
                {
                    if (y.Key == null) cmp = -1; // change cmp to skip next comparisons
                    else return int.MinValue;
                }

                if (cmp == 0)// if x.Key and y.Key are not null
                {
                    if (y.Key == null) return int.MaxValue;
                    // normal check if both weights are not null
                    cmp = x.Key.CompareTo(y.Key);
                }
                else cmp = 0;// if x.Key and y.Key were both null, compare the kvp value

                if (cmp == 0) cmp = x.Value.Key.CompareTo(y.Value.Key);
                if (cmp == 0) cmp = x.Value.Value.CompareTo(y.Value.Value);
                return cmp;
            });
            // Priority queue of the edges for computing. Having the weight as key and as value - another kvp with source vertex as key and destination vertex as value.
            var priorityQueue = new MinPriorityQueue<TWeight, KeyValuePair<TVertex, TVertex>>(kvpComparer);

            var vertices = graph.Vertices.ToList();
            if (vertices.Count == 0) return mst;
            int verticesIndex = 0;

            mst.AddVertices(vertices);

            // Add dummy edge to the priority queue
            priorityQueue.Enqueue(default(TWeight), new KeyValuePair<TVertex, TVertex>(vertices[verticesIndex], vertices[verticesIndex]));
            addedVertices.Add(vertices[verticesIndex]);

            while (priorityQueue.Count > 0)
            {
                var curKVP = priorityQueue.Dequeue();
                TVertex curSourceVertex = curKVP.Value.Key;
                TVertex curDestinationVertex = curKVP.Value.Value;

                // If the destination verex of the edge is not added we add the edge
                if (!addedVertices.Contains(curDestinationVertex))
                {
                    mst.AddEdge(curSourceVertex, curDestinationVertex, curKVP.Key);
                    addedVertices.Add(curDestinationVertex);
                }

                foreach (var edge in graph.OutgoingEdgesSorted(curDestinationVertex))
                {
                    var edgeDestination = edge.Destination;
                    var edgeWeight = edge.Weight;

                    // If the destination vertex is added to the MST we skip it
                    if (addedVertices.Contains(edgeDestination))
                        continue;

                    // Add the edge for computing
                    priorityQueue.Enqueue(edgeWeight, new KeyValuePair<TVertex, TVertex>(curDestinationVertex, edgeDestination));
                }

                // If priority queue is empty
                if (priorityQueue.Count == 0)
                {
                    // Check if all vertices of the graph were added to the MST
                    if (addedVertices.Count != vertices.Count)
                    {
                        // If not we have a forest.
                        // Add the next non added vertex
                        while (verticesIndex < vertices.Count)
                        {
                            if (!addedVertices.Contains(vertices[verticesIndex]))
                            {
                                // Add dummy edge to the priority queue
                                priorityQueue.Enqueue(default(TWeight), new KeyValuePair<TVertex, TVertex>(vertices[verticesIndex], vertices[verticesIndex]));
                                addedVertices.Add(vertices[verticesIndex]);

                                break;
                            }
                            verticesIndex++;
                        }
                    }
                }
            }

            // Return MST
            return mst;
        }
    }
}
