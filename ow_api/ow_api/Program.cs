
using ow_api.Application;
using ow_api.Infrastructure.DependencyInjection;
using ow_api.Infrastructure.Middleware;
using ow_api.Infrastructure.Settings;

namespace ow_api;

public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        ConfigurationRegisterer.AddConfiguration(builder);
        ServicesRegisterer.All(builder);
        ApplicationRegisterer.All(builder);
        ControllersRegisterer.Add(builder);

        var app = builder.Build();

        AddMiddlewarePipeline.All(app);
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
