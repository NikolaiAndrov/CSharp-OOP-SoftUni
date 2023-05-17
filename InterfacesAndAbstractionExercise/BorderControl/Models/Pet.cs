
namespace BorderControl.Models
{
    using Interfaces;
    public class Pet : Creature, IPet
    {
        public Pet(string name, string birthDate)
            : base(name)
        {
            BirthDate = birthDate;
        }

        public string BirthDate { get; private set; }
    }
}
