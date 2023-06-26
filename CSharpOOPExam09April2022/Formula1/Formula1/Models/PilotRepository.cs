using Formula1.Models.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Formula1.Models
{
    public class PilotRepository
    {
        private ICollection<IPilot> models;

        public PilotRepository()
        {
            this.models = new List<IPilot>();
        }

        public IReadOnlyCollection<IPilot> Models
        {
            get { return (IReadOnlyCollection<IPilot>)this.models; }
        }

        public void Add(IPilot pilot)
        {
            this.models.Add(pilot);
        }

        public bool Remove(IPilot pilot) => this.models.Remove(pilot);

        public IPilot FindByName(string fullName)
        {
            IPilot pilot = this.models.FirstOrDefault(x => x.FullName == fullName);

            return pilot;
        }
    }
}
