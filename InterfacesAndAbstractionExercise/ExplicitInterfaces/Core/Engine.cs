namespace ExplicitInterfaces.Core
{
    using ExplicitInterfaces.IO.Interfaces;
    using ExplicitInterfaces.Models;
    using ExplicitInterfaces.Models.Interfaces;
    using Interfaces;
    internal class Engine : IEngine
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
            ICollection<Citizen> citizens = new List<Citizen>();
            string input;

            while ((input = reader.ReadLine()) != "End")
            {
                string[] citizenInfo = input.Split();
                string name = citizenInfo[0];
                string country = citizenInfo[1];
                int age = int.Parse(citizenInfo[2]);
                Citizen citizen = new Citizen(name, country, age);
                citizens.Add(citizen);
            }

            foreach (var citizen in citizens)
            {
                writer.WriteLine(citizen.GetName());
                IResident resident = citizen;
                writer.WriteLine(resident.GetName());
            }
        }
    }
}
