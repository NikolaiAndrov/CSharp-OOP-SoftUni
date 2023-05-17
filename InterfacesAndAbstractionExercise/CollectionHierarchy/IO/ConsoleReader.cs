namespace CollectionHierarchy.IO
{
    using Interfaces;
    public class ConsoleReader : IReader
    {
        public string Readline()
        {
            return Console.ReadLine();
        }
    }
}
