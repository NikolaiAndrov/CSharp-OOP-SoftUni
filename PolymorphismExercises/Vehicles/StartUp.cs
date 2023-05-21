namespace Vehicles
{
    using Core;
    using Core.Interfaces;
    using Factories;
    using Factories.Interfaces;
    using IO;
    using IO.Interfaces;

    public class StartUp
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IVehicleFactory factory = new VehicleFactory();

            IEngine engine = new Engine(reader, writer, factory);
            engine.Start();
        }
    }
}