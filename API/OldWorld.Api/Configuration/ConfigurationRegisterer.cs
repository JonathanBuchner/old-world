namespace OldWorld.Api.Configuration
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

            builder.Services
                .AddOptions<OldWorldApiSettings>()
                .Bind(builder.Configuration.GetSection(nameof(OldWorldApiSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }
    }
}
