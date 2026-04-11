using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Channel;

using ow_api.Infrastructure.Telemetry;

namespace ow_api.Infrastructure.Services
{
    /// <summary>
    /// Registers services for program
    /// </summary>
    public static class ServicesRegisterer
    {
        /// <summar
        /// Registers all services, including in this order:
        /// <para>-Application Insights</para>
        /// </summary>
        public static void All(WebApplicationBuilder builder)
        {

        }

        private static void AddApplicationInsights(WebApplicationBuilder builder)
        {
            var settings = builder.Configuration.GetSection(key:"ApplicationInsightSettings").Get<ApplicationInsightsSettings>();

            if (settings == null)
                throw new ArgumentNullException($"ApplicationInsightSettings has not been set");

            var options = new ApplicationInsightsServiceOptions()
            {
                AddAutoCollectedMetricExtractor = settings.AddAutoCollectedMetricExtractor,
                ConnectionString = settings.ConnectionString,
                EnableDependencyTrackingTelemetryModule = settings.EnableDependencyTrackingTelemetryModule,
                EnablePerformanceCounterCollectionModule = settings.EnablePerformanceCounterCollectionModule,
                EnableRequestTrackingTelemetryModule = settings.EnableRequestTrackingTelemetryModule,
            };

            builder.Services.AddApplicationInsightsTelemetry(options);

            // Register custom TelemetryInitializer to provide role name when running locally
            builder.Services.AddSingleton<ITelemetryInitializer, DevelopmentRoleNameTelemetryInitializer>();

            builder.Services.ConfigureTelemetryModule<QuickPulseTelemetryModule>((module, _) =>
            {
                module.AuthenticationApiKey = settings.ApiKey;
            });
        }

        /// <summary>
        /// Custom telemetry initializer to set role name when running locally
        /// </summary>
        public class DevelopmentRoleNameTelemetryInitializer : ITelemetryInitializer
        {
            /// <summary>
            /// The web host environment
            /// </summary>
            private readonly IWebHostEnvironment _env;

            public DevelopmentRoleNameTelemetryInitializer(IWebHostEnvironment env)
            {
                _env = env;
            }

            /// <summary>
            /// Initializes the telemetry.
            /// </summary>
            /// <param name="telemetry">The telemetry to initialize.</param>
            public void Initialize(ITelemetry telemetry)
            {
                if (_env.IsDevelopment())
                {
                    // Set the role name to the machine name if running in development environment
                    telemetry.Context.Cloud.RoleName = Environment.MachineName;
                }
            }
        }

    }
}
