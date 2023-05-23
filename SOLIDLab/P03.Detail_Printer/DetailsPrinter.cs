namespace P03.DetailPrinter
{
    using P03.Detail_Printer.IO.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class DetailsPrinter
    {
        private IList<Employee> employees;
        private readonly IWriter writer;

        public DetailsPrinter(IList<Employee> employees, IWriter writer)
        {
            this.employees = employees;
            this.writer = writer;
        }

        public void PrintDetails()
        {
            for (int i = 0; i < employees.Count; i++)
            {
                writer.WriteLine(employees[i]);

                if (i < employees.Count - 1)
                {
                    writer.WriteLine("");
                }
            }
        }
    }
}
