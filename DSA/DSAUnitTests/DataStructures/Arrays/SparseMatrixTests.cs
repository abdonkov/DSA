using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Arrays;
using DSA.DataStructures.Lists;

namespace DSAUnitTests.DataStructures.Arrays
{
    [TestClass]
    public class SparseMatrixTests
    {
        [TestMethod]
        public void AddingItemsAndCheckingIfContained()
        {
            var matrix = new SparseMatrix<int>();

            int maxRowsNumber = 1000;
            int maxColsNumber = 500;

            int rowIncrease = 3;
            int colIncrease = 2;

            int itemsCount = 0;

            for (int i = 0; i < maxRowsNumber; i+= rowIncrease)
            {
                for (int j = 0; j < maxColsNumber; j+= colIncrease)
                {
                    if (!matrix.IsCellEmpty(i, j)) Assert.Fail();

                    matrix[i, j] = 5;
                    itemsCount++;

                    if (matrix.IsCellEmpty(i, j)) Assert.Fail();
                }
            }

            Assert.IsTrue(matrix.Count == itemsCount
                            && matrix.Height == (maxRowsNumber - 1) / rowIncrease * rowIncrease + 1
                            && matrix.Width == (maxColsNumber - 1) / colIncrease * colIncrease + 1);
        }

        [TestMethod]
        public void AddingAndRemovingHalfOfTheItems()
        {
            var matrix = new SparseMatrix<int>();

            int maxRowsNumber = 1000;
            int maxColsNumber = 500;

            int rowIncrease = 3;
            int colIncrease = 2;

            int itemsCount = 0;

            var rowsAddedTo = new SinglyLinkedList<int>();
            var colsAddedTo = new SinglyLinkedList<int>();

            for (int i = 0; i < maxRowsNumber; i += rowIncrease)
            {
                for (int j = 0; j < maxColsNumber; j += colIncrease)
                {
                    if (!matrix.IsCellEmpty(i, j)) Assert.Fail();

                    matrix[i, j] = 5;
                    itemsCount++;
                    rowsAddedTo.AddFirst(i);
                    colsAddedTo.AddFirst(j);

                    if (matrix.IsCellEmpty(i, j)) Assert.Fail();
                }
            }

            var removedItems = 0;
            var removedSuccessfully = true;

            while (rowsAddedTo.Count > itemsCount / 2)
            {
                var row = rowsAddedTo.First.Value;
                var col = colsAddedTo.First.Value;

                rowsAddedTo.RemoveFirst();
                colsAddedTo.RemoveFirst();

                if (matrix.IsCellEmpty(row, col)) Assert.Fail();

                if (!matrix.Remove(row, col)) removedSuccessfully = false;
                else removedItems++;

                if (!matrix.IsCellEmpty(row, col)) Assert.Fail();
            }

            Assert.IsTrue(matrix.Count == itemsCount - removedItems
                            && removedSuccessfully);
        }

        [TestMethod]
        public void AddingAndRemovingAllItems()
        {
            var matrix = new SparseMatrix<int>();

            int maxRowsNumber = 1000;
            int maxColsNumber = 500;

            int rowIncrease = 3;
            int colIncrease = 2;

            int itemsCount = 0;

            var rowsAddedTo = new SinglyLinkedList<int>();
            var colsAddedTo = new SinglyLinkedList<int>();

            for (int i = 0; i < maxRowsNumber; i += rowIncrease)
            {
                for (int j = 0; j < maxColsNumber; j += colIncrease)
                {
                    if (!matrix.IsCellEmpty(i, j)) Assert.Fail();

                    matrix[i, j] = 5;
                    itemsCount++;
                    rowsAddedTo.AddFirst(i);
                    colsAddedTo.AddFirst(j);

                    if (matrix.IsCellEmpty(i, j)) Assert.Fail();
                }
            }

            var removedItems = 0;
            var removedSuccessfully = true;

            while (rowsAddedTo.Count > 0)
            {
                var row = rowsAddedTo.First.Value;
                var col = colsAddedTo.First.Value;

                rowsAddedTo.RemoveFirst();
                colsAddedTo.RemoveFirst();

                if (matrix.IsCellEmpty(row, col)) Assert.Fail();

                if (!matrix.Remove(row, col)) removedSuccessfully = false;
                else removedItems++;

                if (!matrix.IsCellEmpty(row, col)) Assert.Fail();
            }

            matrix.UpdateDimensions();

            Assert.IsTrue(matrix.Count == itemsCount - removedItems
                            && removedSuccessfully
                            && matrix.Height == 0
                            && matrix.Width == 0);
        }

        [TestMethod]
        public void CheckIfGetRowItemsReturnItemsSortedByColumnIndex()
        {
            var matrix = new SparseMatrix<int>();

            int maxRowsNumber = 1000;
            int maxColsNumber = 500;

            int rowIncrease = 3;
            int colIncrease = 2;

            int itemsCount = 0;

            for (int i = 0; i < maxRowsNumber; i += rowIncrease)
            {
                for (int j = 0; j < maxColsNumber; j += colIncrease)
                {
                    if (!matrix.IsCellEmpty(i, j)) Assert.Fail();

                    matrix[i, j] = 5;
                    itemsCount++;

                    if (matrix.IsCellEmpty(i, j)) Assert.Fail();
                }
            }

            foreach (var row in matrix.GetNonEmptyRows())
            {
                var lastColumn = int.MinValue;
                foreach (var item in matrix.GetRowItems(row))
                {
                    if (lastColumn > item.Key) Assert.Fail();

                    lastColumn = item.Key;
                }
            }

            Assert.IsTrue(matrix.Count == itemsCount);
        }

        [TestMethod]
        public void CheckIfGetColumnItemsReturnItemsSortedByRowIndex()
        {
            var matrix = new SparseMatrix<int>();

            int maxRowsNumber = 1000;
            int maxColsNumber = 500;

            int rowIncrease = 3;
            int colIncrease = 2;

            int itemsCount = 0;

            for (int i = 0; i < maxRowsNumber; i += rowIncrease)
            {
                for (int j = 0; j < maxColsNumber; j += colIncrease)
                {
                    if (!matrix.IsCellEmpty(i, j)) Assert.Fail();

                    matrix[i, j] = 5;
                    itemsCount++;

                    if (matrix.IsCellEmpty(i, j)) Assert.Fail();
                }
            }

            foreach (var col in matrix.GetNonEmptyColumns())
            {
                var lastRow = int.MinValue;
                foreach (var item in matrix.GetColumnItems(col))
                {
                    if (lastRow > item.Key) Assert.Fail();

                    lastRow = item.Key;
                }
            }

            Assert.IsTrue(matrix.Count == itemsCount);
        }

        [TestMethod]
        public void AddingAfterClearingMatrix()
        {
            var matrix = new SparseMatrix<int>();

            int maxRowsNumber = 1000;
            int maxColsNumber = 500;

            int rowIncrease = 3;
            int colIncrease = 2;

            int itemsCount = 0;

            for (int i = 0; i < maxRowsNumber; i += rowIncrease)
            {
                for (int j = 0; j < maxColsNumber; j += colIncrease)
                {
                    if (!matrix.IsCellEmpty(i, j)) Assert.Fail();

                    matrix[i, j] = 5;
                    itemsCount++;

                    if (matrix.IsCellEmpty(i, j)) Assert.Fail();
                }
            }

            if (matrix.Count != itemsCount) Assert.Fail();

            itemsCount = 0;
            matrix.Clear();

            if (matrix.Count != 0 || matrix.Height != 0 || matrix.Width != 0) Assert.Fail();

            for (int i = 0; i < maxRowsNumber; i += rowIncrease)
            {
                for (int j = 0; j < maxColsNumber; j += colIncrease)
                {
                    if (!matrix.IsCellEmpty(i, j)) Assert.Fail();

                    matrix[i, j] = 5;
                    itemsCount++;

                    if (matrix.IsCellEmpty(i, j)) Assert.Fail();
                }
            }

            Assert.IsTrue(matrix.Count == itemsCount
                            && matrix.Height == (maxRowsNumber - 1) / rowIncrease * rowIncrease + 1
                            && matrix.Width == (maxColsNumber - 1) / colIncrease * colIncrease + 1);
        }


    }
}
