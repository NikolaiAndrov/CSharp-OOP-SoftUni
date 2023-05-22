namespace MoneyTransactions
{
    public class StartUp
    {
        public static void Main()
        {
            string[] inputAccounts = Console.ReadLine().Split(",");
            Dictionary<int, double> accounts = new Dictionary<int, double>();

            foreach (string account in inputAccounts)
            {
                string[] accountInfo = account.Split("-");
                int id = int.Parse(accountInfo[0]);
                double sum = double.Parse(accountInfo[1]);

                accounts[id] = sum;
            }

            string input;

            while ((input = Console.ReadLine()) != "End")
            {
                try
                {
                    string[] commandInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string command = commandInfo[0];

                    int id = int.Parse(commandInfo[1]);
                    double sum = double.Parse(commandInfo[2]);

                    if (command == "Deposit")
                    {
                        accounts[id] += sum;
                        Console.WriteLine($"Account {id} has new balance: {accounts[id]:f2}");
                    }
                    else if (command == "Withdraw")
                    {
                        if (accounts[id] - sum < 0)
                        {
                            throw new InvalidOperationException("Insufficient balance!");
                        }

                        accounts[id] -= sum;
                        Console.WriteLine($"Account {id} has new balance: {accounts[id]:f2}");
                    }
                    else
                    {
                        throw new ArgumentException("Invalid command!");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (InvalidOperationException ex)
                { 
                    Console.WriteLine(ex.Message);
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine("Invalid account!");
                }
                finally
                {
                    Console.WriteLine("Enter another command");
                }
            }
        }
    }
}