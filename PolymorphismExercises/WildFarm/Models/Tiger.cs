namespace WildFarm.Models
{
    public class Tiger : Feline
    {
        private static HashSet<string> foodAllowed = new HashSet<string>() { "Meat" };
        private const double weightPercentage = 1;

        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed, foodAllowed, weightPercentage)
        {
        }

        public override string ProduceSound()
        {
            return "ROAR!!!";
        }
    }
}
