namespace P01.Stream_Progress
{
    using Core;
    using Core.Interfaces;
    using IO;
    using IO.Interfaces;
    using Models;
    public class StartUp
    {
        public static void Main()
        {
            IWriter writer = new ConsoleWriter();
            File file = new File("Music", 3, 320);
            Music music = new Music("Petra", "Hishtna hiena", 4, 320);
            IEngine engine = new Engine(writer, file);
            engine.Run();
        }
    }
}
