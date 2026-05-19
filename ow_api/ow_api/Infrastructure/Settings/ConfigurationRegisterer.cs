using ow_api.Infrastructure.Services;

namespace ow_api.Infrastructure.Settings
{
    public class ConfigurationRegisterer
    {
        public static void AddConfiguration(WebApplicationBuilder builder)
        {
            var environmentName = builder.Environment.EnvironmentName;

            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);

            if (builder.Environment.IsDevelopment())
                builder.Configuration.AddUserSecrets<Program>();

            builder.Configuration.AddEnvironmentVariables();

            AddOptions(builder);
        }

        private static void AddOptions(WebApplicationBuilder builder)
        {
            builder.Services
                .AddOptions<OldWorldApiSettings>()
                .Bind(builder.Configuration.GetSection(nameof(OldWorldApiSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services
                .AddOptions<ApplicationInsightsSettings>()
                .Bind(builder.Configuration.GetSection(nameof(ApplicationInsightsSettings)))
                .ValidateDataAnnotations();
        }
    }
}
