using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const string DefaultDrivingBehavior = "aggressive";
        private const int DefaultDrivingExperience = 10;
        public StreetRacer(string username, ICar car) 
            : base(username, DefaultDrivingBehavior, DefaultDrivingExperience, car)
        {
        }

        public override void Race()
        {
            this.Car.Drive();
            this.DrivingExperience += 5;
        }
    }
}
