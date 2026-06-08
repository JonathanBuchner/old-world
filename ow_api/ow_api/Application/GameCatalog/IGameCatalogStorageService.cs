using ow_api.Application.GameRules.Catalog;
using ow_gen_lib.Enums.Army;

namespace ow_api.Application.GameCatalog
{
    public interface IGameCatalogStorageService
    {
        Task<RulesCatalogFile> GetRulesAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken);
        Task<EquipmentCatalogFile> GetEquipmentAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken);
        Task<LoresCatalogFile> GetLoresAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken);
        Task UpdateRulesAsync(GameVersionEnum gameVersion, RulesCatalogFile rulesCatalogFile, CancellationToken cancellationToken);
        Task UpdateEquipmentAsync(GameVersionEnum gameVersion, EquipmentCatalogFile equipmentCatalogFile, CancellationToken cancellationToken);
        Task UpdateLoresAsync(GameVersionEnum gameVersion, LoresCatalogFile loresCatalogFile, CancellationToken cancellationToken);
    }
}
