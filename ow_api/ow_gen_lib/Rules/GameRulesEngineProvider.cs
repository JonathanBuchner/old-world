using ow_gen_lib.Enums.Army;

namespace ow_gen_lib.Rules
{
    public class GameRulesEngineProvider : IGameRulesEngineProvider
    {
        private readonly IReadOnlyDictionary<GameVersionEnum, IGameRulesEngine> _engines;

        public GameRulesEngineProvider(IEnumerable<IGameRulesEngine> engines)
        {
            _engines = engines.ToDictionary(engine => engine.GameVersion);
        }

        public IGameRulesEngine Get(GameVersionEnum gameVersion)
        {
            if (_engines.TryGetValue(gameVersion, out var engine))
                return engine;

            throw new ArgumentException($"Unsupported game version: {gameVersion}", nameof(gameVersion));
        }
    }
}
