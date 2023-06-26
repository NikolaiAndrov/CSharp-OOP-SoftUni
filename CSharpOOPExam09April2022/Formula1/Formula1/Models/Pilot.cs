using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;

namespace Formula1.Models
{
    public class Pilot : IPilot
    {
        private string fullName;
        private IFormulaOneCar car;
        private int numberOfWins;
        private bool canRace;

        public Pilot(string fullName)
        {
            FullName = fullName;
        }

        public string FullName
        {
            get { return this.fullName; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }

                this.fullName = value;
            }
        }

        public IFormulaOneCar Car
        {
            get { return this.car; }

            private set
            {
                if (value == null)
                {
                    throw new NullReferenceException(string.Format(ExceptionMessages.InvalidCarForPilot, value));
                }

                this.car = value;
            }
        }

        public int NumberOfWins
        { 
            get { return this.numberOfWins; }
            private set { this.numberOfWins = value; }
        }

        public bool CanRace 
        { 
            get { return this.canRace; }
            private set { this.canRace = value; }
        }

        public void AddCar(IFormulaOneCar car)
        {
            this.Car = car;
            this.CanRace = true;
        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }

        public override string ToString()
        {
            return $"Pilot {this.FullName} has {this.NumberOfWins} wins.";
        }
    }
}
