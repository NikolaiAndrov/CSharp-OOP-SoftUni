using NavalVessels.Models.Contracts;
using System.Text;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const double armorThickness = 300;
        public Battleship(string name, double mainWeaponCaliber, double speed)
            : base(name, mainWeaponCaliber, speed, armorThickness)
        {
            this.SonarMode = false;
        }

        public bool SonarMode { get; private set; } 

        public override void RepairVessel()
        {
            this.ArmorThickness = armorThickness;
        }

        public void ToggleSonarMode()
        {
            this.SonarMode = !this.SonarMode;

            if (this.SonarMode == true)
            {
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
            }
            else
            {
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
            }
        }

        public override string ToString()
        {
            string sonarModeOnOff = this.SonarMode ? "ON" : "OFF";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Sonar mode: {sonarModeOnOff}");

            return sb.ToString().TrimEnd();
        }
    }
}
