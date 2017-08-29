using DSA.DataStructures.Trees;
using System;
using System.Collections.Generic;

namespace DSA.Algorithms.Trees
{
    /// <summary>
    /// Static class containing extension methods for recursive walking on binary search trees.
    /// </summary>
    public static class BinarySearchTreeRecursiveWalker
    {
        #region BinarySearchTree visitors

        /// <summary>
        /// Performs the action on all nodes of the subtree in a in-order, left-to-right fashion.
        /// </summary>
        private static void InOrderVisitor<T>(BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action)
        {
            if (root == null) return;

            InOrderVisitor(root.Left, action);
            action(root);
            InOrderVisitor(root.Right, action);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a in-order, right-to-left fashion.
        /// </summary>
        private static void InOrderRightToLeftVisitor<T>(BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action)
        {
            if (root == null) return;

            InOrderRightToLeftVisitor(root.Right, action);
            action(root);
            InOrderRightToLeftVisitor(root.Left, action);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a pre-order, left-to-right fashion.
        /// </summary>
        private static void PreOrderVisitor<T>(BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action)
        {
            if (root == null) return;
            
            action(root);
            PreOrderVisitor(root.Left, action);
            PreOrderVisitor(root.Right, action);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a pre-order, right-to-left fashion.
        /// </summary>
        private static void PreOrderRightToLeftVisitor<T>(BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action)
        {
            if (root == null) return;
            
            action(root);
            PreOrderRightToLeftVisitor(root.Right, action);
            PreOrderRightToLeftVisitor(root.Left, action);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a post-order, left-to-right fashion.
        /// </summary>
        private static void PostOrderVisitor<T>(BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action)
        {
            if (root == null) return;

            PostOrderVisitor(root.Left, action);
            PostOrderVisitor(root.Right, action);
            action(root);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a post-order, right-to-left fashion.
        /// </summary>
        private static void PostOrderRightToLeftVisitor<T>(BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action)
        {
            if (root == null) return;
            
            PostOrderRightToLeftVisitor(root.Right, action);
            PostOrderRightToLeftVisitor(root.Left, action);
            action(root);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a level-order, left-to-right fashion.
        /// </summary>
        private static void LevelOrderVisitor<T>(BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action)
        {
            if (root == null) return;

            action(root);

            var queue = new Queue<BinarySearchTreeNode<T>>();
            if (root.Left != null)
            {
                action(root.Left);
                queue.Enqueue(root.Left);
            }
            if (root.Right != null)
            {
                action(root.Right);
                queue.Enqueue(root.Right);
            }

            while(queue.Count > 0)
            {
                var curNode = queue.Dequeue();

                if (curNode.Left != null)
                {
                    action(curNode.Left);
                    queue.Enqueue(curNode.Left);
                }
                if (curNode.Right != null)
                {
                    action(curNode.Right);
                    queue.Enqueue(curNode.Right);
                }
            }
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a level-order, right-to-left fashion.
        /// </summary>
        private static void LevelOrderRightToLeftVisitor<T>(BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action)
        {
            if (root == null) return;

            action(root);

            var queue = new Queue<BinarySearchTreeNode<T>>();
            if (root.Right != null)
            {
                action(root.Right);
                queue.Enqueue(root.Right);
            }
            if (root.Left != null)
            {
                action(root.Left);
                queue.Enqueue(root.Left);
            }
            

            while (queue.Count > 0)
            {
                var curNode = queue.Dequeue();

                if (curNode.Right != null)
                {
                    action(curNode.Right);
                    queue.Enqueue(curNode.Right);
                }
                if (curNode.Left != null)
                {
                    action(curNode.Left);
                    queue.Enqueue(curNode.Left);
                }
            }
        }

        #endregion

        #region BinarySearchTreeMap visitors

        /// <summary>
        /// Performs the action on all nodes of the subtree in a in-order, left-to-right fashion.
        /// </summary>
        private static void InOrderVisitor<TKey, TValue>(BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action)
        {
            if (root == null) return;

            InOrderVisitor(root.Left, action);
            action(root);
            InOrderVisitor(root.Right, action);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a in-order, right-to-left fashion.
        /// </summary>
        private static void InOrderRightToLeftVisitor<TKey, TValue>(BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action)
        {
            if (root == null) return;

            InOrderRightToLeftVisitor(root.Right, action);
            action(root);
            InOrderRightToLeftVisitor(root.Left, action);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a pre-order, left-to-right fashion.
        /// </summary>
        private static void PreOrderVisitor<TKey, TValue>(BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action)
        {
            if (root == null) return;

            action(root);
            PreOrderVisitor(root.Left, action);
            PreOrderVisitor(root.Right, action);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a pre-order, right-to-left fashion.
        /// </summary>
        private static void PreOrderRightToLeftVisitor<TKey, TValue>(BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action)
        {
            if (root == null) return;

            action(root);
            PreOrderRightToLeftVisitor(root.Right, action);
            PreOrderRightToLeftVisitor(root.Left, action);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a post-order, left-to-right fashion.
        /// </summary>
        private static void PostOrderVisitor<TKey, TValue>(BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action)
        {
            if (root == null) return;

            PostOrderVisitor(root.Left, action);
            PostOrderVisitor(root.Right, action);
            action(root);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a post-order, right-to-left fashion.
        /// </summary>
        private static void PostOrderRightToLeftVisitor<TKey, TValue>(BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action)
        {
            if (root == null) return;

            PostOrderRightToLeftVisitor(root.Right, action);
            PostOrderRightToLeftVisitor(root.Left, action);
            action(root);
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a level-order, left-to-right fashion.
        /// </summary>
        private static void LevelOrderVisitor<TKey, TValue>(BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action)
        {
            if (root == null) return;

            action(root);

            var queue = new Queue<BinarySearchTreeMapNode<TKey, TValue>>();
            if (root.Left != null)
            {
                action(root.Left);
                queue.Enqueue(root.Left);
            }
            if (root.Right != null)
            {
                action(root.Right);
                queue.Enqueue(root.Right);
            }

            while (queue.Count > 0)
            {
                var curNode = queue.Dequeue();

                if (curNode.Left != null)
                {
                    action(curNode.Left);
                    queue.Enqueue(curNode.Left);
                }
                if (curNode.Right != null)
                {
                    action(curNode.Right);
                    queue.Enqueue(curNode.Right);
                }
            }
        }

        /// <summary>
        /// Performs the action on all nodes of the subtree in a level-order, right-to-left fashion.
        /// </summary>
        private static void LevelOrderRightToLeftVisitor<TKey, TValue>(BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action)
        {
            if (root == null) return;

            action(root);

            var queue = new Queue<BinarySearchTreeMapNode<TKey, TValue>>();
            if (root.Right != null)
            {
                action(root.Right);
                queue.Enqueue(root.Right);
            }
            if (root.Left != null)
            {
                action(root.Left);
                queue.Enqueue(root.Left);
            }


            while (queue.Count > 0)
            {
                var curNode = queue.Dequeue();

                if (curNode.Right != null)
                {
                    action(curNode.Right);
                    queue.Enqueue(curNode.Right);
                }
                if (curNode.Left != null)
                {
                    action(curNode.Left);
                    queue.Enqueue(curNode.Left);
                }
            }
        }

        #endregion

        /// <summary>
        /// Recursively visits all nodes of the given tree in the specified order and applies the given action on them. Level-order traversal is iterative.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="BinarySearchTree{T}"/>.</typeparam>
        /// <param name="tree">The <see cref="BinarySearchTree{T}"/> for traversal.</param>
        /// <param name="action">The action to perform on each node.</param>
        /// <param name="traversalMode">The way in which the tree is traversed.</param>
        public static void ForEachRecursive<T>(this BinarySearchTree<T> tree, Action<BinarySearchTreeNode<T>> action, TraversalMode traversalMode = TraversalMode.InOrder)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (tree.Root == null) return;

            switch (traversalMode)
            {
                case TraversalMode.InOrder:
                    InOrderVisitor(tree.Root, action);
                    break;
                case TraversalMode.InOrderRightToLeft:
                    InOrderRightToLeftVisitor(tree.Root, action);
                    break;
                case TraversalMode.PreOrder:
                    PreOrderVisitor(tree.Root, action);
                    break;
                case TraversalMode.PreOrderRightToLeft:
                    PreOrderRightToLeftVisitor(tree.Root, action);
                    break;
                case TraversalMode.PostOrder:
                    PostOrderVisitor(tree.Root, action);
                    break;
                case TraversalMode.PostOrderRightToLeft:
                    PostOrderRightToLeftVisitor(tree.Root, action);
                    break;
                case TraversalMode.LevelOrder:
                    LevelOrderVisitor(tree.Root, action);
                    break;
                case TraversalMode.LevelOrderRightToLeft:
                    LevelOrderRightToLeftVisitor(tree.Root, action);
                    break;
                default:
                    InOrderVisitor(tree.Root, action);
                    break;
            }
        }

        /// <summary>
        /// Recursively visits all nodes of the given subtree in the specified order and applies the given action on them. Level-order traversal is iterative.
        /// </summary>
        /// <typeparam name="T">The data type of the <see cref="BinarySearchTreeNode{T}"/>.</typeparam>
        /// <param name="root">The root of the subtree for traversal.</param>
        /// <param name="action">The action to perform on each node.</param>
        /// <param name="traversalMode">The way in which the subtree is traversed.</param>
        public static void ForEachRecursive<T>(this BinarySearchTreeNode<T> root, Action<BinarySearchTreeNode<T>> action, TraversalMode traversalMode = TraversalMode.InOrder)
        {
            if (root == null) throw new ArgumentNullException(nameof(root));
            if (action == null) throw new ArgumentNullException(nameof(action));

            switch (traversalMode)
            {
                case TraversalMode.InOrder:
                    InOrderVisitor(root, action);
                    break;
                case TraversalMode.InOrderRightToLeft:
                    InOrderRightToLeftVisitor(root, action);
                    break;
                case TraversalMode.PreOrder:
                    PreOrderVisitor(root, action);
                    break;
                case TraversalMode.PreOrderRightToLeft:
                    PreOrderRightToLeftVisitor(root, action);
                    break;
                case TraversalMode.PostOrder:
                    PostOrderVisitor(root, action);
                    break;
                case TraversalMode.PostOrderRightToLeft:
                    PostOrderRightToLeftVisitor(root, action);
                    break;
                case TraversalMode.LevelOrder:
                    LevelOrderVisitor(root, action);
                    break;
                case TraversalMode.LevelOrderRightToLeft:
                    LevelOrderRightToLeftVisitor(root, action);
                    break;
                default:
                    InOrderVisitor(root, action);
                    break;
            }
        }

        /// <summary>
        /// Recursively visits all nodes of the given tree in the specified order and applies the given action on them. Level-order traversal is iterative.
        /// </summary>
        /// <typeparam name="TKey">The data type of the key of the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.</typeparam>
        /// <typeparam name="TValue">The data type of the value of the <see cref="BinarySearchTreeMap{TKey, TValue}"/>.</typeparam>
        /// <param name="tree">The <see cref="BinarySearchTreeMap{TKey, TValue}"/> for traversal.</param>
        /// <param name="action">The action to perform on each node.</param>
        /// <param name="traversalMode">The way in which the tree is traversed.</param>
        public static void ForEachRecursive<TKey, TValue>(this BinarySearchTreeMap<TKey, TValue> tree, Action<BinarySearchTreeMapNode<TKey, TValue>> action, TraversalMode traversalMode = TraversalMode.InOrder)
        {
            if (tree == null) throw new ArgumentNullException(nameof(tree));
            if (action == null) throw new ArgumentNullException(nameof(action));

            if (tree.Root == null) return;

            switch (traversalMode)
            {
                case TraversalMode.InOrder:
                    InOrderVisitor(tree.Root, action);
                    break;
                case TraversalMode.InOrderRightToLeft:
                    InOrderRightToLeftVisitor(tree.Root, action);
                    break;
                case TraversalMode.PreOrder:
                    PreOrderVisitor(tree.Root, action);
                    break;
                case TraversalMode.PreOrderRightToLeft:
                    PreOrderRightToLeftVisitor(tree.Root, action);
                    break;
                case TraversalMode.PostOrder:
                    PostOrderVisitor(tree.Root, action);
                    break;
                case TraversalMode.PostOrderRightToLeft:
                    PostOrderRightToLeftVisitor(tree.Root, action);
                    break;
                case TraversalMode.LevelOrder:
                    LevelOrderVisitor(tree.Root, action);
                    break;
                case TraversalMode.LevelOrderRightToLeft:
                    LevelOrderRightToLeftVisitor(tree.Root, action);
                    break;
                default:
                    InOrderVisitor(tree.Root, action);
                    break;
            }
        }

        /// <summary>
        /// Recursively visits all nodes of the given subtree in the specified order and applies the given action on them. Level-order traversal is iterative.
        /// </summary>
        /// <typeparam name="TKey">The data type of the key of the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/>.</typeparam>
        /// <typeparam name="TValue">The data type of the value of the <see cref="BinarySearchTreeMapNode{TKey, TValue}"/>.</typeparam>
        /// <param name="root">The root of the subtree for traversal.</param>
        /// <param name="action">The action to perform on each node.</param>
        /// <param name="traversalMode">The way in which the subtree is traversed.</param>
        public static void ForEachRecursive<TKey, TValue>(this BinarySearchTreeMapNode<TKey, TValue> root, Action<BinarySearchTreeMapNode<TKey, TValue>> action, TraversalMode traversalMode = TraversalMode.InOrder)
        {
            if (root == null) throw new ArgumentNullException(nameof(root));
            if (action == null) throw new ArgumentNullException(nameof(action));

            switch (traversalMode)
            {
                case TraversalMode.InOrder:
                    InOrderVisitor(root, action);
                    break;
                case TraversalMode.InOrderRightToLeft:
                    InOrderRightToLeftVisitor(root, action);
                    break;
                case TraversalMode.PreOrder:
                    PreOrderVisitor(root, action);
                    break;
                case TraversalMode.PreOrderRightToLeft:
                    PreOrderRightToLeftVisitor(root, action);
                    break;
                case TraversalMode.PostOrder:
                    PostOrderVisitor(root, action);
                    break;
                case TraversalMode.PostOrderRightToLeft:
                    PostOrderRightToLeftVisitor(root, action);
                    break;
                case TraversalMode.LevelOrder:
                    LevelOrderVisitor(root, action);
                    break;
                case TraversalMode.LevelOrderRightToLeft:
                    LevelOrderRightToLeftVisitor(root, action);
                    break;
                default:
                    InOrderVisitor(root, action);
                    break;
            }
        }
    }
}
