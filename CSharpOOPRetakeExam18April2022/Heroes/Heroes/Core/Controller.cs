using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private IRepository<IHero> heroes;
        private IRepository<IWeapon> weapons;

        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }

        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = this.heroes.FindByName(heroName);
            IWeapon weapon = this.weapons.FindByName(weaponName);

            if (hero == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist, heroName));
            }

            if (weapon == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            if (hero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, hero.Name));
            }

            hero.AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAddedToHero, hero.Name, weapon.GetType().Name.ToLower());
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (this.heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyExist, name));
            }

            IHero hero;
            string outputMessage = string.Empty;
            if (type == nameof(Barbarian))
            {
                hero = new Barbarian(name, health, armour);
                outputMessage = string.Format(OutputMessages.SuccessfullyAddedBarbarian, hero.Name);
            }
            else if (type == nameof(Knight))
            {
                hero = new Knight(name, health, armour);
                outputMessage = string.Format(OutputMessages.SuccessfullyAddedKnight, hero.Name);
            }
            else
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroTypeIsInvalid));
            }

            this.heroes.Add(hero);
            return outputMessage;
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if (this.weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponAlreadyExists, name));
            }

            IWeapon weapon;

            if (type == nameof(Claymore))
            {
                weapon = new Claymore(name, durability);
            }
            else if (type == nameof(Mace))
            {
                weapon = new Mace(name, durability);
            }
            else
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponTypeIsInvalid));
            }

            this.weapons.Add(weapon);
            return string.Format(OutputMessages.WeaponAddedSuccessfully, weapon.GetType().Name.ToLower(), weapon.Name);
        }

        public string StartBattle()
        {
            IMap map = new Map();

            ICollection<IHero> heroesReadyToFight = this.heroes.Models
                .Where(h => h.IsAlive && h.Weapon != null).ToArray();

            string outputMessage = map.Fight(heroesReadyToFight);

            return outputMessage;
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var hero in this.heroes.Models
                .OrderBy(h => h.GetType().Name)
                .ThenByDescending(h => h.Health)
                .ThenBy(h => h.Name))
            {
                string weaponName = hero.Weapon != null ? hero.Weapon.Name : "Unarmed";

                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                sb.AppendLine($"--Weapon: {weaponName}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
