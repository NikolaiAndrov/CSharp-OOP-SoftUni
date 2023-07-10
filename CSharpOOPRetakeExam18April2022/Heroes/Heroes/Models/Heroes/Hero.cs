using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;

namespace Heroes.Models.Heroes
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        protected Hero(string name, int health, int armour)
        {
            this.Name = name;
            this.Health = health;
            this.Armour = armour;
        }

        public string Name
        {
            get { return this.name; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.HeroNameNull));
                }

                this.name = value;
            }
        }

        public int Health
        {
            get { return this.health; }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.HeroHealthBelowZero));
                }

                this.health = value;
            }
        }

        public int Armour
        {
            get { return this.armour; }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.HeroArmourBelowZero));
                }

                this.armour = value;
            }
        }

        public IWeapon Weapon
        {
            get { return this.weapon; }

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.WeaponNull));
                }

                this.weapon = value;
            }
        }

        public bool IsAlive 
            => this.Health > 0;

        public void AddWeapon(IWeapon weapon)
        {
            this.Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            if (this.Armour - points < 0)
            {
                int diff = Math.Abs(this.Armour - points);
                this.Armour = 0;

                if (this.Health - diff < 0)
                {
                    this.Health = 0;
                }
                else
                {
                    this.Health -= diff;
                }
            }
            else
            {
                this.Armour -= points;
            }
        }
    }
}
