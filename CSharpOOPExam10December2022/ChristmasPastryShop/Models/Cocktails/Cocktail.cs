using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;

namespace ChristmasPastryShop.Models.Cocktails
{
    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        protected Cocktail(string cocktailName, string size, double price)
        {
            this.Name = cocktailName;
            this.Size = size;
            this.Price = price;
        }

        public string Name
        {
            get { return this.name; }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }

                this.name = value;
            }
        }

        public string Size
        {
            get { return this.size; }
            private set { this.size = value; }
        }

        public double Price
        {
            get { return this.price; }

            private set
            {
                if (this.Size == "Middle")
                {
                    this.price = (2.0 / 3.0) * value;
                }
                else if (this.Size == "Small")
                {
                    this.price = (1.0 / 3.0) * value;
                }
                else if (this.Size == "Large")
                {
                    this.price = value;
                }

            }
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Size}) - {this.Price:f2} lv";
        }
    }
}
