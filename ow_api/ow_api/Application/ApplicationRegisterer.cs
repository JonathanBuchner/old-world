using ow_api.Application.GameCatalog;
using ow_api.Application.GameRules;
using ow_api.Application.ListImport;

namespace ow_api.Application
{
    public static class ApplicationRegisterer
    {
        public static void All(WebApplicationBuilder builder)
        {
            GameCatalogRegisterer.Add(builder.Services);
            ListImportRegisterer.Add(builder.Services);
            GameRulesRegisterer.Add(builder.Services);
        }
    }
}
