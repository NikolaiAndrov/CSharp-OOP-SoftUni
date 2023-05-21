namespace Raiding.Models
{
    using Interfaces;
    public abstract class BaseHero : IHero
    {
        protected BaseHero(string name, int power) 
        {
            Name = name;
            Power = power;
        }
        public string Name { get; private set; }

        public int Power { get; private set; }

        public virtual string Castability()
        {
            return $"{this.GetType().Name} - {Name} healed for {Power}";
        }
    }
}
