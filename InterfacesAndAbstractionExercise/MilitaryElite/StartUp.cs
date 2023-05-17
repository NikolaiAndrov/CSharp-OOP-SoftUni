namespace MilitaryElite
{
    using MilitaryElite.Models;
    using MilitaryElite.IO;
    using MilitaryElite.IO.Interfaces;
    using MilitaryElite.Core.Interfaces;
    using MilitaryElite.Core;

    public class StartUp
    {
        public static void Main()
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            
            IEngine engine = new Engine(reader, writer);
            engine.Start();
        }
    }
}