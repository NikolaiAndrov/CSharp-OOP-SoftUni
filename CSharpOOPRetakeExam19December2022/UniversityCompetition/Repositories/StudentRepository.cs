using System.Collections.Generic;
using System.Linq;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        ICollection<IStudent> models;

        public StudentRepository()
        {
            this.models = new HashSet<IStudent>();
        }

        public IReadOnlyCollection<IStudent> Models
            => (IReadOnlyCollection<IStudent>)this.models;

        public void AddModel(IStudent model)
        {
            this.models.Add(model);
        }

        public IStudent FindById(int id)
            => this.models.FirstOrDefault(x => x.Id == id);

        public IStudent FindByName(string name)
        {
            string[] fullName = name.Split();
            string firstName = fullName[0];
            string lastName = fullName[1];
            return this.models.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }
    }
}
