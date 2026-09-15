using Microsoft.Extensions.Options;
using OldWorld.Api.Application.GameRules.DeferredEngine;
using OldWorld.Api.Application.GameRules.DeferredEngine.OldWorldV152Renegade;
using OldWorld.Api.Configuration;
using OldWorld.Infrastructure.Configuration;

namespace OldWorld.Api.Application.GameRules
{
    public static class GameRulesRegisterer
    {
        public static void Add(IServiceCollection services)
        {
            services.AddSingleton<IGameRulesEngine, OldWorldV152RenegadeRulesEngine>();
            services.AddSingleton<IGameRulesEngineProvider, GameRulesEngineProvider>();
            services.AddSingleton<IValidateOptions<GameRulesSettings>, GameRulesSettingsValidator>();
        }
    }
}
