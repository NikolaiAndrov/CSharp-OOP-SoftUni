
namespace BorderControl.Models
{
    using Interfaces;
    public abstract class Creature : ICreature
    {
        protected Creature(string name, string id)
        {
            Name = name;
            Id = id;
        }

        public string Name { get; private set; }

        public string Id { get; private set; }
    }
}
