
namespace Vehicles.Factories
{
    using Interfaces;
    using Models.Interfaces;
    using Vehicles.Models;

    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle CreateVehicle(string[] args)
        {
            string type = args[0];
            double fuelQuantity = double.Parse(args[1]);
            double fuelConsumption = double.Parse(args[2]);
            double tankCapacity = double.Parse(args[3]);

            IVehicle vehicle;

            if (type == "Car")
            {
                vehicle = new Car(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else if (type == "Truck")
            {
                vehicle = new Truck(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else if (type == "Bus")
            {
                vehicle = new Bus(fuelQuantity, fuelConsumption, tankCapacity);
            }
            else
            {
                throw new ArgumentException("Vehicle type doesn not exist");
            }

            return vehicle;
        }
    }
}
