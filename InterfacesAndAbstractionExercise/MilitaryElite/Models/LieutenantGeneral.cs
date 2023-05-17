namespace MilitaryElite.Models
{
    using Interfaces;
    using System.Collections.Generic;
    using System.Text;

    public class LieutenantGeneral : PrivateSoldier, ILieutenantGeneral
    {
        private readonly ICollection<IPrivateSoldier> privateSoldiers;
        public LieutenantGeneral(int id, string firstName, string lastName, decimal salary, ICollection<IPrivateSoldier> privateSoldiers) 
            : base(id, firstName, lastName, salary)
        {
            this.privateSoldiers = privateSoldiers;
        }

        public IReadOnlyCollection<IPrivateSoldier> PrivateSoldiers
        {
            get
            {
                return (IReadOnlyCollection<IPrivateSoldier>)privateSoldiers;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine("Privates:");

            foreach (IPrivateSoldier soldier in privateSoldiers)
            {
                sb.AppendLine($"  {soldier.ToString()}");
            }
            return sb.ToString().TrimEnd();   
        }
    }
}
