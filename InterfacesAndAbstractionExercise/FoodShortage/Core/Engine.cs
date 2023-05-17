namespace FoodShortage.Core
{
    using FoodShortage.IO.Interfaces;
    using FoodShortage.Models;
    using FoodShortage.Models.Interfaces;
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
            Dictionary<string, IBuyer> people = new Dictionary<string, IBuyer>();
            
            int n = int.Parse(reader.ReadLine());

            for (int i = 0; i < n; i++)
            {
                string[] personInfo = reader.ReadLine().Split();
                string name = personInfo[0];
                int age = int.Parse(personInfo[1]);

                if (personInfo.Length == 3)
                {
                    string group = personInfo[2];
                    IBuyer buyer = new Rebel(name, age, group);
                    people[name] = buyer;
                }
                else if (personInfo.Length == 4)
                {
                    string id = personInfo[2];
                    string birthDate = personInfo[3];
                    IBuyer buyer = new Citizen(name, age, id, birthDate);
                    people[name] = buyer;
                }
            }

            string personName;

            while ((personName = reader.ReadLine()) != "End")
            {
                if (people.ContainsKey(personName))
                {
                    people[personName].BuyFood();
                }
            }

            int sum = 0;

            foreach (var person in people)
            {
                sum += person.Value.Food;
            }

            writer.WriteLine(sum.ToString());
        }
    }
}
