namespace MilitaryElite.Core
{
    using Interfaces;
    using MilitaryElite.IO.Interfaces;
    using MilitaryElite.Models;
    using MilitaryElite.Models.Interfaces;

    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        public Engine(IReader reader, IWriter writer) 
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Start()
        {

            ICollection<ISoldier> soldiers = new List<ISoldier>();

            string input;

            while ((input = reader.ReadLine()) != "End")
            {
                string[] soldierInfo = input.Split();
                string soldierType = soldierInfo[0];
                int id = int.Parse(soldierInfo[1]);
                string firstName = soldierInfo[2];
                string lastName = soldierInfo[3];

                if (soldierType == "Private")
                {
                    decimal salary = decimal.Parse(soldierInfo[4]);
                    ISoldier privateSoldier = new PrivateSoldier(id, firstName, lastName, salary);
                    soldiers.Add(privateSoldier);
                }
                else if (soldierType == "LieutenantGeneral")
                {
                    decimal salary = decimal.Parse(soldierInfo[4]);
                    int[] privatesId = soldierInfo.Skip(5).Select(int.Parse).ToArray();
                    ICollection<IPrivateSoldier> privateSoldiers = GetPrivateSoldiers(privatesId, soldiers);
                    ISoldier lieutenantGeneral = new LieutenantGeneral(id, firstName, lastName, salary, privateSoldiers);
                    soldiers.Add(lieutenantGeneral);
                }
                else if (soldierType == "Engineer")
                {
                    decimal salary = decimal.Parse(soldierInfo[4]);
                    string corps = soldierInfo[5];

                    if (ValidCorps(corps))
                    {
                        string[] repairs = soldierInfo.Skip(6).ToArray();
                        ICollection<IRepair> currentRepairs = GetRepairs(repairs);
                        ISoldier engineer = new Engineer(id, firstName, lastName, salary, corps, currentRepairs);
                        soldiers.Add(engineer);
                    }
                } 
                else if (soldierType == "Commando")
                {
                    decimal salary = decimal.Parse(soldierInfo[4]);
                    string corps = soldierInfo[5];

                    if (ValidCorps(corps))
                    {
                        string[] missions = soldierInfo.Skip(6).ToArray();
                        ICollection<IMission> currentMissions = GetMissions(missions);
                        ISoldier commando = new Commando(id, firstName, lastName, salary, corps, currentMissions);
                        soldiers.Add(commando);
                    }

                }
                else if (soldierType == "Spy")
                {
                    int codeNumber = int.Parse(soldierInfo[4]);
                    ISoldier spy = new Spy(id, firstName, lastName, codeNumber);
                    soldiers.Add(spy);
                }
            }

            PrintSoldiers(soldiers);
        }

        private void PrintSoldiers(ICollection<ISoldier> soldiers)
        {
            foreach (var soldier in soldiers)
            {
                writer.WriteLine(soldier.ToString());
            }
        }
        private ICollection<IMission> GetMissions(string[] missions)
        {
            ICollection<IMission> currentMissions = new List<IMission>();

            for (int i = 0; i < missions.Length; i+=2)
            {
                string codeName = missions[i];
                string state = missions[i+1];
                if (state == "inProgress" || state == "Finished")
                {
                    IMission mission = new Mission(codeName, state);
                    currentMissions.Add(mission);
                }
               
            }

            return currentMissions;
        }
        private bool ValidCorps(string corps)
            => corps == "Airforces" || corps == "Marines";
        private ICollection<IRepair> GetRepairs(string[] repairs)
        {
            ICollection<IRepair> currentRepairs = new List<IRepair>();

            for (int i = 0; i < repairs.Length; i+=2)
            {
                string repairName = repairs[i];
                bool isParsed = int.TryParse(repairs[i + 1], out int hoursWorked);
                if (!isParsed)
                {
                    continue;
                }
                IRepair repair = new Repair(repairName, hoursWorked);
                currentRepairs.Add(repair);
            }
            return currentRepairs;
        }
        private ICollection<IPrivateSoldier> GetPrivateSoldiers(int[] privateId, ICollection<ISoldier> soldiers)
        {
            ICollection<IPrivateSoldier> privateSoldiers = new List<IPrivateSoldier>();

            foreach (int id in privateId)
            {
                foreach (var soldier in soldiers)
                {
                    if (soldier.ID == id)
                    {
                        privateSoldiers.Add((IPrivateSoldier)soldier);
                    }
                }
            }

            return privateSoldiers;
        }
    }
}
