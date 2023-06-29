using NUnit.Framework;
using System.Collections.Generic;

namespace VehicleGarage.Tests
{
    public class Tests
    {
        Garage garage;
        Vehicle vehicle;
        const int defaultCapacity = 3;

        [SetUp]
        public void Setup()
        {
            garage = new Garage(defaultCapacity);
            vehicle = new Vehicle("BMW", "316i", "KH9825AT");
        }

        [Test]
        public void ConstructorShouldInitializeCapacityCorrectly()
        {
            Assert.AreEqual(defaultCapacity, garage.Capacity);
        }

        [Test]
        public void ConstructorShouldInitializeCollectionCorrectly()
        {
            Assert.IsNotNull(garage.Vehicles);
        }

        [Test]
        public void AddMoreVehiclesThanGarageCapacityShouldReturnFalse()
        {
            Vehicle vehicle1 = new Vehicle("some", "some", "123");
            Vehicle vehicle2 = new Vehicle("some2", "some2", "12345");
            Vehicle vehicle3 = new Vehicle("some3", "some3", "1234");
            garage.AddVehicle(vehicle);
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle2);

            Assert.IsFalse(garage.AddVehicle(vehicle3));
        }

        [Test]
        public void AddSameVehicleShouldReturnFalse()
        {
            garage.AddVehicle(vehicle);

            Assert.IsFalse(garage.AddVehicle(vehicle));
        }

        [Test]
        public void AddDifferentVehicleWithSameLicencePlateShouldReturnFalse()
        {
            Vehicle vehicle1 = new Vehicle("some", "some", "KH9825AT");
            garage.AddVehicle(vehicle);

            Assert.IsFalse(garage.AddVehicle(vehicle1));
        }

        [Test]
        public void AddVehichleShouldReturnTrue()
        {
            Assert.IsTrue(garage.AddVehicle(vehicle));
        }

        [Test]
        public void GetCollectionOfVehiclesShoulWorkFine()
        {
            garage.AddVehicle(vehicle);
            
            List<Vehicle> list = garage.Vehicles;
            Vehicle vehicle1 = list[0];

            Assert.AreEqual(vehicle, vehicle1);
        }

        [Test]
        public void ChargeVehicleShouldWorkProperly()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 50, false);

            int expectedChargedVehicles = 1;
            int actualChargedVehicles = garage.ChargeVehicles(50);

            Assert.AreEqual(expectedChargedVehicles, actualChargedVehicles);
        }

        [Test]
        public void ChargeVehicleShouldWorkProperlyAndIncreaseBattery()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 50, false);

            garage.ChargeVehicles(50);

            int expectedBattery = 100;
            int actualBattery = vehicle.BatteryLevel;
            
            Assert.AreEqual(expectedBattery, actualBattery);
        }

        [TestCase(79)]
        [TestCase(50)]
        [TestCase(1)]
        public void ChargeVehicleWithHigherBatterLevelShouldNotCharge(int batteryLevel)
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 20, false);

            garage.ChargeVehicles(batteryLevel);

            int expectedBattery = 80;
            int actualBattery = vehicle.BatteryLevel;

            Assert.AreEqual(expectedBattery, actualBattery);
        }

        [Test]
        public void ChargeVehicleWithHigherBatteryLevelShouldReturnZeroChargedVehicles()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 20, false);

            garage.ChargeVehicles(50);

            int expectedChargedVehicles = 0;
            int actualChargedVehicles = garage.ChargeVehicles(50);

            Assert.AreEqual(expectedChargedVehicles, actualChargedVehicles);
        }

        [Test]
        public void DriveVehicleMethodShouldNotWorkWithDamagedVehicle()
        {
            vehicle.IsDamaged = true;
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 50, false);

            int expectedBatteryLevel = 100;
            int actualBatteryLevel = vehicle.BatteryLevel;

            Assert.AreEqual(expectedBatteryLevel, actualBatteryLevel);
        }

        [TestCase(101)]
        [TestCase(10000)]
        public void DriveVehicleMethodShouldNotWorkWithBatteryDrainageBiggerThanDefaultBatteryLevel(int batteryDrainage)
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", batteryDrainage, false);

            int expectedBatteryLevel = 100;
            int actualBatteryLevel = vehicle.BatteryLevel;

            Assert.AreEqual(expectedBatteryLevel, actualBatteryLevel);
        }

        [Test]
        public void DriveVehicleMethodShouldNotWorkWithBatteryDrainageBiggerThanActualBatteryLevel()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 60, false);
            garage.DriveVehicle("KH9825AT", 60, false);

            int expectedBatteryLevel = 40;
            int actualBatteryLevel = vehicle.BatteryLevel;

            Assert.AreEqual(expectedBatteryLevel, actualBatteryLevel);
        }

        [Test]
        public void DriveVehicleMethodShouldReduceTheBatteryCorrectly()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 60, false);

            int expectedBatteryLevel = 40;
            int actualBatteryLevel = vehicle.BatteryLevel;

            Assert.AreEqual(expectedBatteryLevel, actualBatteryLevel);
        }

        [Test]
        public void DriveVehicleMethodWithAccidentShoudChangeIsDamagedToTrue()
        {
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 60, true);

            Assert.IsTrue(vehicle.IsDamaged);
        }

        [Test]
        public void RepairVehiclesMethodShouldReturnExpectedInformation()
        {
            Vehicle vehicle1 = new Vehicle("BMW", "M6", "KH9106AT");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 50, true);
            garage.DriveVehicle("KH9106AT", 50, false);

            string expectedOutout = "Vehicles repaired: 1";
            string actualOutput = garage.RepairVehicles();

            Assert.AreEqual(expectedOutout, actualOutput);
        }

        [Test]
        public void RepairVehiclesMethodShouldChangeIsDamagedToFalse()
        {
            Vehicle vehicle1 = new Vehicle("BMW", "M6", "KH9106AT");
            garage.AddVehicle(vehicle1);
            garage.AddVehicle(vehicle);
            garage.DriveVehicle("KH9825AT", 50, true);
            garage.DriveVehicle("KH9106AT", 50, false);
            garage.RepairVehicles();

            Assert.IsFalse(vehicle.IsDamaged);
        }
    }
}