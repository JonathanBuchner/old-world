using Microsoft.Extensions.Options;
using ow_gen_lib.Enums.Army;

namespace ow_api.Infrastructure.Settings
{
    public class GameRulesSettingsConfigurationValidator : IValidateOptions<GameRulesSettings>
    {
        public ValidateOptionsResult Validate(string? name, GameRulesSettings options)
        {
            var failures = new List<string>();

            if (options.DefaultGameVersion == GameVersionEnum.Unknown)
                failures.Add($"{nameof(GameRulesSettings.DefaultGameVersion)} must be set.");

            if (options.SupportedGameVersions.Any(gameVersion => gameVersion == GameVersionEnum.Unknown))
                failures.Add($"{nameof(GameRulesSettings.SupportedGameVersions)} cannot contain Unknown.");

            if (!options.SupportedGameVersions.Contains(options.DefaultGameVersion))
                failures.Add($"{nameof(GameRulesSettings.DefaultGameVersion)} must be included in {nameof(GameRulesSettings.SupportedGameVersions)}.");

            if (string.IsNullOrWhiteSpace(options.RulesContainerName))
                failures.Add($"{nameof(GameRulesSettings.RulesContainerName)} must be set.");

            if (options.SupportedGameVersions.Any(gameVersion => !options.VersionFolders.ContainsKey(gameVersion)))
                failures.Add($"{nameof(GameRulesSettings.VersionFolders)} must contain every supported game version.");

            if (options.VersionFolders.Values.Any(string.IsNullOrWhiteSpace))
                failures.Add($"{nameof(GameRulesSettings.VersionFolders)} cannot contain empty folders.");

            if (failures.Count == 0)
                return ValidateOptionsResult.Success;

            return ValidateOptionsResult.Fail(failures);
        }
    }
}
