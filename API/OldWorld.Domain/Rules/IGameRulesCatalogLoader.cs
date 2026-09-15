using OldWorld.Domain.Enums.Army;

namespace OldWorld.Domain.Rules
{
    public interface IGameRulesCatalogLoader
    {
        Task<GameRulesCatalog> LoadAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken);
    }
}
