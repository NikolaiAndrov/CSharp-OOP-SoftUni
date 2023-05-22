namespace SquareRoot
{
    public class StartUp
    {
        public static void Main()
        {

			try
			{
				int n = int.Parse(Console.ReadLine());

				if (n < 0)
				{
					throw new ArgumentException("Invalid number.");
                }

				for (int i = 0; i <= n; i++)
				{
					if (Math.Pow(i, 2) == n)
					{
                        Console.WriteLine(i);
						break;
                    }
				}
			}
			catch (ArgumentException ex)
			{
                Console.WriteLine(ex.Message);
            }
			finally
			{
				Console.WriteLine("Goodbye.");
			}
        }
    }
}