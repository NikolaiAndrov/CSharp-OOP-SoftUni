namespace Raiding.Core
{
    using Interfaces;
    using Raiding.Factory;
    using Raiding.Factory.Interfaces;
    using Raiding.IO.Interfaces;
    using Raiding.Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly IHeroFactory factory;

        public Engine(IReader reader, IWriter writer, IHeroFactory factory)
        {
            this.reader = reader;
            this.writer = writer;
            this.factory = factory;
        }

        public void Run()
        {
            int numberOfHeroes = int.Parse(reader.ReadLine());
            ICollection<IHero> heroes = new List<IHero>();


            while (heroes.Count < numberOfHeroes)
            {
                try
                {
                    string name = reader.ReadLine();
                    string type = reader.ReadLine();
                    IHero hero = CreateHero(name, type);
                    heroes.Add(hero);
                }
                catch (Exception ex)
                {
                    writer.WrteLine(ex.Message);
                }
            }
            

            int bossPower = int.Parse(reader.ReadLine());
            int totalPower = 0;

            foreach (var hero in heroes)
            {
                totalPower += hero.Power;
                writer.WrteLine(hero.Castability());
            }

            if (totalPower >= bossPower)
            {
                writer.WrteLine("Victory!");
            }
            else
            {
                writer.WrteLine("Defeat...");
            }
        }

        private IHero CreateHero(string name, string type)
        {
            
            IHeroFactory factory = new HeroFactory();
            IHero hero = factory.CreateHero(name, type);
            return hero;
        }
    }
}
