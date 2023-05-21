namespace WildFarm.Models
{
    public class Dog : Mammal
    {

        private static HashSet<string> foodAllowed = new HashSet<string>() { "Meat" };
        private const double weightPercentage = 0.40;

        public Dog(string name, double weight, string livingRegion) 
            : base(name, weight, livingRegion, foodAllowed, weightPercentage)
        {
        }

        public override string ProduceSound()
        {
            return "Woof!";
        }
    }
}
