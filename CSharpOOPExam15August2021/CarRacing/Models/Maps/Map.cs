using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public Map()
        {
            
        }

        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }

            if (!racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }

            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }

            double racerOneMultiplier = GetMultiplier(racerOne.RacingBehavior);
            double racerTwoMultiplier = GetMultiplier(racerTwo.RacingBehavior);

            racerOne.Race();
            racerTwo.Race();

            double RacerOneChanceOfWinning = racerOne.Car.HorsePower * racerOne.DrivingExperience * racerOneMultiplier;
            double RacerTwoChanceOfWinning = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * racerTwoMultiplier;

            string winnerMessage = string.Empty;

            if (RacerOneChanceOfWinning > RacerTwoChanceOfWinning)
            {
                winnerMessage = string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerOne.Username);
            }
            else if (RacerTwoChanceOfWinning > RacerOneChanceOfWinning)
            {
                winnerMessage = string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerTwo.Username);
            }

            return winnerMessage;
        }

        private double GetMultiplier(string racingBehavior)
        {
            double multiplier = 0;

            if (racingBehavior == "strict")
            {
                multiplier = 1.2;
            }
            else if (racingBehavior == "aggressive")
            {
                multiplier = 1.1;
            }

            return multiplier;
        }
    }
}
