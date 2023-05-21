namespace WildFarm.Models
{
    public abstract class Bird : Animal
    {
        protected Bird(string name, double weight, double wingSize, HashSet<string> foodAllowed, double weightPercentage)
            : base(name, weight, foodAllowed, weightPercentage)
        {
            WingSize = wingSize;
        }

        public  double WingSize { get; private set; }

        public override string ToString()
        {
            return $"{this.GetType().Name} [{Name}, {WingSize}, {Weight}, {FoodEaten}]";
        }
    }
}
