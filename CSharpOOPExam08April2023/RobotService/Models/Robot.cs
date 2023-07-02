using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Models
{
    public abstract class Robot : IRobot
    {
        private string model;
        private int batteryCapacity;
        private int batteryLevel;
        private int convertionCapacityIndex;
        private ICollection<int> interfaceStandards;

        public Robot(string model, int batteryCapacity, int conversionCapacityIndex)
        {
            this.Model = model;
            this.BatteryCapacity = batteryCapacity;
            this.ConvertionCapacityIndex = conversionCapacityIndex;
            this.BatteryLevel = batteryCapacity;
            this.interfaceStandards = new List<int>();
        }

        public string Model
        {
            get { return this.model; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ModelNullOrWhitespace));
                }

                this.model = value;
            }
        }

        public int BatteryCapacity
        {
            get { return this.batteryCapacity; }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.BatteryCapacityBelowZero));
                }

                this.batteryCapacity = value;
            }
        }

        public int BatteryLevel
        {
            get { return this.batteryLevel; }
            private set { this.batteryLevel = value; }
        }

        public int ConvertionCapacityIndex
        {
            get { return this.convertionCapacityIndex; }
            private set {this.convertionCapacityIndex = value; }
        }

        public IReadOnlyCollection<int> InterfaceStandards 
            => (IReadOnlyCollection<int>)this.interfaceStandards;

        public void Eating(int minutes)
        {
            int energy = this.ConvertionCapacityIndex * minutes;
            this.BatteryLevel += energy;

            if (this.BatteryLevel > this.BatteryCapacity)
            {
                this.BatteryLevel = this.BatteryCapacity;
            }
        }

        public void InstallSupplement(ISupplement supplement)
        {
            this.BatteryCapacity -= supplement.BatteryUsage;
            this.BatteryLevel -= supplement.BatteryUsage;
            this.interfaceStandards.Add(supplement.InterfaceStandard);
        }

        public bool ExecuteService(int consumedEnergy)
        {
            if (this.BatteryLevel >= consumedEnergy)
            {
                this.BatteryLevel -= consumedEnergy;
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string supplementsInstalled = this.interfaceStandards.Any() ?
                string.Join(" ", this.interfaceStandards) : "none";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name} {this.Model}:");
            sb.AppendLine($"--Maximum battery capacity: {this.BatteryCapacity}");
            sb.AppendLine($"--Current battery level: {this.BatteryLevel}");
            sb.AppendLine($"--Supplements installed: {supplementsInstalled}");

            return sb.ToString().TrimEnd();
        }
    }
}
