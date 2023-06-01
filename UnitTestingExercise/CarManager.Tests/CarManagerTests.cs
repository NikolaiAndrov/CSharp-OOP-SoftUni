namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private string make;
        private string model;
        private double fuelConsumption;
        private double fuelCapacity;
        private Car car;
        [SetUp]
        public void SetUp()
        {
            make = "BMW";
            model = "440i";
            fuelConsumption = 12;
            fuelCapacity = 80;
            car = new Car(make, model, fuelConsumption, fuelCapacity);
        }
        [Test]
        public void TestingConstructorShouldWorkProperly()
        {
            string actualMake = car.Make;
            string actualModel = car.Model;
            double actualFuelConsumption = car.FuelConsumption;
            double actualFuelCapacity = car.FuelCapacity;
            double defaultFuelAmount = 0;

            Assert.AreEqual(make, actualMake);
            Assert.AreEqual(model, actualModel);
            Assert.AreEqual(fuelConsumption, actualFuelConsumption);
            Assert.AreEqual(fuelCapacity, actualFuelCapacity);
            Assert.AreEqual(defaultFuelAmount, car.FuelAmount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestingConstructorMakeEmptyOrNullShouldThrolException(string make)
        {
           
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            }, "Make cannot be null or empty!");
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestingConstructorModelEmptyOrNullShouldThrowException(string model)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            }, "Model cannot be null or empty!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-123)]
        public void TestingConstructorFuelConsumptionZeroOrNegativeThrowException(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            }, "Fuel consumption cannot be zero or negative!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-123)]
        public void TestingConstructorFuelCapacityShouldThrowException(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(make, model, fuelConsumption, fuelCapacity);
            }, "Fuel capacity cannot be zero or negative!");
        }

        [TestCase(1)]
        [TestCase(55)]
        [TestCase(79)]
        public void TestingRefuelMethodShouldWorkProperly(double fuelToRefuel)
        {
            car.Refuel(fuelToRefuel);

            double expectedFuel = fuelToRefuel;
            double actualFuel = car.FuelAmount;

            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [TestCase(100)]
        [TestCase(81)]
        [TestCase(99999)]
        public void AddMoreFuelThanCapacityShouldWorkProperly(double fuelToRefuel)
        {
            car.Refuel(fuelToRefuel);

            double expectedFuel = 80;
            double actualFuel = car.FuelAmount;

            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-99999)]
        public void TryingToRefuelWithZeroOrNegativeNumberShouldThrowException(double fuelToRefuel)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car.Refuel(fuelToRefuel);
            }, "Fuel amount cannot be zero or negative!");
        }

        [TestCase(10)]
        [TestCase(35)]
        public void TestingDriveMethodShouldWorkProperly(double distance)
        {
            car.Refuel(80);
            car.Drive(distance);
            double fuelNeeded = (distance / 100) * fuelConsumption;
            double expectedFuel = 80 - fuelNeeded;
            double actualFuel = car.FuelAmount;
            Assert.AreEqual(expectedFuel, actualFuel);
        }

        [TestCase(1000)]
        [TestCase(952.5)]
        public void TestingDriveMethodShouldThrowException(double distance)
        {
            car.Refuel(80);
            Assert.Throws<InvalidOperationException>(() => 
            {
                car.Drive(distance);
            }, "You don't have enough fuel to drive!");
        }
    }
}