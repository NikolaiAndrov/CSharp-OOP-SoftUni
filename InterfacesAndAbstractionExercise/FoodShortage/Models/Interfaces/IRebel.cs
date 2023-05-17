namespace FoodShortage.Models.Interfaces
{
    public interface IRebel : IBuyer
    {
        string Name { get; }
        int Age { get; }
        string Group { get; }
    }
}
