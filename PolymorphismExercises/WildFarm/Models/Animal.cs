namespace WildFarm.Models
{
    using Interfaces;
    public abstract class Animal : IAnimal
    {
        private HashSet<string> foodAllowed;
        private double weightPercentage;
        protected Animal(string name, double weight, HashSet<string> foodAllowed, double weightPercentage)
        {
            Name = name;
            Weight = weight;
            this.foodAllowed = foodAllowed;
            this.weightPercentage = weightPercentage;
            FoodEaten = 0;
        }

        public string Name { get; private set; }

        public double Weight { get; private set; }

        public int FoodEaten { get; private set; }

        public void Eat(IFood food)
        {
            if (!foodAllowed.Contains(food.GetType().Name))
            {
                throw new ArgumentException($"{this.GetType().Name} does not eat {food.GetType().Name}!");
            }

            FoodEaten += food.Quantity;
            Weight += food.Quantity * weightPercentage;
        }

        public abstract string ProduceSound();
    }
}
