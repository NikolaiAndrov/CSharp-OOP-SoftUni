using P03.Detail_Printer.IO;
using P03.Detail_Printer.IO.Interfaces;
using System.Collections.Generic;

namespace P03.DetailPrinter
{
    public class StartUp
    {
        public static void Main()
        {
            IWriter writer = new ConsoleWriter();

            Employee employee = new Employee("Niki");
            Employee manager = new Manager("Pesho", new List<string> { "ID Card", "Passport"});
            List<Employee> employees = new List<Employee>() { employee, manager};

            DetailsPrinter detailsPrinter = new DetailsPrinter(employees, writer);
            detailsPrinter.PrintDetails();
        }
    }
}
