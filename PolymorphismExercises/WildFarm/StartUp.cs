namespace WildFarm
{
    using Models;
    using Models.Interfaces;
    using Factory;
    using Factory.Interfaces;
    using IO;
    using IO.Interfaces;
    using WildFarm.Core.Interfaces;
    using WildFarm.Core;

    public class StartUp
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();                
            IWriter writer = new ConsoleWriter();
            IFoodFactory foodFactory = new FoodFactory();
            IAnimalFactory animalFactory = new AnimalFactory();

            IEngine engine = new Engine(reader, writer, foodFactory, animalFactory);
            engine.Start();
        }
    }
}