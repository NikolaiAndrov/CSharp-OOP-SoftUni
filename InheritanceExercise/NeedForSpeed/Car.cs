namespace NeedForSpeed
{
    public class Car : Vehicle
    {
        private const double DefaultFuelConsumption = 3;
        public Car(int horsePower, double fuel) : base(horsePower, fuel)
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
