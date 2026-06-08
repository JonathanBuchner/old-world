namespace ow_api.Application.GameCatalog
{
    public static class GameCatalogRegisterer
    {
        public static void Add(IServiceCollection services)
        {
            services.AddSingleton<IGameCatalogStorageService, GameCatalogStorageService>();
        }
    }
}
