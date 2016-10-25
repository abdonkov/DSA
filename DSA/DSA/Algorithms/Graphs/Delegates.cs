using System;

namespace DSA.Algorithms.Graphs
{
    /// <summary>
    /// A delegate defining a method that adds two graph edge weights and returns their sum.
    /// </summary>
    /// <typeparam name="TWeight">The data type of the weight of the edges. TWeight implements <see cref="IComparable{T}"/>.</typeparam>
    /// <param name="a">The weight of the first item.</param>
    /// <param name="b">The weight of the second item.</param>
    /// <returns>Returns the sum of the edge weights.></returns>
    public delegate TWeight AddWeights<TWeight>(TWeight a, TWeight b)
        where TWeight : IComparable<TWeight>;
}
