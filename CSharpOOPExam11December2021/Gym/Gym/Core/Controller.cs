using Gym.Core.Contracts;
using Gym.Models.Athletes;
using Gym.Models.Athletes.Contracts;
using Gym.Models.Equipment;
using Gym.Models.Equipment.Contracts;
using Gym.Models.Gyms;
using Gym.Models.Gyms.Contracts;
using Gym.Repositories;
using Gym.Repositories.Contracts;
using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gym.Core
{
    public class Controller : IController
    {
        private readonly IRepository<IEquipment> equipment;
        private readonly ICollection<IGym> gyms;
        public Controller()
        {
            this.equipment = new EquipmentRepository();
            this.gyms = new HashSet<IGym>();
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            IAthlete athlete;
            IGym gym = this.gyms.First(g => g.Name == gymName);

            if (athleteType == nameof(Boxer))
            {
                if (gym.GetType().Name != nameof(BoxingGym))
                {
                    return OutputMessages.InappropriateGym;
                }

                athlete = new Boxer(athleteName, motivation, numberOfMedals);
            }
            else if (athleteType == nameof(Weightlifter))
            {
                if (gym.GetType().Name != nameof(WeightliftingGym))
                {
                    return OutputMessages.InappropriateGym;
                }

                athlete = new Weightlifter(athleteName, motivation, numberOfMedals);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAthleteType);
            }

            gym.AddAthlete(athlete);
            return string.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment newEquipment;
            string outputMessage = string.Empty;

            if (equipmentType == nameof(BoxingGloves))
            {
                newEquipment = new BoxingGloves();
                outputMessage = string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
            }
            else if (equipmentType == nameof(Kettlebell))
            {
                newEquipment = new Kettlebell();
                outputMessage = string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidEquipmentType);
            }

            this.equipment.Add(newEquipment);
            return outputMessage;
        }

        public string AddGym(string gymType, string gymName)
        {
            IGym gym;
            string outputMessage = string.Empty;

            if (gymType == nameof(BoxingGym))
            {
                gym = new BoxingGym(gymName);
                outputMessage = string.Format(OutputMessages.SuccessfullyAdded, gymType);
            }
            else if (gymType == nameof(WeightliftingGym))
            {
                gym = new WeightliftingGym(gymName);
                outputMessage = string.Format(OutputMessages.SuccessfullyAdded, gymType);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidGymType);
            }

            this.gyms.Add(gym);
            return outputMessage;
        }

        public string EquipmentWeight(string gymName)
        {
            IGym gym = this.gyms.First(g => g.Name == gymName);
            return string.Format(OutputMessages.EquipmentTotalWeight, gymName, gym.EquipmentWeight);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment equipmentToAdd = this.equipment.FindByType(equipmentType);

            if (equipmentToAdd == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }

            IGym gym = this.gyms.First(g => g.Name == gymName);
            gym.AddEquipment(equipmentToAdd);

            this.equipment.Remove(equipmentToAdd);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string TrainAthletes(string gymName)
        {
            IGym gym = this.gyms.First(g => g.Name == gymName);
            gym.Exercise();
            return string.Format(OutputMessages.AthleteExercise, gym.Athletes.Count);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var gym in this.gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
