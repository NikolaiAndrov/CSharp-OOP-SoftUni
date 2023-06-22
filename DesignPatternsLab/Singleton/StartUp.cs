namespace Singleton
{
    using Models;
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = SingletonDataContainer.Instance;
        }
    }
}