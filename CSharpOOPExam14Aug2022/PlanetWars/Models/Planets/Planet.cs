using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        private IRepository<IMilitaryUnit> units;
        private IRepository<IWeapon> weapons;

        public Planet(string name, double budget)
        {
            this.Name = name;
            this.Budget = budget;
            this.units = new UnitRepository();
            this.weapons = new WeaponRepository();
        }

        public string Name
        {
            get { return this.name; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPlanetName));
                }

                this.name = value;
            }
        }

        public double Budget
        {
            get { return this.budget; }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidBudgetAmount));
                }

                this.budget = value;
            }
        }

        public double MilitaryPower
        {
            get { return this.GetMilitaryPower(); }

        }

        public IReadOnlyCollection<IMilitaryUnit> Army
            => this.units.Models;

        public IReadOnlyCollection<IWeapon> Weapons 
            => this.weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            this.units.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.AddItem(weapon);
        }

        public void TrainArmy()
        {
            foreach (var army in this.units.Models)
            {
                army.IncreaseEndurance();
            }
        }

        public void Spend(double amount)
        {
            if (this.Budget - amount < 0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnsufficientBudget));
            }

            this.Budget -= amount;
        }

        public void Profit(double amount)
        {
            this.Budget += amount;
        }

        public string PlanetInfo()
        {
            string militaryUnits = GetMilitaryUnitsAsString();
            string allWeapons = GetWeaponsAsString();
            

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");
            sb.AppendLine($"--Forces: {militaryUnits}");
            sb.AppendLine($"--Combat equipment: {allWeapons}");
            sb.Append($"--Military Power: {this.MilitaryPower}");
            
            return sb.ToString();
        }

        private string GetWeaponsAsString()
        {
            IWeapon[] allWeapons = this.weapons.Models.ToArray();
            StringBuilder sb = new StringBuilder();

            if (allWeapons.Length > 0)
            {
                for (int i = 0; i < allWeapons.Length; i++)
                {
                    if (i == allWeapons.Length - 1)
                    {
                        sb.Append(allWeapons[i].GetType().Name);
                    }
                    else
                    {
                        sb.Append($"{allWeapons[i].GetType().Name}, ");
                    }
                }
            }
            else
            {
                sb.Append("No weapons");
            }

            return sb.ToString();
        }

        private string GetMilitaryUnitsAsString()
        {
            IMilitaryUnit[] militaryUnits = this.units.Models.ToArray();
            StringBuilder sb = new StringBuilder();

            if (militaryUnits.Length > 0)
            {
                for (int i = 0; i < militaryUnits.Length; i++)
                {
                    if (i == militaryUnits.Length - 1)
                    {
                        sb.Append(militaryUnits[i].GetType().Name);
                    }
                    else
                    {
                        sb.Append($"{militaryUnits[i].GetType().Name}, ");
                    }
                }
            }
            else
            {
                sb.Append("No units");
            }

            return sb.ToString();
        }

        private double GetMilitaryPower()
        {
            double totalAmount = 0;

            foreach (var army in this.units.Models)
            {
                totalAmount += army.EnduranceLevel;
            }

            foreach (var weapon in this.weapons.Models)
            {
                totalAmount += weapon.DestructionLevel;
            }

            if (this.units.Models.Any(a => a.GetType().Name == "AnonymousImpactUnit"))
            {
                totalAmount *= 1.3;
            }

            if (this.weapons.Models.Any(w => w.GetType().Name == "NuclearWeapon"))
            {
                totalAmount *= 1.45;
            }

            return Math.Round(totalAmount, 3);
        }
    }
}
