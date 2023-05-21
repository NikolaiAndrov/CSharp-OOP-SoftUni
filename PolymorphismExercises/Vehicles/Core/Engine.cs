
namespace Vehicles.Core
{
    using Interfaces;
    using System.Reflection.PortableExecutable;
    using Vehicles.Factories;
    using Vehicles.Factories.Interfaces;
    using Vehicles.IO.Interfaces;
    using Vehicles.Models.Interfaces;

    public class Engine : IEngine
    {

        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IVehicleFactory factory;

        private Dictionary<string, IVehicle> vehicles;

        private Engine()
        {
            vehicles = new Dictionary<string, IVehicle>();
        }
        public Engine(IReader reader, IWriter writer, IVehicleFactory factory)
            : this()
        {
            this.reader = reader;
            this.writer = writer;
            this.factory = factory;
        }

        public void Start()
        {
            IVehicle car = CreateVehicle();
            IVehicle truck = CreateVehicle();
            IVehicle bus = CreateVehicle();

            vehicles[car.GetType().Name] = car;
            vehicles[truck.GetType().Name] = truck;
            vehicles[bus.GetType().Name] = bus;

            UseVehicles(vehicles);

            PrintVehicles(vehicles);
        }

        private void PrintVehicles(Dictionary<string, IVehicle> vehicles)
        {
            foreach (var vehicle in vehicles)
            {
                writer.WriteLine(vehicle.Value);
            }
        }

        private void UseVehicles(Dictionary<string, IVehicle> vehicles)
        {
            int n = int.Parse(reader.ReadLine());

            for (int i = 0; i < n; i++)
            {
                try
                {
                    string[] commandArgs = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string command = commandArgs[0];
                    string vehicleType = commandArgs[1];

                    if (command == "Drive")
                    {
                        double distance = double.Parse(commandArgs[2]);
                        writer.WriteLine(vehicles[vehicleType].Drive(distance));
                    }
                    else if (command == "Refuel")
                    {
                        double liters = double.Parse(commandArgs[2]);
                        vehicles[vehicleType].Refuel(liters);
                    }
                    else if (command == "DriveEmpty")
                    {
                        double distance = double.Parse(commandArgs[2]);
                        writer.WriteLine(vehicles[vehicleType].Drive(distance, false));
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
                
            }
        }

        private IVehicle CreateVehicle()
        {
            string[] vehicleInfo = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            IVehicleFactory factory = new VehicleFactory();
            IVehicle vehicle = factory.CreateVehicle(vehicleInfo);
            return vehicle;
        }
    }
}
