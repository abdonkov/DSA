namespace DSA.Algorithms.Trees
{
    /// <summary>
    /// Specifies the the traversal mode for the binary search tree.
    /// </summary>
    public enum TraversalMode
    {
        /// <summary>
        /// Defines in-order, left-to-right traversal.
        /// </summary>
        InOrder,
        /// <summary>
        /// Defines in-order, right-to-left traversal.
        /// </summary>
        InOrderRightToLeft,
        /// <summary>
        /// Defines pre-order, left-to-right traversal.
        /// </summary>
        PreOrder,
        /// <summary>
        /// Defines pre-order, right-to-left traversal.
        /// </summary>
        PreOrderRightToLeft,
        /// <summary>
        /// Defines post-order, left-to-right traversal.
        /// </summary>
        PostOrder,
        /// <summary>
        /// Defines post-order, right-to-left traversal.
        /// </summary>
        PostOrderRightToLeft,
        /// <summary>
        /// Defines level-order (BFS), left-to-right traversal.
        /// </summary>
        LevelOrder,
        /// <summary>
        /// Defines level-order (BFS), right-to-left traversal.
        /// </summary>
        LevelOrderRightToLeft
    }
}
