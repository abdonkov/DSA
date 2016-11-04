using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Graphs;
using DSA.Algorithms.Graphs;

namespace DSAUnitTests.Algorithms.Graphs
{
    [TestClass]
    public class TopologicalSorterTests
    {
        [TestMethod]
        public void TopologicalSortSmallestVerticesFirst()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedALGraph<int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 2 -> 5
            // 2 <- 3, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6, 7
            // 4 <- 1
            // 5 -> 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 3);
            graph.AddEdge(4, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(6, 2);
            graph.AddEdge(8, 7);

            // Expected topological sort
            // 1 4 3 6 2 5 8 7 9
            var expected = new int[] { 1, 4, 3, 6, 2, 5, 8, 7, 9};
            var topSort = graph.TopologicalSortSmallestFirst();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(topSort[i] == expected[i]);
            }
        }

        [TestMethod]
        public void TopologicalSortBiggestVerticesFirst()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedALGraph<int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 2 -> 5
            // 2 <- 3, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6, 7
            // 4 <- 1
            // 5 -> 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 3);
            graph.AddEdge(4, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(6, 2);
            graph.AddEdge(8, 7);

            // Expected topological sort
            // 9 1 4 3 6 2 5 8 7
            var expected = new int[] { 9, 1, 4, 3, 6, 2, 5, 8, 7 };
            var topSort = graph.TopologicalSortBiggestFirst();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(topSort[i] == expected[i]);
            }
        }

        [TestMethod]
        public void TopologicalSortOnWeightedGraph()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

            graph.AddVertices(vertices);

            // Graph is:
            // 1 -> 3, 4
            // 2 -> 5
            // 2 <- 3, 6
            // 3 -> 2, 6
            // 3 <- 1, 4
            // 4 -> 3, 6, 7
            // 4 <- 1
            // 5 -> 7, 8
            // 5 <- 2, 8
            // 6 -> 2
            // 6 <- 3, 4
            // 7 -> 4
            // 7 <- 5, 8
            // 8 -> 7
            // 8 <- 5
            // 9 ->
            // 9 <-
            // Edges weight is the sum of the vertices
            graph.AddEdge(1, 3, 4);
            graph.AddEdge(1, 4, 5);
            graph.AddEdge(2, 5, 7);
            graph.AddEdge(3, 2, 5);
            graph.AddEdge(3, 6, 9);
            graph.AddEdge(4, 3, 7);
            graph.AddEdge(4, 6, 10);
            graph.AddEdge(4, 7, 11);
            graph.AddEdge(5, 7, 12);
            graph.AddEdge(5, 8, 13);
            graph.AddEdge(6, 2, 8);
            graph.AddEdge(8, 7, 15);

            // Expected topological sort
            // 1 4 3 6 2 5 8 7 9
            var expected = new int[] { 1, 4, 3, 6, 2, 5, 8, 7, 9 };
            var topSort = graph.TopologicalSortSmallestFirst();

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.IsTrue(topSort[i] == expected[i]);
            }
        }

        [TestMethod]
        public void ExceptionForNotDirectedGraph()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new ALGraph<int>();

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
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 5);
            graph.AddEdge(2, 6);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 6);
            graph.AddEdge(4, 7);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(7, 8);

            try
            {
                var topSort = graph.TopologicalSortSmallestFirst();
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                if (!ex.Message.Equals("Graph is not directed!"))
                    Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ExceptionForCyclicGraph()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedALGraph<int>();

            graph.AddVertices(vertices);

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
            graph.AddEdge(1, 3);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 2);
            graph.AddEdge(3, 6);
            graph.AddEdge(4, 3);
            graph.AddEdge(4, 6);
            graph.AddEdge(5, 2);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);
            graph.AddEdge(6, 2);
            graph.AddEdge(7, 4);
            graph.AddEdge(8, 5);
            graph.AddEdge(8, 7);

            try
            {
                var topSort = graph.TopologicalSortSmallestFirst();
                Assert.Fail();
            }
            catch (InvalidOperationException ex)
            {
                if (!ex.Message.Equals("Graph is cyclic!"))
                    Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
