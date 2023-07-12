namespace Gyms.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class GymsTests
    {
        Gym gym;
        Athlete athlete1;
        Athlete athlete2;
        Athlete athlete3;

        [SetUp] 
        public void SetUp() 
        {
            gym = new Gym("Body", 3);
            athlete1 = new Athlete("athlete1");
            athlete2 = new Athlete("athlete2");
            athlete3 = new Athlete("athlete3");
        }

        [TestCase(null)]
        [TestCase("")]
        public void NullOrEmptyGymNameShouldThrow(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Gym gym = new Gym(name, 10);
            });
        }

        [TestCase("B")]
        [TestCase("Body")]
        [TestCase("Body Body Body Body Body Body Body Body Body")]
        public void ConstructorShouldSetGymNameCorrectly(string name)
        {
            Gym gym = new Gym(name, 10);

            Assert.AreEqual(name, gym.Name);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void NegativeCapacityShouldThrow(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Gym gym = new Gym("Body", capacity);
            });
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void ConstructorShouldSetCapacityCorrectly(int capacity)
        {
            Gym gym = new Gym("Body", capacity);
            Assert.AreEqual(capacity, gym.Capacity);
        }

        [Test]
        public void CreatingGymShouldInitializeInternalCollectionWithCountZero()
        {
            Gym gym = new Gym("Body", 10);

            int expectedCount = 0;

            Assert.AreEqual(expectedCount, gym.Count);
        }

        [Test]
        public void AddMethodShouldWorkCorrectly()
        {
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            int expectedCount = 3;

            Assert.AreEqual(expectedCount, gym.Count);
        }

        [Test]
        public void AddMethodMoreThanCapacityShoulTgrow()
        {
            Athlete athlete4 = new Athlete("athlete4");

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.AddAthlete(athlete4);
            });
        }

        [Test]
        public void RemoveNotExistingAthleteShouldThrow()
        {
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.RemoveAthlete("athlete4");
            });
        }

        [Test]
        public void RemoveAthleteShouldWorkCorrectly()
        {
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            gym.RemoveAthlete("athlete3");
            int expectedCount = 2;

            Assert.AreEqual(expectedCount, gym.Count);
        }

        [Test]
        public void InjureNonExistingAthleteShouldThrow()
        {
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.InjureAthlete("athlete4");
            });
        }

        [Test]
        public void InjureAthleteShouldWorkCorrectly()
        {
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            Athlete athlete = gym.InjureAthlete("athlete3");

            Assert.IsTrue(athlete.IsInjured);
            Assert.AreEqual(athlete3.FullName, athlete.FullName);
        }

        [Test]
        public void ReportShouldWorkCorrectlyAndReturnProperMessage()
        {
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            Athlete athlete = gym.InjureAthlete("athlete3");

            string expectedMessage = $"Active athletes at Body: athlete1, athlete2";
            string actualMessage = gym.Report();

            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}
