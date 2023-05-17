namespace MilitaryElite.Models.Interfaces
{
    public interface ISpecialisedSoldier : IPrivateSoldier
    {
        string Corps { get; }
    }
}
