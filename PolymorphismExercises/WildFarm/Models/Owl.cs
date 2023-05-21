namespace WildFarm.Models
{
    public class Owl : Bird
    {
        private static HashSet<string> foodAllowed = new HashSet<string>() { "Meat" };
        private const double weightPercentage = 0.25;

        public Owl(string name, double weight, double wingSize)
            : base(name, weight, wingSize, foodAllowed, weightPercentage)
        {

        }

        public override string ProduceSound()
        {
            return "Hoot Hoot";
        }
    }
}
