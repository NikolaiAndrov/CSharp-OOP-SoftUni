using CommandPattern.Core.Contracts;
using System;

namespace CommandPattern.Core
{
    internal class Engine : IEngine
    {
        private readonly ICommandInterpreter command;
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(ICommandInterpreter command)
        {
            this.command = command;
            reader = new ConsoleReader();
            writer = new ConsoleWriter();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string commandOutput = command.Read(reader.ReadLine());
                    writer.WriteLine(commandOutput);
                }
                catch (InvalidOperationException ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
