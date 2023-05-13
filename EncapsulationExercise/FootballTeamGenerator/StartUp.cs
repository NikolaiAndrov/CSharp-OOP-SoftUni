namespace FootballTeamGenerator
{
    public class StartUp
    {
        public static void Main()
        {
            var teams = new Dictionary<string, Team>();

            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                try
                {
                    string[] args = input.Split(";");
                    string command = args[0];
                    string teamName = args[1];

                    if (command == "Team")
                    {
                        Team team = new Team(teamName);
                        teams.Add(teamName, team);
                    }
                    else if (command == "Add")
                    {
                        if (!teams.ContainsKey(teamName))
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                            continue;
                        }

                        string playerName = args[2];
                        double endurance = double.Parse(args[3]);
                        double sprint = double.Parse(args[4]);
                        double dribble = double.Parse(args[5]);
                        double passing = double.Parse(args[6]);
                        double shooting = double.Parse(args[7]);

                        Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
                        teams[teamName].AddPlayer(player);
                    }
                    else if (command == "Remove")
                    {
                        if (!teams.ContainsKey(teamName))
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                            continue;
                        }

                        string playerName = args[2];
                        teams[teamName].RemovePlayer(playerName);
                    }
                    else if (command == "Rating")
                    {

                        if (!teams.ContainsKey(teamName))
                        {
                            Console.WriteLine($"Team {teamName} does not exist.");
                            continue;
                        }

                        Console.WriteLine(teams[teamName]);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}