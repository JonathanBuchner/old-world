using OldWorld.Domain.Enums.Army;

namespace OldWorld.Domain.Rules
{
    public interface IGameRulesCatalogProvider
    {
        GameRulesCatalog Get(GameVersionEnum gameVersion);
    }
}
