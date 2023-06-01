namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        [TestCase(new int[] {})]
        [TestCase(new int[] {1})]
        [TestCase(new int[] {1,2,3,4,5})]
        [TestCase(new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16})]
        public void TestingConstructorShouldWorkAsExpected(int[] data)
        {
            Database database = new Database(data);
            int expectedCount = data.Length;
            int actualCount = database.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17})]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19})]
        public void TestingConstructorShouldThrowInvalidOperationException(int[] data)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                Database database = new Database(data);
            });
        }

        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void TestingAddMethodShouldWorkProperly(int[] data)
        {
            Database database = new Database();

            for (int i = 1; i <= data.Length; i++)
            {
                database.Add(i);
            }

            int expectedCount = data.Length;
            int actualCount = database.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16,})]
        public void TestingAddMethodShouldTheowInvalidOperationException(int[] data)
        {
            Database database = new Database(data);

            Assert.Throws<InvalidOperationException>(() => 
            {
                database.Add(17);
            });
        }

        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        public void TestingRemoveMethodShouldWorkProperly(int[] data)
        {
            Database database = new Database(data);
            database.Remove();

            int expectedCount = data.Length - 1;
            int actualCount = database.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void TestingRemoveMethodShouldInvalidOperationException()
        {
            Database database = new Database(1, 2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                database.Remove();
                database.Remove();
                database.Remove();
            });
        }

        [Test]
        public void TestingFetchMethodShouldWorkProperly()
        {
            int[] data = { 1, 2, 3 };
            Database database = new Database(data);
            int[] takenFetchElements = database.Fetch();

            CollectionAssert.AreEqual(data, takenFetchElements);
        }
    }
}
