using Facade.Models;

namespace Facade
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var car = new CarBuilderFacade()
                .Info
                    .WithType("BMW")
                    .WithColor("Black")
                    .WithNumberOfDoors(5)
                .Built
                    .InCity("Leipzig")
                    .AtAdress("Some address 254")
                    .Build();

            Console.WriteLine(car);
        }
    }
}