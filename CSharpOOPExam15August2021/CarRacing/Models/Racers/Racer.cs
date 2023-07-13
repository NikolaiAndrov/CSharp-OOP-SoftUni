using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;
using System;
using System.Text;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        private string userName;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;

        protected Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            this.Username = username;
            this.RacingBehavior = racingBehavior;
            this.DrivingExperience = drivingExperience;
            this.Car = car;
        }

        public string Username
        {
            get => this.userName;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);
                }

                this.userName = value;
            }
        }

        public string RacingBehavior
        {
            get => this.racingBehavior;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);
                }

                this.racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get => this.drivingExperience;

            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);
                }

                this.drivingExperience = value;
            }
        }

        public ICar Car
        {
            get => this.car;

            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);
                }

                this.car = value;
            }
        }

        public abstract void Race();

        public bool IsAvailable()
            => this.Car.FuelAvailable >= this.Car.FuelConsumptionPerRace;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}: {this.userName}");
            sb.AppendLine($"--Driving behavior: {this.RacingBehavior}");
            sb.AppendLine($"--Driving experience: {this.DrivingExperience}");
            sb.Append($"--Car: {this.Car.Make} {this.Car.Model} ({this.Car.VIN})");

            return sb.ToString();
        }
    }
}
