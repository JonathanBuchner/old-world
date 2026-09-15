using OldWorld.Domain.Catalog;
using OldWorld.Infrastructure.Catalog;
namespace OldWorld.Infrastructure.DependencyInjection
{
    public static class GameCatalogRegisterer
    {
        public static void Add(IServiceCollection services)
        {
            services.AddSingleton<IGameCatalogStorageService, GameCatalogStorageService>();
        }
    }
}
