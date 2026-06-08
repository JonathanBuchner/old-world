using ow_api.Application.GameRules;
using ow_api.Application.ListImport;

namespace ow_api.Application
{
    public static class ApplicationRegisterer
    {
        public static void All(WebApplicationBuilder builder)
        {
            ListImportRegisterer.Add(builder.Services);
            GameRulesRegisterer.Add(builder.Services);
        }
    }
}
