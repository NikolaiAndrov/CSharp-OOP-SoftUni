using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        [TestFixture]
        public class RepairsShopTests
        {
            Garage garage;

            Car car1;
            Car car2;
            Car car3;

            [SetUp]
            public void SetUp()
            {
                garage = new Garage("Garage", 3);

                car1 = new Car("car1", 1);
                car2 = new Car("car2", 2);
                car3 = new Car("car3", 3);
            }

            [TestCase(null)]
            [TestCase("")]
            public void GarageNameNullOrEmptyShouldThrow(string name)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    Garage garage = new Garage(name, 3);
                });
            }

            [TestCase("Garage")]
            [TestCase("Garage Garage Garage Garage Garage Garage Garage Garage")]
            [TestCase("G")]
            public void ConstructorShoudSetGarageNameProperly(string name)
            {
                Garage garage = new Garage(name, 3);
                Assert.AreEqual(name , garage.Name);
            }

            [TestCase(0)]
            [TestCase(-1)]
            [TestCase(-100)]
            public void ZeroOrNegativeMechanicsAvailableShouldThrow(int mechanicsAvailable)
            {
                Assert.Throws<ArgumentException>(() => 
                {
                    Garage garage = new Garage("Garage", mechanicsAvailable);
                });
            }

            [TestCase(1)]
            [TestCase(10)]
            [TestCase(10000)]
            public void ConstructorShouldSetMechanicsAvailableCorrectly(int mechanicsAvailable)
            {
                Garage garage = new Garage("Garage", mechanicsAvailable);

                Assert.AreEqual(mechanicsAvailable, garage.MechanicsAvailable);
            }

            [Test]
            public void AddMethodShouldWorkCOrrectly()
            {
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                int expectedCount = 3;

                Assert.AreEqual(expectedCount, garage.CarsInGarage);
            }

            [Test]
            public void AddMoreCarsThanMechanicsShouldThrow()
            {
                Car car = new Car("Last Model", 0);

                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(car);
                });
            }

            [Test]
            public void FixNonExistingCarShouldThrow()
            {
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar("Last Model");
                });
            }

            [Test]
            public void FixCarShouldWorkCorrectly()
            {
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                Car fixedCar = garage.FixCar("car3");

                Assert.IsTrue(fixedCar.IsFixed);
            }

            [Test]
            public void RemoveFixedCarWhenNonFixedCarsAvailableShouldThrow()
            {
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.RemoveFixedCar();
                });
            }

            [Test]
            public void RemoveFixedCarShoudReturnCountOfFixedCars()
            {
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                garage.FixCar("car1");
                garage.FixCar("car2");

                int expectedCount = 2;
                int actualCount = garage.RemoveFixedCar();

                Assert.AreEqual(expectedCount, actualCount);
            }

            [Test]
            public void GarageReportShoudWorkProperly()
            {
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                garage.FixCar("car1");
                garage.FixCar("car2");

                string expectedMessage = "There are 1 which are not fixed: car3.";
                string actualMessage = garage.Report();
                
                Assert.AreEqual(expectedMessage, actualMessage);
            }
        }
    }
}