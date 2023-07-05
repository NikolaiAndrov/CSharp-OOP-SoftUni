using System.Collections.Generic;
using System.Linq;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class UniversityRepository : IRepository<IUniversity>
    {
        private ICollection<IUniversity> models;

        public UniversityRepository()
        {
            this.models = new HashSet<IUniversity>();
        }

        public IReadOnlyCollection<IUniversity> Models
            => (IReadOnlyCollection<IUniversity>)this.models;

        public void AddModel(IUniversity model)
        {
            this.models.Add(model);
        }

        public IUniversity FindById(int id)
            => this.models.FirstOrDefault(x => x.Id == id);

        public IUniversity FindByName(string name)
            => this.models.FirstOrDefault(x => x.Name == name);
    }
}
