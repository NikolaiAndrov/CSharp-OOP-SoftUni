using System;

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
            this.FuelAvailable -= this.FuelConsumptionPerRace;

            double hpRemaining = (double)this.HorsePower;
            hpRemaining = Math.Round(hpRemaining * 0.97);

            this.HorsePower = (int)hpRemaining;
        }
    }
}
