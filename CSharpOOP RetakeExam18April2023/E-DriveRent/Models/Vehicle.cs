using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;

namespace EDriveRent.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string brand;
        private string model;
        private double maxMileage;
        private string licensePlateNumber;
        private int batteryLevel;

        public  Vehicle(string brand, string model, double maxMileage, string licensePlateNumber)
        {
            this.Brand = brand;
            this.Model = model;
            this.MaxMileage = maxMileage;
            this.LicensePlateNumber = licensePlateNumber;
            this.BatteryLevel = 100;
            this.IsDamaged = false;
        }

        public string Brand
        {
            get { return this.brand; } 

            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BrandNull);
                }

                this.brand = value;
            }
        }

        public string Model
        {
            get { return this.model; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelNull);
                }

                this.model = value;
            }
        }

        public double MaxMileage
        {
            get { return this.maxMileage; }
            private set { this.maxMileage = value; }
        }

        public string LicensePlateNumber
        {
            get { return this.licensePlateNumber; }

            private set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.LicenceNumberRequired);
                }

                this.licensePlateNumber = value;
            }
        }

        public int BatteryLevel
        {
            get {return this.batteryLevel; }

            private set { this.batteryLevel = value; }
        }

        public bool IsDamaged { get; private set; }

        public void Drive(double mileage)
        {
            double decreasePercentage = mileage / this.MaxMileage;

            if (this.GetType().Name == "CargoVan")
            {
                decreasePercentage += 0.05;
            }

            int roundedPercentage = (int)Math.Round(decreasePercentage * 100);

            this.BatteryLevel -= roundedPercentage;
        }

        public void Recharge()
        {
            this.BatteryLevel = 100;
        }

        public void ChangeStatus()
        {
            this.IsDamaged = !this.IsDamaged;
        }

        public override string ToString()
        {
            string damaged = this.IsDamaged ? "damaged" : "OK";

            return $"{this.Brand} {this.Model} License plate: {this.LicensePlateNumber} Battery: {this.BatteryLevel}% Status: {damaged}";
        }
    }
}
