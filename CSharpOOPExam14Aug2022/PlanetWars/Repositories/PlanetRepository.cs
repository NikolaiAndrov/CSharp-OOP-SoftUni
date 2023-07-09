using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PlanetWars.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private ICollection<IPlanet> models;

        public PlanetRepository()
        {
            this.models = new HashSet<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models 
            => (IReadOnlyCollection<IPlanet>)this.models;

        public void AddItem(IPlanet model)
        {
            this.models.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return this.models.FirstOrDefault(p => p.Name == name);
        }

        public bool RemoveItem(string name)
        {
            IPlanet planet = this.models.FirstOrDefault(p => p.Name == name);
            return this.models.Remove(planet);
        }
    }
}
