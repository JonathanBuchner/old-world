using OldWorld.Infrastructure.Imports;
using OldWorld.Infrastructure.Imports.OldWorldBuilder;

namespace OldWorld.Infrastructure.DependencyInjection
{
    public static class ListImportRegisterer
    {
        public static void Add(IServiceCollection services)
        {
            services.AddSingleton<JsonListBuilderDetector>();
            services.AddSingleton<OldWorldBuilderJsonParser>();
        }
    }
}
