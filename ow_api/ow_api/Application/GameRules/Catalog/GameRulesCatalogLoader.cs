using Microsoft.Extensions.Options;
using ow_api.Infrastructure.Services;
using ow_api.Infrastructure.Settings;
using ow_gen_lib.Enums.Army;
using ow_gen_lib.Models;
using ow_gen_lib.Rules;

namespace ow_api.Application.GameRules.Catalog
{
    public class GameRulesCatalogLoader : IGameRulesCatalogLoader
    {
        private readonly IBlobJsonStorage _blobJsonStorage;
        private readonly ILogger<GameRulesCatalogLoader> _logger;
        private readonly GameRulesSettings _settings;

        public GameRulesCatalogLoader(IBlobJsonStorage blobJsonStorage, ILogger<GameRulesCatalogLoader> logger, IOptions<GameRulesSettings> options)
        {
            _blobJsonStorage = blobJsonStorage;
            _logger = logger;
            _settings = options.Value;
        }

        public async Task<GameRulesCatalog> LoadAsync(GameVersionEnum gameVersion, CancellationToken cancellationToken)
        {
            var folder = GetVersionFolder(gameVersion);
            var rulesBlobName = RulesBlobName(folder);
            var equipmentBlobName = EquipmentBlobName(folder);
            var loresBlobName = LoresBlobName(folder);

            _logger.LogInformation("Reading game rules catalog blobs for {GameVersion} from {ContainerName}/{Folder}", gameVersion, _settings.RulesContainerName, folder);

            var rulesFile = await ReadRequiredAsync<RulesCatalogFile>(rulesBlobName, cancellationToken);
            var equipmentFile = await ReadRequiredAsync<EquipmentCatalogFile>(equipmentBlobName, cancellationToken);
            var loresFile = await ReadRequiredAsync<LoresCatalogFile>(loresBlobName, cancellationToken);

            ValidateGameVersion(gameVersion, rulesFile.GameVersion, rulesBlobName);
            ValidateGameVersion(gameVersion, equipmentFile.GameVersion, equipmentBlobName);
            ValidateGameVersion(gameVersion, loresFile.GameVersion, loresBlobName);

            return new GameRulesCatalog()
            {
                GameVersion = gameVersion,
                Rules = ToDictionary(rulesFile.Rules),
                Equipment = ToDictionary(equipmentFile.EquipmentEntries),
                Lores = ToLoresDictionary(loresFile.Lores)
            };
        }

        private string GetVersionFolder(GameVersionEnum gameVersion)
        {
            if (_settings.VersionFolders.TryGetValue(gameVersion, out var folder))
                return folder;

            _logger.LogError("No rules catalog folder configured for game version {GameVersion}", gameVersion);

            throw new InvalidOperationException($"No rules catalog folder configured for game version: {gameVersion}.");
        }

        private async Task<T> ReadRequiredAsync<T>(string blobName, CancellationToken cancellationToken)
        {
            var content = await _blobJsonStorage.ReadAsync<T>(_settings.RulesContainerName, blobName, cancellationToken);

            if (content != null)
                return content;

            _logger.LogError("Rules catalog blob was not found at {ContainerName}/{BlobName}", _settings.RulesContainerName, blobName);

            throw new InvalidOperationException($"Rules catalog blob was not found: {_settings.RulesContainerName}/{blobName}.");
        }

        private void ValidateGameVersion(GameVersionEnum expectedGameVersion, GameVersionEnum actualGameVersion, string blobName)
        {
            if (actualGameVersion == expectedGameVersion)
                return;

            _logger.LogError("Rules catalog blob {BlobName} has game version {ActualGameVersion}, expected {ExpectedGameVersion}", blobName, actualGameVersion, expectedGameVersion);

            throw new InvalidOperationException($"Rules catalog blob '{blobName}' has game version '{actualGameVersion}', expected '{expectedGameVersion}'.");
        }

        private static IReadOnlyDictionary<string, T> ToDictionary<T>(IEnumerable<T> entries) where T : Entry
        {
            return entries.ToDictionary(entry => entry.NameId);
        }

        private static IReadOnlyDictionary<string, SpellSet> ToLoresDictionary(IEnumerable<SpellSet> lores)
        {
            return lores.ToDictionary(lore => lore.SpellLore.ToString());
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
