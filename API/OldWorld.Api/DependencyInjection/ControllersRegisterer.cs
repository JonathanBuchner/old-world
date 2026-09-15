using System.Text.Json.Serialization;

namespace OldWorld.Api.DependencyInjection
{
    public static class ControllersRegisterer
    {
        public static void Add(WebApplicationBuilder builder)
        {
            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
        }
    }
}
