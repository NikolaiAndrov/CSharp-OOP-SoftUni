using CommandPattern.Core.Contracts;
using System;

namespace CommandPattern.Core
{
    internal class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
