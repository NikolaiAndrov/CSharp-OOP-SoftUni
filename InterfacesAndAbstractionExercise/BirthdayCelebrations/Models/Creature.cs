
namespace BorderControl.Models
{
    using Interfaces;
    public abstract class Creature : ICreature
    {
        protected Creature(string name )
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
