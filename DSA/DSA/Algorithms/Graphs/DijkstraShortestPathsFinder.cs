using DSA.DataStructures.Interfaces;
using DSA.DataStructures.Queues;
using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// A static class containing extension methods for graph shortests paths computing using Dijkstra's algorithm.
    /// </summary>
    public static class DijkstraShortestPathsFinder
    {
        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="long"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="long"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="long"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, long> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, long> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="int"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="int"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="int"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, int> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, int> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="short"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="short"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="short"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, short> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, short> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => (short)(x + y));
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="ulong"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="ulong"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="ulong"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, ulong> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, ulong> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="uint"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="uint"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="uint"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, uint> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, uint> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="ushort"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="ushort"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="ushort"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, ushort> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, ushort> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => (ushort)(x + y));
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="decimal"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="decimal"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="decimal"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, decimal> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, decimal> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="double"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="double"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="double"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, double> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, double> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="float"/>.
        /// </summary>
        /// <typeparam name="TVertex"></typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="float"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="float"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, float> DijkstraShortestPaths<TVertex>(this IWeightedGraph<TVertex, float> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return DijkstraShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <typeparam name="TWeight">The data type of weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="weightAdder">The method corresponding to the <see cref="AddWeights{TWeight}"/> delegate used for calculating the sum of two edge weights.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex.</returns>
        public static WeightedGraphShortestPaths<TVertex, TWeight> DijkstraShortestPaths<TVertex, TWeight>(this IWeightedGraph<TVertex, TWeight> graph, TVertex source, AddWeights<TWeight> weightAdder)
            where TVertex : IComparable<TVertex>
            where TWeight : IComparable<TWeight>
        {
            if (!graph.ContainsVertex(source))
                throw new ArgumentException("Vertex does not belong to the graph!");

            // A dictionary holding a vertex as key and as value a key-value pair of its previous vertex in the path(being the key) and the weight of the edge connecting them(being the value).
            var previousVertices = new Dictionary<TVertex, KeyValuePair<TVertex, TWeight>>();
            // The dictionary holding a vertex as key and as value a key-value pair holding the weight of the path from the source vertex(being the key) and the distance from the source vertex(being the value).
            var weightAndDistance = new Dictionary<TVertex, KeyValuePair<TWeight, int>>();
            // Comparer for the key-value pair in the sorted set
            var kvpComparer = Comparer<KeyValuePair<TWeight, KeyValuePair<TVertex, int>>>.Create((x, y) =>
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
            // Sorted set containing the vertices for computing. Having a kvp with the path weight as key and as value another kvp with the vertex as key and the distance from the source as value.
            var priorityQueue = new MinPriorityQueue<TWeight, KeyValuePair<TVertex, int>>(kvpComparer);

            // Add the source vertex
            priorityQueue.Enqueue(default(TWeight), new KeyValuePair<TVertex, int>(source, 0));

            while (priorityQueue.Count > 0)
            {
                var curKVP = priorityQueue.Dequeue();
                TWeight curWeight = curKVP.Key;
                TVertex curVertex = curKVP.Value.Key;
                int curDistance = curKVP.Value.Value;

                foreach (var edge in graph.OutgoingEdgesSorted(curVertex))
                {
                    var edgeDestination = edge.Destination;
                    var edgeWeight = edge.Weight;

                    // If the edge destination is the source we continue with the next edge
                    if (object.Equals(source, edgeDestination)) continue;

                    // Compute path weight
                    TWeight pathWeight;
                    if (curWeight == null) pathWeight = edgeWeight;
                    else pathWeight = weightAdder(curWeight, edgeWeight);

                    // If this path is longer than an already computed path we continue with the next edge
                    if (weightAndDistance.ContainsKey(edgeDestination))
                    {
                        int cmp = pathWeight.CompareTo(weightAndDistance[edgeDestination].Key);
                        if (cmp > 0) continue;
                        else if (cmp == 0)// if path is equal to an already computed path
                        {
                            // we continue with the next edge only if the distance is bigger or equal
                            if (curDistance + 1 >= weightAndDistance[edgeDestination].Value)
                                continue;
                            // if the distance is smaller we update the path with this one
                        }
                    }

                    // Else we save the path
                    previousVertices[edgeDestination] = new KeyValuePair<TVertex, TWeight>(curVertex, edgeWeight);
                    weightAndDistance[edgeDestination] = new KeyValuePair<TWeight, int>(pathWeight, curDistance + 1);

                    // Add the destination vertex for computing
                    priorityQueue.Enqueue(pathWeight, new KeyValuePair<TVertex, int>(edgeDestination, curDistance + 1));
                }
            }

            var shortestPaths = new WeightedGraphShortestPaths<TVertex, TWeight>(source, previousVertices, weightAndDistance);
            return shortestPaths;
        }

        /// <summary>
        /// Uses Dijkstra's algorithm to compute the shortest paths in an unweighted graph. Shortest paths are computed with the distance between the vertices. Returns an <see cref="UnweightedGraphShortestPaths{TVertex}"/> object containg the shortest paths.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IGraph{TVertex}"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="UnweightedGraphShortestPaths{TVertex}"/> containing the shortest paths for the given source vertex.</returns>
        public static UnweightedGraphShortestPaths<TVertex> DijkstraShortestPaths<TVertex>(this IGraph<TVertex> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            if (!graph.ContainsVertex(source))
                throw new ArgumentException("Vertex does not belong to the graph!");

            // A dictionary holding a vertex as key and its previous vertex in the path as value.
            var previousVertices = new Dictionary<TVertex, TVertex>();
            // A dictionary holding a vertex as key and the distance from the source vertex as value.
            var pathDistance = new Dictionary<TVertex, int>();
            // Comparer for the key-value pair in the sorted set
            var kvpComparer = Comparer<KeyValuePair<int, TVertex>>.Create((x, y) =>
            {
                var cmp = x.Key.CompareTo(y.Key);
                if (cmp == 0) cmp = x.Value.CompareTo(y.Value);
                return cmp;
            });
            // Sorted set containing the vertices for computing. Having a kvp with the distance from the source vertex as a key and the vertex as a value
            var priorityQueue = new MinPriorityQueue<int, TVertex>(kvpComparer);

            // Add the source vertex
            priorityQueue.Enqueue(0, source);

            while (priorityQueue.Count > 0)
            {
                var curKVP = priorityQueue.Dequeue();
                TVertex curVertex = curKVP.Value;
                int curDistance = curKVP.Key;

                foreach (var edge in graph.OutgoingEdgesSorted(curVertex))
                {
                    var edgeDestination = edge.Destination;

                    // If the edge destination is the source we continue with the next edge
                    if (object.Equals(source, edgeDestination)) continue;

                    int newDistance = curDistance + 1;

                    // If this distance is bigger or equal than an already computed distance we continue with the next edge
                    if (pathDistance.ContainsKey(edgeDestination))
                        if (newDistance.CompareTo(pathDistance[edgeDestination]) >= 0)
                            continue;

                    // Else we save the path
                    previousVertices[edgeDestination] = curVertex;
                    pathDistance[edgeDestination] = newDistance;

                    // Add the destination vertex for computing
                    priorityQueue.Enqueue(newDistance, edgeDestination);
                }
            }

            var shortestPaths = new UnweightedGraphShortestPaths<TVertex>(source, previousVertices, pathDistance);
            return shortestPaths;
        }
    }
}
