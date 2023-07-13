using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Core
{
    internal class Controller : IController
    {
        private readonly IRepository<ICar> cars;
        private readonly IRepository<IRacer> racers;
        private readonly IMap map;

        public Controller()
        {
            this.cars = new CarRepository();
            this.racers = new RacerRepository();
            this.map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car;

            if (type == nameof(SuperCar))
            {
                car = new SuperCar(make,model, VIN, horsePower);
            }
            else if (type == nameof(TunedCar))
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }

            this.cars.Add(car);
            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            ICar car = this.cars.FindBy(carVIN);

            if (car == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }

            IRacer racer;

            if (type == nameof(ProfessionalRacer))
            {
                racer = new ProfessionalRacer(username, car);
            }
            else if (type == nameof(StreetRacer))
            {
                racer = new StreetRacer(username, car);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }

            this.racers.Add(racer);
            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racer1 = this.racers.FindBy(racerOneUsername);
            IRacer racer2 = this.racers.FindBy(racerTwoUsername);

            if (racer1 == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }

            if (racer2 == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }

            return map.StartRace(racer1, racer2);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var racer in this.racers.Models.OrderByDescending(r => r.DrivingExperience).ThenBy(r => r.Username))
            {
                sb.AppendLine(racer.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
