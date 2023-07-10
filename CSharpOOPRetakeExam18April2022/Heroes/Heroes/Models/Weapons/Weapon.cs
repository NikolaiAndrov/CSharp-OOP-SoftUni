using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;

namespace Heroes.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;

        protected Weapon(string name, int durability)
        {
            this.Name = name;
            this.Durability = durability;
        }

        public string Name
        {
            get { return this.name; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.WeaponTypeNull));
                }

                this.name = value;
            }
        }

        public int Durability
        {
            get { return this.durability; }

            internal set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.DurabilityBelowZero));
                }

                this.durability = value;
            }
        }

        public abstract int DoDamage();
    }
}
