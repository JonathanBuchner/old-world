using Microsoft.Extensions.Options;
using ow_gen_lib.Rules;

namespace ow_api.Infrastructure.Settings
{
    public class GameRulesSettingsValidator : IValidateOptions<GameRulesSettings>
    {
        private readonly IEnumerable<IGameRulesEngine> _gameRulesEngines;

        public GameRulesSettingsValidator(IEnumerable<IGameRulesEngine> gameRulesEngines)
        {
            _gameRulesEngines = gameRulesEngines;
        }

        public ValidateOptionsResult Validate(string? name, GameRulesSettings options)
        {
            var engineGameVersions = _gameRulesEngines.Select(engine => engine.GameVersion).ToHashSet();
            var unsupportedGameVersions = options.SupportedGameVersions.Where(gameVersion => !engineGameVersions.Contains(gameVersion)).ToList();

            if (unsupportedGameVersions.Count == 0)
                return ValidateOptionsResult.Success;

            return ValidateOptionsResult.Fail($"No {nameof(IGameRulesEngine)} registered for game versions: {string.Join(", ", unsupportedGameVersions)}.");
        }
    }
}
