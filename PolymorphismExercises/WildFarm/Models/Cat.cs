namespace WildFarm.Models
{
    public class Cat : Feline
    {
        private static HashSet<string> foodAllowed = new HashSet<string>() { "Meat", "Vegetable" };
        private const double weightPercentage = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, livingRegion, breed, foodAllowed, weightPercentage)
        {
        }

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
