namespace Raiding.Models
{
    public class Rogue : BaseHero
    {
        private const int Power = 80;
        public Rogue(string name) 
            : base(name, Power)
        {
        }

        public override string Castability()
        {
            return $"{this.GetType().Name} - {Name} hit for {Power} damage";
        }
    }
}
