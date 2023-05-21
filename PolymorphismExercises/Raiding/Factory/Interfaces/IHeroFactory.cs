
namespace Raiding.Factory.Interfaces
{
    using Models.Interfaces;
    public interface IHeroFactory
    {
        IHero CreateHero(string name, string type);
    }
}
