using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private IRepository<ISupplement> supplements;
        private IRepository<IRobot> robots;

        public Controller() 
        {
            this.supplements = new SupplementRepository();
            this.robots = new RobotRepository();
        }

        public string CreateRobot(string model, string typeName)
        {
            IRobot robot;

            if (typeName == "DomesticAssistant")
            {
                robot = new DomesticAssistant(model);
                this.robots.AddNew(robot);
                return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
            }
            else if (typeName == "IndustrialAssistant")
            {
                robot = new IndustrialAssistant(model);
                this.robots.AddNew(robot);
                return string.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
            }

            return string.Format(OutputMessages.RobotCannotBeCreated, typeName);
        }

        public string CreateSupplement(string typeName)
        {
            ISupplement supplement;

            if (typeName == "SpecializedArm")
            {
                supplement = new SpecializedArm();
                this.supplements.AddNew(supplement);
                return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
            }
            else if (typeName == "LaserRadar")
            {
                supplement = new LaserRadar();
                this.supplements.AddNew(supplement);
                return string.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
            }

            return string.Format(OutputMessages.SupplementCannotBeCreated, typeName);
        }

        public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
        {
            IReadOnlyCollection<IRobot> allRepositoryRobots = this.robots.Models();
            ICollection<IRobot> robotsNedded = new List<IRobot>();

            foreach (IRobot robot in allRepositoryRobots.OrderByDescending(x => x.BatteryLevel))
            {
                if (robot.InterfaceStandards.Contains(intefaceStandard))
                {
                    robotsNedded.Add(robot);
                }
            }

            if (robotsNedded.Count == 0)
            {
                return string.Format(OutputMessages.UnableToPerform, intefaceStandard);
            }

            int totalBatteryLevel = 0;

            foreach (var robot in robotsNedded)
            {
                totalBatteryLevel += robot.BatteryLevel;
            }

            if (totalBatteryLevel < totalPowerNeeded)
            {
                return string.Format(OutputMessages.MorePowerNeeded, serviceName, totalPowerNeeded - totalBatteryLevel);
            }

            int robotsCount = 0;

            foreach (var robot in robotsNedded)
            {
                if (totalPowerNeeded == 0)
                {
                    break;
                }

                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                    robotsCount++;
                    break;
                }
                else if (robot.BatteryLevel < totalPowerNeeded)
                {
                    totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                    robotsCount++;
                }
            }

            return string.Format(OutputMessages.PerformedSuccessfully, serviceName, robotsCount);
            
        }

        public string RobotRecovery(string model, int minutes)
        {
            IReadOnlyCollection<IRobot> robotsToFeed = this.robots.Models()
                .Where(x => x.Model == model)
                .ToList();

            int robotsFed = 0;

            foreach (var robot in robotsToFeed)
            {
                int percentage = (robot.BatteryLevel / robot.BatteryCapacity * 100);

                if (percentage < 50)
                {
                    robot.Eating(minutes);
                    robotsFed++;
                }
            }

            return string.Format(OutputMessages.RobotsFed, robotsFed);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement supplementToInstall = this.supplements.Models()
                .FirstOrDefault(x => x.GetType().Name == supplementTypeName);

            int interfaceValue = supplementToInstall.InterfaceStandard;

            IReadOnlyCollection<IRobot> robotsNotSupportingInterface = this.robots.Models()
               .Where(x => !x.InterfaceStandards.Contains(interfaceValue))
               .ToArray();

            ICollection<IRobot> robotsOfGivenModel = robotsNotSupportingInterface
                .Where(x => x.Model == model)
                .ToArray();

            if (robotsOfGivenModel.Count == 0)
            {
                return string.Format(OutputMessages.AllModelsUpgraded, model);
            }

            IRobot robotToUpgrade = robotsOfGivenModel.First();
            robotToUpgrade.InstallSupplement(supplementToInstall);
            this.supplements.RemoveByName(supplementTypeName);
            return string.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
        }

        public string Report()
        {
            IReadOnlyCollection<IRobot> robotsToReport = this.robots.Models()
                .OrderByDescending(x => x.BatteryLevel)
                .ThenBy(x => x.BatteryCapacity)
                .ToArray();

            StringBuilder sb = new StringBuilder();

            foreach (var robot in robotsToReport)
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
