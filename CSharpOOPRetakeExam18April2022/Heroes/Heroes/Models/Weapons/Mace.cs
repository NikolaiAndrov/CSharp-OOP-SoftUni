namespace Heroes.Models.Weapons
{
    public class Mace : Weapon
    {
        public Mace(string name, int durability)
            : base(name, durability)
        {
        }

        public override int DoDamage()
        {
            int damage = 0;

            if (this.Durability == 0)
            {
                return damage;
            }

            this.Durability--;

            damage = 25;

            return damage;
        }
    }
}
