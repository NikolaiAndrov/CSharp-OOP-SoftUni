namespace ShoppingSpree
{
    public class StartUp
    {
        public static void Main()
        {
            var people = new Dictionary<string, Person>();
            var products = new Dictionary<string, Product>();

            var peopleInput = Console.ReadLine().Split(";");
            var productsInput = Console.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);

            try
            {
                foreach (var person in peopleInput)
                {
                    var personInfo = person.Split("=");
                    var name = personInfo[0];
                    var money = decimal.Parse(personInfo[1]);
                    var currentPerson = new Person(name, money);
                    people.Add(name, currentPerson);
                }

                foreach (var product in productsInput)
                {
                    var productInfo = product.Split("=");
                    var name = productInfo[0];
                    var cost = decimal.Parse(productInfo[1]);
                    var currentProduct = new Product(name, cost);
                    products.Add(name, currentProduct);
                }

                string input;

                while ((input = Console.ReadLine()) != "END")
                {
                    var purchaseInfo = input.Split();
                    var personName = purchaseInfo[0];
                    var productName = purchaseInfo[1];

                    Console.WriteLine(people[personName].BuyProduct(products[productName]));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            foreach (var person in people)
            {
                Console.WriteLine(person.Value);
            }
        }
    }
}