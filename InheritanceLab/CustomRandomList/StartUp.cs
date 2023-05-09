namespace CustomRandomList
{
    public class StartUp
    {
        public static void Main()
        {
            RandomList strings = new RandomList();
            strings.Add("1");
            strings.Add("2");
            strings.Add("3");
            strings.Add("4");
            strings.Add("5");

            Console.WriteLine(strings.RandomString());
        }
    }
}