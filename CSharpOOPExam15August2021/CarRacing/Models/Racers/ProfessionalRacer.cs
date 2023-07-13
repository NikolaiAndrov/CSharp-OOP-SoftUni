using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class ProfessionalRacer : Racer
    {
        private const string DefaultDrivingBehavior = "strict";
        private const int DefaultDrivingExperience = 30;
        public ProfessionalRacer(string username, ICar car)
            : base(username, DefaultDrivingBehavior, DefaultDrivingExperience, car)
        {
        }

        public override void Race()
        {
            this.Car.Drive();
            this.DrivingExperience += 10;
        }
    }
}
