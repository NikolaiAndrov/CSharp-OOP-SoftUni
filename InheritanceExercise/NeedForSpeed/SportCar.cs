﻿namespace NeedForSpeed
{
    public class SportCar : Car
    {
        private const double DefaultFuelConsumption = 10;
        public SportCar(int horsePower, double fuel) : base(horsePower, fuel)
        {
        }

        public override double FuelConsumption
        {
            get { return DefaultFuelConsumption; }
            set { base.FuelConsumption = DefaultFuelConsumption; }
        }

        public override void Drive(double kilometers)
        {
            base.Drive(kilometers);
        }
    }
}
