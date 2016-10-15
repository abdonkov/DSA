using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Graphs;
using System.Linq;

namespace DSAUnitTests.DataStructures.Graphs
{
    [TestClass]
    public class DirectedWeightedALGraphTests
    {
        [TestMethod]
        public void CheckingIfVerticesAndEdgesAreAddedProperly()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVerices(vertices);

            Assert.IsTrue(graph.VerticesCount == vertices.Length);

            foreach (var vertex in vertices)
            {
                Assert.IsTrue(graph.ContainsVertex(vertex));
            }

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // With each edge having a weight of the diffrenece between the source and destination vertex (source - destination)
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            Assert.IsTrue(graph.EdgesCount == 15);

            int weight = 0;

            Assert.IsTrue(graph.TryGetEdgeWeight(1, 3, out weight));
            Assert.IsTrue(weight == -2);
            Assert.IsTrue(graph.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 2, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 6, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 3, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 6, out weight));
            Assert.IsTrue(weight == -2);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 7, out weight));
            Assert.IsTrue(weight == -2);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 8, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 2, out weight));
            Assert.IsTrue(weight == 4);
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 4, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 5, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 7, out weight));
            Assert.IsTrue(weight == 1);

            Assert.IsFalse(graph.ContainsEdge(1, 2));
            Assert.IsFalse(graph.ContainsEdge(1, 5));
            Assert.IsFalse(graph.ContainsEdge(1, 6));
            Assert.IsFalse(graph.ContainsEdge(1, 7));
            Assert.IsFalse(graph.ContainsEdge(1, 8));
            Assert.IsFalse(graph.ContainsEdge(2, 3));
            Assert.IsFalse(graph.ContainsEdge(2, 4));
            Assert.IsFalse(graph.ContainsEdge(2, 6));
            Assert.IsFalse(graph.ContainsEdge(2, 7));
            Assert.IsFalse(graph.ContainsEdge(2, 8));
            Assert.IsFalse(graph.ContainsEdge(3, 1));
            Assert.IsFalse(graph.ContainsEdge(3, 4));
            Assert.IsFalse(graph.ContainsEdge(3, 5));
            Assert.IsFalse(graph.ContainsEdge(3, 7));
            Assert.IsFalse(graph.ContainsEdge(3, 8));
            Assert.IsFalse(graph.ContainsEdge(4, 1));
            Assert.IsFalse(graph.ContainsEdge(4, 2));
            Assert.IsFalse(graph.ContainsEdge(4, 5));
            Assert.IsFalse(graph.ContainsEdge(4, 7));
            Assert.IsFalse(graph.ContainsEdge(4, 8));
            Assert.IsFalse(graph.ContainsEdge(5, 1));
            Assert.IsFalse(graph.ContainsEdge(5, 3));
            Assert.IsFalse(graph.ContainsEdge(5, 4));
            Assert.IsFalse(graph.ContainsEdge(5, 6));
            Assert.IsFalse(graph.ContainsEdge(6, 1));
            Assert.IsFalse(graph.ContainsEdge(6, 3));
            Assert.IsFalse(graph.ContainsEdge(6, 4));
            Assert.IsFalse(graph.ContainsEdge(6, 5));
            Assert.IsFalse(graph.ContainsEdge(6, 7));
            Assert.IsFalse(graph.ContainsEdge(6, 8));
            Assert.IsFalse(graph.ContainsEdge(7, 1));
            Assert.IsFalse(graph.ContainsEdge(7, 2));
            Assert.IsFalse(graph.ContainsEdge(7, 3));
            Assert.IsFalse(graph.ContainsEdge(7, 5));
            Assert.IsFalse(graph.ContainsEdge(7, 6));
            Assert.IsFalse(graph.ContainsEdge(7, 8));
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

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVerices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // With each edge having a weight of the diffrenece between the source and destination vertex (source - destination)
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            Assert.IsTrue(graph.EdgesCount == 15);

            Assert.IsTrue(graph.Degree(3) == 4);
            Assert.IsTrue(graph.Degree(6) == 3);
            Assert.IsTrue(graph.ContainsEdge(3, 6));
            graph.RemoveEdge(3, 6);
            Assert.IsFalse(graph.ContainsEdge(3, 6));
            Assert.IsTrue(graph.Degree(3) == 3);
            Assert.IsTrue(graph.Degree(6) == 2);

            Assert.IsTrue(graph.EdgesCount == 14);

            Assert.IsTrue(graph.Degree(1) == 3);
            Assert.IsTrue(graph.Degree(4) == 4);
            Assert.IsTrue(graph.ContainsEdge(1, 4));
            graph.RemoveEdge(1, 4);
            Assert.IsFalse(graph.ContainsEdge(1, 4));
            Assert.IsTrue(graph.Degree(1) == 2);
            Assert.IsTrue(graph.Degree(4) == 3);

            Assert.IsTrue(graph.EdgesCount == 13);

            Assert.IsTrue(graph.Degree(5) == 5);
            Assert.IsTrue(graph.Degree(7) == 3);
            Assert.IsTrue(graph.ContainsEdge(5, 7));
            graph.RemoveEdge(5, 7);
            Assert.IsFalse(graph.ContainsEdge(5, 7));
            Assert.IsTrue(graph.Degree(5) == 4);
            Assert.IsTrue(graph.Degree(7) == 2);

            Assert.IsTrue(graph.EdgesCount == 12);
        }

        [TestMethod]
        public void RemovingVerticesAndCheckingIfRemovedProperly()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVerices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // With each edge having a weight of the diffrenece between the source and destination vertex (source - destination)
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            Assert.IsTrue(graph.EdgesCount == 15);

            Assert.IsTrue(graph.ContainsVertex(3));
            Assert.IsTrue(graph.ContainsEdge(3, 2));
            Assert.IsTrue(graph.ContainsEdge(3, 6));
            Assert.IsTrue(graph.ContainsEdge(1, 3));
            Assert.IsTrue(graph.ContainsEdge(4, 3));
            graph.RemoveVertex(3);
            Assert.IsFalse(graph.ContainsVertex(3));
            Assert.IsFalse(graph.ContainsEdge(3, 2));
            Assert.IsFalse(graph.ContainsEdge(3, 6));
            Assert.IsFalse(graph.ContainsEdge(1, 3));
            Assert.IsFalse(graph.ContainsEdge(4, 3));

            Assert.IsTrue(graph.EdgesCount == 11);

            Assert.IsTrue(graph.ContainsVertex(5));
            Assert.IsTrue(graph.ContainsEdge(5, 2));
            Assert.IsTrue(graph.ContainsEdge(5, 7));
            Assert.IsTrue(graph.ContainsEdge(5, 8));
            Assert.IsTrue(graph.ContainsEdge(2, 5));
            Assert.IsTrue(graph.ContainsEdge(8, 5));
            graph.RemoveVertex(5);
            Assert.IsFalse(graph.ContainsVertex(5));
            Assert.IsFalse(graph.ContainsEdge(5, 2));
            Assert.IsFalse(graph.ContainsEdge(5, 7));
            Assert.IsFalse(graph.ContainsEdge(5, 8));
            Assert.IsFalse(graph.ContainsEdge(2, 5));
            Assert.IsFalse(graph.ContainsEdge(8, 5));

            Assert.IsTrue(graph.EdgesCount == 6);
        }

        [TestMethod]
        public void CheckIfIncomingEdgesAreCorrect()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVerices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // With each edge having a weight of the diffrenece between the source and destination vertex (source - destination)
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            // Vertex 1 expected incoming edges: 2
            var expected = new int[] { 2 };
            var edges = graph.IncomingEdges(1).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Source == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source - edges[i].Destination);
            }

            // Vertex 2 expected incoming edges: 3 5 6
            expected = new int[] { 3, 5, 6 };
            edges = graph.IncomingEdges(2).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Source == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source - edges[i].Destination);
            }

            // Vertex 7 expected incoming edges: 5 8
            expected = new int[] { 5, 8 };
            edges = graph.IncomingEdges(7).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Source == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source - edges[i].Destination);
            }
        }

        [TestMethod]
        public void CheckIfOutgoingEdgesAreCorrect()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVerices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // With each edge having a weight of the diffrenece between the source and destination vertex (source - destination)
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            // Vertex 1 expected outgoing edges: 3 4
            var expected = new int[] { 3, 4 };
            var edges = graph.OutgoingEdges(1).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Destination == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source - edges[i].Destination);
            }

            // Vertex 5 expected outgoing edges: 2 7 8
            expected = new int[] { 2, 7, 8 };
            edges = graph.OutgoingEdges(5).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Destination == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source - edges[i].Destination);
            }

            // Vertex 7 expected outgoing edges: 4
            expected = new int[] { 4 };
            edges = graph.OutgoingEdges(7).ToList();
            Assert.IsTrue(edges.Count == expected.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(edges[i].Destination == expected[i]);
                Assert.IsTrue(edges[i].Weight == edges[i].Source - edges[i].Destination);
            }
        }

        [TestMethod]
        public void CheckIfBFSIsCorrect()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVerices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // With each edge having a weight of the diffrenece between the source and destination vertex (source - destination)
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            // Expected bfs for vertex 1: 
            // 1 | 3 4 | 2 6 | 5 | 7 8
            var bfsExpectedOutput = new int[] { 1, 3, 4, 2, 6, 5, 7, 8 };
            var bfs = graph.BreadthFirstSearch(1).ToList();

            Assert.IsTrue(bfs.Count == bfsExpectedOutput.Length);
            for (int i = 0; i < bfs.Count; i++)
            {
                Assert.IsTrue(bfs[i] == bfsExpectedOutput[i]);
            }

            // Expected bfs edges for vertex 1: 
            // 1 1 | 3 3 | 2 | 5 5
            // 3 4 | 2 6 | 5 | 7 8
            var bfsExpectedSources = new int[] { 1, 1, 3, 3, 2, 5, 5 };
            var bfsExpectedDestinations = new int[] { 3, 4, 2, 6, 5, 7, 8 };
            var bfsEdges = graph.BreadthFirstSearchEdges(1).ToList();

            Assert.IsTrue(bfsEdges.Count == bfsExpectedSources.Length);
            for (int i = 0; i < bfsEdges.Count; i++)
            {
                Assert.IsTrue(bfsEdges[i].Source == bfsExpectedSources[i]);
                Assert.IsTrue(bfsEdges[i].Destination == bfsExpectedDestinations[i]);
                Assert.IsTrue(bfsEdges[i].Weight == bfsEdges[i].Source - bfsEdges[i].Destination);
            }

            // Expected bfs for vertex 8: 
            // 8 | 5 7 | 2 | 4 | 1 | 3 6
            bfsExpectedOutput = new int[] { 8, 5, 7, 2, 4, 1, 3, 6 };
            bfs = graph.BreadthFirstSearch(8).ToList();

            Assert.IsTrue(bfs.Count == bfsExpectedOutput.Length);
            for (int i = 0; i < bfs.Count; i++)
            {
                Assert.IsTrue(bfs[i] == bfsExpectedOutput[i]);
            }

            // Expected bfs edges for vertex 8: 
            // 8 8 | 5 | 7 | 2 | 4 4
            // 5 7 | 2 | 4 | 1 | 3 6
            bfsExpectedSources = new int[] { 8, 8, 5, 7, 2, 4, 4 };
            bfsExpectedDestinations = new int[] { 5, 7, 2, 4, 1, 3, 6 };
            bfsEdges = graph.BreadthFirstSearchEdges(8).ToList();

            Assert.IsTrue(bfsEdges.Count == bfsExpectedSources.Length);
            for (int i = 0; i < bfsEdges.Count; i++)
            {
                Assert.IsTrue(bfsEdges[i].Source == bfsExpectedSources[i]);
                Assert.IsTrue(bfsEdges[i].Destination == bfsExpectedDestinations[i]);
                Assert.IsTrue(bfsEdges[i].Weight == bfsEdges[i].Source - bfsEdges[i].Destination);
            }
        }

        [TestMethod]
        public void CheckIfDFSIsCorrect()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVerices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // With each edge having a weight of the diffrenece between the source and destination vertex (source - destination)
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            // Expected dfs for vertex 1: 
            // 1 | 3 | 2 | 5 | 7 | 4 | 6 | 8
            var dfsExpectedOutput = new int[] { 1, 3, 2, 5, 7, 4, 6, 8 };
            var dfs = graph.DepthFirstSearch(1).ToList();

            Assert.IsTrue(dfs.Count == dfsExpectedOutput.Length);
            for (int i = 0; i < dfs.Count; i++)
            {
                Assert.IsTrue(dfs[i] == dfsExpectedOutput[i]);
            }

            // Expected dfs edges for vertex 1: 
            // 1 | 3 | 2 | 5 | 7 | 4 | 5
            // 3 | 2 | 5 | 7 | 4 | 6 | 8
            var dfsExpectedSources = new int[] { 1, 3, 2, 5, 7, 4, 5 };
            var dfsExpectedDestinations = new int[] { 3, 2, 5, 7, 4, 6, 8 };
            var dfsEdges = graph.DepthFirstSearchEdges(1).ToList();

            Assert.IsTrue(dfsEdges.Count == dfsExpectedSources.Length);
            for (int i = 0; i < dfsEdges.Count; i++)
            {
                Assert.IsTrue(dfsEdges[i].Source == dfsExpectedSources[i]);
                Assert.IsTrue(dfsEdges[i].Destination == dfsExpectedDestinations[i]);
                Assert.IsTrue(dfsEdges[i].Weight == dfsEdges[i].Source - dfsEdges[i].Destination);
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
            // 8 | 5 | 2 | 1 | 3 | 1 | 5
            // 5 | 2 | 1 | 3 | 6 | 4 | 7
            dfsExpectedSources = new int[] { 8, 5, 2, 1, 3, 1, 5 };
            dfsExpectedDestinations = new int[] { 5, 2, 1, 3, 6, 4, 7 };
            dfsEdges = graph.DepthFirstSearchEdges(8).ToList();

            Assert.IsTrue(dfsEdges.Count == dfsExpectedSources.Length);
            for (int i = 0; i < dfsEdges.Count; i++)
            {
                Assert.IsTrue(dfsEdges[i].Source == dfsExpectedSources[i]);
                Assert.IsTrue(dfsEdges[i].Destination == dfsExpectedDestinations[i]);
                Assert.IsTrue(dfsEdges[i].Weight == dfsEdges[i].Source - dfsEdges[i].Destination);
            }
        }

        [TestMethod]
        public void UpdatingAndGettingEdgeWeight()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVerices(vertices);

            Assert.IsTrue(graph.VerticesCount == vertices.Length);

            foreach (var vertex in vertices)
            {
                Assert.IsTrue(graph.ContainsVertex(vertex));
            }

            // Graph is:
            // 1 -> 3, 4
            // 1 <- 2
            // 2 -> 1, 5
            // 2 <- 3, 5, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6
            // 4 <- 1, 7
            // 5 -> 2, 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 5, 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // With each edge having a weight of the diffrenece between the source and destination vertex (source - destination)
            graph.AddEdge(1, 3, -2);
            graph.AddEdge(1, 4, -3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, -3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, -3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, -2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, -2);
            graph.AddEdge(5, 8, -3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            int weight = 0;

            Assert.IsTrue(graph.TryGetEdgeWeight(1, 3, out weight));
            Assert.IsTrue(weight == -2);
            Assert.IsTrue(graph.UpdateEdgeWeight(1, 3, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(1, 3, out weight));
            Assert.IsTrue(weight == -4);
            Assert.IsTrue(graph.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(graph.UpdateEdgeWeight(1, 4, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(1, 4, out weight));
            Assert.IsTrue(weight == -6);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(graph.UpdateEdgeWeight(2, 1, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 1, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(graph.UpdateEdgeWeight(2, 5, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(2, 5, out weight));
            Assert.IsTrue(weight == -6);
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 2, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(graph.UpdateEdgeWeight(3, 2, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 2, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 6, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(graph.UpdateEdgeWeight(3, 6, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(3, 6, out weight));
            Assert.IsTrue(weight == -6);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 3, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(graph.UpdateEdgeWeight(4, 3, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 3, out weight));
            Assert.IsTrue(weight == 2);
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 6, out weight));
            Assert.IsTrue(weight == -2);
            Assert.IsTrue(graph.UpdateEdgeWeight(4, 6, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(4, 6, out weight));
            Assert.IsTrue(weight == -4);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.UpdateEdgeWeight(5, 2, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 2, out weight));
            Assert.IsTrue(weight == 6);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 7, out weight));
            Assert.IsTrue(weight == -2);
            Assert.IsTrue(graph.UpdateEdgeWeight(5, 7, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 7, out weight));
            Assert.IsTrue(weight == -4);
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 8, out weight));
            Assert.IsTrue(weight == -3);
            Assert.IsTrue(graph.UpdateEdgeWeight(5, 8, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(5, 8, out weight));
            Assert.IsTrue(weight == -6);
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 2, out weight));
            Assert.IsTrue(weight == 4);
            Assert.IsTrue(graph.UpdateEdgeWeight(6, 2, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(6, 2, out weight));
            Assert.IsTrue(weight == 8);
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 4, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.UpdateEdgeWeight(7, 4, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(7, 4, out weight));
            Assert.IsTrue(weight == 6);
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 5, out weight));
            Assert.IsTrue(weight == 3);
            Assert.IsTrue(graph.UpdateEdgeWeight(8, 5, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 5, out weight));
            Assert.IsTrue(weight == 6);
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 7, out weight));
            Assert.IsTrue(weight == 1);
            Assert.IsTrue(graph.UpdateEdgeWeight(8, 7, weight * 2));
            Assert.IsTrue(graph.TryGetEdgeWeight(8, 7, out weight));
            Assert.IsTrue(weight == 2);
        }
    }
}
