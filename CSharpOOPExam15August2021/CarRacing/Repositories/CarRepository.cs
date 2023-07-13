﻿using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        private ICollection<ICar> models;

        public CarRepository()
        {
            this.models = new HashSet<ICar>();
        }

        public IReadOnlyCollection<ICar> Models
            => (IReadOnlyCollection<ICar>)this.models; 

        public void Add(ICar model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }

            this.models.Add(model);
        }

        public ICar FindBy(string property)
            => this.models.FirstOrDefault(c => c.VIN == property);

        public bool Remove(ICar model)
            => this.models.Remove(model);
    }
}
