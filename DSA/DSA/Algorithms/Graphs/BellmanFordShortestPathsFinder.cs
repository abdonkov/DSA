using DSA.Algorithms.Sorting;
using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// A static class containing extension methods for graph shortests paths computing using Bellman-Ford's algorithm.
    /// </summary>
    public static class BellmanFordShortestPathsFinder
    {
        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="long"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="long"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="long"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, long> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, long> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="long"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="long"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, long> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, long> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="int"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="int"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="int"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, int> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, int> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="int"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="int"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, int> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, int> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="short"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="short"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="short"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, short> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, short> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => (short)(x + y));
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="short"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="short"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, short> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, short> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => (short)(x + y));
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="ulong"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="ulong"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="ulong"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, ulong> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, ulong> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="ulong"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="ulong"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, ulong> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, ulong> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="uint"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="uint"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="uint"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, uint> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, uint> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="uint"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="uint"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, uint> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, uint> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="ushort"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="ushort"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="ushort"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, ushort> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, ushort> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => (ushort)(x + y));
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="ushort"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="ushort"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, ushort> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, ushort> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => (ushort)(x + y));
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="decimal"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="decimal"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="decimal"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, decimal> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, decimal> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="decimal"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="decimal"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, decimal> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, decimal> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="double"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="double"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="double"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, double> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, double> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="double"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="double"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, double> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, double> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths. TWeight being the type of <see cref="float"/>.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="float"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex. TWeight being the type of <see cref="float"/>.</returns>
        public static WeightedGraphShortestPaths<TVertex, float> BellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, float> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            return BellmanFordShortestPaths(graph, source, (x, y) => x + y);
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/> with TWeight being of type <see cref="float"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object (TWeight being the type of <see cref="float"/>) that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IWeightedGraph<TVertex, float> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, float> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            return TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, (x, y) => x + y);
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in a weighted graph. Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object containing the shortest paths.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <typeparam name="TWeight">The data type of weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="weightAdder">The method corresponding to the <see cref="AddWeights{TWeight}"/> delegate used for calculating the sum of two edge weights.</param>
        /// <returns>Returns a <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> containing the shortest paths for the given source vertex.</returns>
        public static WeightedGraphShortestPaths<TVertex, TWeight> BellmanFordShortestPaths<TVertex, TWeight>(this IWeightedGraph<TVertex, TWeight> graph, TVertex source, AddWeights<TWeight> weightAdder)
            where TVertex : IComparable<TVertex>
            where TWeight : IComparable<TWeight>
        {
            if (!graph.ContainsVertex(source))
                throw new ArgumentException("Vertex does not belong to the graph!");

            WeightedGraphShortestPaths<TVertex, TWeight> shortestPaths;

            if (TryGetBellmanFordShortestPaths(graph, source, out shortestPaths, weightAdder))
                return shortestPaths;
            else
                throw new InvalidOperationException("Negative weight cycle found!");
        }

        /// <summary>
        /// Tries to compute the shortest paths in a weighted graph with the Bellman-Ford's algorithm. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <typeparam name="TWeight">The data type of weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IWeightedGraph{TVertex, TWeight}"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="WeightedGraphShortestPaths{TVertex, TWeight}"/> object that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <param name="weightAdder">The method corresponding to the <see cref="AddWeights{TWeight}"/> delegate used for calculating the sum of two edge weights.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex, TWeight>(this IWeightedGraph<TVertex, TWeight> graph, TVertex source, out WeightedGraphShortestPaths<TVertex, TWeight> shortestPaths, AddWeights<TWeight> weightAdder)
            where TVertex : IComparable<TVertex>
            where TWeight : IComparable<TWeight>
        {
            shortestPaths = null;

            if (!graph.ContainsVertex(source))
                return false;

            // A dictionary holding a vertex as key and as value a key-value pair of its previous vertex in the path(being the key) and the weight of the edge connecting them(being the value).
            var previousVertices = new Dictionary<TVertex, KeyValuePair<TVertex, TWeight>>();
            // The dictionary holding a vertex as key and as value a key-value pair holding the weight of the path from the source vertex(being the key) and the distance from the source vertex(being the value).
            var weightAndDistance = new Dictionary<TVertex, KeyValuePair<TWeight, int>>();

            // Add source vertex to computed weights and distances
            weightAndDistance.Add(source, new KeyValuePair<TWeight, int>(default(TWeight), 0));

            // Get all edges and sort them by their source and then by their destination vertex to create a consistent shortest paths output.
            var allEdges = graph.Edges.ToList();
            if (allEdges.Count > 0)
                allEdges.QuickSort((x, y) =>
                {
                    int cmp = x.Source.CompareTo(y.Source);
                    if (cmp == 0) cmp = x.Destination.CompareTo(y.Destination);
                    return cmp;
                });

            // We relax all edges n - 1 times and if at the n-th step a relaxation is needed we have a negative cycle
            int lastStep = graph.VerticesCount - 1;

            for (int i = 0; i < graph.VerticesCount; i++)
            {
                foreach (var edge in allEdges)
                {
                    var edgeSource = edge.Source;
                    var edgeDestination = edge.Destination;
                    var edgeWeight = edge.Weight;

                    // If we have computed the path to the the source vertex of the edge
                    if (weightAndDistance.ContainsKey(edgeSource))
                    {
                        var wd = weightAndDistance[edgeSource];
                        TWeight curWeight = wd.Key;
                        int curDistance = wd.Value;

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

                        if (i == lastStep)// if we need to relax on the last iteration we have a negative cycle
                            return false;

                        // Else we save the path
                        previousVertices[edgeDestination] = new KeyValuePair<TVertex, TWeight>(edgeSource, edgeWeight);
                        weightAndDistance[edgeDestination] = new KeyValuePair<TWeight, int>(pathWeight, curDistance + 1);
                    }
                }
            }

            // Remove source vertex from weight and distance dictionary
            weightAndDistance.Remove(source);

            shortestPaths = new WeightedGraphShortestPaths<TVertex, TWeight>(source, previousVertices, weightAndDistance);
            return true;
        }

        /// <summary>
        /// Uses Bellman-Ford's algorithm to compute the shortest paths in an unweighted graph. Shortest paths are computed with the distance between the vertices. Returns an <see cref="UnweightedGraphShortestPaths{TVertex}"/> object containg the shortest paths.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IGraph{TVertex}"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <returns>Returns a <see cref="UnweightedGraphShortestPaths{TVertex}"/> containing the shortest paths for the given source vertex.</returns>
        public static UnweightedGraphShortestPaths<TVertex> BellmanFordShortestPaths<TVertex>(this IGraph<TVertex> graph, TVertex source)
            where TVertex : IComparable<TVertex>
        {
            if (!graph.ContainsVertex(source))
                throw new ArgumentException("Vertex does not belong to the graph!");

            UnweightedGraphShortestPaths<TVertex> shortestPaths;

            if (TryGetBellmanFordShortestPaths(graph, source, out shortestPaths))
                return shortestPaths;
            else
                throw new InvalidOperationException("Negative weight cycle found!");
        }

        /// <summary>
        /// Tries to compute the shortest paths in an unweighted graph with the Bellman-Ford's algorithm. Shortest paths are computed with the distance between the vertices. Returns true if successful; otherwise false.
        /// </summary>
        /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
        /// <param name="graph">The graph structure that implements <see cref="IGraph{TVertex}"/>.</param>
        /// <param name="source">The source vertex for which the shortest paths are computed.</param>
        /// <param name="shortestPaths">The <see cref="UnweightedGraphShortestPaths{TVertex}"/> object that contains the shortest paths of the graph if the algorithm was successful; otherwise null.</param>
        /// <returns>Returns true if the shortest paths were successfully computed; otherwise false. Also returns false if the given source vertex does not belong to the graph.</returns>
        public static bool TryGetBellmanFordShortestPaths<TVertex>(this IGraph<TVertex> graph, TVertex source, out UnweightedGraphShortestPaths<TVertex> shortestPaths)
            where TVertex : IComparable<TVertex>
        {
            shortestPaths = null;

            if (!graph.ContainsVertex(source))
                return false;

            // A dictionary holding a vertex as key and its previous vertex in the path as value.
            var previousVertices = new Dictionary<TVertex, TVertex>();
            // A dictionary holding a vertex as key and the distance from the source vertex as value.
            var pathDistance = new Dictionary<TVertex, int>();

            // Add source vertex to computed distances
            pathDistance.Add(source, 0);

            // Get all edges and sort them by their source and then by their destination vertex to create a consistent shortest paths output.
            var allEdges = graph.Edges.ToList();
            if (allEdges.Count > 0)
                allEdges.QuickSort((x, y) =>
                {
                    int cmp = x.Source.CompareTo(y.Source);
                    if (cmp == 0) cmp = x.Destination.CompareTo(y.Destination);
                    return cmp;
                });

            // We relax all edges n - 1 times and if at the n-th step a relaxation is needed we have a negative cycle
            int lastStep = graph.VerticesCount - 1;

            for (int i = 0; i < graph.VerticesCount; i++)
            {
                foreach (var edge in allEdges)
                {
                    var edgeSource = edge.Source;
                    var edgeDestination = edge.Destination;

                    // If we have computed the path to the the source vertex of the edge
                    if (pathDistance.ContainsKey(edgeSource))
                    {
                        int curDistance = pathDistance[edgeSource];

                        // If the edge destination is the source we continue with the next edge
                        if (object.Equals(source, edgeDestination)) continue;

                        int newDistance = curDistance + 1;

                        // If this distance is bigger or equal than an already computed distance we continue with the next edge
                        if (pathDistance.ContainsKey(edgeDestination))
                            if (newDistance.CompareTo(pathDistance[edgeDestination]) >= 0)
                                continue;

                        if (i == lastStep)// if we need to relax on the last iteration we have a negative cycle
                            return false;

                        // Else we save the path
                        previousVertices[edgeDestination] = edgeSource;
                        pathDistance[edgeDestination] = newDistance;
                    }
                }
            }

            // Remove source vertex from path distance dictionary
            pathDistance.Remove(source);

            shortestPaths = new UnweightedGraphShortestPaths<TVertex>(source, previousVertices, pathDistance);
            return true;
        }
    }
}
