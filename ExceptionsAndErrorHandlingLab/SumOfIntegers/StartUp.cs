namespace SumOfIntegers
{
    public class StartUp
    {
        public static void Main()
        {
            string[] input = Console.ReadLine().Split();

            long sum = 0;

            foreach (string line in input)
            {
                try
                {
                    int result = int.Parse(line);

                    sum += result;
                }
                catch (FormatException)
                {
                    Console.WriteLine($"The element '{line}' is in wrong format!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine($"The element '{line}' is out of range!");
                }
                finally
                {
                    Console.WriteLine($"Element '{line}' processed - current sum: {sum}");
                }
            }

            Console.WriteLine($"The total sum of all integers is: {sum}");
        }
    }
}