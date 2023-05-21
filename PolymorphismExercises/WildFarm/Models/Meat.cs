namespace WildFarm.Models
{
    using Interfaces;
    public class Meat : IFood
    {
        public Meat(int quantity) 
        {
            Quantity = quantity;
        }
        public int Quantity { get; private set; }
    }
}
