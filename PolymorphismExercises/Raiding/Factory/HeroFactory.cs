namespace Raiding.Factory
{
    using Interfaces;
    using Models.Interfaces;
    using Raiding.Models;

    public class HeroFactory : IHeroFactory
    {
        public IHero CreateHero(string name, string type)
        {
            IHero hero;

            if (type == "Druid")
            {
                hero = new Druid(name);
            }
            else if (type == "Paladin")
            {
                hero = new Paladin(name);
            }
            else if (type == "Rogue")
            {
                hero = new Rogue(name);
            }
            else if (type == "Warrior")
            {
                hero = new Warrior(name);
            }
            else
            {
                throw new ArgumentException("Invalid hero!");
            }

            return hero;
        }
    }
}
