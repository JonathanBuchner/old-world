using Microsoft.Extensions.Options;
using ow_api.Application.GameRules.Catalog;
using ow_api.Infrastructure.Settings;
using ow_gen_lib.Rules;
using ow_gen_lib.Rules.OldWorldV152Renegade;

namespace ow_api.Application.GameRules
{
    public static class GameRulesRegisterer
    {
        public static void Add(IServiceCollection services)
        {
            services.AddSingleton<GameRulesCatalogProvider>();
            services.AddSingleton<IGameRulesCatalogProvider>(serviceProvider => serviceProvider.GetRequiredService<GameRulesCatalogProvider>());
            services.AddSingleton<IGameRulesCatalogStore>(serviceProvider => serviceProvider.GetRequiredService<GameRulesCatalogProvider>());
            services.AddSingleton<IGameRulesCatalogLoader, GameRulesCatalogLoader>();
            services.AddSingleton<IGameRulesEngine, OldWorldV152RenegadeRulesEngine>();
            services.AddSingleton<IGameRulesEngineProvider, GameRulesEngineProvider>();
            services.AddSingleton<IValidateOptions<GameRulesSettings>, GameRulesSettingsConfigurationValidator>();
            services.AddSingleton<IValidateOptions<GameRulesSettings>, GameRulesSettingsValidator>();
            services.AddHostedService<GameRulesCatalogHostedService>();
        }
    }
}
