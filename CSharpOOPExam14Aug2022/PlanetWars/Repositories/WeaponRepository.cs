﻿using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private ICollection<IWeapon> models;

        public WeaponRepository()
        {
            this.models = new HashSet<IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models 
            => (IReadOnlyCollection<IWeapon>)this.models;

        public void AddItem(IWeapon model)
        {
            this.models.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            return this.models.FirstOrDefault(w => w.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            IWeapon weapon = this.models.FirstOrDefault(x => x.GetType().Name == name);
            return this.models.Remove(weapon);
        }
    }
}
