using DSA.Algorithms.Sorting;
using DSA.DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Graphs
{
    /// <summary>
    /// Represents an undirected and unweighted adjacency list graph with optimized amortized O(1) edge lookup and removal.
    /// </summary>
    /// <typeparam name="TVertex">The data type of the vertices. TVertex implements <see cref="IComparable{T}"/>.</typeparam>
    public class ALGraph<TVertex> : IGraph<TVertex>
        where TVertex : IComparable<TVertex>
    {
        /// <summary>
        /// Represents the connections between each vertex and its neighbours.
        /// </summary>
        internal Dictionary<TVertex, HashSet<TVertex>> adjacencyList;

        /// <summary>
        /// Determines whether the <see cref="ALGraph{TVertex}"/> is directed.
        /// </summary>
        public bool IsDirected { get { return false; } }

        /// <summary>
        /// Deteremines whether the <see cref="ALGraph{TVertex}"/> is weighted.
        /// </summary>
        public bool IsWeighted { get { return false; } }

        /// <summary>
        /// Gets the number of edges in the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        public int EdgesCount { get; internal set; }

        /// <summary>
        /// Gets the number of vertices in the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        public int VerticesCount { get; internal set; }

        /// <summary>
        /// Gets the vertices in the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        public IEnumerable<TVertex> Vertices
        {
            get
            {
                List<TVertex> vertices = new List<TVertex>(VerticesCount);
                foreach (var vertex in adjacencyList.Keys)
                {
                    vertices.Add(vertex);
                }
                return vertices;
            }
        }

        /// <summary>
        /// Gets the vertices in the <see cref="ALGraph{TVertex}"/> in sorted ascending order.
        /// </summary>
        public IEnumerable<TVertex> VerticesSorted
        {
            get
            {
                List<TVertex> vertices = new List<TVertex>(VerticesCount);
                foreach (var vertex in adjacencyList.Keys)
                {
                    vertices.Add(vertex);
                }

                if (vertices.Count > 0)
                    vertices.QuickSort();

                return vertices;
            }
        }

        /// <summary>
        /// Gets the edges in the <see cref="ALGraph{TVertex}"/>. For each edge in the graph returns two <see cref="UnweightedEdge{TVertex}"/> objects with swapped source and destination vertices.
        /// </summary>
        public IEnumerable<UnweightedEdge<TVertex>> Edges
        {
            get
            {
                foreach (var kvp in adjacencyList)
                {
                    foreach (var adjacent in kvp.Value)
                    {
                        yield return new UnweightedEdge<TVertex>(kvp.Key, adjacent);
                    }
                }
            }
        }
        
        /// <summary>
        /// Creates a new instance of the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        public ALGraph()
        {
            adjacencyList = new Dictionary<TVertex, HashSet<TVertex>>();
            EdgesCount = 0;
            VerticesCount = 0;
        }

        /// <summary>
        /// Adds an edge defined by the given vertices to the <see cref="ALGraph{TVertex}"/>. If the vertices are not present in the graph they will be added.
        /// </summary>
        /// <param name="firstVertex">The first vertex.</param>
        /// <param name="secondVertex">The second vertex.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if edge already exists.</returns>
        public bool AddEdge(TVertex firstVertex, TVertex secondVertex)
        {
            if (object.Equals(firstVertex, secondVertex)) return false;

            // Add first vertex if it is not in the graph
            if (!AddVertex(firstVertex))
                if (adjacencyList[firstVertex].Contains(secondVertex))// if the vertices are connected
                    return false;// we return false

            // Here the firstVertex is in the graph, so we connect it with the second
            adjacencyList[firstVertex].Add(secondVertex);

            // Add the other way around. Graph is not directed.
            AddVertex(secondVertex);
            adjacencyList[secondVertex].Add(firstVertex);

            // Counted as one edge because graph is undirected
            EdgesCount++;
            return true;
        }

        /// <summary>
        /// Adds a vertex to the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to add.</param>
        /// <returns>Returns true if the edge was added successfully; otherwise false. Also returns false if the vertex already exists.</returns>
        public bool AddVertex(TVertex vertex)
        {
            if (adjacencyList.ContainsKey(vertex)) return false;

            adjacencyList.Add(vertex, new HashSet<TVertex>());
            VerticesCount++;

            return true;
        }

        /// <summary>
        /// Adds the specified collection of vertices to the <see cref="ALGraph{TVertex}"/>. If some of the vertices are already in the graph exception is not thrown.
        /// </summary>
        /// <param name="vertices">Adds the <see cref="IEnumerable{T}"/> of vertices to the graph.</param>
        public void AddVertices(IEnumerable<TVertex> vertices)
        {
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
        }

        /// <summary>
        /// Returns the incoming edges of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> of all incoming edges of the given vertex.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> IncomingEdges(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var adjacent = adjacencyList[vertex].ToList();

            if (adjacent.Count > 0)
            {
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new UnweightedEdge<TVertex>(adjacent[i], vertex);
                }
            }
        }

        /// <summary>
        /// Returns the outgoing edges of the given vertex sorted.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> of all outgoing edges of the given vertex.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> OutgoingEdges(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var adjacent = adjacencyList[vertex].ToList();

            if (adjacent.Count > 0)
            {
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new UnweightedEdge<TVertex>(vertex, adjacent[i]);
                }
            }
        }

        /// <summary>
        /// Returns the incoming edges of the given vertex sorted by their source vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose incoming edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> of all incoming edges of the given vertex.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> IncomingEdgesSorted(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var adjacent = adjacencyList[vertex].ToList();

            if (adjacent.Count > 0)
            {
                adjacent.QuickSort();
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new UnweightedEdge<TVertex>(adjacent[i], vertex);
                }
            }
        }

        /// <summary>
        /// Returns the outgoing edges of the given vertex sorted by their destination vertex.
        /// </summary>
        /// <param name="vertex">The vertex whose outgoing edges are returned.</param>
        /// <returns>Returns a <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> of all outgoing edges of the given vertex.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> OutgoingEdgesSorted(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var adjacent = adjacencyList[vertex].ToList();

            if (adjacent.Count > 0)
            {
                adjacent.QuickSort();
                for (int i = 0; i < adjacent.Count; i++)
                {
                    yield return new UnweightedEdge<TVertex>(vertex, adjacent[i]);
                }
            }
        }

        /// <summary>
        /// Determines whether the edge is presented in the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        /// <param name="firstVertex">The first vertex of the edge.</param>
        /// <param name="secondVertex">The second vertex of the edge.</param>
        /// <returns>Returns true if the edge is presented in the <see cref="ALGraph{TVertex}"/>; false otherwise.</returns>
        public bool ContainsEdge(TVertex firstVertex, TVertex secondVertex)
        {
            if (!adjacencyList.ContainsKey(firstVertex)) return false;

            return adjacencyList[firstVertex].Contains(secondVertex);
        }

        /// <summary>
        /// Determines whether the vertex is presented in the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to see if presented in the <see cref="ALGraph{TVertex}"/>.</param>
        /// <returns>Returns true if the vertex is presented in the <see cref="ALGraph{TVertex}"/>; false otherwise.</returns>
        public bool ContainsVertex(TVertex vertex)
        {
            return adjacencyList.ContainsKey(vertex);
        }

        /// <summary>
        /// Removes the edge defined by the given vertices from the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        /// <param name="firstVertex">The first vertex.</param>
        /// <param name="secondVertex">The second vertex.</param>
        /// <returns>Returns true if the edge was removed successfully; otherwise false. Also returns false if the vertices are not present in this graph or the edge does not exist.</returns>
        public bool RemoveEdge(TVertex firstVertex, TVertex secondVertex)
        {
            if (!adjacencyList.ContainsKey(firstVertex)) return false;
            if (!adjacencyList[firstVertex].Contains(secondVertex)) return false;

            adjacencyList[firstVertex].Remove(secondVertex);
            adjacencyList[secondVertex].Remove(firstVertex);

            // Counted as one edge because graph is undirected
            EdgesCount--;
            return true;
        }

        /// <summary>
        /// Removes the given vertex from the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to remove.</param>
        /// <returns>Returns true if the vertex was removed successfully; otherwise false. Also returns false if the vertex does not exist.</returns>
        public bool RemoveVertex(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) return false;
            
            // Remove incoming edges
            foreach (var destinationVertex in adjacencyList[vertex])
            {
                adjacencyList[destinationVertex].Remove(vertex);
                EdgesCount--;
            }
            // Remove vertex
            adjacencyList.Remove(vertex);

            VerticesCount--;
            return true;
        }

        /// <summary>
        /// Returns the degree of the given vertex presented in the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        /// <param name="vertex">The vertex to calculate its degeree.</param>
        /// <returns>Returns the degree of the given vertex.</returns>
        public int Degree(TVertex vertex)
        {
            if (!ContainsVertex(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            return adjacencyList[vertex].Count;
        }

        /// <summary>
        /// Removes all edges and vertices from the <see cref="ALGraph{TVertex}"/>.
        /// </summary>
        public void Clear()
        {
            adjacencyList.Clear();
            EdgesCount = 0;
            VerticesCount = 0;
        }

        /// <summary>
        /// Breadth-first search of the <see cref="ALGraph{TVertex}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> of the vertices.</returns>
        public IEnumerable<TVertex> BreadthFirstSearch(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var queue = new Queue<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[adjacencyList.Values.Max(x => x?.Count ?? 0)];

            queue.Enqueue(vertex);
            visited.Add(vertex);

            while (queue.Count > 0)
            {
                TVertex curVertex = queue.Dequeue();

                yield return curVertex;

                int sCount = 0;

                foreach (var adjacentVertex in adjacencyList[curVertex])
                {
                    if (!visited.Contains(adjacentVertex))
                    {
                        sortedLevel[sCount++] = adjacentVertex;
                    }
                }

                if (sCount > 0)
                {
                    sortedLevel.QuickSort(0, sCount, null);
                    for (int i = 0; i < sCount; i++)
                    {
                        queue.Enqueue(sortedLevel[i]);
                        visited.Add(sortedLevel[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Breadth-first search of the <see cref="ALGraph{TVertex}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the breadth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges of the graph.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> BreadthFirstSearchEdges(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var queue = new Queue<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[adjacencyList.Values.Max(x => x?.Count ?? 0)];

            queue.Enqueue(vertex);
            visited.Add(vertex);

            while (queue.Count > 0)
            {
                TVertex curVertex = queue.Dequeue();

                int sCount = 0;

                foreach (var adjacentVertex in adjacencyList[curVertex])
                {
                    if (!visited.Contains(adjacentVertex))
                    {
                        sortedLevel[sCount++] = adjacentVertex;
                    }
                }

                if (sCount > 0)
                {
                    sortedLevel.QuickSort(0, sCount, null);
                    for (int i = 0; i < sCount; i++)
                    {
                        queue.Enqueue(sortedLevel[i]);
                        visited.Add(sortedLevel[i]);

                        yield return new UnweightedEdge<TVertex>(curVertex, sortedLevel[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Depth-first search of the <see cref="ALGraph{TVertex}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of the vertices.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/> of the vertices.</returns>
        public IEnumerable<TVertex> DepthFirstSearch(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var stack = new Stack<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[adjacencyList.Values.Max(x => x?.Count ?? 0)];

            stack.Push(vertex);

            while (stack.Count > 0)
            {
                TVertex curVertex = stack.Pop();

                if (!visited.Contains(curVertex))
                {
                    yield return curVertex;

                    visited.Add(curVertex);

                    int sCount = 0;

                    foreach (var adjacentVertex in adjacencyList[curVertex])
                    {
                        if (!visited.Contains(adjacentVertex))
                        {
                            sortedLevel[sCount++] = adjacentVertex;
                        }
                    }

                    if (sCount > 0)
                    {
                        sortedLevel.QuickSortDescending(0, sCount, null);// descending sort because we add them in stack
                        for (int i = 0; i < sCount; i++)
                        {
                            stack.Push(sortedLevel[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Depth-first search of the <see cref="ALGraph{TVertex}"/> with sorted levels. Returns <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges of the graph.
        /// </summary>
        /// <param name="vertex">The vertex from which the depth-first search starts.</param>
        /// <returns>.Returns <see cref="IEnumerable{T}"/> of <see cref="UnweightedEdge{TVertex}"/> representing the edges of the graph.</returns>
        public IEnumerable<UnweightedEdge<TVertex>> DepthFirstSearchEdges(TVertex vertex)
        {
            if (!adjacencyList.ContainsKey(vertex)) throw new KeyNotFoundException("Vertex does not belong to the graph!");

            var stackSource = new Stack<TVertex>(VerticesCount);
            var stackDestination = new Stack<TVertex>(VerticesCount);
            var visited = new HashSet<TVertex>();
            TVertex[] sortedLevel = new TVertex[adjacencyList.Values.Max(x => x?.Count ?? 0)];
            int sCount = 0;

            // Add vertex neighbours to stack
            foreach (var adjacentVertex in adjacencyList[vertex])
            {
                sortedLevel[sCount++] = adjacentVertex;
            }

            if (sCount > 0)
            {
                sortedLevel.QuickSortDescending(0, sCount, null);// descending sort beacause we add them in stack
                for (int i = 0; i < sCount; i++)
                {
                    stackSource.Push(vertex);
                    stackDestination.Push(sortedLevel[i]);
                }
            }

            visited.Add(vertex);

            while (stackDestination.Count > 0)
            {
                TVertex curSourceVertex = stackSource.Pop();
                TVertex curDestinationVertex = stackDestination.Pop();

                //visited.Add(curVertex);
                if (!visited.Contains(curDestinationVertex))
                {
                    yield return new UnweightedEdge<TVertex>(curSourceVertex, curDestinationVertex);

                    visited.Add(curDestinationVertex);

                    sCount = 0;

                    foreach (var adjacentVertex in adjacencyList[curDestinationVertex])
                    {
                        if (!visited.Contains(adjacentVertex))
                        {
                            sortedLevel[sCount++] = adjacentVertex;
                        }
                    }

                    if (sCount > 0)
                    {
                        sortedLevel.QuickSortDescending(0, sCount, null);// descending sort beacause we add them in stack
                        for (int i = 0; i < sCount; i++)
                        {
                            stackSource.Push(curDestinationVertex);
                            stackDestination.Push(sortedLevel[i]);
                        }
                    }
                }
            }
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.Edges { get { return Edges; } }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.IncomingEdges(TVertex vertex)
        {
            return IncomingEdges(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.OutgoingEdges(TVertex vertex)
        {
            return OutgoingEdges(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.IncomingEdgesSorted(TVertex vertex)
        {
            return IncomingEdgesSorted(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.OutgoingEdgesSorted(TVertex vertex)
        {
            return OutgoingEdgesSorted(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.BreadthFirstSearchEdges(TVertex vertex)
        {
            return BreadthFirstSearchEdges(vertex);
        }

        IEnumerable<IEdge<TVertex>> IGraph<TVertex>.DepthFirstSearchEdges(TVertex vertex)
        {
            return DepthFirstSearchEdges(vertex);
        }
    }
}
