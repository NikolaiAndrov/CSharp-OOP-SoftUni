namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double AdditionalFuelconsumption = 0.9;
        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, AdditionalFuelconsumption, tankCapacity)
        {
        }
    }
}
