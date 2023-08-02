using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const string DefaultRacingBehavior = "aggressive";
        private const int DefaultDrivingExperience = 10;

        public StreetRacer(string username, ICar car) 
            : base(username, DefaultRacingBehavior, DefaultDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            this.DrivingExperience += 5;
        }
    }
}
