using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private int health; 
        private int experience;
        Dummy dummy;

        [SetUp]
        public void SetUp()
        {
            health = 10;
            experience = 10;
            dummy = new Dummy(health, experience);
        }

        [Test]
        public void TestingHealthConstructor()
        {
            Assert.AreEqual(health, dummy.Health);
        }

        [Test]
        public void DummyShouldLoseHealthIfAttacked()
        {
            dummy.TakeAttack(5);
            Assert.AreEqual(health - 5, dummy.Health);
        }

        [Test]
        public void AttackingDeadDummyShouldTrowInvalidOperationException()
        {
            dummy.TakeAttack(10);
            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.TakeAttack(1);
            });
        }

        [Test]
        public void DeadDummyShouldGiveExperience()
        {
            dummy.TakeAttack(10);
            Assert.AreEqual(experience, dummy.GiveExperience());
        }

        [Test]
        public void AliveDummyShouldNotGiveExperience()
        {
            dummy.TakeAttack(5);
            Assert.Throws<InvalidOperationException>(() => 
            {
                dummy.GiveExperience();
            });
        }
    }
}