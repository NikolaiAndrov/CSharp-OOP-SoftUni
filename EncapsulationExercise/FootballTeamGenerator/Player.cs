namespace FootballTeamGenerator
{
    public class Player
    {
        private string name;
        private Stats stats;

        public Player(string name, double endurance, double sprint, double dribble, double passing, double shooting)
        {
            Name = name;
            stats = new Stats(endurance, sprint, dribble, passing, shooting);
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }

        public Stats Stats
        {
            get
            {
                return stats;
            }
        }


        public double SkillLevel
        {
            get
            {
                return CalculateSkillLevel();
            }
        }
        private double CalculateSkillLevel()
        {
            double sum = stats.Endurance;
            sum += stats.Sprint;
            sum += stats.Dribble;
            sum += stats.Passing;
            sum += stats.Shooting;

            if (sum == 0)
            {
                return 0;
            }

            return Math.Round(sum / 5);
        }
    }
}
