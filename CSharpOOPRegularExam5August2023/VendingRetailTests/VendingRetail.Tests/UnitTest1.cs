using NUnit.Framework;
using System.Xml.Linq;

namespace VendingRetail.Tests
{
    [TestFixture]
    public class Tests
    {
        CoffeeMat coffeeMat;
        
        [SetUp]
        public void Setup()
        {
           coffeeMat = new CoffeeMat(100, 3);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(10000)]
        public void WaterCapacityShouldBeSetCorrectly(int capacity)
        {
            CoffeeMat coffeeMat = new CoffeeMat(capacity, 3);

            Assert.AreEqual(capacity, coffeeMat.WaterCapacity);
        }

        [TestCase(10)]
        [TestCase(100)]
        [TestCase(10000)]
        public void ButtonsShouldBeSetCorrectly(int buttons)
        {
            CoffeeMat coffeeMat = new CoffeeMat(100, buttons);

            Assert.AreEqual(buttons, coffeeMat.ButtonsCount);
        }

        [Test]
        public void InitialIncomShouldBeZero()
        {
            double expectedIncome = 0;

            Assert.AreEqual(expectedIncome, coffeeMat.Income);
        }

        [Test]
        public void FillWaterShouldWorkCorrectly()
        {
            string expectedMessage = $"Water tank is filled with 100ml";
            string actualMessage = coffeeMat.FillWaterTank();

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void TryFillFullTankShouldReturnProperMessage()
        {
            coffeeMat.FillWaterTank();

            string expectedMessage = $"Water tank is already full!";
            string actualMessage = coffeeMat.FillWaterTank();

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void AddDrinkShouldWorkCorrectlyAndReturnTrue()
        {
            Assert.IsTrue(coffeeMat.AddDrink("first drink", 1));
        }

        [Test]
        public void AddSameDrinkShouldReturnFalse()
        {
            Assert.IsTrue(coffeeMat.AddDrink("first drink", 1));
            Assert.IsFalse(coffeeMat.AddDrink("first drink", 1));
        }

        [Test]
        public void AddDrinkWithSameNameShouldReturnFalse()
        {
            Assert.IsTrue(coffeeMat.AddDrink("first drink", 1));
            Assert.IsFalse(coffeeMat.AddDrink("first drink", 10));
        }

        [Test]
        public void AddMoreDrinksThanButtonsCountShouldReturnFalse()
        {
            Assert.IsTrue(coffeeMat.AddDrink("first drink", 1));
            Assert.IsTrue(coffeeMat.AddDrink("second drink", 2));
            Assert.IsTrue(coffeeMat.AddDrink("third drink", 3));

            Assert.IsFalse(coffeeMat.AddDrink("drink", 10));
        }

        [Test]
        public void BuyNonExistingDrinkShouldReturnProperMessage()
        {
            coffeeMat.AddDrink("first drink", 1);
            coffeeMat.AddDrink("second drink", 2);
            coffeeMat.AddDrink("third drink", 3);
            coffeeMat.FillWaterTank();

            string expectedMessage = $"coffee is not available!";
            string actualMessage = coffeeMat.BuyDrink("coffee");

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void BuyDrinkWhenNotEnaughWaterShouldReturnProperMessage()
        {
            coffeeMat.AddDrink("first drink", 1);
            coffeeMat.AddDrink("second drink", 2);
            coffeeMat.AddDrink("third drink", 3);
            coffeeMat.FillWaterTank();

            coffeeMat.BuyDrink("first drink");

            string expectedMessage = $"CoffeeMat is out of water!";
            string actualMessage = coffeeMat.BuyDrink("first drink");

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void BuyDrinkShouldWorkCorrectlyAndReturnProperPriceToPay()
        {
            coffeeMat.AddDrink("first drink", 1);
            coffeeMat.AddDrink("second drink", 2);
            coffeeMat.AddDrink("third drink", 3);
            coffeeMat.FillWaterTank();

            string expectedMessage = $"Your bill is 1.00$";
            string actualMessage = coffeeMat.BuyDrink("first drink");

            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [Test]
        public void CollectedIncomeWhenNotBuyShouldBeZero()
        {
            coffeeMat.AddDrink("first drink", 1);
            coffeeMat.AddDrink("second drink", 2);
            coffeeMat.AddDrink("third drink", 3);

            double expectedIncome = 0;

            Assert.AreEqual(expectedIncome, coffeeMat.CollectIncome());
        }

        [Test]
        public void CollectedIncomeShouldBeIncreasedCorrectly()
        {
            coffeeMat.AddDrink("first drink", 1);
            coffeeMat.AddDrink("second drink", 2);
            coffeeMat.AddDrink("third drink", 3);

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("first drink");

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("second drink");

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("third drink");

            double expectedIncome = 6;

            Assert.AreEqual(expectedIncome, coffeeMat.CollectIncome());
        }

        [Test]
        public void IncomeShouldBeIncreasedCorrectly()
        {
            coffeeMat.AddDrink("first drink", 1);
            coffeeMat.AddDrink("second drink", 2);
            coffeeMat.AddDrink("third drink", 3);

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("first drink");

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("second drink");

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("third drink");

            double expectedIncome = 6;

            Assert.AreEqual(expectedIncome, coffeeMat.Income);
        }

        [Test]
        public void IncomeShouldBeSetToZeroAfterCallingCollectedIncome()
        {
            coffeeMat.AddDrink("first drink", 1);
            coffeeMat.AddDrink("second drink", 2);
            coffeeMat.AddDrink("third drink", 3);

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("first drink");

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("second drink");

            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("third drink");

            double expectedIncome = 6;
            double actualIncome = coffeeMat.CollectIncome();

            Assert.AreEqual(expectedIncome, actualIncome);

            double currentExpectedIncome = 0;
            double actualExpectedIncome = coffeeMat.Income;

            Assert.AreEqual(currentExpectedIncome, actualExpectedIncome);
        }
    }
}