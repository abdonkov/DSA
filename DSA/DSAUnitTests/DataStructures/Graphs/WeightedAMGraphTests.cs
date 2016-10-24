using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Graphs;
using System.Linq;

namespace DSAUnitTests.DataStructures.Graphs
{
    [TestClass]
    public class WeightedAMGraphTests
    {
        [TestMethod]
        public void CheckingIfVerticesAndEdgesAreAddedProperly()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedAMGraph<int, int>();

            graph.AddVertices(vertices);

            Assert.IsTrue(graph.VerticesCount == vertices.Length);

            foreach (var vertex in vertices)
            {
                Assert.IsTrue(graph.ContainsVertex(vertex));
            }

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            Assert.IsTrue(graph.EdgesCount == 12);

            int weight = 0;

            Assert.IsTrue(graph.TryGetEdgeWeight(1, 2, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.TryGetEdgeWeight(1, 3, out weight));
            Assert.IsTrue(weight == 4);
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 1, out weight));
            Assert.IsTrue(weight == 4);
            Assert.IsTrue(graph.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == 5);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 1, out weight));
            Assert.IsTrue(weight == 5);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 3, out weight));
            Assert.IsTrue(weight == 5);
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 2, out weight));
            Assert.IsTrue(weight == 5);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == 7);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == 7);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 6, out weight));
            Assert.IsTrue(weight == 8);
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 2, out weight));
            Assert.IsTrue(weight == 8);
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 6, out weight));
            Assert.IsTrue(weight == 9);
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 3, out weight));
            Assert.IsTrue(weight == 9);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 6, out weight));
            Assert.IsTrue(weight == 10);
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 4, out weight));
            Assert.IsTrue(weight == 10);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 7, out weight));
            Assert.IsTrue(weight == 11);
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 4, out weight));
            Assert.IsTrue(weight == 11);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 7, out weight));
            Assert.IsTrue(weight == 12);
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 5, out weight));
            Assert.IsTrue(weight == 12);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 8, out weight));
            Assert.IsTrue(weight == 13);
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 5, out weight));
            Assert.IsTrue(weight == 13);
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 8, out weight));
            Assert.IsTrue(weight == 15);
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 7, out weight));
            Assert.IsTrue(weight == 15);

            Assert.IsFalse(graph.ContainsEdge(1, 7));
            Assert.IsFalse(graph.ContainsEdge(1, 5));
            Assert.IsFalse(graph.ContainsEdge(1, 8));
            Assert.IsFalse(graph.ContainsEdge(2, 4));
            Assert.IsFalse(graph.ContainsEdge(2, 7));
            Assert.IsFalse(graph.ContainsEdge(2, 8));
            Assert.IsFalse(graph.ContainsEdge(3, 4));
            Assert.IsFalse(graph.ContainsEdge(3, 5));
            Assert.IsFalse(graph.ContainsEdge(3, 7));
            Assert.IsFalse(graph.ContainsEdge(3, 8));
            Assert.IsFalse(graph.ContainsEdge(4, 2));
            Assert.IsFalse(graph.ContainsEdge(4, 3));
            Assert.IsFalse(graph.ContainsEdge(4, 5));
            Assert.IsFalse(graph.ContainsEdge(4, 8));
            Assert.IsFalse(graph.ContainsEdge(5, 1));
            Assert.IsFalse(graph.ContainsEdge(5, 3));
            Assert.IsFalse(graph.ContainsEdge(5, 4));
            Assert.IsFalse(graph.ContainsEdge(5, 6));
            Assert.IsFalse(graph.ContainsEdge(6, 1));
            Assert.IsFalse(graph.ContainsEdge(6, 5));
            Assert.IsFalse(graph.ContainsEdge(6, 7));
            Assert.IsFalse(graph.ContainsEdge(6, 8));
            Assert.IsFalse(graph.ContainsEdge(7, 1));
            Assert.IsFalse(graph.ContainsEdge(7, 2));
            Assert.IsFalse(graph.ContainsEdge(7, 3));
            Assert.IsFalse(graph.ContainsEdge(7, 6));
            Assert.IsFalse(graph.ContainsEdge(8, 1));
            Assert.IsFalse(graph.ContainsEdge(8, 2));
            Assert.IsFalse(graph.ContainsEdge(8, 3));
            Assert.IsFalse(graph.ContainsEdge(8, 4));
            Assert.IsFalse(graph.ContainsEdge(8, 6));
        }

        [TestMethod]
        public void RemovingEdgesAndCheckingIfRemovedProperly()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedAMGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            Assert.IsTrue(graph.EdgesCount == 12);

            Assert.IsTrue(graph.Degree(3) == 3);
            Assert.IsTrue(graph.Degree(6) == 3);
            Assert.IsTrue(graph.ContainsEdge(3, 6));
            Assert.IsTrue(graph.ContainsEdge(6, 3));
            graph.RemoveEdge(3, 6);
            Assert.IsFalse(graph.ContainsEdge(3, 6));
            Assert.IsFalse(graph.ContainsEdge(6, 3));
            Assert.IsTrue(graph.Degree(3) == 2);
            Assert.IsTrue(graph.Degree(6) == 2);

            Assert.IsTrue(graph.EdgesCount == 11);

            Assert.IsTrue(graph.Degree(1) == 3);
            Assert.IsTrue(graph.Degree(4) == 3);
            Assert.IsTrue(graph.ContainsEdge(1, 4));
            Assert.IsTrue(graph.ContainsEdge(4, 1));
            graph.RemoveEdge(4, 1);
            Assert.IsFalse(graph.ContainsEdge(1, 4));
            Assert.IsFalse(graph.ContainsEdge(4, 1));
            Assert.IsTrue(graph.Degree(1) == 2);
            Assert.IsTrue(graph.Degree(4) == 2);

            Assert.IsTrue(graph.EdgesCount == 10);

            Assert.IsTrue(graph.Degree(5) == 3);
            Assert.IsTrue(graph.Degree(7) == 3);
            Assert.IsTrue(graph.ContainsEdge(7, 5));
            Assert.IsTrue(graph.ContainsEdge(5, 7));
            graph.RemoveEdge(5, 7);
            Assert.IsFalse(graph.ContainsEdge(7, 5));
            Assert.IsFalse(graph.ContainsEdge(5, 7));
            Assert.IsTrue(graph.Degree(5) == 2);
            Assert.IsTrue(graph.Degree(7) == 2);

            Assert.IsTrue(graph.EdgesCount == 9);
        }

        [TestMethod]
        public void RemovingVerticesAndCheckingIfRemovedProperly()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedAMGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            Assert.IsTrue(graph.EdgesCount == 12);

            Assert.IsTrue(graph.ContainsVertex(3));
            Assert.IsTrue(graph.ContainsEdge(3, 1));
            Assert.IsTrue(graph.ContainsEdge(3, 2));
            Assert.IsTrue(graph.ContainsEdge(3, 6));
            Assert.IsTrue(graph.ContainsEdge(1, 3));
            Assert.IsTrue(graph.ContainsEdge(2, 3));
            Assert.IsTrue(graph.ContainsEdge(6, 3));
            graph.RemoveVertex(3);
            Assert.IsFalse(graph.ContainsVertex(3));
            Assert.IsFalse(graph.ContainsEdge(3, 1));
            Assert.IsFalse(graph.ContainsEdge(3, 2));
            Assert.IsFalse(graph.ContainsEdge(3, 6));
            Assert.IsFalse(graph.ContainsEdge(1, 3));
            Assert.IsFalse(graph.ContainsEdge(2, 3));
            Assert.IsFalse(graph.ContainsEdge(6, 3));

            Assert.IsTrue(graph.EdgesCount == 9);

            Assert.IsTrue(graph.ContainsVertex(5));
            Assert.IsTrue(graph.ContainsEdge(5, 2));
            Assert.IsTrue(graph.ContainsEdge(5, 7));
            Assert.IsTrue(graph.ContainsEdge(5, 8));
            Assert.IsTrue(graph.ContainsEdge(2, 5));
            Assert.IsTrue(graph.ContainsEdge(7, 5));
            Assert.IsTrue(graph.ContainsEdge(8, 5));
            graph.RemoveVertex(5);
            Assert.IsFalse(graph.ContainsVertex(5));
            Assert.IsFalse(graph.ContainsEdge(5, 2));
            Assert.IsFalse(graph.ContainsEdge(5, 7));
            Assert.IsFalse(graph.ContainsEdge(5, 8));
            Assert.IsFalse(graph.ContainsEdge(2, 5));
            Assert.IsFalse(graph.ContainsEdge(7, 5));
            Assert.IsFalse(graph.ContainsEdge(8, 5));

            Assert.IsTrue(graph.EdgesCount == 6);
        }

        [TestMethod]
        public void CheckIfIncomingEdgesAreCorrect()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedAMGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            // Vertex 1 expected incoming edges: 2 3 4
            var expected = new int[] { 2, 3, 4 };
            var edges = graph.IncomingEdgesSorted(1).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Source == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source + edges[i].Destination);
            }

            // Vertex 2 expected incoming edges: 1 3 5 6
            expected = new int[] { 1, 3, 5, 6 };
            edges = graph.IncomingEdgesSorted(2).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Source == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source + edges[i].Destination);
            }

            // Vertex 7 expected incoming edges: 4 5 8
            expected = new int[] { 4, 5, 8 };
            edges = graph.IncomingEdgesSorted(7).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Source == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source + edges[i].Destination);
            }
        }

        [TestMethod]
        public void CheckIfOutgoingEdgesAreCorrect()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedAMGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            // Vertex 1 expected outgoing edges: 2 3 4
            var expected = new int[] { 2, 3, 4 };
            var edges = graph.OutgoingEdgesSorted(1).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Destination == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source + edges[i].Destination);
            }

            // Vertex 2 expected outgoing edges: 1 3 5 6
            expected = new int[] { 1, 3, 5, 6 };
            edges = graph.OutgoingEdgesSorted(2).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Destination == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source + edges[i].Destination);
            }

            // Vertex 7 expected outgoing edges: 4 5 8
            expected = new int[] { 4, 5, 8 };
            edges = graph.OutgoingEdgesSorted(7).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Destination == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source + edges[i].Destination);
            }
        }

        [TestMethod]
        public void CheckIfBFSIsCorrect()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedAMGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            // Expected bfs for vertex 1: 
            // 1 | 2 3 4 | 5 6 | 7 | 8
            var bfsExpectedOutput = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var bfs = graph.BreadthFirstSearch(1).ToList();

            Assert.IsTrue(bfs.Count == bfsExpectedOutput.Length);
            for (int i = 0; i < bfs.Count; i++)
            {
                Assert.IsTrue(bfs[i] == bfsExpectedOutput[i]);
            }

            // Expected bfs edges for vertex 1: 
            // 1 1 1 | 2 2 | 4 | 5
            // 2 3 4 | 5 6 | 7 | 8
            var bfsExpectedSources = new int[] { 1, 1, 1, 2, 2, 4, 5 };
            var bfsExpectedDestinations = new int[] { 2, 3, 4, 5, 6, 7, 8 };
            var bfsEdges = graph.BreadthFirstSearchEdges(1).ToList();

            Assert.IsTrue(bfsEdges.Count == bfsExpectedSources.Length);
            for (int i = 0; i < bfsEdges.Count; i++)
            {
                Assert.IsTrue(bfsEdges[i].Source == bfsExpectedSources[i]);
                Assert.IsTrue(bfsEdges[i].Destination == bfsExpectedDestinations[i]);
                Assert.IsTrue(bfsEdges[i].Weight == bfsEdges[i].Source + bfsEdges[i].Destination);
            }

            // Expected bfs for vertex 8: 
            // 8 | 5 7 | 2 | 4 | 1 3 6
            bfsExpectedOutput = new int[] { 8, 5, 7, 2, 4, 1, 3, 6 };
            bfs = graph.BreadthFirstSearch(8).ToList();

            Assert.IsTrue(bfs.Count == bfsExpectedOutput.Length);
            for (int i = 0; i < bfs.Count; i++)
            {
                Assert.IsTrue(bfs[i] == bfsExpectedOutput[i]);
            }

            // Expected bfs edges for vertex 8: 
            // 8 8 | 5 | 7 | 2 2 2
            // 5 7 | 2 | 4 | 1 3 6
            bfsExpectedSources = new int[] { 8, 8, 5, 7, 2, 2, 2 };
            bfsExpectedDestinations = new int[] { 5, 7, 2, 4, 1, 3, 6 };
            bfsEdges = graph.BreadthFirstSearchEdges(8).ToList();

            Assert.IsTrue(bfsEdges.Count == bfsExpectedSources.Length);
            for (int i = 0; i < bfsEdges.Count; i++)
            {
                Assert.IsTrue(bfsEdges[i].Source == bfsExpectedSources[i]);
                Assert.IsTrue(bfsEdges[i].Destination == bfsExpectedDestinations[i]);
                Assert.IsTrue(bfsEdges[i].Weight == bfsEdges[i].Source + bfsEdges[i].Destination);
            }
        }

        [TestMethod]
        public void CheckIfDFSIsCorrect()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedAMGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            // Expected dfs for vertex 1: 
            // 1 | 2 | 3 | 6 | 4 | 7 | 5 | 8
            var dfsExpectedOutput = new int[] { 1, 2, 3, 6, 4, 7, 5, 8 };
            var dfs = graph.DepthFirstSearch(1).ToList();

            Assert.IsTrue(dfs.Count == dfsExpectedOutput.Length);
            for (int i = 0; i < dfs.Count; i++)
            {
                Assert.IsTrue(dfs[i] == dfsExpectedOutput[i]);
            }

            // Expected dfs edges for vertex 1: 
            // 1 | 2 | 3 | 6 | 4 | 7 | 5
            // 2 | 3 | 6 | 4 | 7 | 5 | 8
            var dfsExpectedSources = new int[] { 1, 2, 3, 6, 4, 7, 5 };
            var dfsExpectedDestinations = new int[] { 2, 3, 6, 4, 7, 5, 8 };
            var dfsEdges = graph.DepthFirstSearchEdges(1).ToList();

            Assert.IsTrue(dfsEdges.Count == dfsExpectedSources.Length);
            for (int i = 0; i < dfsEdges.Count; i++)
            {
                Assert.IsTrue(dfsEdges[i].Source == dfsExpectedSources[i]);
                Assert.IsTrue(dfsEdges[i].Destination == dfsExpectedDestinations[i]);
                Assert.IsTrue(dfsEdges[i].Weight == dfsEdges[i].Source + dfsEdges[i].Destination);
            }

            // Expected dfs for vertex 8: 
            // 8 | 5 | 2 | 1 | 3 | 6 | 4 | 7
            dfsExpectedOutput = new int[] { 8, 5, 2, 1, 3, 6, 4, 7 };
            dfs = graph.DepthFirstSearch(8).ToList();

            Assert.IsTrue(dfs.Count == dfsExpectedOutput.Length);
            for (int i = 0; i < dfs.Count; i++)
            {
                Assert.IsTrue(dfs[i] == dfsExpectedOutput[i]);

            }

            // Expected dfs edges for vertex 8: 
            // 8 | 5 | 2 | 1 | 3 | 6 | 4
            // 5 | 2 | 1 | 3 | 6 | 4 | 7
            dfsExpectedSources = new int[] { 8, 5, 2, 1, 3, 6, 4 };
            dfsExpectedDestinations = new int[] { 5, 2, 1, 3, 6, 4, 7 };
            dfsEdges = graph.DepthFirstSearchEdges(8).ToList();

            Assert.IsTrue(dfsEdges.Count == dfsExpectedSources.Length);
            for (int i = 0; i < dfsEdges.Count; i++)
            {
                Assert.IsTrue(dfsEdges[i].Source == dfsExpectedSources[i]);
                Assert.IsTrue(dfsEdges[i].Destination == dfsExpectedDestinations[i]);
                Assert.IsTrue(dfsEdges[i].Weight == dfsEdges[i].Source + dfsEdges[i].Destination);
            }
        }

        [TestMethod]
        public void UpdatingAndGettingEdgeWeight()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedAMGraph<int, int>();

            graph.AddVertices(vertices);

            Assert.IsTrue(graph.VerticesCount == vertices.Length);

            foreach (var vertex in vertices)
            {
                Assert.IsTrue(graph.ContainsVertex(vertex));
            }

            // Graph is:
            // 1 <-> 2, 3, 4
            // 2 <-> 1, 3, 5, 6
            // 3 <-> 1, 2, 6
            // 4 <-> 1, 6, 7
            // 5 <-> 2, 7, 8
            // 6 <-> 2, 3, 4
            // 7 <-> 4, 8
            // 8 <-> 7, 5
            // 9 <->
            // With each edge having a weight of the sum of its vertices
            graph.AddEdge(1, 2, 3);
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(2, 6, 8);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(7, 8, 15);

            int weight = 0;

            Assert.IsTrue(graph.TryGetEdgeWeight(1, 2, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.UpdateEdgeWeight(1, 2, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(weight == 6);
            Assert.IsTrue(graph.TryGetEdgeWeight(1, 3, out weight));
            Assert.IsTrue(weight == 4);
            Assert.IsTrue(graph.UpdateEdgeWeight(1, 3, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 1, out weight));
            Assert.IsTrue(weight == 8);
            Assert.IsTrue(graph.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == 5);
            Assert.IsTrue(graph.UpdateEdgeWeight(1, 4, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 1, out weight));
            Assert.IsTrue(weight == 10);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 3, out weight));
            Assert.IsTrue(weight == 5);
            Assert.IsTrue(graph.UpdateEdgeWeight(2, 3, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 2, out weight));
            Assert.IsTrue(weight == 10);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == 7);
            Assert.IsTrue(graph.UpdateEdgeWeight(2, 5, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == 14);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 6, out weight));
            Assert.IsTrue(weight == 8);
            Assert.IsTrue(graph.UpdateEdgeWeight(2, 6, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 2, out weight));
            Assert.IsTrue(weight == 16);
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 6, out weight));
            Assert.IsTrue(weight == 9);
            Assert.IsTrue(graph.UpdateEdgeWeight(3, 6, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 3, out weight));
            Assert.IsTrue(weight == 18);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 6, out weight));
            Assert.IsTrue(weight == 10);
            Assert.IsTrue(graph.UpdateEdgeWeight(4, 6, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 4, out weight));
            Assert.IsTrue(weight == 20);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 7, out weight));
            Assert.IsTrue(weight == 11);
            Assert.IsTrue(graph.UpdateEdgeWeight(4, 7, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 4, out weight));
            Assert.IsTrue(weight == 22);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 7, out weight));
            Assert.IsTrue(weight == 12);
            Assert.IsTrue(graph.UpdateEdgeWeight(5, 7, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 5, out weight));
            Assert.IsTrue(weight == 24);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 8, out weight));
            Assert.IsTrue(weight == 13);
            Assert.IsTrue(graph.UpdateEdgeWeight(5, 8, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 5, out weight));
            Assert.IsTrue(weight == 26);
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 8, out weight));
            Assert.IsTrue(weight == 15);
            Assert.IsTrue(graph.UpdateEdgeWeight(7, 8, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 7, out weight));
            Assert.IsTrue(weight == 30);
        }
    }
}
