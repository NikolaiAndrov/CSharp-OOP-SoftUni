namespace Raiding.Models
{
    public class Warrior : BaseHero
    {

        private const int Power = 100;

        public Warrior(string name)
            : base(name, Power)
        {
            
        }

        public override string Castability()
        {
            return $"{this.GetType().Name} - {Name} hit for {Power} damage";
        }
    }
}
