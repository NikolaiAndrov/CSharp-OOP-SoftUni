using MilitaryElite.Models.Interfaces;
using System.Text;

namespace MilitaryElite.Models
{
    public class Commando : SpecialSoldier, ICommando
    {
        private readonly ICollection<IMission> missions;
        public Commando(int id, string firstName, string lastName, decimal salary, string corps, ICollection<IMission> missions)
            : base(id, firstName, lastName, salary, corps)
        {
            this.missions = missions;
        }

        public IReadOnlyCollection<IMission> Missions
        {
            get
            {
                return (IReadOnlyCollection<IMission>)missions;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("Missions:");

            foreach (IMission mission in missions)
            {
                sb.AppendLine($"  {mission.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
