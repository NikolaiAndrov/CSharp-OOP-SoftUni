using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private Axe axe;
        private Dummy dummy;
        private int durabilityPoints;
        private int attackPoints;

        [SetUp]
        public void SetUp()
        {
            durabilityPoints = 1;
            attackPoints = 1;
            axe = new Axe(durabilityPoints, attackPoints);
            dummy = new Dummy(10, 10);
        }

        [Test]
        public void TestingConstructors()
        {
            Assert.AreEqual(durabilityPoints, axe.DurabilityPoints);
            Assert.AreEqual(attackPoints, axe.AttackPoints);
        }

        [Test]
        public void AxeLosesDurabilityAfterAtack()
        {
            axe.Attack(dummy);
            Assert.AreEqual(durabilityPoints - 1, axe.DurabilityPoints, "Axe Durability doesn't change after attack");
        }

        [Test]
        public void AttackingWithBrokenWeapon()
        {
            axe.Attack(dummy);

            Assert.Throws<System.InvalidOperationException>(() => 
            {
                axe.Attack(dummy);
            }, "Shkould throw an InvalidOperationException if trying to attack with broken Axe (durability <= 0)");
        }
    }
}