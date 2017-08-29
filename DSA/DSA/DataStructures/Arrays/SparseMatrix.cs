using System.Collections.Generic;
using System.Linq;

namespace DSA.DataStructures.Arrays
{
    /// <summary>
    /// Represents a Sparse matrix.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    public class SparseMatrix<T>
    {
        /// <summary>
        /// Dictionary containing the row index as a key and as a value another dictionary
        /// containing the column index as a key and the stored value as a value.
        /// </summary>
        internal Dictionary<int, Dictionary<int, T>> rows;

        /// <summary>
        /// Dictionary containing the column index as a key and as a value another dictionary
        /// containing the row index as a key and the stored value as a value.
        /// </summary>
        internal Dictionary<int, Dictionary<int, T>> cols;

        /// <summary>
        /// Gets the maximum reached height of the sparse matrix.
        /// </summary>
        public int Height { get; internal set; }

        /// <summary>
        /// Gets the maximum reached width of the sparse matrix.
        /// </summary>
        public int Width { get; internal set; }

        /// <summary>
        /// Gets the number of items in the <see cref="SparseMatrix{T}"/>.
        /// </summary>
        public int Count { get; internal set; }

        /// <summary>
        /// Gets or sets an item in the sparse matrix. If there is no item on the given position
        /// on get the default value of T is returned and on set the item is added to the matrix.
        /// </summary>
        /// <param name="row">The zero-based row index of the item.</param>
        /// <param name="col">The zero-based column index of the item.</param>
        /// <returns>Returns the item in the sparse matrix. If there is no item on the given position the default value of T is returned instead.</returns>
        public T this[int row, int col]
        {
            get
            {
                if (rows.ContainsKey(row))
                {
                    if (rows[row].ContainsKey(col))
                    {
                        return rows[row][col];
                    }
                }

                //If there is no item on the given position return defaault value
                return default(T);
            }
            set
            {
                if (row >= Height) Height = row + 1;
                if (col >= Width) Width = col + 1;

                //If no items on the current row we have to create a new dictionary
                if (!rows.ContainsKey(row))
                    rows.Add(row, new Dictionary<int, T>());

                //If no items on the current col we have to create a new dictionary
                if (!cols.ContainsKey(col))
                    cols.Add(col, new Dictionary<int, T>());

                rows[row][col] = value;
                cols[col][row] = value;

                Count++;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SparseMatrix{T}"/> class.
        /// </summary>
        public SparseMatrix()
        {
            rows = new Dictionary<int, Dictionary<int, T>>();
            cols = new Dictionary<int, Dictionary<int, T>>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SparseMatrix{T}"/> class from the given two dimensional array.
        /// </summary>
        /// <param name="array">The two dimensional array of items to add.</param>
        /// <param name="zeroItem">The item considered a zero item. All items from the array equal to the zero item won't be added to the matrix.</param>
        public SparseMatrix(T[,] array, T zeroItem)
        {
            rows = new Dictionary<int, Dictionary<int, T>>();
            cols = new Dictionary<int, Dictionary<int, T>>();

            for (int row = 0; row < array.GetLength(0); row++)
            {
                for (int col = 0; col < array.GetLength(1); col++)
                {
                    if (!object.Equals(array[row,col], zeroItem))
                        this[row, col] = array[row, col];
                }
            }
        }

        /// <summary>
        /// Determines if there is an item on the given position.
        /// </summary>
        /// <param name="row">The zero-based row index of the item.</param>
        /// <param name="col">The zero-based column index of the item.</param>
        /// <returns>Returns true if there is an item on the given position; otherwise false.</returns>
        public bool IsCellEmpty(int row, int col)
        {
            if (rows.ContainsKey(row))
            {
                if (rows[row].ContainsKey(col))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Gets the items in the given row sorted by the column index as an <see cref="IEnumerable{T}"/>
        /// of <see cref="KeyValuePair{TKey, TValue}"/> with the key being the column index and the value being the item.
        /// </summary>
        /// <param name="row">The zero-based row index.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/>
        /// with the key being the column index and the value being the item.</returns>
        public IEnumerable<KeyValuePair<int, T>> GetRowItems(int row)
        {
            if (rows.ContainsKey(row))
            {
                var sortedDict = new SortedDictionary<int, T>(rows[row]);
                foreach (var item in sortedDict)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Gets the items in the given column sorted by the row index as an <see cref="IEnumerable{T}"/>
        /// of <see cref="KeyValuePair{TKey, TValue}"/> with the key being the row index and the value being the item.
        /// </summary>
        /// <param name="col">The zero-based column index.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of <see cref="KeyValuePair{TKey, TValue}"/>
        /// with the key being the row index and the value being the item.</returns>
        public IEnumerable<KeyValuePair<int, T>> GetColumnItems(int col)
        {
            if (cols.ContainsKey(col))
            {
                var sortedDict = new SortedDictionary<int, T>(cols[col]);
                foreach (var item in sortedDict)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Gets non empty rows indexes sorted in ascending order.
        /// </summary>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of integers being row indexes sorted in ascending order.</returns>
        public IEnumerable<int> GetNonEmptyRows()
        {
            var sortedRows = new SortedSet<int>(rows.Keys);
            foreach (var row in sortedRows)
            {
                yield return row;
            }
        }

        /// <summary>
        /// Gets non empty columns indexes sorted in ascending order.
        /// </summary>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> of integers being column indexes sorted in ascending order.</returns>
        public IEnumerable<int> GetNonEmptyColumns()
        {
            var sortedCols = new SortedSet<int>(cols.Keys);
            foreach (var col in sortedCols)
            {
                yield return col;
            }
        }

        /// <summary>
        /// Removes the item on the given position.
        /// </summary>
        /// <param name="row">The zero-based row index.</param>
        /// <param name="col">The zero-based column index.</param>
        /// <returns>Returns true if item is removed successfully; otherwise false. Also returns false if the item is not found.</returns>
        public bool Remove(int row, int col)
        {
            if (rows.ContainsKey(row))
            {
                if (rows[row].ContainsKey(col))
                {
                    bool removedSuccessfully = true;
                    if (!rows[row].Remove(col) || !cols[col].Remove(row)) removedSuccessfully = false;

                    if (rows[row].Count == 0)
                    {
                        rows.Remove(row);
                    }

                    if (cols[col].Count == 0)
                    {
                        cols.Remove(col);
                    }

                    if (removedSuccessfully)
                        Count--;

                    return removedSuccessfully;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all elements from the sparse matrix.
        /// </summary>
        public void Clear()
        {
            rows.Clear();
            cols.Clear();

            Count = 0;
            Height = 0;
            Width = 0;
        }

        /// <summary>
        /// Updates the height and the width of the matrix. If no items were removed from the matrix the dimensions will be correct.
        /// </summary>
        public void UpdateDimensions()
        {
            if (rows.Count == 0)
            {
                Height = 0;
                Width = 0;
                return;
            }

            Height = rows.Keys.Max() + 1;
            Width = cols.Keys.Max() + 1;
        }
    }
}
