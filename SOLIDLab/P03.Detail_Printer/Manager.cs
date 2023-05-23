namespace P03.DetailPrinter
{
    using System.Collections.Generic;
    using System.Text;
    public class Manager : Employee
    {
        public Manager(string name, ICollection<string> documents) : base(name)
        {
            this.Documents = new List<string>(documents);
        }

        public IReadOnlyCollection<string> Documents { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());

            foreach (string document in this.Documents)
            {
                sb.AppendLine(document);
            }

            return sb.ToString().TrimEnd();
        }
    }
}
