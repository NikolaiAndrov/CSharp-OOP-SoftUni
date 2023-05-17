namespace MilitaryElite.Models
{
    using System.Text;
    using Interfaces;
    public abstract class SpecialSoldier : PrivateSoldier, ISpecialisedSoldier
    {
        protected SpecialSoldier(int id, string firstName, string lastName, decimal salary, string corps)
            : base(id, firstName, lastName, salary)
        {
            Corps = corps;
        }

        public string Corps { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($"Corps: {Corps}");
            return sb.ToString().TrimEnd();
        }
    }
}
