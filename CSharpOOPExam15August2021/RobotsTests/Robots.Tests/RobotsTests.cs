namespace Robots.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class RobotsTests
    {
        RobotManager manager;
        Robot robot1;
        Robot robot2;
        Robot robot3;

        [SetUp]
        public void SetUp()
        {
            manager = new RobotManager(5);

            robot1 = new Robot("robot1", 100);
            robot2 = new Robot("robot2", 100);
            robot3 = new Robot("robot3", 100);

            manager.Add(robot1);
            manager.Add(robot2);
            manager.Add(robot3);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10000)]
        public void CreatingRobotManagerShouldSetCapacityCorrectly(int capacity)
        {
            RobotManager robotManager = new RobotManager(capacity);

            Assert.AreEqual(capacity, robotManager.Capacity);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-10000)]
        public void CreatingRobotManagerWithNegativeCapacityShouldThrowy(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RobotManager robotManager = new RobotManager(capacity);
            });
        }

        [Test]
        public void CreatingRobotManagerShouldInitializeInternalCollectionWithInitialCountZero()
        {
            RobotManager robotManager = new RobotManager(3);

            int expectedCount = 0;

            Assert.AreEqual(expectedCount, robotManager.Count);
        }

        [Test]
        public void AddMethodShouldWorkCorrectly()
        {
            int expectedCount = 3;
            Assert.AreEqual(expectedCount, manager.Count);
        }

        [Test]
        public void AddSameRobotShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Add(robot3);
            });
        }

        [Test]
        public void AddingDifferentRobotWithSameNameShouldThrow()
        {
            Robot robot = new Robot("robot3", 10000);

            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Add(robot);
            });
        }

        [Test]
        public void AddingRobotsOverCapacityShouldThrow()
        {
            Robot robot4 = new Robot("robot4", 100);
            Robot robot5 = new Robot("robot5", 100);
            Robot robot6 = new Robot("robot6", 100);

            manager.Add(robot4);
            manager.Add(robot5);

            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Add(robot6);
            });
        }

        [Test]
        public void RemoveNonExistingRobotShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Remove("R2");
            });
        }

        [Test]
        public void RemoveMethodShouldRemoveGivenRobot()
        {
            manager.Remove("robot3");

            int expectedCount = 2;

            Assert.AreEqual(expectedCount, manager.Count);
        }

        [Test]
        public void WorkWithNonExistingRobotShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Work("Tripio", "cleaning", 35);
            });
        }

        [Test]
        public void WorkWithBiggerBatteryCapacityShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Work("robot3", "cleaning", 101);
            });
        }

        [Test]
        public void WorkShouldWorkCorrectly()
        {
            manager.Work("robot3", "cleaning", 50);
            int expectedBattery = 50;

            Assert.AreEqual(expectedBattery, robot3.Battery);
        }

        [Test]
        public void ChargeNonExistingRobotShouldThrow()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                manager.Charge("Tripio");
            });
        }

        [Test]
        public void ChargeShouldWorkCorrectly()
        {
            manager.Work("robot3", "cleaning", 50);
            manager.Charge("robot3");

            int expectedBattery = 100;

            Assert.AreEqual(expectedBattery, robot3.Battery);
        }
    }
}
