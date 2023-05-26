namespace Stealer
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            Spy spy = new Spy();
            string result = spy.StealFieldInfo("Stealer.Hacker", "username", "password");
            Console.WriteLine(result);
            Console.WriteLine();

            string result2 = spy.AnalyzeAccessModifiers("Stealer.Hacker");
            Console.WriteLine(result2);
            Console.WriteLine();

            string result3 = spy.RevealPrivateMethods("Stealer.Hacker");
            Console.WriteLine(result3);
            Console.WriteLine();

            string result4 = spy.CollectGettersAndSetters("Stealer.Hacker");
            Console.WriteLine(result4);
        }
    }
}