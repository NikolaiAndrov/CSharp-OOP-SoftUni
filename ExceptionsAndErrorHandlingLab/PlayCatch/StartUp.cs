namespace PlayCatch
{
    public class StartUp
    {
        public static void Main()
        {
            int[] nums = Console.ReadLine()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            int exceptionsCount = 0;

            while (exceptionsCount < 3)
            {
                try
                {
                    string[] commandInfo = Console.ReadLine()
                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    string command = commandInfo[0];

                    if (command == "Replace")
                    {
                        int index = int.Parse(commandInfo[1]);
                        int element = int.Parse(commandInfo[2]);
                        nums[index] = element;
                    }
                    else if (command == "Print")
                    {
                        int startIndex = int.Parse(commandInfo[1]);
                        int endIndex = int.Parse(commandInfo[2]);

                        if (startIndex < 0 || startIndex >= nums.Length
                            || endIndex < 0 || endIndex >= nums.Length)
                        {
                            throw new IndexOutOfRangeException();
                        }

                        for (int i = startIndex; i <= endIndex; i++)
                        {
                            if (i < endIndex)
                            {
                                Console.Write(nums[i] + ", ");
                            }
                            else
                            {
                                Console.WriteLine(nums[i]);
                            }
                        }
                    }
                    else if (command == "Show")
                    {
                        int index = int.Parse(commandInfo[1]);
                        Console.WriteLine(nums[index]);
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("The variable is not in the correct format!");
                    exceptionsCount++;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The index does not exist!");
                    exceptionsCount++;
                }

            }

            Console.WriteLine(string.Join(", ", nums));
        }
    }
}