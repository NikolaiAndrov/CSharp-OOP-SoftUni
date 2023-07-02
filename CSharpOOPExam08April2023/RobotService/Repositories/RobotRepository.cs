using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private ICollection<IRobot> robots;

        public RobotRepository()
        {
            this.robots = new List<IRobot>();
        }
        public void AddNew(IRobot model)
        {
            this.robots.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            IRobot robot = null;
            bool isFound = false;

            foreach (IRobot model in this.robots)
            {
                foreach (var standart in model.InterfaceStandards)
                {
                    if (standart == interfaceStandard)
                    {
                        robot = model;
                        isFound = true;
                        break;
                    }
                }

                if (isFound)
                {
                    break;
                }
            }

            return robot;
        }

        public IReadOnlyCollection<IRobot> Models()
            => (IReadOnlyCollection<IRobot>)this.robots;

        // Model or Type ?!!
        public bool RemoveByName(string typeName)
        {
            IRobot robot = this.robots.FirstOrDefault(x => x.Model == typeName);

            if (robot != null)
            {
                this.robots.Remove(robot);
                return true;
            }

            return false;
        }
    }
}
