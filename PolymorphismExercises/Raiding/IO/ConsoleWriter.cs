namespace Raiding.IO
{
    using Interfaces;
    public class ConsoleWriter : IWriter
    {
        public void Write(string value)
        {
            Console.Write(value);
        }

        public void WrteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
