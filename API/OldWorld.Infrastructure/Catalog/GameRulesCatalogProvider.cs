using OldWorld.Domain.Enums.Army;
using OldWorld.Domain.Rules;

namespace OldWorld.Infrastructure.Catalog
{
    public class GameRulesCatalogProvider : IGameRulesCatalogProvider, IGameRulesCatalogStore
    {
        private readonly Dictionary<GameVersionEnum, GameRulesCatalog> _catalogs = [];

        public GameRulesCatalog Get(GameVersionEnum gameVersion)
        {
            if (_catalogs.TryGetValue(gameVersion, out var catalog))
                return catalog;

            throw new InvalidOperationException($"Game rules catalog has not been loaded for game version: {gameVersion}.");
        }

        public void Set(GameRulesCatalog catalog)
        {
            _catalogs[catalog.GameVersion] = catalog;
        }
    }
}
