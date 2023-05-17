namespace MilitaryElite.Models
{
    using Interfaces;
    using System.Text;

    public class Engineer : SpecialSoldier, IEngineer
    {
        private readonly ICollection<IRepair> repairs;
        public Engineer(int id, string firstName, string lastName, decimal salary, string corps, ICollection<IRepair> repairs)
            : base(id, firstName, lastName, salary, corps)
        {
            this.repairs = repairs;
        }

        public IReadOnlyCollection<IRepair> Repairs
        {
            get
            {
                return (IReadOnlyCollection<IRepair>)repairs;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("Repairs:");

            foreach (var repair in repairs)
            {
                sb.AppendLine($"  {repair.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
