
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
        private  ICollection<ICreature> creatures;

        public Engine(IReader reader, IWriter writer) 
        {
            this.reader = reader;
            this.writer = writer;
        }
        public void Start()
        {
            creatures = new List<ICreature>();
            string input;

            while ((input = reader.ReadLine()) != "End")
            {
                string[] args = input.Split();
                string name = args[0];

                if (args.Length == 3)
                {
                    int age = int.Parse(args[1]);
                    string id = args[2];
                    ICreature creature = new Citizen(name, age, id);
                    creatures.Add(creature);
                }
                else if (args.Length == 2)
                {
                    string id = args[1];
                    ICreature creature = new Robot(name, id);
                    creatures.Add(creature);
                }
            }

            string idWanted = reader.ReadLine();

            foreach (var creature in creatures)
            {
                if (creature.Id.EndsWith(idWanted))
                {
                    writer.WriteLine(creature.Id);
                }
            }
        }
    }
}
