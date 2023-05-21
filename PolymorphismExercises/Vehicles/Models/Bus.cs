
namespace Vehicles.Models
{
   
    public class Bus : Vehicle
    {
        private const double AdditionalFuelconsumption = 1.4;

        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, AdditionalFuelconsumption, tankCapacity)
        {
        }

        
    }
}
