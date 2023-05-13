namespace PizzaCalories
{
    public class Pizza
    {
        private string name;
        private Dough dough;
        private List<Topping> toppings;

        public Pizza(string name)
        {
            Name = name;
            toppings = new List<Topping>();
        }

        public string Name
        {
            get 
            {
                return name; 
            }
            private set
            {
                if (value == string.Empty || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }

                name = value;
            }
        }

        public int ToppingsCount
        {
            get
            {
                return toppings.Count;
            }
        }

        public override string ToString()
        {
            return $"{Name} - {TotalCaories():f2} Calories.";
        }

        private double TotalCaories()
        {
            double calories = dough.TotalCalories;

            foreach (Topping topping in toppings)
            {
                calories += topping.TotalCalories;
            }
            return calories;
        }

        public void AddToping(Topping topping)
        {
            if (toppings.Count == 10)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }

            toppings.Add(topping);
        }

        public void AddDough(Dough dough)
        {
            this.dough = dough;
        }
    }
}
