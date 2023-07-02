using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private ICollection<ISupplement> supplements;

        public SupplementRepository()
        {
            this.supplements = new List<ISupplement>();
        }

        public void AddNew(ISupplement model)
        {
            this.supplements.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
            => this.supplements.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);

        public IReadOnlyCollection<ISupplement> Models()
            => (IReadOnlyCollection<ISupplement>)this.supplements;

        public bool RemoveByName(string typeName)
        {
            ISupplement supplement = this.supplements.FirstOrDefault(x => x.GetType().Name == typeName);

            if (supplement != null)
            {
                this.supplements.Remove(supplement);
                return true;
            }

            return false;
        }
    }
}
