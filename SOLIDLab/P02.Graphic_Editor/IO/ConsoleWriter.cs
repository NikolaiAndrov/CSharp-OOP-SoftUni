namespace P02.Graphic_Editor.IO
{
    using Interfaces;
    using System;

    internal class ConsoleWriter : IWriter
    {
        public void Write(object value)
        {
            Console.Write(value);
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }
    }
}
