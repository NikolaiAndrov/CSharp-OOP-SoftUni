using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        Smartphone smartphone;
        Shop shop;
        const int expectedCapacity = 2;
        const int batteryCharge = 100;

        [SetUp] 
        public void SetUp() 
        {
            smartphone = new Smartphone("Nokia", batteryCharge);
            shop = new Shop(expectedCapacity);
        }

        [Test]
        public void TestingConstructorCapacityShouldWorkFine()
        {
            Assert.AreEqual(expectedCapacity, shop.Capacity);
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void TestingConstructorCapacityShouldThrowException(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Shop shop = new Shop(capacity);
            });
        }

        [Test]
        public void TestingCountPropertyShouldWorkFine()
        {
            shop.Add(smartphone);
            int expectedCount = 1;

            Assert.AreEqual(expectedCount, shop.Count);
        }

        [Test]
        public void AddSamePhoneShouldThrowException()
        {
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(smartphone);
            });
        }

        [Test]
        public void AddPhoneWithExistingNameShouldThrowException()
        {
            shop.Add(smartphone);
            Smartphone phone = new Smartphone("Nokia", 50);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(phone);
            });
        }

        [Test]
        public void AddAboveShopCapacityShouldThrowException()
        {
            Smartphone phone1 = new Smartphone("Samsung", 100);
            Smartphone phone2 = new Smartphone("IPhone", 100);

            shop.Add(smartphone);
            shop.Add(phone1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(phone2);
            });
        }

        [Test]
        public void RemovingNonExistingPhoneShouldThrowException()
        {
            shop.Add(smartphone);
            string modelName = "Samsung";

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Remove(modelName);
            });
        }

        [Test]
        public void RemovePhoneShouldWorkFine()
        {
            Smartphone phone1 = new Smartphone("Samsung", 100);

            shop.Add(smartphone);
            shop.Add(phone1);
            shop.Remove("Samsung");

            int expectedCount = 1;

            Assert.AreEqual(expectedCount, shop.Count);
        }

        [Test]
        public void TestPhoneWithNonExistingPhoneShouldThrowException()
        {
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Samsung", 50);
            });
        }

        [TestCase(101)]
        [TestCase(1000)]
        public void TestPhoneWithWithUsageBiggerThanPhoneChargeShouldThrowException(int batteryUsage)
        {
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Nokia", batteryUsage);
            });
        }

        [TestCase(99)]
        [TestCase(100)]
        [TestCase(50)]
        public void TestPhoneShouldWorkFine(int batteryUsage)
        {
            shop.Add(smartphone);
            shop.TestPhone("Nokia", batteryUsage);

            int expectedBatery = batteryCharge - batteryUsage;

            Assert.AreEqual(expectedBatery, smartphone.CurrentBateryCharge);
        }

        [Test]
        public void TestingChargePhoneWithNonExistingPhoneShouldThrowException()
        {
            shop.Add(smartphone);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.ChargePhone("Samsung");
            });
        }

        [TestCase(99)]
        [TestCase(100)]
        [TestCase(50)]
        public void ChargePhoneShouldWorkFine(int batteryUsage)
        {
            shop.Add(smartphone);
            shop.TestPhone("Nokia", batteryUsage);
            shop.ChargePhone("Nokia");

            Assert.AreEqual(batteryCharge, smartphone.CurrentBateryCharge);
        }
    }
}