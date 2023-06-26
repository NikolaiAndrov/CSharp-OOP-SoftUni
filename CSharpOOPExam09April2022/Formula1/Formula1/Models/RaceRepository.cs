using Formula1.Models.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Formula1.Models
{
    public class RaceRepository
    {
        private ICollection<IRace> models;

        public RaceRepository()
        {
            this.models = new List<IRace>();
        }

        public IReadOnlyCollection<IRace> Models
        {
            get { return (IReadOnlyCollection<IRace>)this.models; }
        }

        public void Add(IRace race)
        {
            this.models.Add(race);
        }

        public bool Remove(IRace race) => this.models.Remove(race);

        public IRace FindByName(string raceName)
        {
            IRace race = this.models.FirstOrDefault(x => x.RaceName == raceName);
            return race;
        }
    }
}
