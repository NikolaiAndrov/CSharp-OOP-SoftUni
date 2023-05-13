namespace FootballTeamGenerator
{
    public class Team
    {
        private string name;
        private Dictionary<string, Player> players;

        public Team(string name)
        {
            Name = name;
            players = new Dictionary<string, Player>();
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

        public void AddPlayer(Player player)
        {
            players[player.Name] = player;
        }

        public void RemovePlayer(string playerName)
        {
            if (!players.ContainsKey(playerName))
            {
                throw new ArgumentException($"Player {playerName} is not in {Name} team.");
            }

            players.Remove(playerName);
        }

        private double CalculateRating()
        {
            double rating = 0;

            foreach (var player in players)
            {
                rating += player.Value.SkillLevel;
            }

            if (rating == 0)
            {
                return 0;
            }

            return Math.Round(rating / players.Count);
        }

        public override string ToString()
        {
            return $"{Name} - {CalculateRating()}";
        }
    }
}
