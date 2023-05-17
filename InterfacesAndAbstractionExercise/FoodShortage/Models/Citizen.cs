
namespace FoodShortage.Models
{
    using Interfaces;
    public class Citizen : ICitizen
    {
        public Citizen(string name, int age, string id, string birthDate)
        {
            Name = name;
            Age = age;
            ID = id;
            BirthDate = birthDate;
            Food = 0;
        }

        public string Name { get; private set; }

        public int Age { get; private set; }

        public string ID { get; private set; }

        public string BirthDate { get; private set; }

        public int Food { get; private set; }

        public void BuyFood()
        {
            Food += 10;
        }
    }
}
