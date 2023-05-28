using CommandPattern.Core.Contracts;
using System;

namespace CommandPattern.Core
{
    internal class ConsoleWriter : IWriter
    {
        public void Write(object message)
        {
            Console.Write(message.ToString());
        }

        public void WriteLine(object message)
        {
            Console.WriteLine(message.ToString());
        }
    }
}
