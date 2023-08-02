using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double DefaultFuelAvailable = 65;
        private const double DefaultFuelConsumptionPerRace = 7.5;

        public TunedCar(string make, string model, string vIN, int horsePower) 
            : base(make, model, vIN, horsePower, DefaultFuelAvailable, DefaultFuelConsumptionPerRace)
        {
        }

        public override void Drive()
        {
            base.Drive();
            double currentHP = Math.Round(this.HorsePower * 0.97);
            this.HorsePower = (int)currentHP;
        }
    }
}
