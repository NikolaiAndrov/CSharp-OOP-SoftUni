namespace WildFarm.Models
{
    public class Mouse : Mammal
    {
        private static HashSet<string> foodAllowed = new HashSet<string>() { "Vegetable", "Fruit" };
        private const double weightPercentage = 0.10;
        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion, foodAllowed, weightPercentage)
        {
        }

        public override string ProduceSound()
        {
            return "Squeak";
        }
    }
}
