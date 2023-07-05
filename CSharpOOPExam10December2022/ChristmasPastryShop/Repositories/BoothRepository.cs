using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System.Collections.Generic;

namespace ChristmasPastryShop.Repositories
{
    public class BoothRepository : IRepository<IBooth>
    {
        private ICollection<IBooth> moels;

        public BoothRepository()
        {
            this.moels = new List<IBooth>();
        }

        public IReadOnlyCollection<IBooth> Models 
            => (IReadOnlyCollection<IBooth>)this.moels;

        public void AddModel(IBooth model)
        {
            this.moels.Add(model);
        }
    }
}
