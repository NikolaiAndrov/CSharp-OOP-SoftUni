namespace Raiding
{
    using Factory;
    using Factory.Interfaces;
    using IO;
    using IO.Interfaces;
    using Core;
    using Core.Interfaces;

    public class StartUp
    {
        public static void Main()
        { 
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();
            IHeroFactory factory = new HeroFactory();

            IEngine engine = new Engine(reader, writer, factory);
            engine.Run();
        }
    }   
}