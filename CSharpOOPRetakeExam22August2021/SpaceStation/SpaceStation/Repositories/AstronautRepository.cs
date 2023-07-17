using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private ICollection<IAstronaut> models;

        public AstronautRepository()
        {
            this.models = new HashSet<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models 
            => (IReadOnlyCollection<IAstronaut>)this.models;

        public void Add(IAstronaut model)
        {
            this.models.Add(model);
        }

        public IAstronaut FindByName(string name)
            => this.models.FirstOrDefault(a => a.Name == name);

        public bool Remove(IAstronaut model)
            => this.models.Remove(model);
    }
}
