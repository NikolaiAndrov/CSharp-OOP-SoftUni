
namespace BorderControl.Core
{
    using BorderControl.IO.Interfaces;
    using BorderControl.Models;
    using BorderControl.Models.Interfaces;
    using Interfaces;
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(IReader reader, IWriter writer) 
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Start()
        {
            var creatures = new List<IBirthable>();
            string input;

            while ((input = reader.ReadLine()) != "End")
            {
                string[] args = input.Split();
                string type = args[0].ToLower();
                string name = args[1];

                if (type == "citizen")
                {
                    int age = int.Parse(args[2]);
                    string id = args[3];
                    string birthDate = args[4];

                    IBirthable creature = new Citizen(name, age, id, birthDate);
                    creatures.Add(creature);
                }
                else if (type == "pet")
                {
                    string birthDate = args[2];
                    IBirthable creature = new Pet(name, birthDate);
                    creatures.Add(creature);
                }
            }

            string birthdateWanted = reader.ReadLine();

            foreach (var creature in creatures)
            {
                if (creature.BirthDate.EndsWith(birthdateWanted))
                {
                    writer.WriteLine(creature.BirthDate);
                }
            }
        }
    }
}
