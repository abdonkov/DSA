using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DSA.DataStructures.Stacks;

namespace DSAUnitTests.DataStructures.Stacks
{
    [TestClass]
    public class ArrayStackTests
    {
        [TestMethod]
        public void PushingItemsOneByOne()
        {
            var stack = new ArrayStack<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            int trueCount = 0;
            int lastItem = int.MaxValue;

            foreach (var item in stack)
            {
                if (lastItem < item) Assert.Fail();
                lastItem = item;
                trueCount++;
            }

            Assert.IsTrue(stack.Count == itemCount
                            && stack.Count == trueCount);
        }

        [TestMethod]
        public void InitializingArrayStackWithCollection()
        {
            var stack = new ArrayStack<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            // items in stack2 are in reversed order
            var stack2 = new ArrayStack<int>(stack);

            int trueCount = 0;
            int lastItem = int.MinValue;
            foreach (var item in stack2)
            {
                if (lastItem > item) Assert.Fail();
                lastItem = item;
                trueCount++;
            }

            Assert.IsTrue(stack.Count == itemCount
                            && stack2.Count == itemCount
                            && stack2.Count == trueCount);
        }

        [TestMethod]
        public void PoppingAllExceptOne()
        {
            var stack = new ArrayStack<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            int lastItem = int.MaxValue;
            for (int i = 0; i < itemCount - 1; i++)
            {
                if (lastItem < stack.Pop()) Assert.Fail();
            }

            int trueCount = 0;


            foreach (var item in stack)
            {
                trueCount++;
            }

            Assert.IsTrue(stack.Count == 1
                            && trueCount == 1);
        }

        [TestMethod]
        public void InitializationWithZeroCapacityAndPushingItemsAfterwards()
        {
            var stack = new ArrayStack<int>(0);

            int itemCount = 100;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            int trueCount = 0;
            int lastItem = int.MaxValue;
            foreach (var item in stack)
            {
                if (lastItem < item) Assert.Fail();
                lastItem = item;
                trueCount++;
            }

            Assert.IsTrue(stack.Count == itemCount
                            && stack.Count == trueCount);
        }

        [TestMethod]
        public void PoppingAllItemsAndPushingAgain()
        {
            var stack = new ArrayStack<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            int lastItem = int.MaxValue;
            for (int i = 0; i < itemCount; i++)
            {
                if (lastItem < stack.Pop()) Assert.Fail();
            }

            bool countWasZero = stack.Count == 0;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            int trueCount = 0;
            lastItem = int.MaxValue;

            foreach (var item in stack)
            {
                if (lastItem < item) Assert.Fail();
                lastItem = item;
                trueCount++;
            }

            Assert.IsTrue(stack.Count == itemCount
                            && stack.Count == trueCount
                            && countWasZero);
        }

        [TestMethod]
        public void CheckIfContainedBeforeAndAfterPopping()
        {
            var stack = new ArrayStack<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                if (stack.Contains(i)) Assert.Fail();
                stack.Push(i);
                if (!stack.Contains(i)) Assert.Fail();
            }

            int lastItem = int.MaxValue;
            for (int i = itemCount - 1; i >= 0; i--)
            {
                var top = stack.Peek();
                if (!stack.Contains(i)) Assert.Fail();
                var popped = stack.Pop();
                if (popped != top) Assert.Fail();
                if (lastItem < popped) Assert.Fail();
                if (stack.Contains(i)) Assert.Fail();
                lastItem = top;
            }

            Assert.IsTrue(stack.Count == 0);
        }

        [TestMethod]
        public void PushingAfterClearingCollection()
        {
            var stack = new ArrayStack<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            stack.Clear();

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            int trueCount = 0;
            int lastItem = int.MaxValue;
            foreach (var item in stack)
            {
                if (lastItem < item) Assert.Fail();
                lastItem = item;
                trueCount++;
            }

            Assert.IsTrue(stack.Count == itemCount
                            && stack.Count == trueCount);
        }

        [TestMethod]
        public void PushingItemsAndCheckingIfIteratedInReversedOrder()
        {
            var stack = new ArrayStack<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            int trueCount = 0;
            int itemNum = itemCount - 1;
            foreach (var item in stack)
            {
                if (itemNum-- != item) Assert.Fail();
                trueCount++;
            }

            Assert.IsTrue(stack.Count == itemCount
                            && stack.Count == trueCount);
        }

        [TestMethod]
        public void ConvertingStackToArray()
        {
            var stack = new ArrayStack<int>();

            int itemCount = 1000;

            for (int i = 0; i < itemCount; i++)
            {
                stack.Push(i);
            }

            var array = stack.ToArray();

            int trueCount = 0;
            for (int i = 0; i < itemCount; i++)
            {
                if (array[i] != itemCount - 1 - i) Assert.Fail();
                trueCount++;
            }

            Assert.IsTrue(stack.Count == itemCount
                            && stack.Count == trueCount);
        }
    }
}
