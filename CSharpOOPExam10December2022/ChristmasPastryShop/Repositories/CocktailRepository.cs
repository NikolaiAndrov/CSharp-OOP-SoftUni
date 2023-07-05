using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System.Collections.Generic;

namespace ChristmasPastryShop.Repositories
{
    public class CocktailRepository : IRepository<ICocktail>
    {
        private ICollection<ICocktail> models;

        public CocktailRepository()
        {
            this.models = new List<ICocktail>();
        }

        public IReadOnlyCollection<ICocktail> Models 
            => (IReadOnlyCollection<ICocktail>)this.models;

        public void AddModel(ICocktail model)
        {
            this.models.Add(model);
        }
    }
}
