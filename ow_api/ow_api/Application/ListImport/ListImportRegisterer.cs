using ow_api.Application.ListImport.OldWorldBuilder;

namespace ow_api.Application.ListImport
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
