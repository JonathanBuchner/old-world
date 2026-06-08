using System.Text.Json.Serialization;

namespace ow_api.Infrastructure.DependencyInjection
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
