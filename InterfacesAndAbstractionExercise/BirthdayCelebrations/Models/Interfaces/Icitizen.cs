namespace BorderControl.Models.Interfaces
{
    public interface Icitizen : ICreature, IBirthable
    {
        int Age { get; }
        string Id { get; }
    }
}
