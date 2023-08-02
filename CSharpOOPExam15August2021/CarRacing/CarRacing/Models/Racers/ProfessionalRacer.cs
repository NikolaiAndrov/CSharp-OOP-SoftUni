using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarRacing.Models.Racers
{
    public class ProfessionalRacer : Racer
    {
        private const string DefaultRacingBehavior = "strict";
        private const int DefaultDrivingExperience = 30;

        public ProfessionalRacer(string username, ICar car) 
            : base(username, DefaultRacingBehavior, DefaultDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            this.DrivingExperience += 10;
        }
    }
}
