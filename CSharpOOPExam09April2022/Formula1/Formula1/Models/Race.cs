using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private bool tookPlace;
        private ICollection<IPilot> pilots;
        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            this.pilots = new List<IPilot>();
        }

        public string RaceName
        {
            get { return this.raceName; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidRaceName, value));
                }

                this.raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get { return this.numberOfLaps; }

            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidLapNumbers, value));
                }

                this.numberOfLaps = value;
            }
        }

        public bool TookPlace
        {
            get { return this.tookPlace; }
            set { this.tookPlace = value; }
        }

        public ICollection<IPilot> Pilots
        {
            get { return this.pilots; }
        }

        public void AddPilot(IPilot pilot)
        {
            pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"The {this.RaceName} race has:");
            sb.AppendLine($"Participants: {this.pilots.Count}");
            sb.AppendLine($"Number of laps: {this.NumberOfLaps}");

            if (this.TookPlace == true)
            {
                sb.AppendLine("Took place: Yes");
            }
            else
            {
                sb.AppendLine("Took place: No");
            }

            return sb.ToString().Trim();
        }
    }
}
