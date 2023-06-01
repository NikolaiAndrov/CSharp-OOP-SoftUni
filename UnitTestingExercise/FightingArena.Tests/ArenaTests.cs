namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
        }

        [Test]
        public void TestingArenaConstructorWarriorsShouldNotBeNull()
        {
            Arena currentArena = new Arena();
            Assert.IsNotNull(arena.Warriors);
        }

        [Test]
        public void EnrollWarriorsShouldWorkFine()
        {
            Warrior warrior = new Warrior("Hercules", 50, 100);
            arena.Enroll(warrior);
            bool isWarriorEnrolled = arena.Warriors.Contains(warrior);
            Assert.IsTrue(isWarriorEnrolled);
        }

        [Test]
        public void EnrollSameWarriorSecondTimeShouldThrowException()
        {
            Warrior warrior = new Warrior("Hercules", 50, 100);
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Enroll(warrior);
            }, "Warrior is already enrolled for the fights!");
        }

        [Test]
        public void EnrollWarriorWithSameNameShouldThrowException()
        {
            Warrior warrior = new Warrior("Hercules", 50, 100);
            Warrior warrior2 = new Warrior("Hercules", 45, 85);
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Enroll(warrior2);
            }, "Warrior is already enrolled for the fights!");
        }

        [Test]
        public void TestingCountShouldWorkFine()
        {
            Warrior warrior = new Warrior("Hercules", 50, 100);
            Warrior warrior2 = new Warrior("Kitana", 50, 100);
            arena.Enroll(warrior);
            arena.Enroll(warrior2);

            int expectedCount = 2;
            int actualCount = arena.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void TestingCountWithNoWarriorsShouldWorkFine()
        {
            int expectedCount = 0;
            int actualCount = arena.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void FightWithNonExistingWarriorShouldThrowException()
        {
            Warrior warrior = new Warrior("Hercules", 50, 100);
            Warrior warrior2 = new Warrior("Kitana", 50, 100);
            arena.Enroll(warrior2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight(warrior.Name, warrior2.Name);
            }, $"There is no fighter with name {warrior.Name} enrolled for the fights!");
        }

        [Test]
        public void FightWithNonExistingEnemyShouldThrowException()
        {
            Warrior warrior = new Warrior("Hercules", 50, 100);
            Warrior warrior2 = new Warrior("Kitana", 50, 100);
            arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                arena.Fight(warrior.Name, warrior2.Name);
            }, $"There is no fighter with name {warrior2.Name} enrolled for the fights!");
        }

        [Test]
        public void FightShouldWorkProperly()
        {
            Warrior warrior = new Warrior("Hercules", 50, 100);
            Warrior warrior2 = new Warrior("Kitana", 50, 100);
            arena.Enroll(warrior);
            arena.Enroll(warrior2);

            int w1ExpectedHp = warrior.HP - warrior2.Damage;
            int w2ExpectedHp = warrior2.HP - warrior.Damage;

            arena.Fight(warrior.Name, warrior2.Name);

            int w1ActualHp = arena.Warriors.FirstOrDefault(w => w.Name == warrior.Name).HP;
            int w2ActualHp = arena.Warriors.FirstOrDefault(w => w.Name == warrior2.Name).HP;

            Assert.AreEqual(w1ExpectedHp, w1ActualHp);
            Assert.AreEqual(w2ExpectedHp, w2ActualHp);
        }
    }
}
