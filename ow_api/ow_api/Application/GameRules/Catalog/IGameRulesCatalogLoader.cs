using ow_gen_lib.Enums.Army;
using ow_gen_lib.Rules;

namespace ow_api.Application.GameRules.Catalog
{
    public interface IGameRulesCatalogLoader
    {
        Task<GameRulesCatalog> LoadAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken);
    }
}
