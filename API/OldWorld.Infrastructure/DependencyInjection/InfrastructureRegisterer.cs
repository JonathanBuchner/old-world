using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OldWorld.Domain.Rules;
using OldWorld.Infrastructure.Catalog;
using OldWorld.Infrastructure.Configuration;
using OldWorld.Infrastructure.Storage;
using OldWorld.Infrastructure.Telemetry;
using OwTelemetry = OldWorld.Infrastructure.Telemetry.Telemetry;

namespace OldWorld.Infrastructure.DependencyInjection
{
    public static class InfrastructureRegisterer
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<AzureStorageSettings>()
                .Bind(configuration.GetSection(nameof(AzureStorageSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddOptions<ApplicationInsightsSettings>()
                .Bind(configuration.GetSection(nameof(ApplicationInsightsSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddOptions<GameRulesSettings>()
                .Bind(configuration.GetSection(nameof(GameRulesSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
            services.AddSingleton<IValidateOptions<GameRulesSettings>, GameRulesSettingsConfigurationValidator>();

            var storageSettings = configuration.GetSection(nameof(AzureStorageSettings)).Get<AzureStorageSettings>();
            if (storageSettings == null)
                throw new ArgumentNullException($"{nameof(AzureStorageSettings)} has not been set");

            services.AddSingleton(new BlobServiceClient(storageSettings.ConnectionString));
            services.AddSingleton<IBlobStorage, AzureBlobStorage>();
            services.AddSingleton<IBlobJsonStorage, BlobJsonStorage>();
            GameCatalogRegisterer.Add(services);
            ListImportRegisterer.Add(services);

            services.AddSingleton<GameRulesCatalogProvider>();
            services.AddSingleton<IGameRulesCatalogProvider>(serviceProvider => serviceProvider.GetRequiredService<GameRulesCatalogProvider>());
            services.AddSingleton<IGameRulesCatalogStore>(serviceProvider => serviceProvider.GetRequiredService<GameRulesCatalogProvider>());
            services.AddSingleton<IGameRulesCatalogLoader, GameRulesCatalogLoader>();
            services.AddHostedService<GameRulesCatalogHostedService>();

            services.AddSingleton<ITelemetryTracker, TelemetryTracker>();
            AddOpenTelemetry(services, configuration);
        }

        private static void AddOpenTelemetry(IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(nameof(ApplicationInsightsSettings)).Get<ApplicationInsightsSettings>();
            if (settings == null)
                throw new ArgumentNullException($"{nameof(ApplicationInsightsSettings)} has not been set");

            services.AddOpenTelemetry()
                .WithTracing(tracing => tracing.AddSource(OwTelemetry.ServiceName))
                .WithMetrics(metrics => metrics.AddMeter(OwTelemetry.ServiceName))
                .UseAzureMonitor(options => options.ConnectionString = settings.ConnectionString);
        }
    }
}
