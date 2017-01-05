using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DSA.DataStructures.Trees;
using DSA.Algorithms.Trees;

namespace DSAUnitTests.Algorithms.Trees
{
    [TestClass]
    public class BinarySearchTreeRecursiveWalkerTests
    {
        /// <summary>
        /// Returns tree elements in the specified order. Iterative traversal of items. This method is HUUUGE.
        /// </summary>
        List<T> GetElements<T>(BinarySearchTree<T> tree, TraversalMode traversalMode)
        {
            var elements = new List<T>(tree.Count);

            HashSet<BinarySearchTreeNode<T>> visited;
            Stack<BinarySearchTreeNode<T>> stack;

            switch (traversalMode)
            {
                case TraversalMode.InOrder:
                    {
                        visited = new HashSet<BinarySearchTreeNode<T>>();
                        stack = new Stack<BinarySearchTreeNode<T>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeNode<T> curNode = stack.Peek();

                            if (curNode.Left == null || visited.Contains(curNode.Left))
                            {
                                visited.Add(curNode);
                                stack.Pop();
                                elements.Add(curNode.Value);

                                if (curNode.Right != null) stack.Push(curNode.Right);
                            }
                            else stack.Push(curNode.Left);
                        }
                    }
                    break;
                case TraversalMode.InOrderRightToLeft:
                    {
                        visited = new HashSet<BinarySearchTreeNode<T>>();
                        stack = new Stack<BinarySearchTreeNode<T>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeNode<T> curNode = stack.Peek();

                            if (curNode.Right == null || visited.Contains(curNode.Right))
                            {
                                visited.Add(curNode);
                                stack.Pop();
                                elements.Add(curNode.Value);

                                if (curNode.Left != null) stack.Push(curNode.Left);
                            }
                            else stack.Push(curNode.Right);
                        }
                    }
                    break;
                case TraversalMode.PreOrder:
                    {
                        visited = new HashSet<BinarySearchTreeNode<T>>();
                        stack = new Stack<BinarySearchTreeNode<T>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeNode<T> curNode = stack.Peek();

                            if (curNode.Left == null || visited.Contains(curNode.Left))
                            {
                                visited.Add(curNode);
                                stack.Pop();
                                if (curNode.Left == null) elements.Add(curNode.Value);

                                if (curNode.Right != null) stack.Push(curNode.Right);
                            }
                            else
                            {
                                elements.Add(curNode.Value);
                                stack.Push(curNode.Left);
                            }
                        }
                    }
                    break;
                case TraversalMode.PreOrderRightToLeft:
                    {
                        visited = new HashSet<BinarySearchTreeNode<T>>();
                        stack = new Stack<BinarySearchTreeNode<T>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeNode<T> curNode = stack.Peek();

                            if (curNode.Right == null || visited.Contains(curNode.Right))
                            {
                                visited.Add(curNode);
                                stack.Pop();
                                if (curNode.Right == null) elements.Add(curNode.Value);

                                if (curNode.Left != null) stack.Push(curNode.Left);
                            }
                            else
                            {
                                elements.Add(curNode.Value);
                                stack.Push(curNode.Right);
                            }
                        }
                    }
                    break;
                case TraversalMode.PostOrder:
                    {
                        visited = new HashSet<BinarySearchTreeNode<T>>();
                        stack = new Stack<BinarySearchTreeNode<T>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeNode<T> curNode = stack.Peek();

                            if (curNode.Left == null || visited.Contains(curNode.Left))
                            {
                                if (curNode.Right == null || visited.Contains(curNode.Right))
                                {
                                    visited.Add(curNode);
                                    stack.Pop();
                                    elements.Add(curNode.Value);
                                }
                                else stack.Push(curNode.Right);
                            }
                            else stack.Push(curNode.Left);
                        }
                    }
                    break;
                case TraversalMode.PostOrderRightToLeft:
                    {
                        visited = new HashSet<BinarySearchTreeNode<T>>();
                        stack = new Stack<BinarySearchTreeNode<T>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeNode<T> curNode = stack.Peek();

                            if (curNode.Right == null || visited.Contains(curNode.Right))
                            {
                                if (curNode.Left == null || visited.Contains(curNode.Left))
                                {
                                    visited.Add(curNode);
                                    stack.Pop();
                                    elements.Add(curNode.Value);
                                }
                                else stack.Push(curNode.Left);
                            }
                            else stack.Push(curNode.Right);
                        }
                    }
                    break;
                case TraversalMode.LevelOrder:
                    {
                        elements.Add(tree.Root.Value);

                        var queue = new Queue<BinarySearchTreeNode<T>>();

                        if (tree.Root.Left != null)
                        {
                            elements.Add(tree.Root.Left.Value);
                            queue.Enqueue(tree.Root.Left);
                        }
                        if (tree.Root.Right != null)
                        {
                            elements.Add(tree.Root.Right.Value);
                            queue.Enqueue(tree.Root.Right);
                        }

                        while (queue.Count > 0)
                        {
                            var curNode = queue.Dequeue();

                            if (curNode.Left != null)
                            {
                                elements.Add(curNode.Left.Value);
                                queue.Enqueue(curNode.Left);
                            }
                            if (curNode.Right != null)
                            {
                                elements.Add(curNode.Right.Value);
                                queue.Enqueue(curNode.Right);
                            }
                        }
                    }
                    break;
                case TraversalMode.LevelOrderRightToLeft:
                    {
                        elements.Add(tree.Root.Value);

                        var queue = new Queue<BinarySearchTreeNode<T>>();

                        if (tree.Root.Right != null)
                        {
                            elements.Add(tree.Root.Right.Value);
                            queue.Enqueue(tree.Root.Right);
                        }
                        if (tree.Root.Left != null)
                        {
                            elements.Add(tree.Root.Left.Value);
                            queue.Enqueue(tree.Root.Left);
                        }

                        while (queue.Count > 0)
                        {
                            var curNode = queue.Dequeue();

                            if (curNode.Right != null)
                            {
                                elements.Add(curNode.Right.Value);
                                queue.Enqueue(curNode.Right);
                            }
                            if (curNode.Left != null)
                            {
                                elements.Add(curNode.Left.Value);
                                queue.Enqueue(curNode.Left);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return elements;
        }

        /// <summary>
        /// Returns tree elements in the specified order. Iterative traversal of items. This method is HUUUGE.
        /// </summary>
        List<KeyValuePair<TKey, TValue>> GetElements<TKey, TValue>(BinarySearchTreeMap<TKey, TValue> tree, TraversalMode traversalMode)
        {
            var elements = new List<KeyValuePair<TKey, TValue>>(tree.Count);

            HashSet<BinarySearchTreeMapNode<TKey, TValue>> visited;
            Stack<BinarySearchTreeMapNode<TKey, TValue>> stack;

            switch (traversalMode)
            {
                case TraversalMode.InOrder:
                    {
                        visited = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

                            if (curNode.Left == null || visited.Contains(curNode.Left))
                            {
                                visited.Add(curNode);
                                stack.Pop();
                                elements.Add(new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value));

                                if (curNode.Right != null) stack.Push(curNode.Right);
                            }
                            else stack.Push(curNode.Left);
                        }
                    }
                    break;
                case TraversalMode.InOrderRightToLeft:
                    {
                        visited = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

                            if (curNode.Right == null || visited.Contains(curNode.Right))
                            {
                                visited.Add(curNode);
                                stack.Pop();
                                elements.Add(new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value));

                                if (curNode.Left != null) stack.Push(curNode.Left);
                            }
                            else stack.Push(curNode.Right);
                        }
                    }
                    break;
                case TraversalMode.PreOrder:
                    {
                        visited = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

                            if (curNode.Left == null || visited.Contains(curNode.Left))
                            {
                                visited.Add(curNode);
                                stack.Pop();
                                if (curNode.Left == null) elements.Add(new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value));

                                if (curNode.Right != null) stack.Push(curNode.Right);
                            }
                            else
                            {
                                elements.Add(new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value));
                                stack.Push(curNode.Left);
                            }
                        }
                    }
                    break;
                case TraversalMode.PreOrderRightToLeft:
                    {
                        visited = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

                            if (curNode.Right == null || visited.Contains(curNode.Right))
                            {
                                visited.Add(curNode);
                                stack.Pop();
                                if (curNode.Right == null) elements.Add(new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value));

                                if (curNode.Left != null) stack.Push(curNode.Left);
                            }
                            else
                            {
                                elements.Add(new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value));
                                stack.Push(curNode.Right);
                            }
                        }
                    }
                    break;
                case TraversalMode.PostOrder:
                    {
                        visited = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

                            if (curNode.Left == null || visited.Contains(curNode.Left))
                            {
                                if (curNode.Right == null || visited.Contains(curNode.Right))
                                {
                                    visited.Add(curNode);
                                    stack.Pop();
                                    elements.Add(new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value));
                                }
                                else stack.Push(curNode.Right);
                            }
                            else stack.Push(curNode.Left);
                        }
                    }
                    break;
                case TraversalMode.PostOrderRightToLeft:
                    {
                        visited = new HashSet<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack = new Stack<BinarySearchTreeMapNode<TKey, TValue>>();
                        stack.Push(tree.Root);
                        while (stack.Count > 0)
                        {
                            BinarySearchTreeMapNode<TKey, TValue> curNode = stack.Peek();

                            if (curNode.Right == null || visited.Contains(curNode.Right))
                            {
                                if (curNode.Left == null || visited.Contains(curNode.Left))
                                {
                                    visited.Add(curNode);
                                    stack.Pop();
                                    elements.Add(new KeyValuePair<TKey, TValue>(curNode.Key, curNode.Value));
                                }
                                else stack.Push(curNode.Left);
                            }
                            else stack.Push(curNode.Right);
                        }
                    }
                    break;
                case TraversalMode.LevelOrder:
                    {
                        elements.Add(new KeyValuePair<TKey, TValue>(tree.Root.Key, tree.Root.Value));

                        var queue = new Queue<BinarySearchTreeMapNode<TKey, TValue>>();

                        if (tree.Root.Left != null)
                        {
                            elements.Add(new KeyValuePair<TKey, TValue>(tree.Root.Left.Key, tree.Root.Left.Value));
                            queue.Enqueue(tree.Root.Left);
                        }
                        if (tree.Root.Right != null)
                        {
                            elements.Add(new KeyValuePair<TKey, TValue>(tree.Root.Right.Key, tree.Root.Right.Value));
                            queue.Enqueue(tree.Root.Right);
                        }

                        while (queue.Count > 0)
                        {
                            var curNode = queue.Dequeue();

                            if (curNode.Left != null)
                            {
                                elements.Add(new KeyValuePair<TKey, TValue>(curNode.Left.Key, curNode.Left.Value));
                                queue.Enqueue(curNode.Left);
                            }
                            if (curNode.Right != null)
                            {
                                elements.Add(new KeyValuePair<TKey, TValue>(curNode.Right.Key, curNode.Right.Value));
                                queue.Enqueue(curNode.Right);
                            }
                        }
                    }
                    break;
                case TraversalMode.LevelOrderRightToLeft:
                    {
                        elements.Add(new KeyValuePair<TKey, TValue>(tree.Root.Key, tree.Root.Value));

                        var queue = new Queue<BinarySearchTreeMapNode<TKey, TValue>>();

                        if (tree.Root.Right != null)
                        {
                            elements.Add(new KeyValuePair<TKey, TValue>(tree.Root.Right.Key, tree.Root.Right.Value));
                            queue.Enqueue(tree.Root.Right);
                        }
                        if (tree.Root.Left != null)
                        {
                            elements.Add(new KeyValuePair<TKey, TValue>(tree.Root.Left.Key, tree.Root.Left.Value));
                            queue.Enqueue(tree.Root.Left);
                        }

                        while (queue.Count > 0)
                        {
                            var curNode = queue.Dequeue();

                            if (curNode.Right != null)
                            {
                                elements.Add(new KeyValuePair<TKey, TValue>(curNode.Right.Key, curNode.Right.Value));
                                queue.Enqueue(curNode.Right);
                            }
                            if (curNode.Left != null)
                            {
                                elements.Add(new KeyValuePair<TKey, TValue>(curNode.Left.Key, curNode.Left.Value));
                                queue.Enqueue(curNode.Left);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return elements;
        }

        [TestMethod]
        public void BSTreeInOrderLeftToRightTraversal()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.InOrder);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x => 
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.InOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeInOrderRightToLeftTraversal()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.InOrderRightToLeft);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.InOrderRightToLeft);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreePreOrderLeftToRightTraversal()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.PreOrder);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.PreOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreePreOrderRightToLeftTraversal()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.PreOrderRightToLeft);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.PreOrderRightToLeft);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreePostOrderLeftToRightTraversal()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.PostOrder);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.PostOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreePostOrderRightToLeftTraversal()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.PostOrderRightToLeft);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.PostOrderRightToLeft);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeLevelOrderLeftToRightTraversal()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.LevelOrder);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.LevelOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeLevelOrderRightToLeftTraversal()
        {
            var tree = new BinarySearchTree<int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50);
            tree.Add(75);
            tree.Add(25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.LevelOrderRightToLeft);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.LevelOrderRightToLeft);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void AVLTreeTraversal()
        {
            var tree = new AVLTree<int>();

            int elementsCount = 10000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el))
                    {
                        tree.Add(el);
                    }
                    el += i;
                }
            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.InOrder);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.InOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);

        }

        [TestMethod]
        public void RedBlackTreeTraversal()
        {
            var tree = new RedBlackTree<int>();

            int elementsCount = 10000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.Contains(el)) tree.Add(el);
                    el += i;
                }
            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.InOrder);
            var listOfElementFromRecursiveWalker = new List<int>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeNode<int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(x.Value)),
                                        TraversalMode.InOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i], listOfElementFromRecursiveWalker[i]))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeMapInOrderLeftToRightTraversal()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.InOrder);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.InOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeMapInOrderRightToLeftTraversal()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.InOrderRightToLeft);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.InOrderRightToLeft);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeMapPreOrderLeftToRightTraversal()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.PreOrder);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.PreOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeMapPreOrderRightToLeftTraversal()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.PreOrderRightToLeft);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.PreOrderRightToLeft);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeMapPostOrderLeftToRightTraversal()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.PostOrder);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.PostOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeMapPostOrderRightToLeftTraversal()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.PostOrderRightToLeft);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.PostOrderRightToLeft);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeMapLevelOrderLeftToRightTraversal()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.LevelOrder);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.LevelOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void BSTreeMapLevelOrderRightToLeftTraversal()
        {
            var tree = new BinarySearchTreeMap<int, int>();

            int elementsCount = 5000;

            //To make it more balanced
            tree.Add(50, 50);
            tree.Add(75, 75);
            tree.Add(25, 25);

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }

            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.LevelOrderRightToLeft);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.LevelOrderRightToLeft);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }

        [TestMethod]
        public void AVLTreeMapTraversal()
        {
            var tree = new AVLTreeMap<int, int>();

            int elementsCount = 10000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el))
                    {
                        tree.Add(el, el);
                    }
                    el += i;
                }
            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.InOrder);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.InOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);

        }

        [TestMethod]
        public void RedBlackTreeMapTraversal()
        {
            var tree = new RedBlackTreeMap<int, int>();

            int elementsCount = 10000;

            //Adding every seventh number, then every fifth number,
            //every third and at last all numbers
            for (int i = 7; i > 0; i -= 2)
            {
                int el = 0;
                while (el < elementsCount)
                {
                    if (!tree.ContainsKey(el)) tree.Add(el, el);
                    el += i;
                }
            }

            var listOfElemntsFromTest = GetElements(tree, TraversalMode.InOrder);
            var listOfElementFromRecursiveWalker = new List<KeyValuePair<int, int>>(tree.Count);
            tree.ForEachRecursive(new Action<BinarySearchTreeMapNode<int, int>>(x =>
                                        listOfElementFromRecursiveWalker.Add(new KeyValuePair<int, int>(x.Key, x.Value))),
                                        TraversalMode.InOrder);

            for (int i = 0; i < tree.Count; i++)
            {
                if (!object.Equals(listOfElemntsFromTest[i].Key, listOfElementFromRecursiveWalker[i].Key))
                    Assert.Fail();
                if (!object.Equals(listOfElemntsFromTest[i].Value, listOfElementFromRecursiveWalker[i].Value))
                    Assert.Fail();
            }

            Assert.IsTrue(listOfElemntsFromTest.Count == tree.Count
                            && listOfElementFromRecursiveWalker.Count == tree.Count);
        }
    }
}
