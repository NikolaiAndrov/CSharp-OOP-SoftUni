namespace NeedForSpeed
{
    public abstract class Vehicle
    {
        private const double DefaultFuelConsumption = 1.25;
        public Vehicle(int horsePower, double fuel) 
        { 
            this.HorsePower = horsePower;
            this.Fuel = fuel;
            this.FuelConsumption = DefaultFuelConsumption;
        }

        public virtual double FuelConsumption  { get; set; }
        public double Fuel { get; set; }
        public int HorsePower  { get; set; }

        public virtual void Drive(double kilometers)
        {
            Fuel -= (FuelConsumption * kilometers);
        }
    }
}
