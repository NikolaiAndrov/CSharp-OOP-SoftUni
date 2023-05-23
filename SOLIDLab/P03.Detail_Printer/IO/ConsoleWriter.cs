namespace P03.Detail_Printer.IO
{
    using Interfaces;
    using System;
    internal class ConsoleWriter : IWriter
    {
        public void Write(object value)
        {
            Console.Write(value.ToString());
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value.ToString());
        }
    }
}
