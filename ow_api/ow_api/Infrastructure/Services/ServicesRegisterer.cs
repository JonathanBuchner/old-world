using Azure.Monitor.OpenTelemetry.AspNetCore;
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
        }

        private static void AddTelemetryTracker(WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ITelemetryTracker, TelemetryTracker>();
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
