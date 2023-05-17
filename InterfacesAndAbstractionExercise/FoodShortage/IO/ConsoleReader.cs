
namespace FoodShortage.IO
{
    using Interfaces;
    internal class ConsoleReader : IReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
