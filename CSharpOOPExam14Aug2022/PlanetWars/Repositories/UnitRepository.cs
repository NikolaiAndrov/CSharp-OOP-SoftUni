using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private ICollection<IMilitaryUnit> models;

        public UnitRepository()
        {
            this.models = new HashSet<IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models 
            => (IReadOnlyCollection<IMilitaryUnit>)this.models;

        public void AddItem(IMilitaryUnit model)
        {
            this.models.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
            => this.models.FirstOrDefault(u => u.GetType().Name == name);

        public bool RemoveItem(string name)
        {
            IMilitaryUnit militaryUnit = this.models.FirstOrDefault(u => u.GetType().Name == name);
            return this.models.Remove(militaryUnit);
        }
    }
}
