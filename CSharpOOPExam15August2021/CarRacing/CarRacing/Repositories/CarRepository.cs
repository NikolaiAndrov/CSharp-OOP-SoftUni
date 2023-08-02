using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        private ICollection<ICar> cars;

        public CarRepository()
        {
            this.cars = new HashSet<ICar>();
        }

        public IReadOnlyCollection<ICar> Models 
            => (IReadOnlyCollection<ICar>)this.cars;

        public void Add(ICar model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }

            this.cars.Add(model);
        }

        public ICar FindBy(string property)
            => this.cars.FirstOrDefault(x => x.VIN == property);

        public bool Remove(ICar model)
            => this.cars.Remove(model);
    }
}
