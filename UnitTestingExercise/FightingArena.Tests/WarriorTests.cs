namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [TestCase("N")]
        [TestCase("Konan")]
        [TestCase("Long name Konan Konan Konan Konan Konan Konan Konan Konan Konan Konan")]
        public void TestingConstructorNameShouldWorkFine(string name)
        {
            Warrior warrior = new Warrior(name, 50, 100);
            string expectedName = name;
            string actualName = warrior.Name;
            Assert.AreEqual(expectedName, actualName);
        }

        [TestCase(1)]
        [TestCase(100)]
        [TestCase(1000000)]
        public void TestingConstructorDamageShouldWorkFine(int damage)
        {
            Warrior warrior = new Warrior("Konan", damage, 100);
            int expectedDamage = damage;
            int actualDamage = warrior.Damage;
            Assert.AreEqual(expectedDamage, actualDamage);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(1000000)]
        public void TestingConstructorHpShouldWorkFine(int hp)
        {
            Warrior warrior = new Warrior("Konan", 50, hp);
            int expectedHp = hp;
            int actualHp = warrior.HP;
            Assert.AreEqual(expectedHp, actualHp);
        }

        [TestCase(null)]
        [TestCase("     ")]
        [TestCase("")]
        public void TestingConstructorNameWithNullOrEmptySpaceShouldTrowException(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
               Warrior warrior = new Warrior(name, 50, 100);
            }, "Name should not be empty or whitespace!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-100)]
        public void TestingConstructorDamageWithZeroOrNegativeNumShouldTrowException(int damage)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Konan", damage, 100);
            }, "Damage value should be positive!");
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void TestingConstructorHpWithNegativeNumShouldTrowException(int hp)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warrior = new Warrior("Konan", 50, hp);
            }, "HP should not be negative!");
        }

        [TestCase(0)]
        [TestCase(30)]
        [TestCase(25)]
        public void AttackerWithLessHpThanAllowedShouldThrowException(int w1Hp)
        {
            int w1Damage = 50;
            int w2Damage = 50;
            int w2Hp = 100;

            Warrior w1 = new Warrior("Konan", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Herkules", w2Damage, w2Hp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                w1.Attack(w2);
            }, "Your HP is too low in order to attack other warriors!");
        }

        [TestCase(0)]
        [TestCase(30)]
        [TestCase(25)]
        public void EnemyWithLessHpThanAllowedShouldThrowException(int w2Hp)
        {
            int w1Damage = 50;
            int w1Hp = 100;
            int w2Damage = 50;

            Warrior w1 = new Warrior("Konan", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Herkules", w2Damage, w2Hp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                w1.Attack(w2);
            }, $"Enemy HP must be greater than 30 in order to attack him!");
        }

        [TestCase(35)]
        [TestCase(50)]
        [TestCase(89)]
        public void AttackingStrongerEnemyShouldThrowException(int w1Hp)
        {
            int w1Damage = 50;
            int w2Damage = 90;
            int w2Hp = 100;

            Warrior w1 = new Warrior("Konan", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Herkules", w2Damage, w2Hp);

            Assert.Throws<InvalidOperationException>(() =>
            {
                w1.Attack(w2);
            }, $"You are trying to attack too strong enemy");
        }

        [Test]
        public void AttackingEnemyShouldWorkProperly()
        {
            int w1Damage = 50;
            int w1Hp = 100;
            int w2Damage = 50;
            int w2Hp = 100;

            Warrior w1 = new Warrior("Konan", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Herkules", w2Damage, w2Hp);

            int w1ExpectedHp = w1Hp - w2Damage;
            int w2ExpectedHp = w2Hp - w1Damage;

            w1.Attack(w2);

            int w1ActualHp = w1.HP;
            int w2ActualHp = w2.HP;

            Assert.AreEqual(w1ExpectedHp, w1ActualHp);
            Assert.AreEqual(w2ExpectedHp, w2ActualHp);
        }

        [TestCase(49)]
        [TestCase(35)]
        public void AttackingAndKillEnemyShouldWorkProperly(int w2Hp)
        {
            int w1Damage = 50;
            int w1Hp = 100;
            int w2Damage = 50;

            Warrior w1 = new Warrior("Konan", w1Damage, w1Hp);
            Warrior w2 = new Warrior("Herkules", w2Damage, w2Hp);

            int w1ExpectedHp = w1Hp - w2Damage;
            int w2ExpectedHp = 0;

            w1.Attack(w2);

            int w1ActualHp = w1.HP;
            int w2ActualHp = w2.HP;

            Assert.AreEqual(w1ExpectedHp, w1ActualHp);
            Assert.AreEqual(w2ExpectedHp, w2ActualHp);
        }
    }
}