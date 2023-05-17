namespace MilitaryElite.Models.Interfaces
{
    public interface ILieutenantGeneral : IPrivateSoldier
    {
        IReadOnlyCollection<IPrivateSoldier> PrivateSoldiers { get; }
    }
}
