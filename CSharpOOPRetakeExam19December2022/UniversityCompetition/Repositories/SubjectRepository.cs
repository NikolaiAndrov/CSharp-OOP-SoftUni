using System.Collections.Generic;
using System.Linq;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class SubjectRepository : IRepository<ISubject>
    {
        ICollection<ISubject> models;

        public SubjectRepository()
        {
            this.models = new HashSet<ISubject>();
        }

        public IReadOnlyCollection<ISubject> Models
            => (IReadOnlyCollection<ISubject>)this.models;

        public void AddModel(ISubject model)
        {
            this.models.Add(model);
        }

        public ISubject FindById(int id)
            => this.models.FirstOrDefault(x => x.Id == id);

        public ISubject FindByName(string name)
            => this.models.FirstOrDefault(x => x.Name == name); 
    }
}
