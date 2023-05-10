using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            RaceMotorcycle raceMotorcycle = new RaceMotorcycle(110, 100);
            raceMotorcycle.Drive(1);
            Console.WriteLine(raceMotorcycle.Fuel);

            CrossMotorcycle crossMotorcycle = new CrossMotorcycle(110, 100);
            crossMotorcycle.Drive(1);
            Console.WriteLine(crossMotorcycle.Fuel);
        }
    }
}
