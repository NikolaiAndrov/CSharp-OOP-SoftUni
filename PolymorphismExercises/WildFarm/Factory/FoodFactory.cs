namespace WildFarm.Factory
{
    using Factory.Interfaces;
    using Models.Interfaces;
    using WildFarm.Models;

    public class FoodFactory : IFoodFactory
    {
        public IFood CreateFood(string[] args)
        {
            string type = args[0];
            int quantity = int.Parse(args[1]);

            IFood food;

            if (type == "Vegetable")
            {
                food = new Vegetable(quantity);
            }
            else if (type == "Fruit")
            {
                food = new Fruit(quantity);
            }
            else if (type == "Meat")
            {
                food = new Meat(quantity);
            }
            else if (type == "Seeds")
            {
                food = new Seeds(quantity);
            }
            else
            {
                throw new ArgumentException("Invalid food type!");
            }

            return food;
        }
    }
}
