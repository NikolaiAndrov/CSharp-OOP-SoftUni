namespace ShoppingSpree
{
    using System;
    public class Person
    {
        private string name;
        private decimal money;
        private List<Product> bag;

        public Person()
        {
            bag = new List<Product>();
        }

        public Person(string name, decimal money) : this()
        {
            Name = name;
            Money = money;
        }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException($"Name cannot be empty");
                }
                name = value;
            }
        }

        public decimal Money
        {
            get
            {
                return money;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }

        public string BuyProduct(Product product)
        {
            if (Money - product.Cost >= 0)
            {
                Money -= product.Cost;
                bag.Add(product);
                return $"{Name} bought {product.Name}";
            }

            return $"{Name} can't afford {product.Name}";
        }

        public override string ToString()
        {
            if (bag.Count == 0)
            {
                return $"{Name} - Nothing bought";
            }

            return $"{Name} - {string.Join(", ", bag)}";
        }
    }
}
