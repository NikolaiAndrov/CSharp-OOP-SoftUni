namespace WildFarm.Models
{
    using Interfaces;
    public class Vegetable : IFood
    {
        public Vegetable(int quantity)
        {
            Quantity = quantity;
        }
        public int Quantity { get; private set; }
    }
}
