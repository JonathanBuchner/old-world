using Microsoft.Extensions.Options;
using ow_api.Infrastructure.Settings;

namespace ow_api.Application.GameRules.Catalog
{
    public class GameRulesCatalogHostedService : IHostedService
    {
        private readonly IGameRulesCatalogLoader _gameRulesCatalogLoader;
        private readonly IGameRulesCatalogStore _gameRulesCatalogStore;
        private readonly ILogger<GameRulesCatalogHostedService> _logger;
        private readonly GameRulesSettings _settings;

        public GameRulesCatalogHostedService(IGameRulesCatalogLoader gameRulesCatalogLoader, IGameRulesCatalogStore gameRulesCatalogStore, ILogger<GameRulesCatalogHostedService> logger, IOptions<GameRulesSettings> options)
        {
            _gameRulesCatalogLoader = gameRulesCatalogLoader;
            _gameRulesCatalogStore = gameRulesCatalogStore;
            _logger = logger;
            _settings = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var gameVersion in _settings.SupportedGameVersions)
            {
                try
                {
                    _logger.LogInformation("Loading game rules catalog for {GameVersion}", gameVersion);

                    var catalog = await _gameRulesCatalogLoader.LoadAsync(gameVersion, cancellationToken);
                    _gameRulesCatalogStore.Set(catalog);

                    _logger.LogInformation("Loaded game rules catalog for {GameVersion}", gameVersion);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to load game rules catalog for {GameVersion}", gameVersion);

                    throw;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
