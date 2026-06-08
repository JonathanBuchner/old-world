
using ow_api.Application;
using ow_api.Infrastructure.DependencyInjection;
using ow_api.Infrastructure.Settings;
using ow_api.Infrastructure.Middleware;

namespace ow_api;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        ConfigurationRegisterer.AddConfiguration(builder);
        ServicesRegisterer.All(builder);
        ApplicationRegisterer.All(builder);
        
        builder.Services.AddControllers();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // app.MapOpenApi();
        }

        app.UseMiddleware<ExceptionTrackingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
