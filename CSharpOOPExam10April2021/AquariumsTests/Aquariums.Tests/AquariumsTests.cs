namespace Aquariums.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class AquariumsTests
    {
        Aquarium aquarium;

        Fish fish1;
        Fish fish2;
        Fish fish3;

        [SetUp]
        public void SetUp()
        {
            aquarium = new Aquarium("Aqua World", 3);
            fish1 = new Fish("fish1");
            fish2 = new Fish("fish2");
            fish3 = new Fish("fish3");
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreatingAquariumNameWithNullOrEmptyStringShouldThrow(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Aquarium aquarium = new Aquarium(name, 5);
            });
        }

        [TestCase("Name")]
        [TestCase("Name Name Name Name Name Name Name Name Name Name Name Name Name")]
        [TestCase("N")]
        public void ConstructorShouldSetNameCorrectly(string name)
        {
            Aquarium aquarium = new Aquarium(name, 5);

            Assert.AreEqual(name, aquarium.Name);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-1000)]
        public void CreatingAquariumWithNegativeCapacityShouldThrow(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Aquarium aquarium = new Aquarium("Name", capacity);
            });
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10000)]
        public void ConstructorShouldSetCapacityCorrectly(int capacity)
        {
            Aquarium aquarium = new Aquarium("Name", capacity);
            Assert.AreEqual(capacity, aquarium.Capacity);
        }

        [Test]
        public void InternalCollectionShouldBeInitializedAndReturnCountZero()
        {
            int expectedCount = 0;

            Assert.AreEqual(expectedCount, aquarium.Count);
        }

        [Test]
        public void AddFishShouldWorkCorrectly()
        {
            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            int expectedCount = 3;

            Assert.AreEqual(expectedCount, aquarium.Count);
        }

        [Test]
        public void AddFishOverCapacityShouldThrow()
        {
            Fish nemo = new Fish("Nemo");

            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.Add(nemo);
            });
        }

        [Test]
        public void RemoveNonExistingFishShoildThrow()
        {
            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.RemoveFish("Nemo");
            });
        }

        [Test]
        public void RemoveShouldWorkCorrectly()
        {
            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            aquarium.RemoveFish("fish1");

            int expectedCount = 2;

            Assert.AreEqual(expectedCount, aquarium.Count);
        }

        [Test]
        public void SellNonExistingFishShouldThrow()
        {
            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                aquarium.SellFish("Nemo");
            });
        }

        [Test]
        public void SellFishShlouldWorkCorrectly()
        {
            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            Fish soldFish = aquarium.SellFish("fish3");
            Assert.IsFalse(soldFish.Available);
        }

        [Test]
        public void ReportShouldReturnProperMessage()
        {
            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);

            string expectedMessage = $"Fish available at {aquarium.Name}: fish1, fish2, fish3";
            string actualMessage = aquarium.Report();

            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}
