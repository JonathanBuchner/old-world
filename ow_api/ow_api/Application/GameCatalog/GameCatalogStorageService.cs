using Microsoft.Extensions.Options;
using ow_api.Application.GameRules.Catalog;
using ow_api.Exceptions;
using ow_api.Infrastructure.Services;
using ow_api.Infrastructure.Settings;
using ow_gen_lib.Enums.Army;

namespace ow_api.Application.GameCatalog
{
    public class GameCatalogStorageService : IGameCatalogStorageService
    {
        private readonly IBlobJsonStorage _blobJsonStorage;
        private readonly GameRulesSettings _settings;

        public GameCatalogStorageService(IBlobJsonStorage blobJsonStorage, IOptions<GameRulesSettings> options)
        {
            _blobJsonStorage = blobJsonStorage;
            _settings = options.Value;
        }

        public Task<RulesCatalogFile> GetRulesAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken)
        {
            return ReadRequiredAsync<RulesCatalogFile>(gameVersion, RulesBlobName, cancellationToken);
        }

        public Task<EquipmentCatalogFile> GetEquipmentAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken)
        {
            return ReadRequiredAsync<EquipmentCatalogFile>(gameVersion, EquipmentBlobName, cancellationToken);
        }

        public Task<LoresCatalogFile> GetLoresAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken)
        {
            return ReadRequiredAsync<LoresCatalogFile>(gameVersion, LoresBlobName, cancellationToken);
        }

        public Task UpdateRulesAsync(GameVersionEnum gameVersion, RulesCatalogFile rulesCatalogFile, CancellationToken cancellationToken)
        {
            ValidateCatalogGameVersion(gameVersion, rulesCatalogFile.GameVersion);

            return UpdateAsync(gameVersion, RulesBlobName, rulesCatalogFile, cancellationToken);
        }

        public Task UpdateEquipmentAsync(GameVersionEnum gameVersion, EquipmentCatalogFile equipmentCatalogFile, CancellationToken cancellationToken)
        {
            ValidateCatalogGameVersion(gameVersion, equipmentCatalogFile.GameVersion);

            return UpdateAsync(gameVersion, EquipmentBlobName, equipmentCatalogFile, cancellationToken);
        }

        public Task UpdateLoresAsync(GameVersionEnum gameVersion, LoresCatalogFile loresCatalogFile, CancellationToken cancellationToken)
        {
            ValidateCatalogGameVersion(gameVersion, loresCatalogFile.GameVersion);

            return UpdateAsync(gameVersion, LoresBlobName, loresCatalogFile, cancellationToken);
        }

        private async Task<T> ReadRequiredAsync<T>(GameVersionEnum gameVersion, Func<string, string> blobNameFactory, CancellationToken cancellationToken)
        {
            var blobName = GetBlobName(gameVersion, blobNameFactory);
            var catalogFile = await _blobJsonStorage.ReadAsync<T>(_settings.RulesContainerName, blobName, cancellationToken);

            if (catalogFile != null)
                return catalogFile;

            throw new GameCatalogException($"Game catalog blob was not found: {_settings.RulesContainerName}/{blobName}.");
        }

        private Task UpdateAsync<T>(GameVersionEnum gameVersion, Func<string, string> blobNameFactory, T catalogFile, CancellationToken cancellationToken)
        {
            var blobName = GetBlobName(gameVersion, blobNameFactory);

            return _blobJsonStorage.UpdateAsync(_settings.RulesContainerName, blobName, catalogFile, cancellationToken);
        }

        private string GetBlobName(GameVersionEnum gameVersion, Func<string, string> blobNameFactory)
        {
            if (!_settings.SupportedGameVersions.Contains(gameVersion))
                throw new GameCatalogException($"Game version is not supported: {gameVersion}.");

            if (_settings.VersionFolders.TryGetValue(gameVersion, out var folder))
                return blobNameFactory(folder);

            throw new GameCatalogException($"No catalog folder configured for game version: {gameVersion}.");
        }

        private static void ValidateCatalogGameVersion(GameVersionEnum expectedGameVersion, GameVersionEnum actualGameVersion)
        {
            if (actualGameVersion == expectedGameVersion)
                return;

            throw new GameCatalogException($"Catalog game version '{actualGameVersion}' does not match route game version '{expectedGameVersion}'.");
        }

        private static string RulesBlobName(string folder)
        {
            return $"{folder}/rules.json";
        }

        private static string EquipmentBlobName(string folder)
        {
            return $"{folder}/equipment.json";
        }

        private static string LoresBlobName(string folder)
        {
            return $"{folder}/lores.json";
        }
    }
}
