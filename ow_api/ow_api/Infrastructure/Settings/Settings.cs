namespace ow_api.Infrastructure.Settings
{
    /// <summary>
    /// Configures settings for program
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Add configuration for:
        /// <para>- appsettings.json</para>
        /// <para>- Any appsettings.{environment}.jsons</para>
        /// <para>- Secrets</para>
        /// <para>- Environment variables</para>
        /// </summary>
        /// <param name="builder">WebApplicationBuilder builder</param>
        public static void AddConfiguration(WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();
        }
    }
}
