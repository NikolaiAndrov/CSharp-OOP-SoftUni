using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private ICollection<IRacer> racers;

        public RacerRepository()
        {
            this.racers = new HashSet<IRacer>();
        }

        public IReadOnlyCollection<IRacer> Models 
            => (IReadOnlyCollection<IRacer>)this.racers;

        public void Add(IRacer model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }

            this.racers.Add(model);
        }

        public IRacer FindBy(string property)
            => this.racers.FirstOrDefault(x => x.Username == property);

        public bool Remove(IRacer model)
            => this.racers.Remove(model);
    }
}
