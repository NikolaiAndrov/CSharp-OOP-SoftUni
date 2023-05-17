
namespace BorderControl.Models
{
    using Interfaces;
    public class Citizen : Creature, Icitizen, IBirthable
    {
        public Citizen(string name, int age, string id, string birthdate)
            : base(name)
        {
            Age = age;
            Id = id;
            BirthDate = birthdate;
        }

        public int Age { get; private set; }

        public string Id { get; private set; }

        public string BirthDate { get ; private set; }
    }
}
