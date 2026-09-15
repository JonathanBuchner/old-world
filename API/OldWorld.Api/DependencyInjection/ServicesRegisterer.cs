using Asp.Versioning;
using OldWorld.Api.Configuration;
using OldWorld.Api.Telemetry;
using OldWorld.Infrastructure.DependencyInjection;

namespace OldWorld.Api.DependencyInjection
{
    public static class ServicesRegisterer
    {
        public static void All(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            InfrastructureRegisterer.Add(builder.Services, builder.Configuration);
            builder.Services.AddSingleton<IControllerTelemetry, ControllerTelemetry>();
            AddApiVersioning(builder);
            AddSwagger(builder);

            if (builder.Environment.IsDevelopment())
                builder.Logging.AddConsole();
        }

        private static void AddApiVersioning(WebApplicationBuilder builder)
        {
            builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        private static void AddSwagger(WebApplicationBuilder builder)
        {
            var settings = builder.Configuration.GetSection(nameof(OldWorldApiSettings)).Get<OldWorldApiSettings>();

            if (!builder.Environment.IsDevelopment() || settings?.EnableSwagger != true)
                return;

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
