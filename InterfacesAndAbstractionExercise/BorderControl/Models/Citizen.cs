
namespace BorderControl.Models
{
    using Interfaces;
    public class Citizen : Creature, Icitizen
    {
        public Citizen(string name, int age, string id)
            : base(name, id)
        {
            Age = age;
        }

        public int Age { get; private set; }
    }
}
