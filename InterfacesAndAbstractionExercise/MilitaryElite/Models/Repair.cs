namespace MilitaryElite.Models
{
    using Interfaces;
    using System.Text;

    public class Repair : IRepair
    {
        public Repair(string name, int hoursWorked)
        {
            Name = name;
            HoursWorked = hoursWorked;
        }

        public string Name { get; private set; }

        public int HoursWorked { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Part Name: {Name} Hours Worked: {HoursWorked}");
            return sb.ToString().TrimEnd();
        }
    }
}
