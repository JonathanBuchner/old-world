using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Storage.Blobs;
using ow_api.Application.ListImport;
using ow_api.Infrastructure.Telemetry;
using OwTelemetry = ow_api.Infrastructure.Telemetry.Telemetry;

namespace ow_api.Infrastructure.Services
{
    public static class ServicesRegisterer
    {
        public static void All(WebApplicationBuilder builder)
        {
            AddOpenTelemetry(builder);
            AddTelemetryTracker(builder);
            AddBlobStorage(builder);
            AddListImport(builder);
        }

        private static void AddListImport(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<JsonListBuilderDetector>();
        }

        private static void AddBlobStorage(WebApplicationBuilder builder)
        {
            var settings = builder.Configuration.GetSection(nameof(AzureStorageSettings)).Get<AzureStorageSettings>();

            if (settings == null)
                throw new ArgumentNullException($"{nameof(AzureStorageSettings)} has not been set");

            builder.Services.AddSingleton(new BlobServiceClient(settings.ConnectionString));
            builder.Services.AddSingleton<IBlobStorage, AzureBlobStorage>();
            builder.Services.AddSingleton<IBlobJsonStorage, BlobJsonStorage>();
        }

        private static void AddTelemetryTracker(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ITelemetryTracker, TelemetryTracker>();
            builder.Services.AddSingleton<IControllerTelemetry, ControllerTelemetry>();
        }

        private static void AddOpenTelemetry(WebApplicationBuilder builder)
        {
            var settings = builder.Configuration.GetSection(nameof(ApplicationInsightsSettings)).Get<ApplicationInsightsSettings>();

            if (settings == null)
                throw new ArgumentNullException($"{nameof(ApplicationInsightsSettings)} has not been set");

            builder.Logging.ClearProviders();

            builder.Services
                .AddOpenTelemetry()
                .WithTracing(tracing => tracing.AddSource(OwTelemetry.ServiceName))
                .WithMetrics(metrics => metrics.AddMeter(OwTelemetry.ServiceName))
                .UseAzureMonitor(options =>
                {
                    options.ConnectionString = settings.ConnectionString;
                });

            if (builder.Environment.IsDevelopment())
                builder.Logging.AddConsole();
        }
    }
}
