
namespace ExplicitInterfaces
{
    using ExplicitInterfaces.Core;
    using ExplicitInterfaces.Core.Interfaces;
    using IO;
    public class StartUp
    {
        public static void Main()
        {
            ConsoleReader consoleReader = new ConsoleReader();
            ConsoleWriter consoleWriter = new ConsoleWriter();

            IEngine engine = new Engine(consoleReader, consoleWriter);
            engine.Start();
        }
    }
}