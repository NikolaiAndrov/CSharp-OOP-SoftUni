using NUnit.Framework;
using System;
using System.ComponentModel;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            Planet planet;

            Weapon weapon1;
            Weapon weapon2;
            Weapon weapon3;

            [SetUp]
            public void Setup()
            {
                planet = new Planet("Earth", 10);
                weapon1 = new Weapon("Bomb1", 2, 5);
                weapon2 = new Weapon("Bomb2", 2, 5);
                weapon3 = new Weapon("Bomb3", 2, 100);
            }

            [TestCase("Bomb")]
            [TestCase("l")]
            [TestCase("Bomb Bomb Bomb Bomb Bomb Bomb Bomb Bomb Bomb Bomb Bomb")]
            public void WeaponConstructorShouldSetNameProperly(string weapontName)
            {
                Weapon weapon = new Weapon(weapontName, 10, 10);

                Assert.AreEqual(weapontName, weapon.Name);
            }

            [TestCase(-0.1)]
            [TestCase(-1)]
            [TestCase(-10000000)]
            public void NegativeWeaponPriceShouldThrow(double price)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Weapon weapon = new Weapon("Bomb", price, 10);
                }, "Price cannot be negative.");
            }

            [TestCase(0)]
            [TestCase(1)]
            [TestCase(10000000)]
            public void WeaponConstructorShouldSetPriceProperly(double price)
            {
                Weapon weapon = new Weapon("Bomb", price, 10);

                Assert.AreEqual(price, weapon.Price);
            }

            [TestCase(0)]
            [TestCase(9)]
            [TestCase(10000)]
            public void WeaponConstructorShouldSetDestructionLevelProperly(int destructionLevel)
            {
                Weapon weapon = new Weapon("Bomb", 10, destructionLevel);
                Assert.AreEqual(destructionLevel, weapon.DestructionLevel);
            }

            [TestCase(1)]
            [TestCase(5)]
            [TestCase(9)]
            public void DestructionLevelLessThan10IsNuclearShouldBeFalse(int destructionLevel)
            {
                Weapon weapon = new Weapon("Bomb", 10, destructionLevel);
                Assert.IsFalse(weapon.IsNuclear);
            }

            [TestCase(10)]
            [TestCase(11)]
            [TestCase(10000)]
            public void DestructionLeve10OrMoreIsNuclearShouldBeTrue(int destructionLevel)
            {
                Weapon weapon = new Weapon("Bomb", 10, destructionLevel);
                Assert.IsTrue(weapon.IsNuclear);
            }

            [Test]
            public void IncreaseDestructionLevelShouldWorkProperlyIsNuclearShouldBeFalse()
            {
                Weapon weapon = new Weapon("Bomb", 10, 5);

                for (int i = 0; i < 3; i++)
                {
                    weapon.IncreaseDestructionLevel();
                }

                int expectedDestructionLevel = 8;

                Assert.AreEqual(expectedDestructionLevel, weapon.DestructionLevel);
                Assert.IsFalse(weapon.IsNuclear);
            }

            [Test]
            public void IncreaseDestructionLevelShouldWorkProperlyIsNuclearShouldBeTrue()
            {
                Weapon weapon = new Weapon("Bomb", 10, 9);

                for (int i = 0; i < 3; i++)
                {
                    weapon.IncreaseDestructionLevel();
                }

                int expectedDestructionLevel = 12;

                Assert.AreEqual(expectedDestructionLevel, weapon.DestructionLevel);
                Assert.IsTrue(weapon.IsNuclear);
            }

            [TestCase("")]
            [TestCase(null)]
            public void NullOrEmptyPlanetNameShouldThrow(string planetName)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Planet planet = new Planet(planetName, 10);
                }, "Invalid planet Name");
            }

            [TestCase("Earth")]
            [TestCase("l")]
            [TestCase("Earth Earth Earth Earth Earth Earth Earth Earth Earth Earth")]
            public void ConstructorShouldSetPlanetNameProperly(string planetName)
            {
                Planet planet = new Planet(planetName, 10);

                Assert.AreEqual(planetName, planet.Name);
            }

            [TestCase(-0.1)]
            [TestCase(-1)]
            [TestCase(-100)]
            public void NegativeBudgetShouldThrow(double budget)
            {

                Assert.Throws<ArgumentException>(() =>
                {
                    Planet planet = new Planet("Earth", budget);
                }, "Budget cannot drop below Zero!");
            }

            [TestCase(0)]
            [TestCase(1)]
            [TestCase(1000.5)]
            public void ConstructorShouldSetBudgetCorrectly(double budget)
            {
                Planet planet = new Planet("Earth", budget);

                Assert.AreEqual(budget, planet.Budget);
            }

            [Test]
            public void WeaponsCollectionShoudBeInitialized()
            {
                Planet planet = new Planet("Earth", 10);
                Assert.IsNotNull(planet.Weapons);
            }

            [Test]
            public void AddMethodShouldWorkFine()
            {
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                int expectedCount = 3;

                Assert.AreEqual(expectedCount, planet.Weapons.Count);
            }

            [Test]
            public void AddSameWeaponShouldThrow()
            {
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapon3);
                }, $"There is already a {weapon3.Name} weapon.");
            }


            [Test]
            public void AddWeaponWithSameNameShouldThrow()
            {
                Weapon weapon = new Weapon("Bomb3", 1000, 10000);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapon);
                }, $"There is already a {weapon.Name} weapon.");
            }

            [Test]
            public void MilitaryRatioMethodShouldWorkProperly()
            {
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                int expectedMilitaryRatio = 110;

                Assert.AreEqual(expectedMilitaryRatio, planet.MilitaryPowerRatio);
            }

            [Test]
            public void ProfitMethodShouldIncreaseBudget()
            {
                planet.Profit(100);

                double expectedBudget = 110;

                Assert.AreEqual(expectedBudget, planet.Budget);
            }

            [Test]
            public void SpendFundsMoreThanBudgetShouldThrow()
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.SpendFunds(100);
                }, "Not enough funds to finalize the deal.");
            }

            [Test]
            public void SpendsFundShouldDecreaseBudget()
            {
                planet.SpendFunds(5);
                double expectedBudget = 5;

                Assert.AreEqual(expectedBudget, planet.Budget);
            }

            [Test]
            public void RemoveMethodShouldWorkProperly()
            {
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                planet.RemoveWeapon(weapon3.Name);

                int expectedCount = 2;

                Assert.AreEqual(expectedCount, planet.Weapons.Count);
            }

            [Test]
            public void RemoveNotExistingWeaponShuldNotRemoveAnything()
            {
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                planet.RemoveWeapon("Non existingWeapon");

                int expectedCount = 3;

                Assert.AreEqual(expectedCount, planet.Weapons.Count);
            }

            [Test]
            public void UpgradeNonExistingWeaponShouldThrow()
            {
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                string nonExistingWeapon = "firework";

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.UpgradeWeapon(nonExistingWeapon);
                }, $"{nonExistingWeapon} does not exist in the weapon repository of {planet.Name}");
            }

            [Test]
            public void UpgradeWeaponShouldIncreaseDestructionLevelOfWeapon()
            {
                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                planet.UpgradeWeapon(weapon3.Name);

                int expectedDestructionLevel = 101;

                Assert.AreEqual(expectedDestructionLevel, weapon3.DestructionLevel);
            }

            [Test]
            public void DestructOpponentMorePowerfulOponentShouldThrow()
            {
                Planet opponent = new Planet("Oponent", 10000);

                planet.AddWeapon(weapon1);

                opponent.AddWeapon(weapon1);
                opponent.AddWeapon(weapon2);
                opponent.AddWeapon(weapon3);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.DestructOpponent(opponent);
                }, $"{opponent.Name} is too strong to declare war to!");
            }


            [Test]
            public void DestructOpponentShouldWorkFine()
            {
                Planet opponent = new Planet("Oponent", 10);

                planet.AddWeapon(weapon1);
                planet.AddWeapon(weapon2);
                planet.AddWeapon(weapon3);

                opponent.AddWeapon(weapon1);

                string expectedMessage = $"{opponent.Name} is destructed!";
                string actualMessage = planet.DestructOpponent(opponent);

                Assert.AreEqual(expectedMessage, actualMessage);
            }
        }
    }
}
