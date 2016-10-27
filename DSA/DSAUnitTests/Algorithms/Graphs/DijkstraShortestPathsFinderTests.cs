using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Graphs;
using DSA.Algorithms.Graphs;

namespace DSAUnitTests.Algorithms.Graphs
{
    [TestClass]
    public class DijkstraShortestPathsFinderTests
    {
        [TestMethod]
        public void UndirectedAndUnweightedGraphShortestPathsCheck()
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
            // 7 <-> 4, 5, 8
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

            // Expected shortest paths for vertex 1
            // 1 to 2 : 1 -> 2
            // 1 to 3 : 1 -> 3
            // 1 to 4 : 1 -> 4
            // 1 to 5 : 1 -> 2 -> 5
            // 1 to 6 : 1 -> 2 -> 6
            // 1 to 7 : 1 -> 4 -> 7
            // 1 to 8 : 1 -> 2 -> 5 -> 8
            // 1 to 9 : no path
            var expectedPaths = new int[7][]
            {
                new int[] { 1, 2 },
                new int[] { 1, 3 },
                new int[] { 1, 4 },
                new int[] { 1, 2, 5 },
                new int[] { 1, 2, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 1, 2, 5, 8 }
            };

            var paths = graph.DijkstraShortestPaths(1);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 2);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));

            // Expected shortest paths for vertex 8
            // 8 to 1 : 8 -> 5 -> 2 -> 1
            // 8 to 2 : 8 -> 5 -> 2
            // 8 to 3 : 8 -> 5 -> 2 -> 3
            // 8 to 4 : 8 -> 7 -> 4
            // 8 to 5 : 8 -> 5
            // 8 to 6 : 8 -> 5 -> 2 -> 6
            // 8 to 7 : 8 -> 7
            // 8 to 9 : no path
            expectedPaths = new int[7][]
            {
                new int[] { 8, 5, 2, 1 },
                new int[] { 8, 5, 2 },
                new int[] { 8, 5, 2, 3 },
                new int[] { 8, 7, 4 },
                new int[] { 8, 5 },
                new int[] { 8, 5, 2, 6 },
                new int[] { 8, 7 }
            };

            paths = graph.DijkstraShortestPaths(8);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 1);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));
        }

        [TestMethod]
        public void DirectedAndUnweightedGraphShortestsPathsCheck()
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

            // Expected shortest paths for vertex 1
            // 1 to 2 : 1 -> 3 -> 2
            // 1 to 3 : 1 -> 3
            // 1 to 4 : 1 -> 4
            // 1 to 5 : 1 -> 3 -> 2 -> 5
            // 1 to 6 : 1 -> 3 -> 6
            // 1 to 7 : 1 -> 3 -> 2 -> 5 -> 7
            // 1 to 8 : 1 -> 3 -> 2 -> 5 -> 8
            // 1 to 9 : no path
            var expectedPaths = new int[7][]
            {
                new int[] { 1, 3, 2 },
                new int[] { 1, 3 },
                new int[] { 1, 4 },
                new int[] { 1, 3, 2, 5 },
                new int[] { 1, 3, 6 },
                new int[] { 1, 3, 2, 5, 7 },
                new int[] { 1, 3, 2, 5, 8 }
            };

            var paths = graph.DijkstraShortestPaths(1);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 2);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));

            // Expected shortest paths for vertex 8
            // 8 to 1 : 8 -> 5 -> 2 -> 1
            // 8 to 2 : 8 -> 5 -> 2
            // 8 to 3 : 8 -> 7 -> 4 -> 3
            // 8 to 4 : 8 -> 7 -> 4
            // 8 to 5 : 8 -> 5
            // 8 to 6 : 8 -> 7 -> 4 -> 6
            // 8 to 7 : 8 -> 7
            // 8 to 9 : no path
            expectedPaths = new int[7][]
            {
                new int[] { 8, 5, 2, 1 },
                new int[] { 8, 5, 2 },
                new int[] { 8, 7, 4, 3 },
                new int[] { 8, 7, 4 },
                new int[] { 8, 5 },
                new int[] { 8, 7, 4, 6 },
                new int[] { 8, 7 }
            };

            paths = graph.DijkstraShortestPaths(8);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 1);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));
        }

        [TestMethod]
        public void UndirectedAndWeightedGraphShortestPathsCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedALGraph<int, int>();

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
            // With each edge having a weight of the absolute value of the difference of the vertices
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(1, 3, 2);
            graph.AddEdge(1, 4, 3);
            graph.AddEdge(2, 3, 1);
            graph.AddEdge(2, 5, 3);
            graph.AddEdge(2, 6, 4);
            graph.AddEdge(3, 6, 3);
            graph.AddEdge(4, 6, 2);
            graph.AddEdge(4, 7, 3);
            graph.AddEdge(5, 7, 2);
            graph.AddEdge(5, 8, 3);
            graph.AddEdge(7, 8, 1);

            // Expected shortest paths for vertex 1
            // 1 to 2 : 1 -> 2
            // 1 to 3 : 1 -> 3
            // 1 to 4 : 1 -> 4
            // 1 to 5 : 1 -> 2 -> 5
            // 1 to 6 : 1 -> 2 -> 6
            // 1 to 7 : 1 -> 4 -> 7
            // 1 to 8 : 1 -> 2 -> 5 -> 8
            // 1 to 9 : no path
            var expectedPaths = new int[7][]
            {
                new int[] { 1, 2 },
                new int[] { 1, 3 },
                new int[] { 1, 4 },
                new int[] { 1, 2, 5 },
                new int[] { 1, 2, 6 },
                new int[] { 1, 4, 7 },
                new int[] { 1, 2, 5, 8 }
            };

            var expectedEdgesWeights = new int[7][]
            {
                new int[] { 1 },
                new int[] { 2 },
                new int[] { 3 },
                new int[] { 1, 3 },
                new int[] { 1, 4 },
                new int[] { 3, 3 },
                new int[] { 1, 3, 3 }
            };

            var paths = graph.DijkstraShortestPaths(1);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 2);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }

                var curEdgesPath = paths.EdgesPathTo(i + 2);

                for (int j = 0; j < curEdgesPath.Count; j++)
                {
                    if (expectedEdgesWeights[i][j] != curEdgesPath[j].Weight) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));

            // Expected shortest paths for vertex 8
            // 8 to 1 : 8 -> 7 -> 4 -> 1
            // 8 to 2 : 8 -> 5 -> 2
            // 8 to 3 : 8 -> 5 -> 2 -> 3
            // 8 to 4 : 8 -> 7 -> 4
            // 8 to 5 : 8 -> 5
            // 8 to 6 : 8 -> 7 -> 4 -> 6
            // 8 to 7 : 8 -> 7
            // 8 to 9 : no path
            expectedPaths = new int[7][]
            {
                new int[] { 8, 7, 4, 1 },
                new int[] { 8, 5, 2 },
                new int[] { 8, 5, 2, 3 },
                new int[] { 8, 7, 4 },
                new int[] { 8, 5 },
                new int[] { 8, 7, 4, 6 },
                new int[] { 8, 7 }
            };

            expectedEdgesWeights = new int[7][]
            {
                new int[] { 1, 3, 3 },
                new int[] { 3, 3 },
                new int[] { 3, 3, 1 },
                new int[] { 1, 3 },
                new int[] { 3 },
                new int[] { 1, 3, 2 },
                new int[] { 1 }
            };

            paths = graph.DijkstraShortestPaths(8);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 1);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }

                var curEdgesPath = paths.EdgesPathTo(i + 1);

                for (int j = 0; j < curEdgesPath.Count; j++)
                {
                    if (expectedEdgesWeights[i][j] != curEdgesPath[j].Weight) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));
        }

        [TestMethod]
        public void DirectedAndWeightedGraphShortestPathsCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new DirectedWeightedALGraph<int, int>();

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
            // With each edge having a weight of the absolute value of the difference of the vertices
            graph.AddEdge(1, 3, 2);
            graph.AddEdge(1, 4, 3);
            graph.AddEdge(2, 1, 1);
            graph.AddEdge(2, 5, 3);
            graph.AddEdge(3, 2, 1);
            graph.AddEdge(3, 6, 3);
            graph.AddEdge(4, 3, 1);
            graph.AddEdge(4, 6, 2);
            graph.AddEdge(5, 2, 3);
            graph.AddEdge(5, 7, 2);
            graph.AddEdge(5, 8, 3);
            graph.AddEdge(6, 2, 4);
            graph.AddEdge(7, 4, 3);
            graph.AddEdge(8, 5, 3);
            graph.AddEdge(8, 7, 1);

            // Expected shortest paths for vertex 1
            // 1 to 2 : 1 -> 3 -> 2
            // 1 to 3 : 1 -> 3
            // 1 to 4 : 1 -> 4
            // 1 to 5 : 1 -> 3 -> 2 -> 5
            // 1 to 6 : 1 -> 3 -> 6
            // 1 to 7 : 1 -> 3 -> 2 -> 5 -> 7
            // 1 to 8 : 1 -> 3 -> 2 -> 5 -> 8
            // 1 to 9 : no path
            var expectedPaths = new int[7][]
            {
                new int[] { 1, 3, 2 },
                new int[] { 1, 3 },
                new int[] { 1, 4 },
                new int[] { 1, 3, 2, 5 },
                new int[] { 1, 3, 6 },
                new int[] { 1, 3, 2, 5, 7 },
                new int[] { 1, 3, 2, 5, 8 }
            };

            var expectedEdgesWeights = new int[7][]
            {
                new int[] { 2, 1 },
                new int[] { 2 },
                new int[] { 3 },
                new int[] { 2, 1, 3 },
                new int[] { 2, 3 },
                new int[] { 2, 1, 3, 2 },
                new int[] { 2, 1, 3, 3 }
            };

            var paths = graph.DijkstraShortestPaths(1);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 2);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }

                var curEdgesPath = paths.EdgesPathTo(i + 2);

                for (int j = 0; j < curEdgesPath.Count; j++)
                {
                    if (expectedEdgesWeights[i][j] != curEdgesPath[j].Weight) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));

            // Expected shortest paths for vertex 8
            // 8 to 1 : 8 -> 5 -> 2 -> 1
            // 8 to 2 : 8 -> 5 -> 2
            // 8 to 3 : 8 -> 7 -> 4 -> 3
            // 8 to 4 : 8 -> 7 -> 4
            // 8 to 5 : 8 -> 5
            // 8 to 6 : 8 -> 7 -> 4 -> 6
            // 8 to 7 : 8 -> 7
            // 8 to 9 : no path
            expectedPaths = new int[7][]
            {
                new int[] { 8, 5, 2, 1 },
                new int[] { 8, 5, 2 },
                new int[] { 8, 7, 4, 3 },
                new int[] { 8, 7, 4 },
                new int[] { 8, 5 },
                new int[] { 8, 7, 4, 6 },
                new int[] { 8, 7 }
            };

            expectedEdgesWeights = new int[7][]
            {
                new int[] { 3, 3, 1 },
                new int[] { 3, 3 },
                new int[] { 1, 3, 1 },
                new int[] { 1, 3 },
                new int[] { 3 },
                new int[] { 1, 3, 2 },
                new int[] { 1 }
            };

            paths = graph.DijkstraShortestPaths(8);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 1);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }

                var curEdgesPath = paths.EdgesPathTo(i + 1);

                for (int j = 0; j < curEdgesPath.Count; j++)
                {
                    if (expectedEdgesWeights[i][j] != curEdgesPath[j].Weight) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));
        }

        [TestMethod]
        public void StringWeightsShortestPathsWithAddWeightsDelegateUsageCheck()
        {
            var vertices = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var graph = new WeightedALGraph<int, string>();

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
            graph.AddEdge(1, 2, "a");
            graph.AddEdge(1, 3, "b");
            graph.AddEdge(1, 4, "b");
            graph.AddEdge(2, 3, "a");
            graph.AddEdge(2, 5, "c");
            graph.AddEdge(2, 6, "c");
            graph.AddEdge(3, 6, "c");
            graph.AddEdge(4, 6, "b");
            graph.AddEdge(4, 7, "b");
            graph.AddEdge(5, 7, "b");
            graph.AddEdge(5, 8, "c");
            graph.AddEdge(7, 8, "a");

            // Expected shortest paths for vertex 1
            // 1 to 2 : 1 -> 2
            // 1 to 3 : 1 -> 2 -> 3
            // 1 to 4 : 1 -> 2 -> 3 -> 6 -> 4
            // 1 to 5 : 1 -> 2 -> 3 -> 6 -> 4 -> 7 -> 8 -> 5
            // 1 to 6 : 1 -> 2 -> 3 -> 6
            // 1 to 7 : 1 -> 2 -> 3 -> 6 -> 4 -> 7
            // 1 to 8 : 1 -> 2 -> 3 -> 6 -> 4 -> 7 -> 8
            // 1 to 9 : no path
            var expectedPaths = new int[7][]
            {
                new int[] { 1, 2 },
                new int[] { 1, 2, 3 },
                new int[] { 1, 2, 3, 6, 4 },
                new int[] { 1, 2, 3, 6, 4, 7, 8, 5 },
                new int[] { 1, 2, 3, 6 },
                new int[] { 1, 2, 3, 6, 4, 7 },
                new int[] { 1, 2, 3, 6, 4, 7, 8 }
            };

            var expectedEdgesWeights = new string[7][]
            {
                new string[] { "a" },
                new string[] { "a", "a" },
                new string[] { "a", "a", "c", "b" },
                new string[] { "a", "a", "c", "b", "b", "a", "c" },
                new string[] { "a", "a", "c" },
                new string[] { "a", "a", "c", "b", "b" },
                new string[] { "a", "a", "c", "b", "b", "a" }
            };

            var paths = graph.DijkstraShortestPaths(1, (x, y) => x + y);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 2);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }

                var curEdgesPath = paths.EdgesPathTo(i + 2);

                for (int j = 0; j < curEdgesPath.Count; j++)
                {
                    if (expectedEdgesWeights[i][j] != curEdgesPath[j].Weight) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));

            // Expected shortest paths for vertex 8
            // 8 to 1 : 8 -> 7 -> 4 -> 1
            // 8 to 2 : 8 -> 7 -> 4 -> 1 -> 2
            // 8 to 3 : 8 -> 7 -> 4 -> 1 -> 2 -> 3
            // 8 to 4 : 8 -> 7 -> 4
            // 8 to 5 : 8 -> 7 -> 5
            // 8 to 6 : 8 -> 7 -> 4 -> 6
            // 8 to 7 : 8 -> 7
            // 8 to 9 : no path
            expectedPaths = new int[7][]
            {
                new int[] { 8, 7, 4, 1 },
                new int[] { 8, 7, 4, 1, 2 },
                new int[] { 8, 7, 4, 1, 2, 3 },
                new int[] { 8, 7, 4 },
                new int[] { 8, 7, 5 },
                new int[] { 8, 7, 4, 6 },
                new int[] { 8, 7 }
            };

            expectedEdgesWeights = new string[7][]
            {
                new string[] { "a", "b", "b" },
                new string[] { "a", "b", "b", "a" },
                new string[] { "a", "b", "b", "a", "a" },
                new string[] { "a", "b" },
                new string[] { "a", "b" },
                new string[] { "a", "b", "b" },
                new string[] { "a" }
            };

            paths = graph.DijkstraShortestPaths(8, (x, y) => x + y);

            for (int i = 0; i < 7; i++)
            {
                var curPath = paths.VerticesPathTo(i + 1);

                for (int j = 0; j < curPath.Count; j++)
                {
                    if (expectedPaths[i][j] != curPath[j]) Assert.Fail();
                }

                var curEdgesPath = paths.EdgesPathTo(i + 1);

                for (int j = 0; j < curEdgesPath.Count; j++)
                {
                    if (expectedEdgesWeights[i][j] != curEdgesPath[j].Weight) Assert.Fail();
                }
            }
            Assert.IsFalse(paths.HasPathTo(9));
        }
    }
}
