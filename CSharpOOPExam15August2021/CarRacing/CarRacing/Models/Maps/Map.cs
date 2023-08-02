using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.RaceCannotBeCompleted);
            }

            if (racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);
            }

            if (!racerOne.IsAvailable() && racerTwo.IsAvailable())
            {
                return string.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }

            racerOne.Race();
            racerTwo.Race();

            double racerOneChanceOfWinning = (double)racerOne.Car.HorsePower * racerOne.DrivingExperience;
            double racerTwoChanceOfWinning = (double)racerTwo.Car.HorsePower * racerTwo.DrivingExperience;

            if (racerOne.RacingBehavior == "strict")
            {
                racerOneChanceOfWinning *= 1.2;
            }
            else if (racerOne.RacingBehavior == "aggressive")
            {
                racerOneChanceOfWinning *= 1.1;
            }

            if (racerTwo.RacingBehavior == "strict")
            {
                racerTwoChanceOfWinning *= 1.2;
            }
            else if (racerTwo.RacingBehavior == "aggressive")
            {
                racerTwoChanceOfWinning *= 1.1;
            }

            if (racerOneChanceOfWinning > racerTwoChanceOfWinning)
            {
                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerOne.Username);
            }
            else
            {
                return string.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, racerTwo.Username);
            }
        }
    }
}
