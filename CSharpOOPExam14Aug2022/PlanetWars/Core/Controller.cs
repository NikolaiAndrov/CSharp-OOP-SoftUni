using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private IRepository<IPlanet> planets;

        public Controller()
        {
            this.planets = new PlanetRepository();
        }

        public string CreatePlanet(string name, double budget)
        {
            if (this.planets.FindByName(name) != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }

            IPlanet planet = new Planet(name, budget);
            this.planets.AddItem(planet);

            return string.Format(OutputMessages.NewPlanet, planet.Name);
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planet.Name));
            }

            IMilitaryUnit militaryUnit;

            if (unitTypeName == nameof(AnonymousImpactUnit))
            {
                militaryUnit = new AnonymousImpactUnit();
            }
            else if (unitTypeName == nameof(SpaceForces))
            {
                militaryUnit = new SpaceForces();
            }
            else if (unitTypeName == nameof(StormTroopers))
            {
                militaryUnit = new StormTroopers();
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            planet.Spend(militaryUnit.Cost);
            planet.AddUnit(militaryUnit);

            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon weapon;

            if (weaponTypeName == nameof(BioChemicalWeapon))
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(SpaceMissiles))
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);

            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);

            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            }

            double trainingBudget = 1.25;
            planet.Spend(trainingBudget);
            planet.TrainArmy();

            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = this.planets.FindByName(planetOne);
            IPlanet secondPlanet = this.planets.FindByName(planetTwo);
            string resultMessage = string.Empty;

            if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                resultMessage = TakeActionOfWinningPlanet(firstPlanet, secondPlanet);
            }
            else if (secondPlanet.MilitaryPower > firstPlanet.MilitaryPower)
            {
                resultMessage = TakeActionOfWinningPlanet(secondPlanet, firstPlanet);
            }
            else if (firstPlanet.MilitaryPower == secondPlanet.MilitaryPower &&
                firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)) &&
                !secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                resultMessage = TakeActionOfWinningPlanet(firstPlanet, secondPlanet);
            }
            else if (firstPlanet.MilitaryPower == secondPlanet.MilitaryPower &&
                !firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)) &&
                secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                resultMessage = TakeActionOfWinningPlanet(secondPlanet, firstPlanet);
            }
            else if (firstPlanet.MilitaryPower == secondPlanet.MilitaryPower &&
                firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)) &&
                secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                firstPlanet.Spend(firstPlanet.Budget / 2);
                secondPlanet.Spend(secondPlanet.Budget / 2);
                resultMessage = string.Format(OutputMessages.NoWinner);
            }
            else if (firstPlanet.MilitaryPower == secondPlanet.MilitaryPower &&
                !firstPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)) &&
                !secondPlanet.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon)))
            {
                firstPlanet.Spend(firstPlanet.Budget / 2);
                secondPlanet.Spend(secondPlanet.Budget / 2);
                resultMessage = string.Format(OutputMessages.NoWinner);
            }

            return resultMessage;
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in this.planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        private string TakeActionOfWinningPlanet(IPlanet winningPlanet, IPlanet losingPlanet)
        {
            string resultMessage = string.Empty;
            winningPlanet.Spend(winningPlanet.Budget / 2);
            winningPlanet.Profit(losingPlanet.Budget / 2);
            double sumOfLosingPlanet = TakeSumOfLosingPlanet(losingPlanet);
            winningPlanet.Profit(sumOfLosingPlanet);
            this.planets.RemoveItem(losingPlanet.Name);

            resultMessage = string.Format(OutputMessages.WinnigTheWar, winningPlanet.Name, losingPlanet.Name);

            return resultMessage;
        }
        private double TakeSumOfLosingPlanet(IPlanet planet)
        {
            double sum = 0;

            foreach (var army in planet.Army)
            {
                sum += army.Cost;
            }

            foreach (var weapon in planet.Weapons)
            {
                sum += weapon.Price;
            }

            return sum;
        }

    }
}
