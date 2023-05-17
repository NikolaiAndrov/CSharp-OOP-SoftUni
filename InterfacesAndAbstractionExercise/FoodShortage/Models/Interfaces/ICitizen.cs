namespace FoodShortage.Models.Interfaces
{
    public interface ICitizen : IBuyer
    {
        string Name { get; }
        int Age { get; }
        string ID { get; }
        string BirthDate { get; }
    }
}
