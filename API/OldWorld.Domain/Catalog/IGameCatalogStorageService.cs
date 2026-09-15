using OldWorld.Domain.Enums.Army;

namespace OldWorld.Domain.Catalog
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
