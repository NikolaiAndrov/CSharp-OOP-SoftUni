namespace WildFarm.Core
{
    using Interfaces;
    using IO.Interfaces;
    using WildFarm.Factory.Interfaces;
    using WildFarm.Models.Interfaces;

    public class Engine : IEngine
    {

        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IFoodFactory foodFactory;
        private readonly IAnimalFactory animalFactory;

        public Engine(IReader reader, IWriter writer, IFoodFactory foodFactory, IAnimalFactory animalFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.foodFactory = foodFactory;
            this.animalFactory = animalFactory;
        }

        public void Start()
        {
            List<IAnimal> animals = new List<IAnimal>();
            string input;

            while ((input = reader.ReadLine()) != "End")
            {
                try
                {
                    string[] animalInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    IAnimal animal = animalFactory.CreateAnimal(animalInfo);
                    writer.WriteLine(animal.ProduceSound());
                    animals.Add(animal);

                    string[] foodInfo = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    IFood food = foodFactory.CreateFood(foodInfo);

                    animal.Eat(food);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }

            foreach (IAnimal animal in animals)
            {
                writer.WriteLine(animal.ToString());
            }
        }
    }
}
