namespace WildFarm.Models
{
    using Interfaces;
    public class Fruit : IFood
    {
        public Fruit(int quantity)
        {
            Quantity = quantity;
        }

        public int Quantity { get; private set; }
    }
}
