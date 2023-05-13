namespace PizzaCalories
{
    public class StartUp
    {
        public static void Main()
        {
            Pizza pizza = null;

            try
            {
                string input;
                while ((input = Console.ReadLine()) != "END")
                {
                    var args = input.Split();
                    var ingradient = args[0].ToLower();

                    if (ingradient == "pizza")
                    {
                        var name = args[1];
                        pizza = new Pizza(name);
                    }
                    else if (ingradient == "dough")
                    {
                        var flourType = args[1];
                        var bakingType = args[2];
                        var weight = double.Parse(args[3]);
                        Dough dough = new Dough(flourType, bakingType, weight);
                        pizza.AddDough(dough);
                    }
                    else if (ingradient == "topping")
                    {
                        var toppingType = args[1];
                        var weight = double.Parse(args[2]);
                        Topping topping = new Topping(toppingType, weight);
                        pizza.AddToping(topping);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            Console.WriteLine(pizza);
        }
    }
}