using OldWorld.Api.Application.GameRules;

namespace OldWorld.Api.Application
{
    public static class ApplicationRegisterer
    {
        public static void All(WebApplicationBuilder builder)
        {
            GameRulesRegisterer.Add(builder.Services);
        }
    }
}
