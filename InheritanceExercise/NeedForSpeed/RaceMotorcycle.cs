namespace NeedForSpeed
{
    public class RaceMotorcycle : Motorcycle
    {
        private const double DefaultFuelConsumption = 8;
        public RaceMotorcycle(int horsePower, double fuel) : base(horsePower, fuel)
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
