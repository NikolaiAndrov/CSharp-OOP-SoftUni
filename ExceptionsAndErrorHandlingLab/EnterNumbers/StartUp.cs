namespace EnterNumbers
{
    public class StartUp
    {
        public static void Main()
        {
            List<int> numbers = new List<int>();
            int start = 1;
            int end = 100;

            while (numbers.Count < 10)
			{
                try
                {
                    string input = Console.ReadLine();

                    bool isParsed = int.TryParse(input, out int result);

                    if (!isParsed)
                    {
                        throw new InvalidOperationException("Invalid Number!");
                    }

                    if (result <= start || result >= end)
                    {
                        throw new ArgumentException($"Your number is not in range {start} - {end}!");
                    }

                    numbers.Add(result);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}