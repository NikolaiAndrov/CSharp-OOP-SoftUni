namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class PresentsTests
    {
        Bag bag;
        Present present1;
        Present present2;
        Present present3;

        [SetUp]
        public void SetUp()
        {
            bag = new Bag();
            present1 = new Present("present1", 1);
            present2 = new Present("present2", 2);
            present3 = new Present("present3", 3);
        }

        [Test]
        public void PresentsShouldNotBeNull()
        {
            Assert.IsNotNull(bag.GetPresents());
        }

        [Test]
        public void CreateShouldWorkCorrectly()
        {
            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);

            int expectedCount = 3;

            Assert.AreEqual(expectedCount, bag.GetPresents().Count);
        }

        [Test]
        public void CreateShouldWorkCorrectlyAndReturnProperMessage()
        {
            string expectedMessage = $"Successfully added present {present1.Name}.";
            string actualMessage = bag.Create(present1);
            
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void CreateNullPresentShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                bag.Create(null);
            });
        }

        [Test]
        public void CreateSamePresentShouldThrow()
        {
            bag.Create(present3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                bag.Create(present3);
            });
        }

        [Test]
        public void RemoveShouldWorkCorrectly()
        {
            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);

            bag.Remove(present1);
            bag.Remove(present3);

            int expectedCount = 1;

            Assert.AreEqual(expectedCount, bag.GetPresents().Count);
        }

        [Test]
        public void RemoveExistingPresentShouldReturnTrue()
        {
            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);

            Assert.IsTrue(bag.Remove(present3));
        }

        [Test]
        public void RemoveNonExistingPresentShouldReturnFalse()
        {
            Present present = new Present("new", 100);

            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);

            Assert.IsFalse(bag.Remove(present));
        }

        [Test]
        public void GetPresentWithLeastMagicShouldWorkCorrectly()
        {
            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);

            Present present = bag.GetPresentWithLeastMagic();

            Assert.AreEqual(present, present1);
        }

        [Test]
        public void GetPresentShouldWorkCorrectly()
        {
            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);

            Present present = bag.GetPresent("present2");

            Assert.AreEqual(present2, present);
        }

        [Test]
        public void GetNonExistingPresentShouldReturnNull()
        {
            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);

            Assert.IsNull(bag.GetPresent("new"));
        }

        [Test]
        public void GetPresentsShouldReturnExpectedCollection()
        {
            var expectedCollection = new List<Present>();

            bag.Create(present1);
            bag.Create(present2);
            bag.Create(present3);

            expectedCollection.Add(present1);
            expectedCollection.Add(present2);
            expectedCollection.Add(present3);

            var actualCollection = bag.GetPresents();

            Assert.AreEqual(expectedCollection, actualCollection);

        }
    }
}
