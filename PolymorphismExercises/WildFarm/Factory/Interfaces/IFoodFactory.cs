namespace WildFarm.Factory.Interfaces
{
    using Models.Interfaces;
    public interface IFoodFactory
    {
        IFood CreateFood(string[] args);
    }
}
