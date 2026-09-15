using OldWorld.Api.Application;
using OldWorld.Api.DependencyInjection;
using OldWorld.Api.Middleware;
using OldWorld.Api.Configuration;

namespace OldWorld.Api;

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
