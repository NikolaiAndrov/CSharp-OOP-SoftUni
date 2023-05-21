namespace WildFarm.Models
{
    public class Hen : Bird
    {

        private static HashSet<string> foodAllowed = new HashSet<string>() { "Meat", "Vegetable", "Fruit", "Seeds" };
        private const double weightPercentage = 0.35;

        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize, foodAllowed, weightPercentage)
        {
        }

        public override string ProduceSound()
        {
            return "Cluck";
        }
    }
}
