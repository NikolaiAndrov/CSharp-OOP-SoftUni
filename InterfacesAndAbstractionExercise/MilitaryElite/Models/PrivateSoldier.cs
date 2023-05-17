namespace MilitaryElite.Models
{
    using Interfaces;
    using System.Text;

    public class PrivateSoldier : Soldier, IPrivateSoldier
    {
        public PrivateSoldier(int id, string firstName, string lastName, decimal salary)
            : base(id, firstName, lastName)
        {
            Salary = salary;
        }

        public decimal Salary { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString() + " " + $"Salary: {Salary:f2}");
            return sb.ToString().TrimEnd();
        }
    }
}
