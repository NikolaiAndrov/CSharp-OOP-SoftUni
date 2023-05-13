namespace PizzaCalories
{
    public class Topping
    {
        private string toppingType;
        private double weight;

        public Topping(string toppingType, double weight)
        {
            ToppingType = toppingType;
            Weight = weight;
        }

        public string ToppingType
        {
            get
            {
                return toppingType;
            }
            private set
            {
                if (value.ToLower() != "meat" 
                    && value.ToLower() != "veggies" 
                    && value.ToLower() != "cheese" 
                    && value.ToLower() != "sauce")
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                toppingType = value;
            }
        }

        public double Weight
        {
            get
            {
                return weight;
            }
            private set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{ToppingType} weight should be in the range [1..50].");
                }
                weight = value;
            }
        }

        public double TotalCalories
        {
            get
            {
                return CalculateCalories();
            }
        }

        private double CalculateCalories()
        {
            double toppingCalories = 0;
            string type = ToppingType.ToLower();

            if (type == "meat")
            {
                toppingCalories = 1.2;
            }
            else if (type == "veggies")
            {
                toppingCalories = 0.8;
            }
            else if (type == "cheese")
            {
                toppingCalories = 1.1;
            }
            else if (type == "sauce")
            {
                toppingCalories = 0.9;
            }

            return (2 * Weight) * toppingCalories;
        }
    }
}
