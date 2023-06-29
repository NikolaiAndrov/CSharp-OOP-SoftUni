using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private ICollection<IRoute> routes;

        public RouteRepository()
        {
            routes = new List<IRoute>();
        }

        public void AddModel(IRoute model)
        {
            this.routes.Add(model);
        }

        public IRoute FindById(string identifier)
        {
            int id = int.Parse(identifier);
            IRoute route = this.routes.FirstOrDefault(r => r.RouteId == id);
            return route;
        }

        public IReadOnlyCollection<IRoute> GetAll()
            => (IReadOnlyCollection<IRoute>)this.routes;

        public bool RemoveById(string identifier)
        {
            int id = int.Parse(identifier);
            IRoute route = this.routes.FirstOrDefault(l => l.RouteId == id);
            return this.routes.Remove(route);
        }
    }
}
